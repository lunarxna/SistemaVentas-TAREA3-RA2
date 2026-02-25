using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion.Facturacion
{
    public partial class FormProductos : Form
    {


        private ProductoBL bl = new ProductoBL();
        private int idSeleccionado = 0;

        private CategoriaBL cl = new CategoriaBL();
        public FormProductos()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();
            Listar();
        }

        private void Listar()
        {
            // Cargar productos
            List<Producto> productos = bl.Listar();

            // Crear un DataTable personalizado para mostrar los datos como quieres
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_Producto", typeof(int));
            dt.Columns.Add("Nombre_Producto", typeof(string));
            dt.Columns.Add("Precio_Producto", typeof(decimal));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("Categoria", typeof(string)); // Esta columna mostrará el nombre

            // Llenar el DataTable
            foreach (Producto p in productos)
            {
                dt.Rows.Add(
                    p.Id_Producto,
                    p.Nombre_Producto,
                    p.Precio_Producto,
                    p.Stock,
                    p.ObjCategoria != null ? p.ObjCategoria.Nombre_Categoria : "SIN CATEGORÍA"
                );
            }

            // Asignar el DataSource
            dgvProductos.DataSource = dt;

            // Cargar categorías en el ComboBox
            cmbCategorias.DataSource = cl.Listar();
            cmbCategorias.DisplayMember = "Nombre_categoria";
            cmbCategorias.ValueMember = "Id_categoria";


        }

        private void FormVentas_Load(object sender, EventArgs e)
        {
            nudStock.Maximum = 1000000;

            // Ya no necesitas ocultar estas columnas porque ahora usas DataTable
            // Solo configura las columnas que quieres mostrar
            if (dgvProductos.Columns.Contains("Id_Producto"))
                dgvProductos.Columns["Id_Producto"].Visible = false;

            if (dgvProductos.Columns.Contains("Categoria"))
            {
                dgvProductos.Columns["Categoria"].HeaderText = "Categoría";
                dgvProductos.Columns["Categoria"].Width = 150;
            }
        }

        

       
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProducto.Text))
                {
                    MessageBox.Show("Por favor, ingrese un nombre.");
                    return;
                }

                int idCat = Convert.ToInt32(cmbCategorias.SelectedValue);

                Producto p = new Producto
                {
                    Id_Producto = idSeleccionado,
                    Nombre_Producto = txtProducto.Text,
                    Precio_Producto = Convert.ToDecimal(txtPrecio.Text),
                    Stock = (int)nudStock.Value,
                    Id_Categoria = idCat

                };


                if (idSeleccionado == 0)
                {

                    bl.Insertar(p);
                    MessageBox.Show("Producto guardado con éxito");
                }
                else
                {

                    bl.Actualizar(p);
                    MessageBox.Show("Producto actualizado con éxito");
                }

                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void Limpiar()
        {
            idSeleccionado = 0;           // Resetear el ID seleccionado
            txtProducto.Clear();           // Limpiar nombre
            txtPrecio.Clear();             // Limpiar precio
            nudStock.Value = 1;            // Resetear stock a valor por defecto

            // Resetear el ComboBox al primer elemento (opcional)
            if (cmbCategorias.Items.Count > 0)
            {
                cmbCategorias.SelectedIndex = 0;  // Seleccionar la primera categoría
            }

            // Cambiar el texto del botón de agregar/actualizar
            btnAgregar.Text = "Agregar";   // Cambiar a "Agregar" para nuevo producto

            // Opcional: enfocar el campo de nombre para empezar a escribir
            txtProducto.Focus();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            

        }
     
       

        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                idSeleccionado = Convert.ToInt32(fila.Cells["Id_Producto"].Value);
                txtProducto.Text = fila.Cells["Nombre_Producto"].Value.ToString();
                txtPrecio.Text = fila.Cells["Precio_Producto"].Value.ToString();
                nudStock.Value = Convert.ToDecimal(fila.Cells["Stock"].Value);

                // Obtener el nombre de la categoría de la fila seleccionada
                string nombreCategoria = fila.Cells["Categoria"].Value.ToString();

                // Buscar en el ComboBox el item que tenga ese nombre y seleccionarlo
                for (int i = 0; i < cmbCategorias.Items.Count; i++)
                {
                    // Obtener el objeto Categoria del item actual
                    Categoria cat = (Categoria)cmbCategorias.Items[i];

                    // Comparar el nombre de la categoría
                    if (cat.Nombre_Categoria == nombreCategoria)
                    {
                        cmbCategorias.SelectedIndex = i;
                        break;
                    }
                }

                btnAgregar.Text = "Actualizar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado > 0)
            {
                // 2. Preguntar al usuario para evitar accidentes
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultado == DialogResult.Yes)
                {
                    // 3. Llamamos a la lógica de negocio (BL) de productos
                
                    bl.Eliminar(idSeleccionado);

                    MessageBox.Show("Producto eliminado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Refrescamos la interfaz
                    Limpiar();
                    Listar();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista primero.", "Aviso");
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Inicio primerForm = new Inicio();
            primerForm.ShowDialog();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormFactura quintoForm = new FormFactura();
            quintoForm.ShowDialog();
            this.Hide();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FormVentas segundoForm = new FormVentas();
            segundoForm.ShowDialog();
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

        }

        private void btnCLientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Hide();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            // mostrar un mensaje
            MessageBox.Show("Formulario limpiado. Puede agregar un nuevo producto.", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
