# Board - Desarrollo de Sistemas Seguros (SI6007)

Profesor(a): Manuel Humberto Santander Peláez · Universidad EAFIT · 35-502 (vie) / 35-302 (sáb)

Pega aquí, sin formato, todo lo que diga el profesor. Se organiza después.

## Equipo

**Equipo ?** (el mío):
- ?

Otros equipos:
- (pendiente)

## Evaluación

| Componente | Peso | Individual/Equipo | Fecha |
|------------|------|-------------------|-------|
| (completar con el syllabus) | | | |

## Entregas

| # | Nombre | Fecha | Peso | Entrega por | Estado |
|---|--------|-------|------|-------------|--------|
| 1 | Hackademic CMS - retos 1,2,5,6,7,10 (paso a paso) | por confirmar | por confirmar | Brightspace (individual) | Entregado (2026-10-09, Brightspace): `assignments/01-hackademic/Entrega1-Hackademic-Junca.pdf` |
| 2 | Vulnerabilidades de un marketplace (Silk Road vs Coupang), PDF de 2 págs | por confirmar | por confirmar | por confirmar | Hecho (PDF listo, 2026-10-16; .tm7 validado y listo para subir, 2026-10-17): `assignments/02-marketplace/Entrega2-Marketplace-Junca.pdf` y `Marketplace_SergioJunca.tm7`. Falta confirmar si es entrega oficial y el canal |
| 3 | Juice Shop - vulnerabilidad del carrito para dejar la compra en 0 | por confirmar | por confirmar | por confirmar | Hecho (2026-10-23): PoC explotada en vivo (cantidad negativa en `PUT /api/BasketItems/:id`, pedido confirmado en 0.00). Entrega offline `assignments/03-juiceshop-carrito/final.html` con capturas. Falta confirmar si es entrega oficial y el canal |
| 4 | Trabajo final - SQLi (manual+SQLMap), XSS, ZAP, SonarQube sobre DVWA/Mutillidae/Juice Shop | por confirmar | por confirmar | por confirmar | En curso (2026-10-24): evidencias reales ejecutadas en Docker (mismas apps que la VM). SQLi UNION DVWA, dump SQLMap `dvwa.users`, tarjetas Mutillidae, DOM XSS Juice Shop, ZAP scan, SonarQube DVWA (61 hotspots) + Mutillidae (116). Publicado: https://sjunka.github.io/eafit-trabajo-final-seguridad/ (+ `docker-corriendo.html`, `checklist-docker.html`). Añadido 2026-10-24: capturas de las sesiones SQLMap con los **prompts** que muestra la guía (pasos q/r/s DVWA + paso t tarjetas Mutillidae) en `assignments/04-trabajo-final/img/sqlmap-*.png`, y reflexión XSS DVWA probada en `dvwa-xss-reflected.png`. Pendiente: appliance VMware (no descargado aún) para repetir contra IP asignada; pantallazo del cuadro de alert nativo (1 clic en Firefox, URL en el README). Guía en `material-md/Guia-trabajo-final-EAFIT.md` |
| 5 | Verificación de seguridad nivel 1 (ASVS 5.0.0) del marketplace de la Entrega 2 | por confirmar | por confirmar | por confirmar | Hecho (PDF listo, 2026-10-29): `assignments/05-verificacion-asvs/Verificacion-ASVS-Junca.pdf` (11 págs). 70 requisitos L1 reales (26 Cumple, 37 Pendiente, 7 N.A., 0 Brecha), AISVS excluido (sin componentes de IA), Anexo A con la matriz completa. Falta subirlo a la plataforma y confirmar si es entrega oficial y el canal |

Estados: Pendiente, En curso, Hecho, Entregado, Calificado.

## Bitácora

### 2026-10-03 - Clase 1
- Taller en clase: Hackademic CMS Challenges (playground.kontinuagroup.com, id=1, class_id=1). Fecha límite y entrega: por confirmar.

### 2026-10-09 - Clase (vie)
- Para sáb 10-oct: llevar instalada la herramienta del enlace del chat de Teams (Windows). Pendiente: confirmar cuál es.

### 2026-10-24 - Trabajo final documentado

- Guía `material/Guía para trabajo final EAFIT.pdf` convertida a `material-md/Guia-trabajo-final-EAFIT.md`.
- 6 partes: (1) montar VMs, (2) SQLi manual DVWA, (3) SQLMap + tarjetas Mutillidae, (4) XSS DVWA + Juice Shop, (5) ZAP automated scan Juice Shop, (6) SonarQube DVWA y Mutillidae.
- `assignments/04-trabajo-final/final.html`: resumen offline con método, comandos y payloads exactos de las 6 partes, estado por parte y guía de entrega. Parte 3 apoyada en evidencia previa real `evidencias/sqli-mutillidae.md`.
- Pendiente (requiere VMware): montar las dos VMs, correr SQLMap/ZAP/SonarQube contra la IP asignada y pegar capturas en `assignments/04-trabajo-final/img/`. No se inventó ninguna captura.

## Sin clasificar

### 2026-10-04 - Avance entrega 1
- Sitio React+Vite (igual que el repo de Nube) en `app/`, ruta `#/reto/01..10`. Documentados retos 1,2,5,6,7,10: filosofia + paso a paso + alternativas + defensa (desarrollo seguro) + mapeo OWASP.
- Falta: pegar capturas de la instancia en `app/public/img/retoNN/` y confirmar valores propios (correo reto 1, clave reto 2/6, serial reto 10). Crear repo GitHub + habilitar Pages para publicar.

### 2026-10-05 - Entrega 1 resuelta y publicada
- Los 6 retos (1,2,5,6,7,10) resueltos sobre la instancia real (login admin del playground) y capturados con Playwright. Evidencia en `app/public/img/retoNN/`.
- Respuestas: R1 correo `Friday13@JasonLives.com`; R2 clave `enter a coin to play`; R5 User-Agent `p0wnBrowser`; R6 codigo `easyyyyyyy!`; R7 info disclosure `index_files/lastlogin.txt` -> usuario `Irene` -> notas privadas (broken access control; la cookie es senuelo); R10 `LetMeIn=True` -> serial `TRVN-67Q2-RU98-546F-H1ZT`.
- Sitio en vivo: https://sjunka.github.io/DesarrolloDeSistemasSeguros/#/reto/01
- Pendiente: confirmar con el profe fecha/peso/canal de entrega y nombre de equipo.

### 2026-10-09 - Entrega 1 PDF finalizada (individual)
- PDF de bitácora listo: `assignments/01-hackademic/Entrega1-Hackademic-Junca.pdf` (14 págs, 6 hallazgos con evidencia).
- Reto 1: agregada la evidencia que faltaba, capturada en vivo del playground (solo lectura): `view-source` de `ch001/index.php` con `white, rabbit` en `color="#FFFFFF"` (blanco sobre blanco, línea 30) y el listado de directorios de `secret_area_/` (mails.gif, mails.txt). Capturas en `app/public/img/reto01/04-fuente-credenciales.png` y `05-listado-secret-area.png`; pies corregidos (la portada renderizada no muestra las credenciales).
- Modalidad confirmada: **individual**, entrega por **Brightspace**. **Entregado el 2026-10-09 por Brightspace.** Pendiente solo: confirmar fecha/peso con el profe.

### 2026-10-16 - PDF de marketplace (2 págs)
- `assignments/02-marketplace/Entrega2-Marketplace-Junca.pdf`: Silk Road (2011-2014) vs Coupang (2025-2026), 5 puntos de falla, controles con trade-offs, referencias APA 7. Fuente `pdf/marketplace.html`, build con `python3 pdf/build.py`.
- Pendiente: confirmar con el profe si es entrega oficial, fecha, peso y canal (README de la carpeta lista lo que no quedó verificado).

### 2026-10-17 - .tm7 del marketplace validado
- `assignments/02-marketplace/Marketplace_SergioJunca.tm7` regenerado: el anterior no abría en el Threat Modeling Tool (faltaba la KnowledgeBase). Es el DFD del mapa del PDF: frontera de confianza, 13 flujos, números de los 5 puntos de falla y 74 amenazas STRIDE generadas por la herramienta.
- Validado con el código de carga del TMT 7.3.51110 en macOS (`tm7/build.sh`). No se probó en la interfaz de Windows. Listo para subir a la plataforma del profe.

### 2026-10-24 - Entrega 3: vector SQLi del profe verificado y descartado
- El profe sugirió lograrlo con SQLi + UPDATE. Probado en vivo: search (`/rest/products/search?q=`) y login son inyectables (SQLite), pero solo `SELECT`. Stacked `;UPDATE Products SET price=...` → `SQLITE_ERROR: near ";"`; precio id=1 sigue 1.99. No hay UPDATE por SQLi en esta instancia.
- El reto real es **Payback Time** (Improper Input Validation): cantidad negativa en el carrito. Nuestra PoC ya lo resuelve.
- `final.html` actualizado con sección 4 "Vector SQLi con UPDATE: descartado (verificado)".

### 2026-10-29 - Entrega 5: verificación ASVS 5.0.0 nivel 1
- Tomado como base un ejemplo de un compañero (taller "verificación L1 de su proyecto", `Marketplace_verificacion-JohnJimenez-JuanCastro.pdf`): misma metodología, aplicada al propio proyecto (Entrega 2, no al marketplace del ejemplo).
- Los 70 requisitos L1 de ASVS 5.0.0 se descargaron del CSV oficial de `OWASP/ASVS` (tag `v5.0.0_release`), no se transcribieron de memoria. Resultado: 26 Cumple, 0 Heredado, 37 Pendiente, 7 N.A., 0 Brecha.
- AISVS no se evaluó: el marketplace no tiene componentes de IA; forzarlo habría significado inventar un asistente o modelo de fraude que el proyecto no tiene.
- Hallazgo central: `v5.0.0-9.1.3` (origen/distribución de la llave que valida los tokens) es la causa raíz real del caso Coupang y el único de los 5 puntos priorizados que el diseño actual no resuelve.
- `assignments/05-verificacion-asvs/Verificacion-ASVS-Junca.pdf` (11 págs). Fuente `pdf/report.html`, build con `python3 pdf/build.py`. 2 diagramas propios (arquitectura + condición de carrera del retiro atómico).
