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

## Ejercicios a entregar

- [x] Ejercicio 1 - Datos sensibles en la fuente (white/rabbit → buzón → ruta oculta en imagen → emails.txt → agente viernes 13). **Falta:** correo exacto de tu instancia.
- [x] Ejercicio 2 - Validación de contraseña del lado del cliente (JS `GetPassInfo` → consola/debugger). **Falta:** clave exacta.
- [x] Ejercicio 5 - Control de acceso por `User-Agent` (falsificar UA).
- [x] Ejercicio 6 - JS ofuscado (`unescape`/`document.write` → consola). Clave original `easyyyyyyy!`, **confirmar**.
- [x] Ejercicio 7 - Escalada por cookie (`userlevel=user`→`admin`; usuario desde `lastlogin.txt`).
- [x] Ejercicio 10 - Campo oculto `LetMeIn=False`→`True` → decodificar serial. **Falta:** serial exacto.

Cada reto documentado con: filosofía, recon, paso a paso, soluciones alternativas, cómo se previene (desarrollo seguro) y mapeo OWASP Top 10. Faltan solo las capturas de la instancia y confirmar los valores propios marcados arriba.

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
