using CapaDatos;
using CapaEntidades;
using CapaNegocio;
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
    
    public partial class FormVentas : Form
    {
        private int idVentaActual = 0;
        private decimal totalAcumulado = 0;
        private VentaBL ventaBL = new VentaBL();

        public FormVentas()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void FormVentas_Load(object sender, EventArgs e)
        {

            CargarProductos();
            InicializarEstadoFormulario(); 
            CargarClientes();


        }
        private void InicializarEstadoFormulario()
        {
            cmbClientes.Enabled = true;
           

            btnCrear.Enabled = false;  
            btnAgregarArriba.Enabled = false;
            nudCantidad.Enabled = false;
            cmbProducto.Enabled = false;
            btnFacturar.Enabled = false;
        }

        private void CargarClientes()
        {
            ClienteBL clienteBL = new ClienteBL();
            List<Cliente> lista = clienteBL.Listar();

            cmbClientes.DataSource = lista;
            cmbClientes.DisplayMember = "Nombre";
            cmbClientes.ValueMember = "Id_cliente";

            // SELECCIONAR EL CLIENTE GENÉRICO (ID = 1)
            SeleccionarClienteGenerico(lista);
        }
        private void SeleccionarClienteGenerico(List<Cliente> listaClientes)
        {
           
            var clienteGenerico = listaClientes.FirstOrDefault(c => c.Id_cliente == 1);

            if (clienteGenerico != null)
            {
               
                cmbClientes.SelectedItem = clienteGenerico;

                // El botón Crear se habilitará automáticamente por el evento SelectedIndexChanged
            }
            else
            {
                // Si por alguna razón no existe el cliente con ID 1, dejar sin selección
                cmbClientes.SelectedIndex = -1;

                MessageBox.Show("No se encontró el cliente genérico (ID 1) en la base de datos.",
                               "Aviso",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
        }

        private void CargarProductos()
        {
            ProductoBL productoBL = new ProductoBL();
            List<Producto> lista = productoBL.Listar();

            cmbProducto.DataSource = lista;
            cmbProducto.DisplayMember = "Nombre_Producto";
            cmbProducto.ValueMember = "Id_Producto";       
            cmbProducto.SelectedIndex = -1;               
        }


        private void button4_Click(object sender, EventArgs e)
        {
            NavegarAFormulario(new FormCategoria());
        }

        private void btnAgregarArriba_Click(object sender, EventArgs e)
        {


            if (idVentaActual <= 0)
            {
                MessageBox.Show("Primero debe crear una venta",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            var productoSeleccionado = cmbProducto.SelectedItem as Producto;

            if (productoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un producto",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            decimal precio = productoSeleccionado.Precio_Producto;
            decimal subtotal = cantidad * precio;

            Detalle_Venta detalle = new Detalle_Venta()
            {
                Id_Venta = idVentaActual,
                Id_Producto = productoSeleccionado.Id_Producto,
                Cantidad = cantidad,
                Precio_unitario = precio
            };

            DetalleVentaDAL detalleDAL = new DetalleVentaDAL();
            detalleDAL.InsertarDetalle(detalle);

            dgvDetalleVenta.Rows.Add(cantidad, productoSeleccionado.Nombre_Producto, precio, subtotal);

            totalAcumulado += subtotal;
            txtTotal.Text = totalAcumulado.ToString("C2");

            btnFacturar.Enabled = true;

            cmbProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;

        }
        

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalleVenta.Rows.Count == 0)
                {
                    MessageBox.Show("No hay productos en la venta",
                                    "Aviso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

               
                VentaDAL dalVenta = new VentaDAL();
                dalVenta.ActualizarTotal(idVentaActual, totalAcumulado);

           
                int idVentaFacturada = idVentaActual;
                decimal totalFacturado = totalAcumulado;

                MessageBox.Show($"Venta finalizada con éxito. Total: {totalFacturado:C2}",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);


                LimpiarFormulario();

             
                AbrirFacturaEmergente(idVentaFacturada);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al facturar: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private void AbrirFacturaEmergente(int idVenta)
        {
            try
            {
             
                FormFacturaEmergente facturaEmergente = new FormFacturaEmergente();
                facturaEmergente.CargarFactura(idVenta);
                facturaEmergente.ShowDialog(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la factura: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }




        

        private void LimpiarFormulario()
        {
            idVentaActual = 0;
            totalAcumulado = 0;
            dgvDetalleVenta.Rows.Clear();
            txtTotal.Text = "$0.00";

            cmbClientes.Enabled = true;
            cmbClientes.SelectedIndex = -1;

            btnCrear.Enabled = false;
            btnAgregarArriba.Enabled = false;
            nudCantidad.Enabled = false;
            cmbProducto.Enabled = false;
            btnFacturar.Enabled = false;
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCrear.Enabled = (cmbClientes.SelectedIndex != -1);

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {

            if (cmbClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            Venta nuevaVenta = new Venta()
            {
                Fecha_Venta = DateTime.Now,
                Id_cliente = (int)cmbClientes.SelectedValue
            };

            idVentaActual = ventaBL.RegistrarVenta(nuevaVenta);
            if (idVentaActual > 0)
            {
                cmbClientes.Enabled = false;
                btnCrear.Enabled = false;

                cmbProducto.Enabled = true;
                nudCantidad.Enabled = true;
                btnAgregarArriba.Enabled = true;
                btnFacturar.Enabled = false;

                MessageBox.Show("Venta creada. Puede agregar productos.");
            }
            else
            {
                MessageBox.Show("Error al crear la venta",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void NavegarAFormulario(Form formulario)
        {
            formulario.Show();
            this.Hide();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            NavegarAFormulario(new Inicio());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NavegarAFormulario(new FormFactura());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavegarAFormulario(new FormProductos());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Ya está en el módulo de Ventas",
                             "Información",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                Clientes crearCliente = new Clientes();
                crearCliente.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar crear el cliente: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnCLientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Hide();
        }
    }
}
