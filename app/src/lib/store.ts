import { useState } from 'react'

export const KEY = 'dss-retos-v1'

/** Estado recordado en localStorage; si el navegador no deja guardar, funciona igual. */
export function useStored<T>(key: string, initial: T): [T, (v: T) => void] {
  const [value, setValue] = useState<T>(() => {
    try {
      const s = localStorage.getItem(key)
      return s == null ? initial : (JSON.parse(s) as T)
    } catch { return initial }
  })
  const set = (v: T) => {
    setValue(v)
    try { localStorage.setItem(key, JSON.stringify(v)) } catch { /* sin almacenamiento */ }
  }
  return [value, set]
}
