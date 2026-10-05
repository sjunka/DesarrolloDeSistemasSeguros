import { useState } from 'react'
import { stepId } from './content'
import type { Reto, Paso } from './content/types'
import { CodeBlock, Html, Shot } from './ui'

interface Props {
  reto: Reto
  isDone: (id: string, def: boolean) => boolean
  toggle: (id: string, v: boolean) => void
  expandAll: boolean
}

const NIVEL: Record<string, string> = {
  Facil: 'bg-done-bg text-done',
  Medio: 'bg-warn-bg text-warn-ink',
  Dificil: 'bg-bad-bg text-bad',
}

export default function RetoView({ reto, isDone, toggle, expandAll }: Props) {
  return (
    <div className="grid gap-4">
      <div className="grid gap-2.5">
        <h2 className="text-xl font-semibold text-balance">{reto.name} · {reto.sub}</h2>
        <div className="flex flex-wrap gap-1.5">
          <span className="pill bg-accent-bg text-accent">{reto.vuln}</span>
          {reto.nivel && <span className={`pill ${NIVEL[reto.nivel]}`}>{reto.nivel}</span>}
          {reto.owasp && <span className="pill bg-con-bg text-con">{reto.owasp}</span>}
        </div>
        {reto.lead && <Html as="p" html={reto.lead} className="max-w-[70ch] text-base" />}
        {reto.why && (
          <div className="why grid gap-1 rounded-lg bg-accent-bg px-4 py-3 text-sm">
            <strong>Filosofia del reto</strong>
            <Html as="div" html={reto.why} />
          </div>
        )}
      </div>

      {reto.recon && (
        <section className="card">
          <h3 className="font-semibold">Reconocimiento</h3>
          <ul className="grid list-disc gap-1 pl-[18px] text-sm">
            {reto.recon.map((r, i) => <Html key={i} as="li" html={r} />)}
          </ul>
        </section>
      )}

      {reto.phases.map((ph, pi) => {
        const ids = ph.steps.map((_, si) => stepId(reto, pi, si))
        const done = ids.filter(id => isDone(id, false)).length
        const complete = done === ids.length
        return (
          <section key={pi} className="card">
            <header className="flex flex-wrap items-baseline justify-between gap-2.5">
              <h3 className="font-semibold">{ph.h}</h3>
              <span className={`font-mono text-xs font-medium tabular-nums ${complete ? 'text-done' : 'text-muted'}`}>{done}/{ids.length}</span>
            </header>
            <ul className="grid gap-1">
              {ph.steps.map((st, si) => (
                <Step key={ids[si] + expandAll} id={ids[si]} st={st}
                  checked={isDone(ids[si], false)} onCheck={v => toggle(ids[si], v)} initialOpen={expandAll} />
              ))}
            </ul>
          </section>
        )
      })}

      {reto.alt && (
        <section className="card">
          <h3 className="font-semibold">Soluciones alternativas</h3>
          <ul className="grid list-disc gap-1 pl-[18px] text-sm">
            {reto.alt.map((a, i) => <Html key={i} as="li" html={a} />)}
          </ul>
        </section>
      )}

      {reto.defensa && (
        <section className="grid gap-2.5 rounded-lg border border-done bg-done-bg px-4 py-3.5">
          <h3 className="font-semibold text-done">Como se previene (desarrollo seguro)</h3>
          <ul className="grid list-disc gap-1 pl-[18px] text-sm">
            {reto.defensa.map((d, i) => <Html key={i} as="li" html={d} />)}
          </ul>
        </section>
      )}
    </div>
  )
}

interface StepProps { id: string; st: Paso; checked: boolean; onCheck: (v: boolean) => void; initialOpen: boolean }

function Step({ id, st, checked, onCheck, initialOpen }: StepProps) {
  const [open, setOpen] = useState(initialOpen)
  const hasBody = !!(st.e || st.c || st.img)
  return (
    <li className={`-mx-2 grid grid-cols-[18px_1fr] gap-x-2.5 rounded-md px-2 py-1.5 transition-[background,box-shadow] motion-reduce:transition-none ${checked ? 'bg-done-bg shadow-[inset_3px_0_0_var(--done)]' : 'hover:bg-bg'}`}>
      <input type="checkbox" id={'cb-' + id} aria-label="Hecho" checked={checked} onChange={e => onCheck(e.target.checked)}
        className="mt-[5px] size-[17px] cursor-pointer accent-done" />
      <button type="button" aria-expanded={hasBody ? open : undefined} aria-controls={hasBody ? 'body-' + id : undefined}
        onClick={() => setOpen(!open)} className="rounded text-left text-[15px] hover:underline hover:decoration-muted hover:decoration-dotted hover:underline-offset-[3px]">
        <Html html={st.t} />
        {hasBody && <span className="ml-1.5 font-mono text-[11px] font-medium whitespace-nowrap text-accent">{open ? 'ocultar' : 'detalle'}</span>}
      </button>
      {hasBody && open && (
        <div id={'body-' + id} className="col-start-2 mt-1.5 grid min-w-0 gap-2">
          {st.c && <CodeBlock code={st.c} />}
          {st.e && <Html as="div" html={st.e} className="border-l-[3px] border-accent pl-3 text-sm" />}
          {st.img?.map(im => <Shot key={im.src} img={im} />)}
        </div>
      )}
    </li>
  )
}
