# Evidencia - Juice Shop: compra en 0 (taller 2026-10-23/24)

Target: https://playground.kontinuagroup.com:3000 (OWASP Juice Shop 20.3.0, SQLite).

## Exploit que funciona (reto 61 Payback Time, Improper Input Validation)
- `PUT /api/BasketItems/:id` acepta `quantity` negativa (sin validación server-side).
- 2 productos a 1.99 (Apple + Banana Juice), al 2º `quantity:-1` → items total 0.
- Checkout con Standard Delivery (0) → pedido confirmado **Total Price 0.00**.
- Capturas y paso a paso: `assignments/03-juiceshop-carrito/final.html`.

## Vector SQLi + UPDATE (pista del profe) - DESCARTADO tras prueba
- search (`/rest/products/search?q=`) inyectable: `q=test'` → `SQLITE_ERROR`. Solo SELECT.
- login (`' OR 1=1--`) → token admin. Solo SELECT.
- Stacked `;UPDATE Products SET price=...;--` → `SQLITE_ERROR: near ";"`. Precio id=1 sigue 1.99.
- SQLite prepara una sola sentencia: no hay stacked queries. UNION solo exfiltra.
- Único "update por inyección": NoSQL en reseñas (reto 58), no afecta precio/total.
- Conclusión: SQLi-UPDATE no logra la compra en 0 en esta instancia.
