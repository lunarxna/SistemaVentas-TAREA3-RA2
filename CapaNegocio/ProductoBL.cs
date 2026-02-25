using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoBL
    {
        private ProductoDAL dal = new ProductoDAL();

        public List<Producto> Listar()
        {
            return dal.ListarProductos();
        }

        public void Insertar(Producto producto)
        {
            dal.InsertarProducto(producto);
        }

        public void Actualizar(Producto producto)
        {
            dal.Actualizar(producto);
        }

        public void Eliminar(int id)
        {
            dal.EliminarProducto(id);
        }
    }
}
