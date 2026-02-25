using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    internal class DetalleVentaBL
    {
        private DetalleVentaDAL dal = new DetalleVentaDAL();

        public List<Detalle_Venta> ObtenerDetallePorVenta(int idVenta)
        {
            return dal.ObtenerDetallePorVenta(idVenta);
        }
    }
}
