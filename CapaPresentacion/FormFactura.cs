using CapaDatos;
using CapaEntidades;
using CapaNegocio;
using CapaPresentacion.Facturacion;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormFactura : Form
    {
        private int idVentaSeleccionada = 0;

        public FormFactura()
        {
            InitializeComponent();
        }

        private void FormFactura_Load(object sender, EventArgs e)
        {
            try
            {
                // Cargar todas las ventas en el DataGridView
                VentaBL ventaBL = new VentaBL();
                DataTable dtVentas = ventaBL.ObtenerVentas();

                
                CargarDataGridView(dtVentas);

               
                if (dtVentas != null && dtVentas.Rows.Count > 0)
                {
                    idVentaSeleccionada = (int)dtVentas.Rows[0]["Id_venta"];

                    
                }
                else
                {
                    MessageBox.Show(
                        "No hay ventas registradas en la base de datos.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDataGridView(DataTable dt)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();

                ConfigurarColumnasDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar DataGridView: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void ConfigurarColumnasDataGrid()
        {
            try
            {
                if (dataGridView1.Columns.Count >= 2)
                {
                    // Cambiar nombres de columnas
                    if (dataGridView1.Columns.Contains("Id_venta"))
                    {
                        dataGridView1.Columns["Id_venta"].HeaderText = "N° Venta";
                        dataGridView1.Columns["Id_venta"].Width = 80;
                        dataGridView1.Columns["Id_venta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    if (dataGridView1.Columns.Contains("Fecha_Venta"))
                    {
                        dataGridView1.Columns["Fecha_Venta"].HeaderText = "Fecha de Venta";
                        dataGridView1.Columns["Fecha_Venta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                        dataGridView1.Columns["Fecha_Venta"].Width = 150;
                    }

                    // Agregar columna para Cliente si existe
                    if (dataGridView1.Columns.Contains("Nombre_Cliente"))
                    {
                        dataGridView1.Columns["Nombre_Cliente"].HeaderText = "Cliente";
                        dataGridView1.Columns["Nombre_Cliente"].Width = 200;
                    }

                    // Agregar columna para Total si existe
                    if (dataGridView1.Columns.Contains("Total"))
                    {
                        dataGridView1.Columns["Total"].HeaderText = "Total";
                        dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";
                        dataGridView1.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns["Total"].Width = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar columnas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (e.RowIndex < 0)
                    return;

             
                if (dataGridView1.Rows[e.RowIndex].Cells["Id_venta"].Value != null)
                {
                    idVentaSeleccionada = (int)dataGridView1.Rows[e.RowIndex].Cells["Id_venta"].Value;

                    
                    AbrirFacturaEmergente(idVentaSeleccionada);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar venta: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirFacturaEmergente(int idVenta)
        {
            try
            {
                // Crear y mostrar la ventana emergente con la factura
                FormFacturaEmergente facturaEmergente = new FormFacturaEmergente();
                facturaEmergente.CargarFactura(idVenta);
                facturaEmergente.ShowDialog(); 

                // El usuario puede cerrar la ventana cuando termine de ver la factura
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir factura: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                VentaBL ventaBL = new VentaBL();
                DataTable dt = ventaBL.ObtenerVentas();
                MessageBox.Show("Total de ventas: " + dt.Rows.Count, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCategoria quintoForm = new FormCategoria();
            quintoForm.Show();
            this.Hide();
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

        private void button3_Click(object sender, EventArgs e)
        {
            FormProductos cuartoForm = new FormProductos();
            cuartoForm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnCLientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}