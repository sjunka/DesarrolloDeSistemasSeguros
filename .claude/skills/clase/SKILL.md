---
name: clase
description: Registra la clase de hoy en notes.md y board.md y lo cruza con el calendario. Usar cuando Sergio diga "clase de hoy", "/clase", "anota la clase", "pegar notas" o pegue comentarios del profesor.
argument-hint: "[fecha opcional]"
---

# clase

1. Fecha: la del argumento o la de hoy. Con Google Calendar, buscar el evento del curso ese día para confirmar hora y aula.
2. Leer lo que Sergio pegó. Separar:
   - Conceptos y aclaraciones: a `notes.md`, sección nueva arriba con `## AAAA-MM-DD - Clase`.
   - Tareas, fechas, cambios de calendario, pesos: a la tabla Entregas y la Bitácora de `board.md`.
   - Términos nuevos: al Glosario de `notes.md`.
3. Anotar sin explicar ni corregir lo que dijo el profe, salvo que Sergio lo pida.
4. Si aparece una entrega nueva: crear `assignments/NN-nombre/README.md` desde `assignments/_plantilla-entrega.md`.
5. Si hay fecha de entrega, ofrecer crear el recordatorio en el calendario (no crearlo sin confirmar).
6. Cerrar con 3 líneas: qué se anotó, qué entregas nuevas hay, qué falta preguntar al profe.
