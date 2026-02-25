using Microsoft.Reporting.WinForms;
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
    public partial class FormFacturaEmergente : Form
    {
        public FormFacturaEmergente()
        {
            InitializeComponent();
        }

        public void CargarFactura(int idVenta)
        {
            try
            {
                // Obtener datos de la factura
                CapaNegocio.VentaBL ventaBL = new CapaNegocio.VentaBL();
                DataTable dtFactura = ventaBL.ObtenerDatosFactura(idVenta);

                if (dtFactura == null || dtFactura.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encontraron datos para la venta #{idVenta}",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                // Configurar ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource = new ReportDataSource("DatosDeLaFactura", dtFactura);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Ruta del reporte
                string rutaReporte = System.IO.Path.Combine(
                    Application.StartupPath,
                    "Reportes",
                    "ReporteVentas.rdlc"
                );

                if (!System.IO.File.Exists(rutaReporte))
                {
                    MessageBox.Show($"Archivo de reporte no encontrado:\n{rutaReporte}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                reportViewer1.LocalReport.ReportPath = rutaReporte;
                reportViewer1.RefreshReport();

                // Configurar título de la ventana
                this.Text = $"Factura Venta #{idVenta} - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la factura: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void FormFacturaEmergente_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
