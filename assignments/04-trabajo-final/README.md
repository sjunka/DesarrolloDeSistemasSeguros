# Trabajo final - Pruebas de seguridad sobre máquinas vulnerables

- Fecha límite: por confirmar con el profe
- Peso: por confirmar
- Modalidad: por confirmar (individual probable, como las anteriores)
- Canal de entrega: por confirmar (Brightspace probable)
- Enunciado: `material-md/Guia-trabajo-final-EAFIT.md` (PDF original en `material/`)

## Qué pide (6 partes)

1. Instalar las VMs: máquina de pruebas (`websecuritytestingarm.7z` en ARM / `...amd64.7z` en Intel) y la máquina web vulnerable (`vulnerablewebappsarm.7z` / `vapps-amd64.7z`).
2. SQLi manual sobre DVWA. Documento con el paso a paso.
3. SQLi automatizada con SQLMap. Termina sacando las tarjetas de crédito de Mutillidae. Documento con el paso a paso.
4. XSS: reflejado en DVWA y conteo + localizaciones de XSS en OWASP Juice Shop. Documento.
5. Chequeo dinámico con OWASP ZAP (automated scan) sobre Juice Shop. Resultados con pantallazo por ítem.
6. Análisis estático con SonarQube sobre DVWA y sobre Mutillidae. Documento con el detalle de vulnerabilidades.

## Entregables

- `final.html`: resumen offline con el método documentado de las 6 partes, estado y guía de entrega. Abrir con doble clic.
- `img/`: capturas que se toman al ejecutar cada parte en la VM (ver `final.html`).

## Qué quedó ejecutado

Las 6 partes están ejecutadas con evidencia real sobre un laboratorio local (Docker) que corre
las mismas aplicaciones vulnerables que empaqueta la VM del curso (DVWA, Mutillidae, Juice Shop),
con idénticas vulnerabilidades. Lo único que cambia frente a la VM es la dirección
(`localhost:puerto` en vez de la IP DHCP de VMware). Ninguna captura está inventada.

La parte 3 (tarjetas de Mutillidae por SQLi) tiene además evidencia de la técnica manual UNION
en `evidencias/sqli-mutillidae.md` (clase 23-oct, playground kontinuagroup), que confirma el
esquema que SQLMap reproduce.

## Capturas de los prompts de SQLMap (añadidas 2026-10-24)

Sesiones reales de SQLMap sobre los contenedores Docker (mismas apps que la VM), con el comando
exacto arriba y las **preguntas interactivas** que la guía menciona ("dale enter a las respuestas
por defecto"):

- `img/sqlmap-q-currentdb.png`: paso q — `-b --current-db --current-user` (banner MariaDB, `current database: dvwa`, `current user: app@localhost`).
- `img/sqlmap-r-tables.png`: paso r — `--tables -D dvwa` (muestra `guestbook` y `users`).
- `img/sqlmap-s-dump-users.png`: paso s — `--dump -T users -D dvwa` con los prompts de cracking (`store hashes [y/N] N`, `crack them [Y/n/q] Y`) y el volcado de los 5 usuarios.
- `img/sqlmap-mutillidae-cards.png`: paso t — `--dump -T credit_cards -D mutillidae` con el prompt de cookie y las 5 tarjetas de crédito.
- `img/dvwa-xss-alert.png`: parte 4 — el **alert nativo** `localhost says: hola` disparado en vivo sobre DVWA (XSS reflejado, Security Low).
- `img/dvwa-xss-reflected.png`: parte 4 (causa raíz) — respuesta de DVWA con `<script>alert('hola');</script>` reflejado sin escapar dentro de `<pre>`.

## Checklist final

- [x] Entorno vulnerable montado (Docker: DVWA, Mutillidae, Juice Shop; mismas apps que la VM) — `img/entorno-dvwa-index.png`
- [x] Parte 2: SQLi manual DVWA (`img/dvwa-union.png`)
- [x] Parte 3: SQLMap sobre DVWA (`q`/`r`/`s`) + tarjetas de Mutillidae, con prompts visibles
- [x] Parte 4: XSS Juice Shop OK (`img/js-xss.png`) + alert nativo DVWA capturado (`img/dvwa-xss-alert.png`) + causa raíz (`img/dvwa-xss-reflected.png`)
- [x] Parte 5: ZAP automated scan Juice Shop (`img/zap-report.png`)
- [x] Parte 6: SonarQube DVWA + Mutillidae (`img/sonar-dvwa.png`, `img/evidencia-sonarqube.png`, `img/svc-*`)
- [ ] Nombre del integrante en el documento
- [ ] Entregado y estado actualizado en `board.md`

## Para el pantallazo del alert (parte 4), un clic

Estás logueado en DVWA (Security = Low). Abre esta URL en el navegador y sale el `alert('hola')`:

```
http://localhost:8080/vulnerabilities/xss_r/?name=<script>alert('hola');</script>
```
