using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class VentaBL
    {
        private VentaDAL ventaDAL = new VentaDAL();
        private DetalleVentaDAL detalleDAL = new DetalleVentaDAL();

       
        
        
        public int RegistrarVenta(Venta venta)
        {

            int idVenta = ventaDAL.RegistrarVenta(venta);


            if (venta.Detalle != null)
            {
                foreach (var detalle in venta.Detalle)
                {
                    detalle.Id_Venta = idVenta;
                    detalleDAL.InsertarDetalle(detalle);
                }
            }
            return idVenta;
        }

        public Venta ObtenerVentaPorId(int idVenta)
        {
            return ventaDAL.ObtenerVentaPorId(idVenta);
        }

        public List<Venta> ListarVentas()
        {
            return ventaDAL.ListarVentas();
        }

        public bool AnularVenta(int idVenta)
        {
            return ventaDAL.AnularVenta(idVenta);
        }

        public DataTable ObtenerVentas()
        {
            return ventaDAL.CargarVentasConSP();
    
        
        
     
        
        }

        public DataTable ObtenerDatosFactura(int idVenta)
        {
            try
            {
                return ventaDAL.ObtenerDatosFactura(idVenta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de factura: " + ex.Message);
            }
        }
    }

}


