using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VentaDAL
    {
        public List<Venta> ListarVentas()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarVentas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Venta
                    {
                        Id_Venta = Convert.ToInt32(dr["Id_venta"]),
                        Fecha_Venta = Convert.ToDateTime(dr["Fecha_Venta"]),
                        Total_general = Convert.ToDecimal(dr["Total_general"]),
                        Estado_venta = Convert.ToBoolean(dr["Estado_venta"])
                    });
                }
            }
            return lista;
        }

        public void ActualizarTotal(int idVenta, decimal total)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarTotalVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_venta", idVenta);
                cmd.Parameters.AddWithValue("@Total_general", total);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int RegistrarVenta(Venta venta)
        {
            int idVenta;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fecha_Venta", venta.Fecha_Venta);
                cmd.Parameters.AddWithValue("@Id_cliente", venta.Id_cliente);

                cn.Open();
                idVenta = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return idVenta;
        }

        public Venta ObtenerVentaPorId(int idVenta)
        {
            Venta venta = null;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerVentaPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);
             
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    venta = new Venta
                    {
                        Id_Venta = Convert.ToInt32(dr["Id_venta"]),
                        Fecha_Venta = Convert.ToDateTime(dr["Fecha_Venta"]),
                        Id_cliente = Convert.ToInt32(dr["Id_cliente"]),
                        Total_general = Convert.ToDecimal(dr["Total_general"]),
                        Estado_venta = Convert.ToBoolean(dr["Estado_venta"])
                    };
                }
            }
            return venta;
        }


        public bool AnularVenta(int idVenta)
        {
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_AnularVenta", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_ventas", idVenta);
                    cn.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }


        public DataTable CargarVentasConSP()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarVentasResumen", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
        public DataTable ObtenerDatosFactura(int idVenta)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Mostrar_Factura", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Venta", idVenta);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de factura: " + ex.Message);
            }
        }

    }
}