import guiaJson from './guia.json'
import type { Guia, Reto } from './types'

const mods = import.meta.glob<Reto>('./reto*.json', { eager: true, import: 'default' })
export const RETOS: Reto[] = Object.keys(mods).sort().map(p => mods[p])
export const GUIA = guiaJson as Guia

export const stepId = (reto: Reto, fase: number, paso: number) => `reto${reto.id}-${fase}-${paso}`
