// Modelo de contenido. Agregar un reto = agregar retoNN.json.
// Los textos admiten HTML en linea (<code>, <b>, <em>, <span class='k'>).

export interface Shot {
  src: string       // captura en public/img/<src>, p.ej. "reto01/01-fuente.png"
  cap?: string
}

export interface Paso {
  t: string         // que se hace
  e?: string        // por que / explicacion
  c?: string        // comando, payload o URL (bloque mono con copiar)
  img?: Shot[]      // evidencia del paso
}

export interface Fase {
  h: string
  steps: Paso[]
}

export interface Reto {
  id: string        // "01" -> ruta #/reto/01
  name: string      // "Reto 1"
  sub: string       // titulo corto
  vuln: string      // etiqueta de la vulnerabilidad
  nivel?: 'Facil' | 'Medio' | 'Dificil'
  owasp?: string    // mapeo OWASP Top 10
  lead?: string     // enunciado / contexto
  why?: string      // filosofia: por que existe la vuln, como piensa el atacante
  recon?: string[]  // reconocimiento inicial
  phases: Fase[]    // paso a paso
  alt?: string[]    // soluciones alternativas
  defensa?: string[]// como se previene (enfoque desarrollo seguro)
}

export interface Guia {
  titulo: string
  intro: string
  pie: string
  equipo: string[]
}
