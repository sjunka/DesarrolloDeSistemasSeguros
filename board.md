# Board - Desarrollo de Sistemas Seguros (SI6007)

Profesor(a): Danny Andres Salcedo Saldana · Universidad EAFIT · 35-502 (vie) / 35-302 (sáb)

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

Estados: Pendiente, En curso, Hecho, Entregado, Calificado.

## Bitácora

### 2026-10-03 - Clase 1
- Taller en clase: Hackademic CMS Challenges (playground.kontinuagroup.com, id=1, class_id=1). Fecha límite y entrega: por confirmar.

### 2026-10-09 - Clase (vie)
- Para sáb 10-oct: llevar instalada la herramienta del enlace del chat de Teams (Windows). Pendiente: confirmar cuál es.

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
