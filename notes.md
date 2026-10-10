# Notas de clase - Desarrollo de Sistemas Seguros (SI6007)

Profesor(a): Danny Andres Salcedo Saldana · Universidad EAFIT · 35-502 (vie) / 35-302 (sáb)
Vie 6-8 pm, Sáb 8 am-12 m, 2 oct - 24 oct 2026

Aquí va lo que dice el profesor: conceptos, ejemplos, aclaraciones.
Tareas, fechas y entregas van en `board.md`. Clases recientes arriba.

---

## 2026-10-09 - Clase (vie)

**Notas:**
- Dicta: Manuel Humberto Santander Peláez (virtual, Teams con transcripción).
- Repaso: hace 8 días vimos los 7 pasos para que se materialice un incidente.
- Tácticas = categorías de acciones del atacante; técnicas = acciones particulares dentro de cada categoría.
- Persistencia: cualquier cosa que el atacante haga en un equipo para mantener acceso. Puede quedarse meses sin que uno se entere.
- Nube (IaaS): región con zonas de disponibilidad (cómputo, almacenamiento, BD, networking); encima van servicios SaaS/PaaS/IaaS. Si me hackean, AWS/Azure no responde por los datos de mis clientes. En IaaS yo protejo datos de clientes, plataformas, aplicaciones e IAM.
- Todo software de base, sin excepción, debe tener línea base de seguridad y paquetes actualizados.
- Librerías: el 99% de las veces se bajan de GitHub. Riesgo de cadena de suministro.
- Video (min 28-32) sobre fallos recientes:
  - RCE en servidores de GitHub agregando una cabecera en un push (reportado por la empresa Wiz, ya corregido).
  - Acceso no autorizado a ~3.800 repos internos de GitHub: un desarrollador instaló una versión alterada de una extensión de VS Code (Angular / Nx Console, manejador de monorepos) que robaba el GitHub token. ~28 instalaciones de la versión maliciosa. La extensión no es maliciosa, solo esa versión.
  - Paquetes con hooks que envían credenciales (GitHub) desde los repos donde se instalan.
  - Vulnerabilidades de años en software base salen a la luz; zero-day reportado en Nginx.
  - Lección: secrets en el repo + token del desarrollador = acceso a infraestructura y CI/CD.
- DevSecOps: CI/CD como ciclo infinito. Desarrollo = planear, codificar, construir, probar. Operación = release, implementar, operar, monitorear. Seguridad en cada etapa:
  - Planear: modelado de amenazas (desde mañana en detalle, modelo básico). Un análisis completo puede dar 500.000-1.000.000 escenarios de riesgo, inmanejable; enfocarse en los más críticos deja el riesgo razonablemente controlado. De las amenazas salen los requerimientos (la otra semana, incluye temas de IA). También planificación organizacional para integrar el proceso de punta a punta.
  - Codificar: gestión de información sensible (secrets, que no se filtren usuarios y contraseñas), pruebas estáticas, lineamientos de seguridad por framework.
  - Construir: revisar seguridad de librerías de terceros, actualizarlas sin romper nada, pruebas estáticas en el build.
  - Probar: pruebas dinámicas.
  - Release: autorización de seguridad, verificar que no se fuguen secrets.
  - Desplegar: revisar vulnerabilidades de contenedores y artefactos de despliegue.
  - Operar: parchar seguridad. Monitorear la operación.
- Fin lección 1.
- Tarea para mañana (sáb 10-oct): llegar con una herramienta instalada (enlace para Windows que el profe puso en el chat de Teams; no quedó en la transcripción). Se trabaja en la sesión.

---

## 2026-10-02 - Clase (vie)

**Notas:**
- Ingeniería social: hay gente que cree lo que sea y cae en el fraude.

---

## 2026-10-03 - Clase 1 (sáb)

**Notas:**
- El profesor pide hacer el taller en https://playground.kontinuagroup.com/pages/showchallenges.php?id=1&class_id=1
- Plataforma: Hackademic CMS Challenge Series. Requiere login (registro o recuperar clave).
- Reto 1: encontrar el correo del agente desaparecido (investigación a empresa de logística sospechosa).
  Enunciado: "Our agents informed us that there is reasonable suspicion that the site of this Logistics Company is a blind for a human organs smuggling organisation. Your objective is to find the missing agent's email address."
- El profe muestra el Gartner Magic Quadrant for Access Management (Okta aparece como referente).
- El profe dice usar AWS Cognito.
- El profe dice que podemos usar SAML.
- El profe menciona OAuth2 + OpenID Connect (OIDC).
- El profe abre la página de FingerprintJS (device/browser fingerprinting).
- Impedir clonación de sitios para fraude:
  1. Bloquear GET recursivos (clonadores: wget --mirror, HTTrack piden todo el árbol).
  2. Rate limit: no permitir GETs ilimitados (nginx `limit_req`, WAF).
  3. fail2ban: lee logs y banea por iptables la IP que supera umbral.
  4. User-Agent: filtrar/verificar UA. Señal débil (se falsifica), capa de primera línea.
     - Bloquear UA de clonadores: wget, HTTrack, curl, python-requests, Scrapy, UA vacío.
     - Bots buenos (Googlebot): verificar por reverse+forward DNS, no solo por el string UA.
     - nginx: `if ($http_user_agent ~* (wget|httrack|curl|python-requests|scrapy)) { return 403; }`
  5. WAF (Web Application Firewall): centraliza rate limit, filtro UA/IP y antibot detection en el borde, antes de la app. Ejemplos: AWS WAF, Cloudflare, Akamai, Imperva, F5. Bot management (módulo) usa fingerprint, JS challenge, reputación IP y ML. fail2ban es la versión self-hosted/barata.
  6. robots.txt: voluntario, no es control. Lo respetan crawlers honestos (Googlebot, GPTBot, ClaudeBot). Lo ignoran scrapers maliciosos y el scraping self-hosted/on-prem (scripts propios, Scrapy, wget). Nota: Ollama (runtime local de LLM) y Kimi (modelo) no son crawlers; el punto válido es que el scraping on-prem no consulta robots.txt.
- Detectar phishing ya clonado: monitoreo de dominios typosquatting, CT logs (certificados nuevos con tu marca), servicios de takedown.
- Trade-off: rate limit/UA agresivos molestan a usuarios y SEO legítimos; afinar umbrales y hacer allowlist de bots buenos.
- La suplantación de sitios web es delito. Hay que crear denuncia penal por suplantación.
- Atribución del atacante:
  - Logs (nginx/WAF) dan la IP; la identidad (nombre) requiere orden judicial al ISP. VPN/proxy/Tor/CGNAT rompen la cadena.
  - "El convenio da nombres, el otro da direcciones IP": el Convenio de Budapest (Cibercrimen) + MLAT permite pedir datos de identidad a autoridades extranjeras; los logs técnicos dan la IP.
  - WHOIS del dominio falso: el dato de quién compró el dominio es el menos prometedor (privacy/WHOIS protegido, datos falsos).
  - Petición bajo Convenio de Budapest la maneja la DIJIN en el marco de la investigación penal.
  - IP no es culpable (equipo comprometido). Correlacionar IP + fingerprint + UA + horarios + artefactos.
  - Clonar por GET solo se lleva el front-end público (HTML/CSS/JS). El código server-side/repositorio solo se expone por mala config: `.git/` accesible, directory listing, backups `.zip`/`.bak`, fuente servida como texto. Bloquear esas rutas.
- El profe pone Let's Encrypt en el servidor para cifrado (TLS/HTTPS).
  - Let's Encrypt: CA gratuita, cert TLS automático vía protocolo ACME (certbot). Solo DV (Domain Validation). Caduca a 90 días → renovación automática. Da HTTPS (cifrado en tránsito), no en reposo.
  - DV solo prueba control del dominio, no identidad de la org. Un phisher también saca cert LE → HTTPS no equivale a sitio legítimo.
  - Sectigo (ex-Comodo): CA comercial de pago. Ofrece DV, OV (Organization Validation) y EV (Extended Validation) que validan identidad de la empresa. Certs hasta 1 año, wildcard, warranty, soporte, code signing, S/MIME.
  - Ambos dan el mismo cifrado TLS. Diferencia: nivel de validación de identidad y servicios, no la fuerza del cifrado.
  - Cuándo Sectigo: se necesita OV/EV, soporte formal o requisito de cumplimiento. Para cifrado básico basta Let's Encrypt.
- El profe menciona un repo "web digital watermarking": dice que dificulta la clonación porque la marca no queda bien al copiar el sitio.
- El profe comparte otro repo: "an open source toolkit for LLM watermarking".
  - LLM watermarking: marcar el texto que genera un modelo para luego probar que salió de esa IA.
  - Cómo: al generar, sesga la elección de tokens con una clave secreta (listas "verde/roja", prefiere verde). Invisible al lector.
  - Detección: con la clave, test estadístico mide el exceso de tokens verdes → dice si lo generó la IA.
  - Repo candidato: probablemente MarkLLM (toolkit open source, algoritmos KGW/EXP/SIR + visualización). CONFIRMAR nombre/URL con el profe.
  - Usos: detectar contenido IA (plagio, desinfo, bots), trazabilidad de salidas.
  - Límites: frágil (parafrasear/traducir/editar borra la marca); solo si controlas el modelo generador; marca fuerte degrada calidad.
  - Diferencia: web watermarking marca el sitio; LLM watermarking marca texto generado por un modelo.
- Diapositiva "Gestión de riesgos ISO 31000". Identificar riesgos = conocer qué cosas nos pueden pasar.
- El profe menciona la secuencia de pasos llamada kill chain (Cyber Kill Chain).
- Proceso ISO 31000 (según el profe):
  1. Establecer contexto.
  2. Identificar riesgos.
  3. Analizar riesgos.
  4. Evaluar riesgos.
  5. Tratar riesgos.
  Transversales: Comunicación y consulta; Monitoreo y revisión.
  (Pasos 2-4 = valoración del riesgo / risk assessment.)
- DevSecOps incluye monitoreo y ciberseguridad.
- Ante incidentes de seguridad: si la regulación lo exige, hay deber de notificar (en Colombia a la SIC bajo Ley 1581; GDPR 72h; SuperFinanciera para sector financiero).
- Si una persona cae en fraude, aumenta el riesgo de daño reputacional (impacto del riesgo, no solo pérdida económica).
- Estándares para dar a conocer / compartir casos de fraude y riesgos:
  - Threat intel sharing: STIX (formato de amenazas/IOCs), TAXII (protocolo de intercambio), MISP (plataforma open source), TLP (Traffic Light Protocol: ROJO/ÁMBAR/VERDE/BLANCO).
  - Comunidades: ISACs/ISAOs sectoriales (FS-ISAC banca), CSIRT/CERT nacionales (COLCERT, CSIRT Gobierno Colombia), APWG (Anti-Phishing Working Group).
  - Bases públicas de vulnerabilidades/riesgos: CVE/NVD (IDs de vulnerabilidad), CVSS (severidad), MITRE ATT&CK (tácticas/técnicas), OWASP Top 10 (riesgos web).
  - Gestión de incidentes: ISO 27035, NIST 800-61.
  - Caso fraude/clonación: reportar a APWG + Google Safe Browsing (takedown), compartir IOCs vía STIX/TAXII o MISP, reporte legal a la autoridad.
- Diapositiva "Opciones para el tratamiento de riesgos cibernéticos":
  Términos de la diapositiva (terminología ISO 31000/27005):
  1. Modificación del riesgo (mitigar/reducir): controles que bajan probabilidad o impacto (WAF, MFA, parches).
  2. Retención del riesgo (aceptar): asumir el riesgo si está bajo el apetito de riesgo (costo de control > impacto). Deja riesgo residual.
  3. Evitar el riesgo: eliminar la actividad o activo que lo genera.
  4. Compartir el riesgo (transferir): pasar a un tercero (seguro cibernético, outsourcing).
- Diapositiva "Modelo de ciberseguridad": tiene un componente de gobierno porque estos temas no pasan por los desarrolladores (son decisiones de dirección/gobierno, no técnicas del día a día).
  Componentes del modelo (identificar sobre qué se trabaja):
  - Activos
  - Ambiente de negocio
  - Gobierno
  - Riesgos / estrategia de riesgos
  - Controles de acceso
  - Concienciación (awareness)
  - Seguridad en los datos
  - Procesos y procedimientos para la protección de la información
  - Mantenimiento
  - Tecnologías de protección
  Categorías de la función Detect (NIST CSF):
  - Anomalías y eventos
  - Monitoreo continuo de seguridad
  - Procesos de detección
  Categorías de la función Respond (NIST CSF):
  - Plan de respuesta
  - Comunicaciones
  - Análisis
  - Mitigación
  - Mejoras
- Diapositiva "Aspectos vitales en la gestión del riesgo cibernético":
  - Apetito y tolerancia al riesgo: cuánto está dispuesto a perder la organización por el riesgo cibernético.
  - Balance entre la experiencia de usuario (UX) y las medidas de ciberseguridad.
  - Identificar la información crítica para el negocio (activos).
  - Ciberseguridad orientada a los datos y no a la tecnología.
  - La ciberseguridad es un habilitador de la tecnología, no una razón para no aprovecharla.
  - IMPORTANTE - propiedades de la seguridad de la información: Confidencialidad, Integridad, Disponibilidad, Trazabilidad y No repudio (el profe agregó No repudio). Los 5 criterios están orientados a la información/datos; eso se asegura en los datos.
  - Tecnologías de protección mencionadas: firewalls, NDR (Network Detection and Response), cajas encriptoras (HSM / cifradores de red). Son aparatos/appliances físicos de seguridad.
  - Preguntas clave en la gestión del riesgo cibernético (para valorar impacto):
    - ¿Afecta la operación del negocio?
    - ¿Implica la reconstrucción de información?
    - ¿Implica una posibilidad de crisis reputacional?
    - ¿Aplican impactos financieros de corto, mediano o largo plazo?
- El profe abre OSINT Framework (osintframework.com): directorio de herramientas de inteligencia de fuentes abiertas.
- Diapositiva siguiente: "Cómo se realizan los ciberataques" (contenido por pegar).
- El profe muestra un info stealer: RedLine Stealer (malware que roba credenciales, cookies, datos de navegador y wallets).
- OBJETIVO DEL CURSO: entender las implicaciones del riesgo cibernético y tener claridad sobre cuáles son los controles a implementar.

**SQL Injection (SQLi):**
- Qué es: inyectar SQL por un input que la app concatena sin sanear en su query. Atacante cambia la lógica de la consulta.
- Ejemplo login vulnerable: `SELECT * FROM users WHERE user='$u' AND pass='$p'`.
  - Payload en user: `' OR '1'='1' -- ` → query siempre verdadera, bypass de login.
  - `-- ` comenta el resto de la query (ojo con el espacio tras `--` en MySQL).
- Tipos:
  - In-band / clásica: resultado visible. Error-based y UNION-based.
    - UNION: `' UNION SELECT col1,col2 FROM users -- ` para extraer datos. Requiere igual nº de columnas → hallar con `ORDER BY n` o `UNION SELECT NULL,NULL,...`.
  - Blind: sin salida directa. Boolean-based (respuesta cambia true/false) y Time-based (`SLEEP(5)`).
  - Out-of-band: exfiltra por otro canal (DNS/HTTP).
- Defensas (clave del curso):
  1. Consultas parametrizadas / prepared statements. Defensa principal.
  2. ORM con binding de parámetros.
  3. Validación de entrada (allowlist) + escape como capa extra, no principal.
  4. Menor privilegio en la cuenta de BD. WAF como mitigación, no solución.
- Trade-off: prepared statements casi no tienen coste y cortan la causa raíz; validación/escape manual es frágil y escala mal.

**Dudas / pendientes:**
- Fecha límite, peso y forma de entrega del taller (no aparecen en la página).

---

## Glosario

| Término | Definición | Clase |
|---------|------------|-------|
| SQL Injection | Inyección de SQL por input no saneado que altera la query de la app. | 1 |
| Prepared statement | Query con parámetros vinculados (binding); separa código SQL de datos. Defensa principal contra SQLi. | 1 |
| UNION-based SQLi | Técnica que usa `UNION SELECT` para extraer datos de otras tablas. | 1 |
| Blind SQLi | SQLi sin salida directa; se infiere por booleanos o tiempo (`SLEEP`). | 1 |
