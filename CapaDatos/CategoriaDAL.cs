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
    public class CategoriaDAL
    
        {
            public List<Categoria> Listar()
            {
                List<Categoria> lista = new List<Categoria>();

                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarCategorias", cn);
                cmd.CommandType = CommandType.StoredProcedure; 
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Categoria
                    {
                        Id_categoria = (int)dr["Id_categoria"],
                        Nombre_Categoria = dr["Nombre_Categoria"].ToString(),
                        Estado = (bool)dr["Estado"]
                    });
                }
                dr.Close(); 
            }
        
                return lista;
            }

            public void Insertar(Categoria categoria)
            {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_CrearCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre_Categoria", categoria.Nombre_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

            public void Actualizar(Categoria categoria)
            {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCategoria", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_categoria", categoria.Id_categoria);
                cmd.Parameters.AddWithValue("@Nombre_Categoria", categoria.Nombre_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

            public void Eliminar(int id)
            {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_categoria", id);
                  
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        }
        
}
