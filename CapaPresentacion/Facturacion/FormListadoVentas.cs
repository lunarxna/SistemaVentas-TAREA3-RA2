using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Facturacion
{
    public partial class FormListadoVentas : Form
    {
        public FormListadoVentas()
        {
            InitializeComponent();
        }

        private void FormListadoVentas_Load(object sender, EventArgs e)
        {
            CargarHistorial();
        }
        private void CargarHistorial()
        {
            VentaDAL dal = new VentaDAL();
            dgvHistorial.DataSource = dal.ListarVentas();

            
        }
    }
}
