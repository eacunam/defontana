//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace persistencia
{
    using System;
    using System.Collections.Generic;
    
    public partial class VentaDetalle
    {
        public long ID_VentaDetalle { get; set; }
        public long ID_Venta { get; set; }
        public int Precio_Unitario { get; set; }
        public int Cantidad { get; set; }
        public int TotalLinea { get; set; }
        public long ID_Producto { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Venta Venta { get; set; }
    }
}
