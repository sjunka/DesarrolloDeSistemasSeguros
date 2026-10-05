# Entrega 01 - Hackademic CMS Challenges

- Fecha límite: por confirmar
- Peso: por confirmar
- Modalidad: por confirmar (preguntar al profe)
- Canal de entrega: por confirmar
- Enunciado: taller en https://playground.kontinuagroup.com/pages/showchallenges.php?id=1&class_id=1 (plataforma Hackademic CMS)

## Objetivo

Resolver y documentar paso a paso los ejercicios 1, 2, 5, 6, 7 y 10 de Hackademic.
Nivel técnico sencillo, pero hay que pensar como hacker. Documentar:
- La filosofía/razonamiento del reto.
- El paso a paso de la solución.
- Posibles soluciones alternativas.

## Sitio de la entrega

App React+Vite (mismo stack que el repo de Nube) en `../../app/`. Rutas `#/reto/NN`.
Correr local: `cd app && npm install && npm run dev`. Publica a GH Pages vía `.github/workflows/deploy.yml` (base `/DesarrolloDeSistemasSeguros/`).
Contenido por reto en `app/src/content/retoNN.json`; capturas en `app/public/img/retoNN/`.

## Ejercicios a entregar (resueltos sobre la instancia real + evidencia)

- [x] Ejercicio 1 - Datos sensibles en fuente + listado de directorios. white/rabbit → `mails.php` → imagen apunta a `secret_area_/` → `mails.txt`. **Respuesta: `Friday13@JasonLives.com`** (Jasson Killer, viernes 13).
- [x] Ejercicio 2 - Validación de contraseña del lado del cliente (`GetPassInfo()` sobre "givesacountinatoap lary"). **Clave: `enter a coin to play`**.
- [x] Ejercicio 5 - Control de acceso por `User-Agent`. **UA: `p0wnBrowser`**.
- [x] Ejercicio 6 - JS ofuscado (`document.write(unescape(...))`) + comparación en claro. **Código: `easyyyyyyy!`**.
- [x] Ejercicio 7 - Info disclosure (`index_files/lastlogin.txt` filtra `Irene`) + broken access control (`ch007.php` entrega notas privadas sin autorización; la cookie `not_the_cookie_you_are_looking_for` es señuelo).
- [x] Ejercicio 10 - Campo oculto `LetMeIn=False`→`True` → alert URL-encoded. **Serial: `TRVN-67Q2-RU98-546F-H1ZT`**.

Cada reto documentado con: filosofía, recon, paso a paso, soluciones alternativas, cómo se previene (desarrollo seguro) y mapeo OWASP Top 10. Las 14 capturas son reales (Playwright sobre la instancia del curso), en `../../app/public/img/retoNN/`.

Sitio en vivo: https://sjunka.github.io/DesarrolloDeSistemasSeguros/#/reto/01

Pendiente (no técnico): confirmar con el profe fecha/peso/canal de entrega y el nombre/número de equipo.

## Plan

1. Entrar a cada reto, hacer recon (fuente, URLs, forms, robots.txt).
2. Identificar la vulnerabilidad/técnica.
3. Resolver y capturar evidencia (screenshots → `evidencias/`).
4. Documentar filosofía + paso a paso + alternativas por reto.

## Checklist final

- [ ] Los 6 ejercicios (1,2,5,6,7,10) documentados paso a paso
- [ ] Filosofía del tema por reto
- [ ] Posibles soluciones alternativas
- [ ] Evidencias/capturas incluidas
- [ ] Nombres de integrantes en el documento
- [ ] Formato pedido (PDF/DOCX/repo) - confirmar
- [ ] Entregado y estado actualizado en `board.md`
