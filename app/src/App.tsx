import { useEffect, useState } from 'react'
import { Navigate, NavLink, Route, Routes, useLocation, useParams } from 'react-router-dom'
import { GUIA, RETOS, stepId } from './content'
import RetoView from './Reto'
import { KEY, useStored } from './lib/store'
import { Html, ZoomProvider } from './ui'

const PASOS = RETOS.flatMap(r => r.phases.flatMap((ph, pi) => ph.steps.map((_, si) => ({ reto: r.id, id: stepId(r, pi, si) }))))

export default function App() {
  const [saved, setSaved] = useStored<Record<string, boolean>>(KEY, {})
  const [tab, setTab] = useStored<string>(KEY + '-tab', `/reto/${RETOS[0]?.id}`)
  const [expandAll, setExpandAll] = useState(false)
  const [confirm, setConfirm] = useState(false)
  const { pathname } = useLocation()

  useEffect(() => { if (pathname !== '/' && pathname !== tab) setTab(pathname) }, [pathname]) // eslint-disable-line react-hooks/exhaustive-deps
  useEffect(() => { window.scrollTo(0, 0) }, [pathname])
  useEffect(() => {
    if (!confirm) return
    const t = setTimeout(() => setConfirm(false), 3000)
    return () => clearTimeout(t)
  }, [confirm])

  const isDone = (id: string, def: boolean) => (id in saved ? saved[id] : def)
  const toggle = (id: string, v: boolean) => setSaved({ ...saved, [id]: v })
  const count = (reto?: string) => {
    const ps = PASOS.filter(p => !reto || p.reto === reto)
    return [ps.filter(p => isDone(p.id, false)).length, ps.length]
  }
  const [done, total] = count()
  const reset = () => { if (confirm) { setSaved({}); setConfirm(false) } else setConfirm(true) }

  const tabCls = ({ isActive }: { isActive: boolean }) =>
    `flex items-center gap-2 rounded-md border px-[11px] py-[5px] text-sm font-semibold ${isActive ? 'border-accent bg-accent text-white' : 'border-line bg-surface text-muted hover:border-muted hover:text-ink'}`

  return (
    <ZoomProvider>
      <main className="mx-auto grid max-w-[820px] gap-[18px] px-4 pt-7 pb-18">
        <header className="grid gap-2">
          <h1 className="text-[1.8rem] leading-tight font-bold text-balance max-[520px]:text-[1.45rem]">{GUIA.titulo}</h1>
          <Html as="p" html={GUIA.intro} className="max-w-[68ch] text-muted" />
          <p className="text-[13px] text-muted">Equipo: {GUIA.equipo.join(', ')}</p>
          <div className="flex flex-wrap items-center gap-3">
            <div className="h-2 min-w-[140px] flex-1 overflow-hidden rounded bg-line" role="progressbar" aria-label="Avance" aria-valuemin={0} aria-valuemax={total} aria-valuenow={done}>
              <div className="h-full bg-done transition-[width] duration-250 motion-reduce:transition-none" style={{ width: `${total ? (100 * done) / total : 0}%` }} />
            </div>
            <span className="font-mono text-[13px] font-medium text-muted tabular-nums">{done}/{total}</span>
            <button type="button" className="btn" onClick={() => setExpandAll(!expandAll)}>
              {expandAll ? 'Ocultar detalles' : 'Mostrar todos los detalles'}
            </button>
            <button type="button" className={`btn ${confirm ? 'border-bad text-bad' : ''}`} onClick={reset}>
              {confirm ? '¿Seguro? Clic otra vez' : 'Reiniciar'}
            </button>
          </div>
        </header>

        <nav aria-label="Retos" className="sticky top-0 z-10 flex flex-wrap gap-1.5 bg-bg py-2">
          {RETOS.map(r => {
            const [d, t] = count(r.id)
            return (
              <NavLink key={r.id} to={`/reto/${r.id}`} className={tabCls}>
                {r.name}<span className="font-mono text-[11px] font-medium opacity-85 max-[520px]:hidden">{d}/{t}</span>
              </NavLink>
            )
          })}
        </nav>

        <Routes>
          <Route path="/reto/:id" element={<RetoRoute isDone={isDone} toggle={toggle} expandAll={expandAll} />} />
          <Route path="*" element={<Navigate to={tab} replace />} />
        </Routes>

        <Html as="p" html={GUIA.pie} className="text-[13px] text-muted" />
      </main>
    </ZoomProvider>
  )
}

function RetoRoute(props: Omit<Parameters<typeof RetoView>[0], 'reto'>) {
  const reto = RETOS.find(r => r.id === useParams().id)
  return reto ? <RetoView key={reto.id} reto={reto} {...props} /> : <Navigate to={`/reto/${RETOS[0]?.id}`} replace />
}
