--El total de ventas de los últimos 30 días(monto total y cantidad total de ventas).
SELECT *
FROM Venta v
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE());
--El total ventas últimos 30 días
SELECT sum(v.Total)
FROM Venta v
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE())
--El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
SELECT top 1 v.Total
FROM Venta v
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE())
ORDER BY v.Total DESC
--Indicar cuál es el producto con mayor monto total de ventas.
SELECT top 1 v.Total, p.Nombre
FROM Venta v
INNER JOIN VentaDetalle vd ON v.ID_Venta = vd.ID_Venta
INNER JOIN Producto p ON vd.ID_Producto = p.ID_Producto
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE())
ORDER BY v.Total DESC
--Indicar el local con mayor monto de ventas.
SELECT top 1 v.Total, l.Nombre
FROM Venta v
INNER JOIN Local l on v.ID_Local = l.ID_Local
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE())
ORDER BY v.Total DESC
--¿Cuál es la marca con mayor margen de ganancias?
SELECT top 1 v.Total, m.Nombre
FROM Venta v
INNER JOIN VentaDetalle vd ON v.ID_Venta = vd.ID_Venta
INNER JOIN Producto p ON vd.ID_Producto = p.ID_Producto
INNER JOIN Marca m ON p.ID_Marca = m.ID_Marca
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE())
ORDER BY v.Total DESC
--¿Cómo obtendrías cuál es el producto que más se vende en cada local?
SELECT l.Nombre, p.Nombre
FROM Venta v
INNER JOIN VentaDetalle vd ON v.ID_Venta = vd.ID_Venta
INNER JOIN Producto p ON vd.ID_Producto = p.ID_Producto
INNER JOIN Local l on v.ID_Local = l.ID_Local
GROUP BY l.Nombre, p.Nombre
HAVING COUNT(*) = (
    SELECT MAX(subquery.Total)
    FROM (
        SELECT l.ID_Local, p.ID_Producto, COUNT(*) AS Total
		FROM Venta v
		INNER JOIN VentaDetalle vd ON v.ID_Venta = vd.ID_Venta
		INNER JOIN Producto p ON vd.ID_Producto = p.ID_Producto
		INNER JOIN Local l on v.ID_Local = l.ID_Local
        GROUP BY l.ID_Local, p.ID_Producto
    ) AS subquery
)