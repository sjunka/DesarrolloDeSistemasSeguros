// Microsoft Threat Modeling Tool 7.3 sin interfaz: corre el codigo real de la herramienta (ThreatModeling.*.dll de la
// distribucion oficial) con shims minimos de WPF, para abrir, validar y construir .tm7 en macOS o Linux.
// Uso: harness <cache> fetch
//      harness <cache> build <salida.tm7>
//      harness <cache> open <archivo.tm7> [--list]
//      harness <cache> preview <archivo.tm7> <salida.svg>
// <cache> guarda los binarios del TMT (tmt/) y los shims compilados (shim/).
using System.Collections;
using System.Net;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

var (cache, cmd) = (Path.GetFullPath(args[0]), args[1]);
var tmtDir = Path.Combine(cache, "tmt");
if (cmd == "fetch") return await Fetch(tmtDir);
if (cmd == "preview") { Preview(args[2], args[3]); return 0; }

var file = Path.GetFullPath(args[2]);
var alc = new TmtContext(Path.Combine(cache, "shim"), tmtDir);
Assembly Load(string n) => alc.LoadFromAssemblyPath(Path.Combine(tmtDir, n + ".dll"));
var model = Load("ThreatModeling.Model");
var storage = Load("ThreatModeling.ExternalStorage");
var local = Load("ThreatModeling.ExternalStorage.Local");
const BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
object Get(object o, string name) => o.GetType().GetProperty(name, All)?.GetValue(o) ?? o.GetType().GetField(name, All)?.GetValue(o);
IEnumerable<object> Items(object o) => ((IEnumerable)o).Cast<object>();

if (cmd == "build")
{
    var built = Builder.Run(model, Path.Combine(tmtDir, "KnowledgeBase", "Default.tb7"));
    if (Process(built) != 0) return 1;
    built.GetType().GetMethod("SaveModel", new[] { typeof(string), typeof(string), typeof(bool), typeof(bool) }).Invoke(built, new object[] { file, file, false, false });
    Console.WriteLine($"Guardado con SaveModel del TMT: {file}\n");
}
return Open(file, args.Contains("--list"));

int Open(string path, bool list)
{
    Console.WriteLine($"== {Path.GetFileName(path)}");
    // 1) El mismo DataContractSerializer que ObjectModel(StorageFile) usa al abrir.
    var ser = (DataContractSerializer)storage.GetType("ThreatModeling.ExternalStorage.OM.SerializableModelData").GetMethod("GetSerializer").Invoke(null, null);
    try
    {
        using var fs = File.OpenRead(path);
        var smd = ser.ReadObject(fs);
        Console.WriteLine($"[1] Deserializacion DataContract: OK (KnowledgeBase {(Get(smd, "KnowledgeBase") == null ? "AUSENTE" : "presente")})");
    }
    catch (Exception e) { Console.WriteLine($"[1] Deserializacion DataContract: FALLA {e.GetType().Name}: {e.Message}"); return 1; }

    // 2) Constructor real de apertura: KnowledgeBase, ModelData, Configure, Validate.
    object om;
    try
    {
        var lf = Activator.CreateInstance(local.GetType("ThreatModeling.ExternalStorage.Local.LocalFile"), path);
        om = model.GetType("ThreatModeling.Model.ObjectModel").GetConstructor(new[] { lf.GetType().BaseType, typeof(bool) }).Invoke(new[] { lf, true });
    }
    catch (TargetInvocationException e) { Console.WriteLine($"[2] Apertura con ObjectModel: FALLA {e.InnerException.GetType().Name}: {e.InnerException.Message}"); return 1; }
    var issues = (string[])Get(om, "ModelLoadIssues");
    Console.WriteLine(issues == null ? "[2] Apertura con ObjectModel: OK, sin avisos de carga" : "[2] Apertura con ObjectModel: AVISOS\n      " + string.Join("\n      ", issues));

    // 3) Mismo procesamiento que la vista de analisis; las amenazas guardadas deben ser las que el motor regenera.
    var saved = Items(Get(Get(om, "ModelDynamicData"), "AllThreats")).Select(t => (string)Get(t, "Key")).ToHashSet();
    var fails = (issues == null ? 0 : 1) + Process(om);
    var md = Get(om, "ModelDynamicData");
    var threats = Items(Get(md, "AllThreats")).ToList();
    var same = saved.SetEquals(threats.Select(t => (string)Get(t, "Key")));
    Console.WriteLine($"    amenazas guardadas={saved.Count}, regeneradas={threats.Count}, iguales={same}");
    if (!same) fails++;

    // 4) Lo que se ve en la herramienta: elementos con su tipo y amenazas por categoria STRIDE.
    foreach (var surface in Items(Get(md, "DrawingSurfaces")).Select(kv => Get(kv, "Value")))
    {
        Console.WriteLine($"[4] Diagrama '{Get(surface, "Header")}'");
        foreach (var coll in new[] { "Borders", "Lines" })
            foreach (var el in Items(Get(surface, coll)).Select(kv => Get(kv, "Value")))
            {
                var flow = Get(el, "Source") is { } s ? $"   [{Get(s, "Name")} -> {Get(Get(el, "Target"), "Name")}]" : "";
                Console.WriteLine($"      {Get(Get(el, "Type"), "Name"),-29} | {Get(el, "Name")}{flow}");
            }
    }
    foreach (var g in threats.GroupBy(t => (string)Get(t, "Category")).OrderBy(g => g.Key))
        Console.WriteLine($"      {g.Key,-24} {g.Count()}");
    if (list)
        foreach (var t in threats.OrderBy(t => (int)Get(t, "Id")))
            Console.WriteLine($"      #{Get(t, "Id")} [{Get(t, "Category")}] {Get(t, "Title")} | {Get(t, "InteractionString")}");
    Console.WriteLine(fails == 0 ? "RESULTADO: el TMT lo abre sin errores" : $"RESULTADO: {fails} problema(s)");
    return fails;
}

// Validacion y generacion de amenazas (ObjectModel.ProcessModelImmediately, el que dispara la vista de analisis).
int Process(object om)
{
    om.GetType().GetField("_suppressProcessModel", All).SetValue(om, false);
    var mode = Enum.Parse(model.GetType("ThreatModeling.Model.ModelProcessingMode"), "FullModel");
    var done = (bool)om.GetType().GetMethod("ProcessModelImmediately").Invoke(om, new[] { mode });
    var validations = Items(Get(Get(om, "ModelDynamicData"), "Validations")).ToList();
    Console.WriteLine($"[3] Validacion y generacion de amenazas: {(done ? "OK" : "NO CORRIO")}; avisos de validacion={validations.Count}");
    foreach (var v in validations) Console.WriteLine($"      aviso: {Get(v, "Message")}");
    return done ? validations.Count : 1;
}

// Descarga la version vigente del instalador ClickOnce oficial y verifica cada archivo contra el SHA-256 de su manifiesto.
// Copia los ensamblados sin el flag 32BITREQUIRED (equivale a "corflags /32BITREQ-"): el IL no cambia.
static async Task<int> Fetch(string dir)
{
    using var http = new HttpClient();
    var deploy = await http.GetAsync("https://aka.ms/threatmodelingtool");
    var root = deploy.RequestMessage.RequestUri;
    var manifestPath = Regex.Match(await deploy.Content.ReadAsStringAsync(), "codebase=\"([^\"]+\\.manifest)\"").Groups[1].Value.Replace('\\', '/');
    var manifestUri = new Uri(root, string.Join('/', manifestPath.Split('/').Select(Uri.EscapeDataString)));
    var manifest = await http.GetStringAsync(manifestUri);
    var entries = Regex.Matches(manifest, "<(?:dependentAssembly|file)[^>]*?(?:codebase|name)=\"([^\"]+)\"[^>]*>.*?<dsig:DigestValue>([^<]+)</dsig:DigestValue>", RegexOptions.Singleline)
        .Select(m => (Path: m.Groups[1].Value.Replace('\\', '/'), Digest: m.Groups[2].Value))
        .Where(e => e.Path.EndsWith(".dll") || e.Path == "KnowledgeBase/Default.tb7");
    Console.WriteLine($"TMT: {manifestUri}");
    foreach (var (path, digest) in entries)
    {
        var bytes = await http.GetByteArrayAsync(new Uri(manifestUri, string.Join('/', path.Split('/').Select(Uri.EscapeDataString)) + ".deploy"));
        if (Convert.ToBase64String(SHA256.HashData(bytes)) != digest) { Console.WriteLine($"SHA-256 no coincide: {path}"); return 1; }
        if (path.EndsWith(".dll")) ClearRequire32Bit(bytes);
        Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(dir, path)));
        await File.WriteAllBytesAsync(Path.Combine(dir, path), bytes);
    }
    Console.WriteLine($"{entries.Count()} archivos verificados con SHA-256 en {dir}");
    return 0;
}

static void ClearRequire32Bit(byte[] b)
{
    int I(int o) => BitConverter.ToInt32(b, o);
    int pe = I(0x3c), opt = pe + 24, clrRva = I(opt + (BitConverter.ToUInt16(b, opt) == 0x10b ? 96 : 112) + 14 * 8);
    int sections = opt + BitConverter.ToUInt16(b, pe + 20);
    for (int s = 0; s < BitConverter.ToUInt16(b, pe + 6); s++)
    {
        int at = sections + s * 40, va = I(at + 12), size = Math.Max(I(at + 8), I(at + 16));
        if (clrRva < va || clrRva >= va + size) continue;
        int flags = clrRva - va + I(at + 20) + 16;
        BitConverter.GetBytes(I(flags) & ~2).CopyTo(b, flags);
    }
}

// Dibuja el diagrama guardado en SVG (aproxima el lienzo del TMT) para revisarlo sin Windows.
static void Preview(string tm7, string svgPath)
{
    XNamespace m = "http://schemas.datacontract.org/2004/07/ThreatModeling.Model", a = "http://schemas.datacontract.org/2004/07/ThreatModeling.Model.Abstracts",
        k = "http://schemas.datacontract.org/2004/07/ThreatModeling.KnowledgeBase", r = "http://schemas.microsoft.com/2003/10/Serialization/Arrays",
        i = "http://www.w3.org/2001/XMLSchema-instance";
    var surface = XDocument.Load(tm7).Root.Element(m + "DrawingSurfaceList").Elements().First();
    string Name(XElement v) => v.Element(a + "Properties").Elements().First(p => (string)p.Element(k + "DisplayName") == "Name").Element(k + "Value").Value;
    double N(XElement v, string t) => (double)v.Element(a + t);
    string Text(double x, double y, string s, int size, string fill, string anchor = "middle") =>
        $"<text x='{x}' y='{y}' font-size='{size}' fill='{fill}' text-anchor='{anchor}' font-family='Segoe UI, Arial'>{WebUtility.HtmlEncode(s)}</text>";
    var svg = new List<string>();
    double w = 0, h = 0;
    foreach (var v in surface.Element(m + "Borders").Elements().Select(e => e.Element(r + "Value")))
    {
        double x = N(v, "Left"), y = N(v, "Top"), wd = N(v, "Width"), ht = N(v, "Height"), cx = x + wd / 2, cy = y + ht / 2;
        (w, h) = (Math.Max(w, x + wd), Math.Max(h, y + ht));
        var shape = (string)v.Element(a + "TypeId") == "GE.A" ? "Annotation" : (string)v.Attribute(i + "type");
        svg.Add(shape switch
        {
            "BorderBoundary" => $"<rect x='{x}' y='{y}' width='{wd}' height='{ht}' fill='none' stroke='#c00' stroke-width='2' stroke-dasharray='8 5'/>" + Text(x + 8, y + 16, Name(v), 13, "#c00", "start"),
            "StencilEllipse" => $"<ellipse cx='{cx}' cy='{cy}' rx='{wd / 2}' ry='{ht / 2}' fill='#fff' stroke='#000'/>" + Text(cx, cy + 4, Name(v), 12, "#000"),
            "StencilParallelLines" => $"<path d='M{x},{y}h{wd}M{x},{y + ht}h{wd}' stroke='#000'/>" + Text(cx, cy + 4, Name(v), 12, "#000"),
            "StencilRectangle" => $"<rect x='{x}' y='{y}' width='{wd}' height='{ht}' fill='#fff' stroke='#000'/>" + Text(cx, cy + 4, Name(v), 12, "#000"),
            _ => Text(cx, cy + 4, Name(v), 11, "#555"),
        });
    }
    foreach (var v in surface.Element(m + "Lines").Elements().Select(e => e.Element(r + "Value")))
    {
        double x0 = N(v, "SourceX"), y0 = N(v, "SourceY"), x1 = N(v, "TargetX"), y1 = N(v, "TargetY"), mx = N(v, "HandleX"), my = N(v, "HandleY");
        svg.Add($"<path d='M{x0},{y0} Q{2 * mx - (x0 + x1) / 2},{2 * my - (y0 + y1) / 2} {x1},{y1}' fill='none' stroke='#333' marker-end='url(#f)'/>" + Text(mx, my - 6, Name(v), 11, "#036"));
    }
    File.WriteAllText(svgPath, $"<svg xmlns='http://www.w3.org/2000/svg' width='{w + 30}' height='{h + 30}'><rect width='100%' height='100%' fill='#fff'/>" +
        "<defs><marker id='f' viewBox='0 0 10 10' refX='10' refY='5' markerWidth='8' markerHeight='8' orient='auto'><path d='M0,0L10,5L0,10z' fill='#333'/></marker></defs>" +
        string.Join("", svg) + "</svg>");
    Console.WriteLine($"Vista previa: {Path.GetFullPath(svgPath)}");
}

class TmtContext(params string[] dirs) : AssemblyLoadContext("tmt")
{
    protected override Assembly Load(AssemblyName name) =>
        dirs.Select(d => Path.Combine(d, name.Name + ".dll")).Where(File.Exists).Select(LoadFromAssemblyPath).FirstOrDefault();
}
