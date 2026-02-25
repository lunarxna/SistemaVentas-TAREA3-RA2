using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ClienteBL
    {
        private ClienteDAL dal = new ClienteDAL();

        public List<Cliente> Listar()
        {
            return dal.Listar();
        }

        public void Insertar(Cliente cliente)
        {
            dal.Insertar(cliente);
        }

        public void Actualizar(Cliente cliente)
        {
            dal.Actualizar(cliente);
        }

        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }
    }
}
