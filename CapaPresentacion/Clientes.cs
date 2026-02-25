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
    public partial class Clientes : Form
    {
        private ClienteBL clienteBL = new ClienteBL();
        private ClienteDAL clienteDAL = new ClienteDAL(); 

       
        private bool modoEdicion = false;

        // Variable para almacenar el ID del cliente seleccionado
        private int idClienteSeleccionado = 0;

        public Clientes()
        {
            InitializeComponent();
            this.dgvClientes.SelectionChanged += new System.EventHandler(this.dgvClientes_SelectionChanged);
    
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            this.txtTele.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTele_KeyPress);
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();

            CargarClientes();

            ConfigurarBotonAgregar();
        }

        private void ConfigurarDataGridView()
        {
            // Configurar las columnas del DataGridView
            dgvClientes.AutoGenerateColumns = false;

            // Limpiar columnas existentes si las hay
            dgvClientes.Columns.Clear();

            // Columna ID
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "Id_cliente";
            colId.HeaderText = "ID";
            colId.Width = 50;
            colId.Visible = true;
            dgvClientes.Columns.Add(colId);

            // Columna Nombre
            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn();
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.Width = 150;
            dgvClientes.Columns.Add(colNombre);

            // Columna Dirección
            DataGridViewTextBoxColumn colDireccion = new DataGridViewTextBoxColumn();
            colDireccion.DataPropertyName = "Direccion";
            colDireccion.HeaderText = "Dirección";
            colDireccion.Width = 200;
            dgvClientes.Columns.Add(colDireccion);

            // Columna Teléfono
            DataGridViewTextBoxColumn colTelefono = new DataGridViewTextBoxColumn();
            colTelefono.DataPropertyName = "Telefono";
            colTelefono.HeaderText = "Teléfono";
            colTelefono.Width = 100;
            dgvClientes.Columns.Add(colTelefono);

            // Columna Correo
            DataGridViewTextBoxColumn colCorreo = new DataGridViewTextBoxColumn();
            colCorreo.DataPropertyName = "Correo";
            colCorreo.HeaderText = "Correo";
            colCorreo.Width = 150;
            dgvClientes.Columns.Add(colCorreo);

            // Configurar selección
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
        }

        private void CargarClientes()
        {
            try
            {
                // Obtener la lista de clientes desde la capa de negocio
                List<Cliente> listaClientes = clienteBL.Listar();

                // Asignar al DataGridView
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = listaClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarBotonAgregar()
        {
            btnAgregar.Text = "Agregar";
            btnAgregar.BackColor = Color.FromKnownColor(KnownColor.Control);
            modoEdicion = false;
            idClienteSeleccionado = 0;
        }

        private void ConfigurarBotonEditar()
        {
            btnAgregar.Text = "Editar";
            btnAgregar.BackColor = Color.LightBlue;
            modoEdicion = true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTele.Text = string.Empty;
            txtCorreo.Text = string.Empty;

            // Deseleccionar fila en el DataGridView
            dgvClientes.ClearSelection();

            // Volver a modo agregar
            ConfigurarBotonAgregar();
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvClientes.SelectedRows[0];

                // Obtener el cliente seleccionado
                Cliente clienteSeleccionado = fila.DataBoundItem as Cliente;

                if (clienteSeleccionado != null)
                {
                    // Cargar datos en los campos
                    idClienteSeleccionado = clienteSeleccionado.Id_cliente;
                    txtNombre.Text = clienteSeleccionado.Nombre;
                    txtDireccion.Text = clienteSeleccionado.Direccion;
                    txtTele.Text = clienteSeleccionado.Telefono;
                    txtCorreo.Text = clienteSeleccionado.Correo;

                    // Cambiar botón a modo edición
                    ConfigurarBotonEditar();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos
                if (!ValidarCampos())
                {
                    return;
                }

                // Crear objeto cliente con los datos del formulario
                Cliente cliente = new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTele.Text.Trim(),
                    Correo = txtCorreo.Text.Trim()
                };

                if (modoEdicion)
                {
                    // Modo EDICIÓN - Actualizar cliente existente
                    cliente.Id_cliente = idClienteSeleccionado;

                    // Llamar al método Actualizar de la capa de negocio/Datos
                    clienteDAL.Actualizar(cliente); // O clienteBL.Actualizar(cliente)

                    MessageBox.Show("Cliente actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modo AGREGAR - Insertar nuevo cliente

                    // Llamar al método Insertar de la capa de negocio/Datos
                    clienteDAL.Insertar(cliente); // O clienteBL.Insertar(cliente)

                    MessageBox.Show("Cliente agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Recargar la lista de clientes
                CargarClientes();

                // Limpiar campos
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTele.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTele.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            // Validar formato de correo electrónico
            try
            {
                var addr = new System.Net.Mail.MailAddress(txtCorreo.Text);
            }
            catch
            {
                MessageBox.Show("El correo electrónico no tiene un formato válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            return true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este cliente?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    // Obtener el ID del cliente seleccionado
                    DataGridViewRow fila = dgvClientes.SelectedRows[0];
                    Cliente clienteSeleccionado = fila.DataBoundItem as Cliente;

                    if (clienteSeleccionado != null)
                    {
                        // Llamar al método Eliminar
                        clienteDAL.Eliminar(clienteSeleccionado.Id_cliente);

                        MessageBox.Show("Cliente eliminado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar lista
                        CargarClientes();

                        // Limpiar campos
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el cliente: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTele_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, teclas de control (backspace, delete) y caracteres especiales
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '-' && e.KeyChar != ' '
                && e.KeyChar != '(' && e.KeyChar != ')'
                && e.KeyChar != '+' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnCLientes_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            FormFactura quintoForm = new FormFactura();
            quintoForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCategoria quintoForm = new FormCategoria();
            quintoForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormProductos cuartoForm = new FormProductos();
            cuartoForm.Show();
            this.Hide();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
