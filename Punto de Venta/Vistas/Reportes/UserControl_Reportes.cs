using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Reportes : UserControl
    {
        private readonly DashboardController dashboardController = new DashboardController();
        private LoadingControl loadingOverlay;

        public UserControl_Reportes()
        {
            InitializeComponent();

            Load += UserControl_Reportes_Load;
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            dgv_productos.BackgroundColor = Color.White;
            dgv_productos.EnableHeadersVisualStyles = false;
            dgv_productos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(21, 57, 93);
            dgv_productos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_productos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 12, FontStyle.Bold);
            dgv_productos.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Regular);
            dgv_productos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(4, 46, 87);
            dgv_productos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv_productos.ReadOnly = true;
            dgv_productos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_productos.MultiSelect = false;
            dgv_productos.TabStop = false;
            dgv_productos.SelectionChanged += (s, e) => dgv_productos.ClearSelection();

            DateTime fecha = DateTime.Today; // O cualquier fecha específica
            string formato = fecha.ToString("dddd, dd 'de' MMMM 'del' yyyy", new CultureInfo("es-MX"));
            formato = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formato);

            lbl_fecha.Text = formato;
        }

        private async void UserControl_Reportes_Load(object sender, EventArgs e)
        {
            await CargarDatosDashboardAsync();
        }

        public async Task CargarDatosDashboardAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                DateTime hoy = DateTime.Today;

                // Total ventas del día
                decimal totalVentasDia = await dashboardController.ObtenerTotalVentasDiaAsync(hoy);
                lbl_ventas_dia.Text = totalVentasDia.ToString("C2");

                // Ventas por forma de pago
                var ventasPorFormaPago = await dashboardController.ObtenerVentasPorFormaPagoDiaAsync(hoy);

                lbl_efectivo.Text = ventasPorFormaPago.ContainsKey("EFECTIVO") ? ventasPorFormaPago["EFECTIVO"].ToString("C2") : 0m.ToString("C2");
                lbl_tarjeta.Text = ventasPorFormaPago.ContainsKey("TARJETA") ? ventasPorFormaPago["TARJETA"].ToString("C2") : 0m.ToString("C2");
                lbl_transferencia.Text = ventasPorFormaPago.ContainsKey("TRANSFERENCIA") ? ventasPorFormaPago["TRANSFERENCIA"].ToString("C2") : 0m.ToString("C2");

                // Ventas por día del mes
                var ventasMes = await dashboardController.ObtenerVentasPorDiaDelMesAsync(hoy);
                ConfigurarChartVentas(chart_ventas_mes, ventasMes);

                // Productos más vendidos del mes (top 5)
                var productosMasVendidos = await dashboardController.ObtenerProductosMasVendidosDelMesAsync(hoy, 10);
                dgv_productos.DataSource = productosMasVendidos.Select(p => new
                {
                    Producto = p.NombreProducto,
                    Cantidad = p.CantidadVendida
                }).ToList();

                if (dgv_productos.Columns.Count == 2)
                {
                    dgv_productos.Columns[0].HeaderText = "Producto";
                    dgv_productos.Columns[1].HeaderText = "Cantidad";
                    dgv_productos.AutoResizeColumns();
                }
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }


        private void ConfigurarChartVentas(Chart chart, List<(DateTime Fecha, decimal Total)> ventasMes)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            var chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Día";
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F);

            chartArea.AxisY.Title = "Total ($)";
            chartArea.AxisY.LabelStyle.Format = "C";
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;

            chart.ChartAreas.Add(chartArea);

            var series = new Series("Ventas")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "C2",
                Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold),
                Color = System.Drawing.Color.FromArgb(72, 145, 220)
            };

            foreach (var item in ventasMes.OrderBy(x => x.Fecha))
            {
                series.Points.AddXY($"Día {item.Fecha.Day}", item.Total);
            }

            chart.Series.Add(series);

            var legend = new Legend();
            legend.Docking = Docking.Top;
            chart.Legends.Add(legend);
        }

    }
}
