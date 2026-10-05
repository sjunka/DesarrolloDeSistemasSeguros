import { createContext, useContext, useEffect, useRef, useState, type ReactNode } from 'react'
import type { Shot as ShotT } from './content/types'

export const imgUrl = (src: string) => `${import.meta.env.BASE_URL}img/${src}`

/** Texto del contenido: trae HTML en linea escrito por nosotros (<code>, <b>, ...). */
export function Html({ html, as: Tag = 'span', className = '' }: { html: string; as?: 'span' | 'p' | 'div' | 'li'; className?: string }) {
  return <Tag className={`html ${className}`} dangerouslySetInnerHTML={{ __html: html }} />
}

export function CodeBlock({ code }: { code: string }) {
  const [label, setLabel] = useState('Copiar')
  const pre = useRef<HTMLPreElement>(null)
  const copy = () => {
    const fallback = () => {
      const r = document.createRange()
      r.selectNodeContents(pre.current!)
      getSelection()?.removeAllRanges()
      getSelection()?.addRange(r)
      setLabel('Cmd+C')
    }
    navigator.clipboard?.writeText(code).then(() => {
      setLabel('Copiado'); setTimeout(() => setLabel('Copiar'), 1400)
    }, fallback) ?? fallback()
  }
  return (
    <div className="relative min-w-0">
      <pre ref={pre} className="overflow-x-auto rounded-md bg-code-bg px-3.5 py-3 font-mono text-[12.5px] leading-normal text-code-ink">{code}</pre>
      <button type="button" onClick={copy} className="absolute top-1.5 right-1.5 rounded-md border border-[#33445a] bg-[#1e2a3a] px-2 py-0.5 text-[11px] text-[#c9d4e3] hover:border-[#6a7d96] hover:text-white">
        {label}
      </button>
    </div>
  )
}

// ---------- visor de capturas ----------
interface Zoom { title?: string; cap?: string; src: string }
const ZoomCtx = createContext<(z: Zoom) => void>(() => {})
export const useZoom = () => useContext(ZoomCtx)

export function ZoomProvider({ children }: { children: ReactNode }) {
  const [z, setZ] = useState<Zoom | null>(null)
  const dlg = useRef<HTMLDialogElement>(null)
  useEffect(() => { if (z && !dlg.current?.open) dlg.current?.showModal() }, [z])
  return (
    <ZoomCtx.Provider value={setZ}>
      {children}
      <dialog ref={dlg} aria-labelledby="ztitle" onClose={() => setZ(null)}
        onClick={e => e.target === dlg.current && dlg.current.close()}
        className="m-auto max-h-[92vh] w-[min(1200px,96vw)] rounded-[10px] border border-line bg-surface p-0 text-ink">
        {z && (
          <div className="grid max-h-[92vh] gap-2.5 overflow-auto px-4 py-3.5">
            <div className="flex flex-wrap items-baseline justify-between gap-3">
              <h3 id="ztitle" className="font-semibold">{z.title}</h3>
              <button type="button" className="btn" onClick={() => dlg.current?.close()}>Cerrar</button>
            </div>
            {z.cap && <Html as="p" html={z.cap} className="text-sm text-muted" />}
            <img src={z.src} alt={z.title ?? ''} className="h-auto w-full rounded-md border border-line" />
          </div>
        )}
      </dialog>
    </ZoomCtx.Provider>
  )
}

/** Captura: si el archivo aun no existe, muestra un hueco marcado. */
export function Shot({ img }: { img: ShotT }) {
  const zoom = useZoom()
  const [ok, setOk] = useState(true)
  const label = img.cap || ''
  if (!ok) {
    return (
      <figure className="grid gap-1">
        <div className="grid min-h-[90px] place-items-center rounded-md border border-dashed border-warn-ink bg-warn-bg px-3 py-4 text-center text-[13px] text-warn-ink">
          Hueco de captura: <code className="mx-1">img/{img.src}</code><br />pega aqui tu screenshot de la instancia
        </div>
        {img.cap && <figcaption className="text-[12.5px] text-muted">{img.cap}</figcaption>}
      </figure>
    )
  }
  return (
    <figure className="grid gap-1">
      <button type="button" aria-label={`Ampliar: ${label}`}
        onClick={() => zoom({ title: img.cap, cap: img.cap, src: imgUrl(img.src) })}
        className="block cursor-zoom-in overflow-hidden rounded-md border border-line bg-bg">
        <img src={imgUrl(img.src)} alt={label} loading="lazy" onError={() => setOk(false)} className="block h-auto w-full" />
      </button>
      {img.cap && <figcaption className="text-[12.5px] text-muted">{img.cap}</figcaption>}
    </figure>
  )
}
