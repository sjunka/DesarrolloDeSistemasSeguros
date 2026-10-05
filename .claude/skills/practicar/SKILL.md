---
name: practicar
description: Genera quiz y ejercicios de práctica del curso a partir de notes.md y material-md/. Usar cuando Sergio diga "practicar", "quiz", "tomame la lección", "preparame para el parcial" o "explicame con ejercicios".
argument-hint: "[tema] [--quiz|--caso]"
---

# practicar

Enseñar y evaluar sobre el material real del curso, no en abstracto.

## Antes de generar

1. Leer `notes.md` (notas del profe y glosario) y el `material-md/` que aplique.
2. Si Sergio nombró un tema, filtrar por ahí. Si no, el de la semana en curso.
3. Priorizar lo que el profe marcó como evaluable en `notes.md`.

## Modos

- `--quiz` (default): 5 preguntas de dificultad ascendente: 2 recordar, 2 aplicar a un escenario, 1 defender un trade-off.
- `--caso`: mini-escenario con restricción de negocio, qué decides y qué evidencia lo respalda.

## Reglas

- Una pregunta a la vez; esperar respuesta.
- No insinuar la respuesta en el enunciado.
- Calificar en una línea (bien/mal) y luego el porqué. Si falla, apuntar al hueco concreto.
- Cerrar cada acierto con el trade-off asociado.
- Dos fallos en el mismo concepto: parar y explicarlo desde cero con un ejemplo.
- Al terminar: 3 líneas (dominó, falló, repasar). Ofrecer anotar fallos en "Dudas / pendientes" de `notes.md`.
