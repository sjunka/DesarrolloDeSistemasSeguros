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

## Qué quedó documentado y qué falta ejecutar

El método, comandos y payloads de las 6 partes están redactados y verificados como técnica.
Lo que exige la VMware (capturas de SQLMap, ZAP y SonarQube corriendo contra la IP asignada)
hay que ejecutarlo sobre la máquina vulnerable montada en VMware y pegar las capturas en `img/`.
Ninguna captura está inventada: las que falten quedan marcadas en `final.html`.

La parte 3 (tarjetas de Mutillidae por SQLi) ya tiene evidencia real de la técnica manual UNION
en `evidencias/sqli-mutillidae.md` (clase 23-oct, playground kontinuagroup). La guía pide hacerlo
con SQLMap sobre la VM; el `final.html` trae los comandos exactos.

## Checklist final

- [ ] VMs montadas y con IP asignada por VMware
- [ ] Parte 2: capturas de DVWA (SQL Injection, Low) con los payloads
- [ ] Parte 3: capturas de SQLMap (`--dbs`, `--tables`, `--dump`) sobre DVWA y tarjetas de Mutillidae
- [ ] Parte 4: captura del alert en DVWA + localizaciones XSS confirmadas en la instancia de Juice Shop
- [ ] Parte 5: ZAP automated scan sobre Juice Shop, un pantallazo por alerta
- [ ] Parte 6: SonarQube con el detalle de DVWA y de Mutillidae
- [ ] Nombre del integrante en el documento
- [ ] Entregado y estado actualizado en `board.md`
