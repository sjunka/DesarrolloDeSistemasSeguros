# Guía para trabajo final EAFIT (texto extraído del PDF)

                                  GUIA PARA TRABAJO FINAL

1. Instalación máquinas virtuales
Si 1enes un procesador ARM en tu mac, instala por favor las siguientes máquinas virtuales:
    • websecuritytes1ngarm.7z
    • hNps://sourceforge.net/projects/vapps/ﬁles/VMAppliance/V4/vulnerablewebappsarm.7
       z/download (Maquina Web Vulnerable)

Si 1enes un procesador Intel en tu máquina, instala por favor las siguientes máquinas virtuales:
    • websecuritytes1ngamd64.7z
    • hNps://sourceforge.net/projects/vapps/ﬁles/VMAppliance/V4/vapps-
       amd64.7z/download (Maquina Web Vulnerable)

2. Prueba manual SQL Injec;on
A par1r del montaje de la máquina web vulnerable, por favor ejecute la prueba correspondiente.
Redacte un documento con el paso a paso de la solución de este.

3. Pruebas automa;zadas SQL Injec;on
U1lizaremos la maquina Web Vulnerable y la máquina de pruebas de seguridad:
   a. Ingresa a la maquina Web Vulnerable u1lizando la dirección IP que le haya sido asignada
       por VMWare u1lizando Firefox:




   b. Ingresa al administrador de complementos y busca tamper data
c. Dale click al primero: tamper data for FF quantum




d. Dale click en agregar a Firefox




e. Pide permiso para añadir. Dile añadir
f. Dale aceptar




g. Regresa al si1o principal de la maquina web vulnerable
h. Ingresa al link Damn Vulnerable Web Applica1on




i.   U1liza usuario admin y clave password. Llegaras a la siguiente ventana:
j.   Selecciona DVWA Security. Busca la opción Low y dale submit.
k. Dale click a SQL Injec1on




l.   Digita 1 en user ID. Guarda el URL resultante:
m. Busca el icono de complementos e inicia la ejecución de tamper data dándole click




n. Aparece la siguiente ventana. Dale click en yes
o. Selecciona SQL Injec1on del menú izquierdo. Apare la siguiente ventana. Dale OK:
   p. Aparece una nueva ventana. Copia aparte el contenido de la cookie en otro lado y dale
      click en OK.




   q. Ejecutemos SQLMap con los parámetros -u con el URL que guardaron previamente, -b
      para saber que base de datos es, --cookie con la cookie que guardaron previamente, --
      current-db para saber la base de datos actual y --current-user para saber el usuario actual
      de la base de datos. Dale enter a las respuestas por defecto de las preguntas que te
      saldrán:




El resultado que obtendrás es:
r. Ejecutemos SQLMap con los parámetros -u con el URL que guardaron previamente, --
   cookie con la cookie que guardaron previamente, --tables para saber las tablas de las
   bases de datos. Si buscamos en la salida, veremos guestbook y users para dvwa:




s. Ejecutemos SQLMap con los parámetros u con el URL que guardaron previamente, --
   cookie con la cookie que guardaron previamente, --dump para sacar datos y -T con la tabla
   users que vimos. U1liza las respuestas por defecto planteadas a las preguntas:
   t. Ahora saca con SQLmap las tarjetas de crédito de la aplicación vulnerable mu1llidae (la
      segunda de arriba hacia abajo). Redacta un documento con el paso a paso u1lizado.




4. Cross Site Scrip;ng

   a. Ingresemos a la aplicacion Damn Vulnerable web Applica1on con el usuario admin y la
      clave password:
b. Busca DVWA Security y selecciona la opción de low
c. Busca DVWA Security y selecciona la opcion de low




d. Selecciona XSS Reﬂected Digita en el campo de texto <script>alert('hola');</script>:
   e. Se materializa la vulnerabilidad cuando sale la alerta:




   f. Toma la aplicación OWASP Juice Shop. ¿Cuántas vulnerabilidades de Cross Site Scrip1ng
      1ene y en que localizaciones? Redacta un documento con el paso a paso ejecutado.

5. Chequeo dinámico de vulnerabilidades

   a. Inicia OWASP ZAP desde la terminal con el comando zap.sh. Ac1varas la pantalla
      principal:
b. Selecciona la primera opción y quedaras en la pantalla principal. Dale click en automated
   scan:
   c. Ingresa el URL correspondiente con la direccion IP de tu máquina virtual. Dale click en
      aNack:




   d. Dale click en alerts. Veras los problemas de seguridad que van iden1ﬁcandose:




   e. Ejecuta un análisis de vulnerabilidades a la aplicación de Juice Shop. Envía los resultados
      con pantallazos por cada ítem encontrado.

6. Chequeo dinámico de vulnerabilidades

   a. Ingresa a sonarqube. U1liza el usuario admin y la clave websecuritytes1ng




   b. Seleccione “create Project manually”.
c. U1lice como nombre Damn Vulnerable Web Applica1on. Deje todos los otros campos
   por defecto y luego dale click en next:




d. U1liza las opciones globales y dale click en create Project:
e. Seleccione locally como método de análisis.




f. Dale click a generate.
g. Dale click en con1nue:
h. Dale click en Other.




i.   Dale click en Linux. Ejecuta el comando desde la terminal que allí aparece. Revisa el
     siguiente paso.
j.   Abre la terminal. Cambia el directorio a /opt/dvwa donde encontraras el código fuente de
     Damn Vulnerable Web Apps. Digita el comando que aparece en el si1o web, anteponiendo
     el path donde se encuentra.




k. Una vez termine la ejecución en la terminal, podrás ver el resultado en el si1o web.
l.   Dale click a las vulnerabilidades de seguridad. Veras el detalle:
m. Baja     el     código       de     la     aplicación     mu1llidae     (git     clone
   hNps://github.com/webpwnized/mu1llidae.git) y ejecuta un análisis esta1co. Envia en un
   documento el detalle de las vulnerabilidades encontradas.
