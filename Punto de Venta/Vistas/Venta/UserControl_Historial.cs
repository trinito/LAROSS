using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Historial : UserControl
    {
        VentaController ventasController;
        private readonly BindingSource bindingSource;
        private List<VentaDTO> ventas;
        private VentaDTO ventaSelect;
        private LoadingControl loadingOverlay;


        public UserControl_Historial()
        {
            InitializeComponent();

            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            ventasController = new VentaController();
            bindingSource = new BindingSource();
            ventas = new List<VentaDTO>();
            ventaSelect = new VentaDTO();

            GridViewHelper();

            //  lbl_hora.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        }

        private async void UserControl_Historial_Load(object sender, EventArgs e)
        {
            await CargarVentasEnDataGridView();
        }

        public async Task CargarVentasEnDataGridView()
        {
            ventas = await ventasController.ObtenerVentasDelDiaAsync(DateTime.Today);
            bindingSource.DataSource = ventas;
            decimal totalVentas = await ventasController.ObtenerTotalVentasDelDiaAsync(DateTime.Today);
            lbl_ventas.Text = totalVentas.ToString("C2"); // Formato moneda con 2 decimales
        }


        private void GridViewHelper()
        {
            dgv_ventas.AutoGenerateColumns = false;
            dgv_ventas.DataSource = bindingSource;
            dgv_ventas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv_ventas.Columns.Clear();

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdVenta",
                DataPropertyName = "IdVenta",
                HeaderText = "ID",
                Width = 40
            });

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 90,
                DefaultCellStyle = { Format = "d" }  // Formato corto de fecha
            });

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hora",
                DataPropertyName = "Hora",
                HeaderText = "Hora",
                Width = 90
            });

          

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                Width = 150

            });

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modificado",
                DataPropertyName = "Modificado",
                HeaderText = "Modificado",
                Width = 180,
                DefaultCellStyle = { Format = "MM/dd/yyyy hh:mm tt" }  // Ej: 06/27/2025 02:35 PM
            });
            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormaPago",
                DataPropertyName = "FormaPago",
                HeaderText = "Forma de pago",
                Width = 140
            });
     
            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = { Format = "C2" } // Formato moneda
            });

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pago",
                DataPropertyName = "Pago",
                HeaderText = "Pagó",
                Width = 100,
                DefaultCellStyle = { Format = "C2" } // Formato moneda
            });

            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cambio",
                DataPropertyName = "Cambio",
                HeaderText = "Cambio",
                Width = 100,
                DefaultCellStyle = { Format = "C2" } // Formato moneda
            });


            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadProductos",
                DataPropertyName = "CantidadProductos",
                HeaderText = "Productos",
                Width = 80
            });
            dgv_ventas.Columns["CantidadProductos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv_ventas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                DataPropertyName = "Estatus",
                HeaderText = "Estatus",
                Width = 100
            });
            dgv_ventas.Columns["Estatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv_ventas.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_ventas.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);



            if (dgv_ventas.Columns.Contains("IdVenta"))
                dgv_ventas.Columns["IdVenta"].Frozen = true;

            dgv_ventas.DoubleBuffered(true);
        }

        private async void btn_cancelar_Click(object sender, EventArgs e)
        {
            if (dgv_ventas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una venta para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var venta = (VentaDTO)dgv_ventas.CurrentRow.DataBoundItem;

            if (venta.Estatus == "CANCELADA")
            {
                MessageBox.Show("Esta venta ya está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"¿Cancelar la venta ID {venta.IdVenta}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await ventasController.CancelarVentaAsync(venta.IdVenta, SesionUsuario.UsuarioActual.id);

                    if (resultado)
                    {
                        MessageBox.Show("Venta cancelada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (venta.FormaPago =="EFECTIVO")
                        {
                            var cajaController = new CajaMovimientosController();
                            await cajaController.RegistrarRetiroAsync(venta.Total, "CANCELACION DE VENTA", SesionUsuario.UsuarioActual.id);
                        }

                        await CargarVentasEnDataGridView(); // recarga el grid con datos actualizados
                    }
                    else
                    {
                        MessageBox.Show("No se pudo cancelar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cancelar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_copia_Click(object sender, EventArgs e)
        {

        }

    }
}
