using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CategoriaBL
    {
        private CategoriaDAL dal = new CategoriaDAL();

        public List<Categoria> Listar()
        {
            return dal.Listar();
        }

        public void Insertar(Categoria categoria)
        {
            dal.Insertar(categoria);
        }

        public void Actualizar(Categoria categoria)
        {
            dal.Actualizar(categoria);
        }

        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }
    }
}
