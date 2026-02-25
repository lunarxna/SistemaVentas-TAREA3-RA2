using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Producto
    {
        public int Id_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public decimal Precio_Producto { get; set; }
        public int Stock {  get; set; }
        public int Id_Categoria { get; set; }

        public Categoria ObjCategoria { get; set; }
    }
}
