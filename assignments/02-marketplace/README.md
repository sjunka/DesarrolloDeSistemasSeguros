# Entrega 02 - Vulnerabilidades de un marketplace (Silk Road vs Coupang)

- Fecha límite: por confirmar
- Peso: por confirmar
- Modalidad: por confirmar (individual, como la entrega 1)
- Canal de entrega: por confirmar
- Enunciado: pedido de Sergio, sin material del profe. PDF de 2 páginas, fácil de leer y de explicar en clase, con research (Silk Road y un caso reciente).

## Entregable

`Entrega2-Marketplace-Junca.pdf` (2 págs, Letter). Fuente en `pdf/marketplace.html`; regenerar con `python3 pdf/build.py` (requiere Google Chrome).

Estructura: idea central, 3 cifras, mapa de 5 puntos de falla, línea de tiempo de cada caso, tabla comparativa con controles y trade-offs, ejemplo de retiro atómico (SQL), tratamiento del riesgo (ISO 31000), objeción y respuesta, referencias APA 7.

## Conexión con el curso

OWASP Top 10 (como en la entrega 1), propiedades CIA + trazabilidad + no repudio, etapas DevSecOps, WAF / rate limit / fail2ban, tratamiento de riesgos ISO 31000, deber de notificar (SIC, Ley 1581; GDPR 72 h) y tokens de GitHub (clase del 9 de oct.).

## Qué no quedó verificado

- justice.gov bloquea la descarga automática: las cifras del exploit de Zhong (9 cuentas, más de 140 transacciones, sep. 2012, más de 50.000 BTC) se verificaron en Bitcoin Magazine (2022), no en el comunicado del DOJ.
- Cómo halló el FBI el servidor de Silk Road sigue en disputa (CAPTCHA según el FBI; PHPMyAdmin según Weaver, citado por Schneier). El PDF lo dice así.
- Coupang discute el alcance de la fuga; la multa del PIPC (jun. 2026) puede cambiar si la impugna.
- La cita del Ministerio de Ciencia es traducción propia del inglés de Reuters.
- El mecanismo interno exacto del exploit de retiros no es público: el SQL del PDF es un ejemplo de defensa, no una reconstrucción.

## Checklist final

- [x] 2 páginas
- [x] Referencias APA 7 y sin datos inventados
- [x] Nombre del autor en el documento
- [ ] Confirmar con el profe si es entrega oficial, fecha y canal
- [ ] Entregado y estado actualizado en `board.md`
