// Construye el modelo del marketplace con la API interna del TMT 7.3.51110, igual que al dibujarlo en la interfaz:
// plantilla oficial Default.tb7 -> AddElement / AttachConnector -> motor de amenazas -> SaveModel.
using System.Collections;
using System.Reflection;

static class Builder
{
    const BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

    // clave, tipo de la plantilla, nombre, x, y, ancho, alto
    static readonly (string Key, string Type, string Name, int X, int Y, int W, int H)[] Elements =
    {
        ("borde",    "GE.TB.B",              "Plataforma del marketplace",        250,  30, 720, 600),
        ("comprador","SE.EI.TMCore.User",    "Comprador",                          40, 160, 130,  70),
        ("vendedor", "SE.EI.TMCore.User",    "Vendedor",                         1050, 160, 130,  70),
        ("login",    "SE.P.TMCore.WebApp",   "Login, sesiones y tokens",          545,  60, 130, 130),
        ("pagos",    "SE.P.TMCore.WebSvc",   "Pagos, retiros y escrow",           545, 290, 130, 130),
        ("bd",       "SE.DS.TMCore.SQL",     "Servidores, llaves y configuración", 290, 500, 170,  80),
        ("logs",     "GE.DS",                "Logs y monitoreo",                  760, 500, 170,  80),
        ("personal", "SE.EI.TMCore.User",    "Personal con acceso",               535, 700, 150,  70),
        // Numero del punto de falla junto a cada elemento, como en el mapa del PDF, y leyenda abajo.
        ("m1a",      "GE.A", "1",  522,  60,  22,  22),
        ("m1b",      "GE.A", "1",  522, 290,  22,  22),
        ("m2a",      "GE.A", "2",   40, 135,  22,  22),
        ("m2b",      "GE.A", "2", 1050, 135,  22,  22),
        ("m3",       "GE.A", "3",  265, 555,  22,  22),
        ("m4",       "GE.A", "4",  690, 724,  22,  22),
        ("m5",       "GE.A", "5",  908, 475,  22,  22),
        ("ley0",     "GE.A", "Puntos de falla (ver PDF, pág. 2):",       20, 650, 300,  22),
        ("ley1",     "GE.A", "1 · Una sola validación crítica (A06)",    20, 674, 300,  22),
        ("ley2",     "GE.A", "2 · Cuentas falsas o suplantadas (A07)",   20, 698, 300,  22),
        ("ley3",     "GE.A", "3 · Secretos y configuración (A02, A04)",  20, 722, 300,  22),
        ("ley4",     "GE.A", "4 · Personas con acceso (gobierno)",       20, 746, 300,  22),
        ("ley5",     "GE.A", "5 · Detectar y responder (A09)",           20, 770, 300,  22),
    };

    // origen, destino, tipo de flujo, nombre, puerto de origen y de destino (null: el mas cercano, como la interfaz),
    // posicion del asa/etiqueta a lo largo del flujo (0..1) y desvio perpendicular (mismo signo en un par de ida y vuelta).
    static readonly (string From, string To, string Type, string Name, string FromPort, string ToPort, double T, int Bend)[] Flows =
    {
        ("comprador","login",   "SE.DF.TMCore.HTTPS",  "Credenciales y token",       null, null, 0.5, 0),
        ("vendedor", "login",   "SE.DF.TMCore.HTTPS",  "Credenciales y token",       null, null, 0.5, 0),
        ("comprador","pagos",   "SE.DF.TMCore.HTTPS",  "Pago al escrow",             null, null, 0.5, 0),
        ("vendedor", "pagos",   "SE.DF.TMCore.HTTPS",  "Solicitud de retiro",        null, null, 0.5, 30),
        ("pagos",    "vendedor","SE.DF.TMCore.HTTPS",  "Retiro pagado (cobro)",      null, null, 0.5, 30),
        ("login",    "pagos",   "SE.DF.TMCore.HTTPS",  "Identidad validada",         null, null, 0.5, 0),
        ("bd",       "login",   "SE.DF.TMCore.Binary", "Llave de firma y sesiones",  "NorthWest", "SouthWest", 0.3, 0),
        ("pagos",    "bd",      "SE.DF.TMCore.Binary", "Descuento de saldo",         "South", "East", 0.5, -30),
        ("bd",       "pagos",   "SE.DF.TMCore.Binary", "Saldo y datos personales",   "NorthEast", "SouthWest", 0.4, -30),
        ("login",    "logs",    "SE.DF.TMCore.Binary", "Eventos de login",           "SouthEast", "North", 0.3, 0),
        ("pagos",    "logs",    "SE.DF.TMCore.Binary", "Eventos de pagos y retiros", null, null, 0.5, 0),
        ("personal", "bd",      "SE.DF.TMCore.Binary", "Acceso administrativo",      null, null, 0.3, 0),
        ("logs",     "personal","SE.DF.TMCore.HTTPS",  "Alertas y auditoría",        null, null, 0.7, 0),
    };

    // Atributos configurables de la plantilla que describen el sistema (afectan la generacion de amenazas).
    static readonly (string Key, string Attribute, string Value)[] Attributes =
    {
        ("bd",   "Stores Credentials", "Yes"),
        ("logs", "Stores Log Data",    "Yes"),
    };

    public static object Run(Assembly model, string template)
    {
        var om = model.GetType("ThreatModeling.Model.ObjectModel")
            .GetConstructor(new[] { typeof(string), typeof(bool), typeof(bool), typeof(bool) })
            .Invoke(new object[] { template, true, false, false });
        // Sin interfaz no hay temporizador de validacion: se pospone el procesamiento hasta terminar de dibujar.
        om.GetType().GetField("_suppressProcessModel", All).SetValue(om, true);
        dynamic ds = ((IEnumerable)Get(Get(Get(om, "ModelDynamicData"), "DrawingSurfaces"), "Values")).Cast<object>().Single();
        ds.Header = "Marketplace: Silk Road y Coupang";

        var el = new Dictionary<string, dynamic>();
        foreach (var e in Elements)
        {
            dynamic t = ds.AddElement(e.Type, e.X, e.Y, e.Name) ?? throw new InvalidOperationException($"Tipo no existe en la plantilla: {e.Type}");
            t.Width = e.W; t.Height = e.H;
            el[e.Key] = t;
        }
        foreach (var a in Attributes)
        {
            dynamic attr = ((IEnumerable)el[a.Key].PropertyMap.Values).Cast<dynamic>().Single(p => p.DisplayName == a.Attribute);
            attr.SelectedIndex = ((IList<string>)attr.Value).IndexOf(a.Value);
        }
        var endpoint = model.GetType("ThreatModeling.Model.LineEndpoint");
        foreach (var f in Flows)
        {
            dynamic c = ds.AddElement(f.Type, 0, 0, f.Name);
            ds.AttachConnector(c, el[f.From], el[f.To]);
            var portType = ((object)c.PortSource).GetType();
            if (f.FromPort != null) c.Attach(el[f.From], (dynamic)Enum.Parse(portType, f.FromPort), (dynamic)Enum.Parse(endpoint, "Source"));
            if (f.ToPort != null) c.Attach(el[f.To], (dynamic)Enum.Parse(portType, f.ToPort), (dynamic)Enum.Parse(endpoint, "Target"));
            // Asa (y etiqueta) sobre el flujo en la posicion T, desplazada en perpendicular si hay Bend.
            double dx = c.X1 - c.X0, dy = c.Y1 - c.Y0, len = Math.Max(1, Math.Sqrt(dx * dx + dy * dy));
            c.MPX = (int)Math.Round(c.X0 + dx * f.T - dy / len * f.Bend);
            c.MPY = (int)Math.Round(c.Y0 + dy * f.T + dx / len * f.Bend);
        }

        dynamic meta = Get(om, "MetaInformation");
        meta.ThreatModelName = "Vulnerabilidades de un marketplace: Silk Road frente a Coupang";
        meta.Owner = "Sergio Alfredo Junca Valero";
        meta.Contributors = "Sergio Alfredo Junca Valero";
        meta.HighLevelSystemDescription =
            "Marketplace genérico que custodia dinero y datos de terceros. Comprador y vendedor se autentican (login, sesiones y tokens), " +
            "pagan y cobran por el servicio de pagos, retiros y escrow. La plataforma guarda datos personales, llaves de firma y configuración; " +
            "el personal con acceso la administra y los logs permiten detectar y responder.";
        meta.Assumptions =
            "Análisis y fuentes en Entrega2-Marketplace-Junca.pdf (Silk Road 2011-2014 y Coupang 2024-2026). " +
            "Los números 1 a 5 junto a los elementos son los cinco puntos de falla del PDF (leyenda abajo a la izquierda). " +
            "Es un modelo genérico: no reconstruye la arquitectura real de ninguno de los dos sitios.";
        return om;
    }

    static object Get(object o, string name) => o.GetType().GetProperty(name, All)?.GetValue(o) ?? o.GetType().GetField(name, All)?.GetValue(o);
}
