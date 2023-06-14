using persistencia;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace defontana
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (PruebaEntities model = new PruebaEntities())
                {
                    //El total de ventas de los últimos 30 días(monto total y cantidad total de ventas).
                    DateTime fechaLimite = DateTime.Now.AddDays(-30);
                    var data = model.Venta.Where(v => v.Fecha >= fechaLimite);

                    //El total ventas últimos 30 días
                    var totalVenta = data.Sum(d => d.Total);

                    //El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
                    var ventaMasAlta = data.OrderByDescending(d => d.Total).FirstOrDefault().Total;

                    //Indicar cuál es el producto con mayor monto total de ventas.
                    var productoMayorMonto = data.Join(model.VentaDetalle, v => v.ID_Venta, d => d.ID_Venta, (v, d) => new { v, d })
                        .Join(model.Producto, d => d.d.ID_Producto, p => p.ID_Producto, (d, p) => new { d, p })
                        .OrderByDescending(d => d.d.d.Venta.Total).FirstOrDefault().p.Nombre;

                    //Indicar el local con mayor monto de ventas.
                    var localMayorMonto = data.Join(model.Local, v => v.ID_Local, l => l.ID_Local, (v, l) => new { v, l }
                    ).OrderByDescending(d => d.v.Total).FirstOrDefault().l.Nombre;

                    //¿Cuál es la marca con mayor margen de ganancias?
                    var marcaMayorMonto = data.Join(model.VentaDetalle, v => v.ID_Venta, d => d.ID_Venta, (v, d) => new { v, d })
                        .Join(model.Producto, d => d.d.ID_Producto, p => p.ID_Producto, (d, p) => new { d, p })
                        .Join(model.Marca, p => p.p.ID_Marca, m => m.ID_Marca, (p, m) => new { p, m })
                        .OrderByDescending(v => v.p.d.v.Total).FirstOrDefault().m.Nombre;

                    Console.WriteLine("El total ventas últimos 30 días: " + totalVenta);
                    Console.WriteLine("la venta con el monto más alto: " + ventaMasAlta);
                    Console.WriteLine("Producto con mayor monto total: " + productoMayorMonto);
                    Console.WriteLine("Local con mayor monto de ventas: " + localMayorMonto);
                    Console.WriteLine("Marca con mayor margen de ganancias: " + marcaMayorMonto);

                    Console.ReadKey();
                }
            }
            catch (Exception)
            {
                throw;
            }   
        }
    }
}
