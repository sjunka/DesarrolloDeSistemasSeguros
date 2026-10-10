"""Genera la bitacora PDF de la entrega 1 desde app/src/content/retoNN.json.
Uso: python3 build.py  (requiere Google Chrome para imprimir a PDF)."""
import json, pathlib, subprocess, datetime
from PIL import Image, ImageChops

ROOT = pathlib.Path(__file__).resolve().parents[3]
OUT = pathlib.Path(__file__).parent
IMG = ROOT / "app/public/img"
RETOS = ["01", "02", "05", "06", "07", "10"]
SEV = {"01": "Alta", "02": "Alta", "05": "Media", "06": "Alta", "07": "Alta", "10": "Media"}
CHROME = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"

CROP = OUT / "img"

def recorte(src):
    """Quita el margen blanco de la captura para que no ocupe media pagina vacia."""
    out = CROP / src; out.parent.mkdir(parents=True, exist_ok=True)
    im = Image.open(IMG / src).convert("RGB")
    box = ImageChops.difference(im, Image.new("RGB", im.size, (255, 255, 255))).getbbox()
    if box: im = im.crop((max(box[0]-12, 0), max(box[1]-12, 0), min(box[2]+12, im.width), min(box[3]+12, im.height)))
    im.save(out); return out.as_uri()

def li(xs): return "<ul>" + "".join(f"<li>{x}</li>" for x in xs) + "</ul>"

def reto(r):
    pasos = []
    for f in r["phases"]:
        pasos.append(f"<h4>{f['h']}</h4>")
        for s in f["steps"]:
            p = f"<div class='paso'><p>{s['t']}</p>"
            if s.get("e"): p += f"<p class='e'>{s['e']}</p>"
            if s.get("c"): p += f"<pre>{s['c']}</pre>"
            for im in s.get("img", []):
                p += f"<figure><img src='{recorte(im['src'])}'><figcaption>Evidencia: {im.get('cap','')}</figcaption></figure>"
            pasos.append(p + "</div>")
    return f"""<section class='reto'>
<h2>H-{r['id']} · {r['name']}: {r['sub']}</h2>
<table class='meta'><tr><th>Vulnerabilidad</th><td>{r['vuln']}</td></tr>
<tr><th>OWASP Top 10</th><td>{r['owasp']}</td></tr>
<tr><th>Severidad estimada</th><td>{SEV[r['id']]}</td></tr></table>
<h3>Contexto</h3><p>{r['lead']}</p>
<h3>Análisis (cómo piensa el atacante)</h3><p>{r['why']}</p>
<h3>Reconocimiento</h3>{li(r['recon'])}
<h3>Explotación paso a paso</h3>{''.join(pasos)}
<h3>Vías alternativas</h3>{li(r['alt'])}
<h3>Remediación (desarrollo seguro)</h3>{li(r['defensa'])}
</section>"""

rs = [json.load(open(ROOT / f"app/src/content/reto{n}.json")) for n in RETOS]
resumen = "".join(f"<tr><td>H-{r['id']}</td><td>{r['sub']}</td><td>{r['vuln']}</td><td>{r['owasp']}</td><td>{SEV[r['id']]}</td></tr>" for r in rs)
html = f"""<!doctype html><html lang='es'><meta charset='utf-8'><title>Bitácora de seguridad - Hackademic</title>
<style>
@page {{ size: Letter; margin: 16mm 15mm; }}
body {{ font: 10pt/1.4 -apple-system, Helvetica, Arial, sans-serif; color:#1a1a1a; }}
h1 {{ font-size:18pt; margin:0 0 4px; }} h2 {{ font-size:13pt; border-bottom:2px solid #b3261e; padding-bottom:3px; margin-top:0; }}
h3 {{ font-size:10.5pt; color:#b3261e; margin:10px 0 3px; }} h4 {{ font-size:10pt; margin:8px 0 2px; }}
table {{ border-collapse:collapse; width:100%; font-size:9pt; }} th,td {{ border:1px solid #ccc; padding:3px 6px; text-align:left; vertical-align:top; }}
th {{ background:#f3f3f3; }} .meta th {{ width:28%; }}
code,pre {{ font-family: Menlo, monospace; font-size:8.5pt; background:#f5f5f5; }} pre {{ padding:4px 6px; white-space:pre-wrap; margin:3px 0; }}
.e {{ color:#555; font-style:italic; margin:2px 0; }} .paso p {{ margin:3px 0; }} ul {{ margin:2px 0; padding-left:18px; }}
figure {{ margin:4px 0 8px; break-inside:avoid; }} img {{ max-width:100%; max-height:85mm; border:1px solid #bbb; display:block; }}
figcaption {{ font-size:8pt; color:#555; }} .reto {{ margin-top:14px; }} h2,h3,h4 {{ break-after:avoid; }} .muted {{ color:#666; font-size:9pt; }}
</style>
<h1>Bitácora de pruebas de seguridad web</h1>
<p class='muted'>Objetivo: OWASP Hackademic CMS (playground.kontinuagroup.com) · Retos 1, 2, 5, 6, 7 y 10<br>
SI6007 Desarrollo de Sistemas Seguros · Universidad EAFIT · {datetime.date.today():%d/%m/%Y}<br>
Autor: Sergio Alfredo Junca Valero · Versión web: https://sjunka.github.io/DesarrolloDeSistemasSeguros/</p>
<h3>Alcance y metodología</h3>
<p>Prueba de caja negra autorizada sobre la instancia del curso. Para cada reto: reconocimiento (fuente HTML/JS, recursos, cabeceras, formularios), identificación de la vulnerabilidad, explotación mínima para obtener la bandera, captura de evidencia y propuesta de remediación. Herramientas: navegador con DevTools y Playwright para las capturas.</p>
<h3>Resumen de hallazgos</h3>
<table><tr><th>ID</th><th>Reto</th><th>Vulnerabilidad</th><th>OWASP</th><th>Severidad</th></tr>{resumen}</table>
<h3>Conclusión</h3>
<p>Patrón común: <b>confiar en el cliente</b>. Credenciales, validaciones, controles de acceso y secretos están en HTML/JS, cabeceras o campos manipulables por el usuario. Todo lo que llega al navegador es público; la autorización y la validación deben ocurrir en el servidor. Prioridad: control de acceso del lado del servidor (A01), retirar secretos del cliente y deshabilitar el listado de directorios (A05).</p>
{''.join(reto(r) for r in rs)}
</html>"""
h = OUT / "bitacora.html"; h.write_text(html)
pdf = ROOT / "assignments/01-hackademic/Entrega1-Hackademic-Junca.pdf"
subprocess.run([CHROME, "--headless", "--disable-gpu", "--no-pdf-header-footer", "--allow-file-access-from-files", f"--print-to-pdf={pdf}", h.as_uri()], check=True, capture_output=True)
print(pdf)
