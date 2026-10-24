# Entrega 3 (taller en clase) - Carrito Juice Shop: compra en 0

Objetivo: encontrar y explotar la vulnerabilidad del carrito para dejar la compra en 0.
Target: https://playground.kontinuagroup.com:3000/#/search (OWASP Juice Shop), entorno educativo del curso.

## Vulnerabilidad
Business logic / validacion server-side ausente. `PUT /api/BasketItems/:id` acepta `quantity` negativa.
Total = Σ(precio×cantidad) sin cota inferior -> total 0. OWASP A04 + A01.

## Archivos
- `final.html`: entrega offline autocontenida (paso a paso, evidencia con capturas embebidas, causa raiz y remediacion). Abrir en navegador.
- `img/01-carrito-normal.png`: carrito normal, total 3.98.
- `img/04-pedido-confirmado.png`: pedido confirmado, Total Price 0.00 con cantidad -1.
- `datos.json`: datos de la sesion real capturada (status de las peticiones, orderId).


## Vector SQLi (pista del profe) - descartado
Probado en vivo: search y login son inyectables (SQLite) pero solo `SELECT`. Stacked `;UPDATE` se rechaza (`SQLITE_ERROR: near ";"`), precio nunca cambia. El objetivo corresponde al reto Payback Time (Improper Input Validation), no a SQLi-UPDATE. Documentado en `final.html` seccion 4.

## Como se capturo
Navegador + consola; capturas automatizadas con Playwright sobre la instancia en vivo. Usuario de prueba desechable.

## Pendiente
Confirmar con el profe si es entrega oficial, fecha, peso y canal.
