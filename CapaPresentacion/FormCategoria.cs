using CapaEntidades;
using CapaNegocio;
using CapaPresentacion.Facturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormCategoria : Form
    {
        private CategoriaBL bl = new CategoriaBL();
        private int idSeleccionado = 0;

        public FormCategoria()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();
            Listar();
        }

       
        private void Listar()
        {
            dgvCategoria.DataSource = bl.Listar();
        }

        private void FormCategoria_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Por favor, ingrese un nombre.");
                    return;
                }

                Categoria c = new Categoria
                {
                    Id_categoria = idSeleccionado,
                    Nombre_Categoria = txtNombre.Text
                };

         
                if (idSeleccionado == 0)
                {
                  
                    bl.Insertar(c);
                    MessageBox.Show("Categoría guardada con éxito");
                }
                else
                {
                  
                    bl.Actualizar(c); 
                    MessageBox.Show("Categoría actualizada con éxito");
                }

                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idSeleccionado = Convert.ToInt32(dgvCategoria.Rows[e.RowIndex].Cells["Id_categoria"].Value);
                txtNombre.Text = dgvCategoria.Rows[e.RowIndex].Cells["Nombre_Categoria"].Value.ToString();
            }
            btnAgregar.Text = "Actualizar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado > 0) 
            { bl.Eliminar(idSeleccionado);
                MessageBox.Show("Categoria eliminada con éxito");
                Limpiar();
                Listar(); 
            }
        }
        private void Limpiar() 
        { idSeleccionado = 0; 
        txtNombre.Clear();  
        btnAgregar.Text = "Guardar actalizacion";
        }

        private void FormCategoria_Load_1(object sender, EventArgs e)
        {
                              
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void dgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Inicio primerForm = new Inicio();
            primerForm.Show();
            this.Hide();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {

            FormVentas segundoForm = new FormVentas();
            segundoForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormFactura quintoForm = new FormFactura();
            quintoForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormProductos cuartoForm = new FormProductos();
            cuartoForm.Show();
            this.Hide();
        }

        private void btnCLientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Hide();
        }
    }
}
