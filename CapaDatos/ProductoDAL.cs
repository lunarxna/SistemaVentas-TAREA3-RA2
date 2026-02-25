using CapaEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProductoDAL
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Producto
                    {
                        Id_Producto = (int)dr["Id_Producto"],
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Precio_Producto = (decimal)dr["Precio_Producto"],
                        Stock = (int)dr["Stock"],
                        ObjCategoria = new Categoria
                        {
                            Nombre_Categoria = dr["Nombre_Categoria"].ToString()
                        }
                    });
                }
            }
            return lista;
        }


        public List<Producto> Buscarproducto(Producto producto)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                // Llamamos al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("sp_BuscarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos el parámetro de búsqueda
                cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);

                cn.Open();

                // Usamos ExecuteReader para obtener los datos encontrados
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Producto
                        {
                            Id_Producto = (int)dr["Id_Producto"],
                            Nombre_Producto = dr["Nombre_Producto"].ToString(),
                            Precio_Producto = (decimal)dr["Precio_Producto"],
                            Stock = (int)dr["Stock"],
                            Id_Categoria = (int)dr["Id_categoria"],
                            ObjCategoria = new Categoria
                            {
                                Id_categoria = (int)dr["Id_categoria"],
                                Nombre_Categoria = dr["Nombre_Categoria"].ToString()
                            }
                        });
                    }
                }
            }
            return lista;
        }
        public void ListarProducto(Producto producto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto", producto.Id_Producto);
                cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);
                cmd.Parameters.AddWithValue("@Precio_Producto", producto.Precio_Producto);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertarProducto(Producto producto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("InsertarProducto", cn);
                
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);
                    cmd.Parameters.AddWithValue("@Precio_Producto", producto.Precio_Producto);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@Id_Categoria", producto.Id_Categoria);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                
            }
        }

        public void Actualizar(Producto producto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto", producto.Id_Producto);
                cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);
                cmd.Parameters.AddWithValue("@Precio_Producto", producto.Precio_Producto);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@Id_Categoria", producto.Id_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarProducto(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
