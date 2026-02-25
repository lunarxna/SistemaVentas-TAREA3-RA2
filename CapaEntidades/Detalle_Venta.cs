using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Detalle_Venta
    {
        public int Id_Detalle { get; set; }
        public int Id_Venta { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_unitario { get; set; }
        public decimal Subtotal { get; set; }

        public Producto ObjProducto { get; set; }
    }
}
