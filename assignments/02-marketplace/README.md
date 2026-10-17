# Entrega 02 - Vulnerabilidades de un marketplace (Silk Road vs Coupang)

- Fecha límite: por confirmar
- Peso: por confirmar
- Modalidad: por confirmar (individual, como la entrega 1)
- Canal de entrega: por confirmar
- Enunciado: pedido de Sergio, sin material del profe. PDF de 2 páginas, fácil de leer y de explicar en clase, con research (Silk Road y un caso reciente).

## Entregable

`Entrega2-Marketplace-Junca.pdf` (2 págs, Letter). Fuente en `pdf/marketplace.html`; regenerar con `python3 pdf/build.py` (requiere Google Chrome).

Estructura: idea central, 3 cifras, mapa de 5 puntos de falla, línea de tiempo de cada caso, tabla comparativa con controles y trade-offs, ejemplo de retiro atómico (SQL), tratamiento del riesgo (ISO 31000), objeción y respuesta, referencias APA 7.

### Modelo de amenazas (.tm7)

`Marketplace_SergioJunca.tm7`, para el Microsoft Threat Modeling Tool 7.3 (plantilla SDL TM Knowledge Base (Core) 4.1.0.11). Es el mapa del PDF como diagrama de flujo de datos:

- Frontera de confianza "Plataforma del marketplace" con Login, Pagos, Servidores/llaves y Logs adentro. Comprador, Vendedor y Personal con acceso quedan afuera.
- 13 flujos con nombre (HTTPS hacia los usuarios, Binary hacia la BD y los logs).
- Números 1 a 5 junto a cada elemento y una leyenda: son los puntos de falla del PDF.
- 74 amenazas STRIDE generadas por la herramienta, en estado "Not Started" (sin triage).

Regenerar y validar: `tm7/build.sh`. Valida otro archivo: `tm7/build.sh archivo.tm7`. Necesita el SDK de .NET 8 (`~/.dotnet`). El script baja el TMT oficial (`aka.ms/threatmodelingtool`), verifica cada archivo con el SHA-256 del manifiesto y lo guarda fuera del repo. Arma el modelo con la API interna de la herramienta y lo guarda con su propio `SaveModel`. Después lo vuelve a abrir con el mismo código que usa el TMT al abrir un archivo. Los shims de `tm7/shim/` imitan solo lo mínimo de WPF que esa carga toca.

Validado el 2026-10-17. Abre sin avisos de carga, con 0 avisos de validación, y las amenazas guardadas son las mismas que el motor regenera. El validador se calibró con un .tm7 hecho en la interfaz por compañeros: abre igual y regenera exactamente sus 198 amenazas. La KnowledgeBase embebida es idéntica byte a byte a la de ese archivo (salvo un id interno). El .tm7 anterior, hecho a mano, no abría: le faltaba la KnowledgeBase y sus campos tenían el namespace equivocado.

## Conexión con el curso

OWASP Top 10 (como en la entrega 1), propiedades CIA + trazabilidad + no repudio, etapas DevSecOps, WAF / rate limit / fail2ban, tratamiento de riesgos ISO 31000, deber de notificar (SIC, Ley 1581; GDPR 72 h) y tokens de GitHub (clase del 9 de oct.).

## Qué no quedó verificado

- justice.gov bloquea la descarga automática: las cifras del exploit de Zhong (9 cuentas, más de 140 transacciones, sep. 2012, más de 50.000 BTC) se verificaron en Bitcoin Magazine (2022), no en el comunicado del DOJ.
- Cómo halló el FBI el servidor de Silk Road sigue en disputa (CAPTCHA según el FBI; PHPMyAdmin según Weaver, citado por Schneier). El PDF lo dice así.
- Coupang discute el alcance de la fuga; la multa del PIPC (jun. 2026) puede cambiar si la impugna.
- La cita del Ministerio de Ciencia es traducción propia del inglés de Reuters.
- El mecanismo interno exacto del exploit de retiros no es público: el SQL del PDF es un ejemplo de defensa, no una reconstrucción.
- El .tm7 no se abrió en la interfaz gráfica del TMT en Windows: se validó con el código de carga de la herramienta en macOS. El dibujo final de la interfaz se aproxima con `tm7/preview.svg`.

## Checklist final

- [x] 2 páginas
- [x] Referencias APA 7 y sin datos inventados
- [x] Nombre del autor en el documento
- [x] .tm7 validado con el código del TMT 7.3 (`tm7/build.sh`)
- [ ] Confirmar con el profe si es entrega oficial, fecha y canal
- [ ] Entregado y estado actualizado en `board.md`
