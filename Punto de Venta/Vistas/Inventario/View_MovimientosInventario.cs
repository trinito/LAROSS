using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Punto_de_Venta.Controlador.InventarioMovimientosController;

namespace Punto_de_Venta.Vistas.Inventario
{
    public partial class View_MovimientosInventario : Form
    {
        private readonly BindingSource bindingSource;
        private LoadingControl loadingOverlay;
        private InventarioMovimientosController inventarioMovimientosController;
         // Crea tu controlador

        public View_MovimientosInventario()
        {
            InitializeComponent();
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();
            bindingSource = new BindingSource();
            inventarioMovimientosController = new InventarioMovimientosController();
        }

        private async void View_MovimientosInventario_Load(object sender, EventArgs e)
        {
            GridViewHelper();
            await CargarMovimientosAsync();
        }

        private void GridViewHelper()
        {
            dgv_movimientos.AutoGenerateColumns = false;
            dgv_movimientos.DataSource = bindingSource;
            dgv_movimientos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_movimientos.Columns.Clear();

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdMovimiento",
                DataPropertyName = "IdMovimiento",
                HeaderText = "ID",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoBarras",
                DataPropertyName = "CodigoBarras",
                HeaderText = "Código",
                Width = 90,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                DataPropertyName = "NombreProducto",
                HeaderText = "Producto",
                Width = 200
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Motivo",
                DataPropertyName = "Motivo",
                HeaderText = "Motivo",
                Width = 250
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                Width = 150
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockAntes",
                DataPropertyName = "StockAntes",
                HeaderText = "Stock Antes",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockDespues",
                DataPropertyName = "StockDespues",
                HeaderText = "Stock Después",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });



            dgv_movimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 170,
                DefaultCellStyle = { Format = "dd/MM/yyyy hh:mm: tt", 
                Alignment = DataGridViewContentAlignment.MiddleCenter }
            });


            dgv_movimientos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_movimientos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);

            if (dgv_movimientos.Columns.Contains("IdMovimiento"))
                dgv_movimientos.Columns["IdMovimiento"].Frozen = true;

            dgv_movimientos.DoubleBuffered(true);
        }


        private async Task CargarMovimientosAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();
                var movimientos = await inventarioMovimientosController.ObtenerMovimientosAsync(dtp_time.Value);
                dgv_movimientos.DataSource = new BindingList<MovimientoDTO>(movimientos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar movimientos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async void dtp_time_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                loadingOverlay.ShowOverlay();
                var movimientos = await inventarioMovimientosController.ObtenerMovimientosAsync(dtp_time.Value);
                bindingSource.DataSource = movimientos;
                dgv_movimientos.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar movimientos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
            
        }

    }
}
