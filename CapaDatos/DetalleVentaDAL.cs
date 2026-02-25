using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DetalleVentaDAL
    {
        public void InsertarDetalle(Detalle_Venta detalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarDetalleVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_venta", detalle.Id_Venta);
                cmd.Parameters.AddWithValue("@Id_producto", detalle.Id_Producto);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@Precio_unitario", detalle.Precio_unitario);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public List<Detalle_Venta> ObtenerDetallePorVenta(int idVenta)
        {
            List<Detalle_Venta> lista = new List<Detalle_Venta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDetallePorVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Detalle_Venta
                    {
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Precio_unitario = Convert.ToDecimal(dr["Precio_unitario"]),
                        ObjProducto = new Producto
                        {
                            Nombre_Producto = dr["NombreProducto"].ToString()
                        }
                    });
                }
            }
            return lista;
        }


    }
}
