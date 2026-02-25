using CapaPresentacion.Facturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCategoria quintoForm = new FormCategoria();
            quintoForm.Show();
            this.Hide();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FormVentas segundoForm = new FormVentas();
            segundoForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormProductos cuartoForm = new FormProductos();
            cuartoForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormFactura quintoForm = new FormFactura();
            quintoForm.Show();
            this.Hide();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCLientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Hide();
        }
    }
}
