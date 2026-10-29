# Entrega 05 - Verificación de seguridad nivel 1 (ASVS 5.0.0)

- Fecha límite: por confirmar
- Peso: por confirmar
- Modalidad: individual (como las anteriores)
- Canal de entrega: por confirmar
- Enunciado: sin material del profe. Se tomó como base un ejemplo de verificación L1 de un compañero (`Marketplace_verificacion-JohnJimenez-JuanCastro.pdf`, taller "verificación L1 de su proyecto"), usando su misma metodología (alcance → modelo de amenazas → evaluación → reporte) pero aplicada al propio proyecto de la Entrega 02 (Silk Road vs Coupang + `Marketplace_SergioJunca.tm7`), no al marketplace del ejemplo.

## Entregable

`Marketplace_verificacion-SergioJunca.pdf` (11 págs, Letter). Fuente en `pdf/report.html`; regenerar con `python3 pdf/build.py` (requiere Google Chrome).

Estructura: alcance (diagrama de arquitectura con los 5 puntos de falla de la Entrega 2), diagnóstico del `.tm7` real (74 amenazas sin triar), tabla de las 5 amenazas prioritarias mapeadas a requisitos ASVS reales, matriz de autorización, evaluación completa de los 70 requisitos L1 de ASVS 5.0.0 (26 Cumple / 0 Heredado / 37 Pendiente / 7 N.A., sin ningún "Brecha" abierta), evidencia del retiro atómico con un diagrama de la condición de carrera, plan de remediación, ruta a L2 y Anexo A con la matriz completa de los 70 requisitos.

## Decisiones clave

- **Solo ASVS, no AISVS.** El marketplace de la Entrega 2 no tiene componentes de IA; evaluar AISVS habría significado inventar un asistente o un modelo de fraude que el proyecto no tiene (regla del curso: no inventar datos ni resultados).
- **Los 70 requisitos L1 son reales**, descargados del CSV oficial del repositorio `OWASP/ASVS` (tag `v5.0.0_release`), no transcritos de memoria. El conteo (345 requisitos totales, 70 de nivel 1) se verificó contra ese archivo.
- **El estado de cada requisito se decidió con una regla explícita**: "Cumple" exige una decisión de arquitectura o de documentación (nueva o ya tomada en la Entrega 2), nunca una promesa de comportamiento en código que no se puede confirmar sin desplegar. Por eso buena parte de V3 (cabeceras/cookies) y de V6.2 (detalles de contraseña) quedan "Pendiente": son requisitos sobre lo que la aplicación *hace*, no sobre lo que el diseño *decide*.
- **No hay ningún requisito en "Brecha".** Donde faltaba una decisión barata de tomar (política de contraseñas, algoritmo de cifrado, plazos de remediación) se tomó en el propio documento; donde faltaba algo que solo el código prueba, quedó "Pendiente" y declarado, nunca aprobado sin evidencia.
- **El hallazgo central** es `v5.0.0-9.1.3` (origen/distribución de la llave que valida los tokens): es la causa raíz real del caso Coupang y el único punto, de los cinco priorizados, que el diseño actual no resuelve.

## Diagramas

Ambos son SVG inline hechos a mano siguiendo la skill `diagram-design` (conectores ortogonales, nodo focal único, presupuesto de 9 nodos), no generados por una librería, y reusan la paleta ya usada en `02-marketplace/pdf/marketplace.html` para que las dos entregas se vean como del mismo autor:

- Figura 1: arquitectura del marketplace por zona de confianza, con los mismos 5 puntos de falla de la Entrega 2 numerados.
- Figura 2: condición de carrera del retiro (dos peticiones concurrentes) frente al `UPDATE` atómico, como trazas pareadas con primera divergencia marcada.

## Qué no quedó verificado

- Los 69 amenazas restantes del `.tm7` (de 74) siguen en estado `Not Started`: solo se triaron a mano, en este documento, las 5 ya priorizadas en la Entrega 2. Triar el resto queda en el plan de remediación.
- El `.tm7` no se tocó: el triage vive en este informe, no en el archivo del Threat Modeling Tool.

## Checklist final

- [x] Basado en requisitos reales de ASVS 5.0.0 (no inventados), verificados contra el CSV oficial
- [x] Los 70 requisitos L1 evaluados, con justificación individual (Anexo A)
- [x] Diagramas propios (arquitectura + condición de carrera), sin componentes de IA inventados
- [x] Nombre del autor en el documento
- [ ] Confirmar con el profe si es entrega oficial, fecha y canal
- [ ] Entregado y estado actualizado en `board.md`
