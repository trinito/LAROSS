using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PdfSharp.Fonts;
using PdfSharp.Pdf.IO;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Reportes : UserControl
    {
        private readonly DashboardController dashboardController = new DashboardController();
        private LoadingControl loadingOverlay;

        public UserControl_Reportes()
        {
            // ⚙️ Esto activa la resolución automática de Arial en Windows
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            InitializeComponent();

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

        //private async void UserControl_Reportes_Load(object sender, EventArgs e)
        //{
        //    await CargarDatosDashboardAsync();
        //}

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

                var controller = new CajaMovimientosController();
                decimal saldo = await controller.ObtenerSaldoActualAsync();
                lbl_caja.Text = saldo.ToString("C2");

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

        private async void button_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                button_imprimir.Enabled = false;
                var dashboard = new DashboardController();
                DateTime fechaSeleccionada = dtp_time.Value.Date;

                var productos = await dashboard.ObtenerDetalleProductosVendidosDiaAsync(fechaSeleccionada);
                var resumen = await dashboard.ObtenerResumenVentasDiaAsync(fechaSeleccionada);

                if (productos == null || productos.Count == 0 || resumen.MontoTotal == 0)
                {
                    MessageBox.Show("No hay ventas registradas para la fecha seleccionada.", "Sin ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_imprimir.Enabled = true;
                    return; // Detiene el flujo aquí
                }

                var ticket = new ImprimirTickets();

                ticket.TextoCentro("CORTE DE CAJA");
                ticket.TextoExtremos(fechaSeleccionada.ToString("dd/MM/yyyy"), DateTime.Now.ToString("hh:mm tt"));
                ticket.TextoIzquierda(" ");
                ticket.EncabezadoCorte();
                ticket.lineasGuio();

                foreach (var item in productos)
                {
                    ticket.AgregaArticulo2(item.NombreProducto, item.CantidadVendida, item.TotalProducto);
                }

                ticket.lineasGuio();

                ticket.AgregarTotales("            EFECTIVO:  ", resumen.TotalEfectivo);
                ticket.AgregarTotales("             TARJETA:  ", resumen.TotalTarjeta);
                ticket.AgregarTotales("       TRANSFERENCIA:  ", resumen.TotalTransferencia);
                ticket.AgregarTotales("         TOTAL VENTA: ", resumen.MontoTotal);

                ticket.lineasGuio();
                ticket.AgregarTotales("      FONDO EN CAJA: ", resumen.FondoCajaFinal);

                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.CortaTicket();
                ticket.ImprimirTicket("XP-58");

                // Al final, después de imprimir el ticket
                GenerarPdfCorteCaja(fechaSeleccionada, productos, resumen);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir el ticket: {ex.Message}", "Error");
            }
            finally
            {
                button_imprimir.Enabled = true;
            }
        }

        public void GenerarPdfCorteCaja(DateTime fechaSeleccionada, List<(string NombreProducto, int CantidadVendida, decimal TotalProducto)> productos, (int TotalVentas, decimal MontoTotal, decimal TotalEfectivo, decimal TotalTarjeta, decimal TotalTransferencia, decimal FondoCajaFinal) resumen)
        {
            PdfDocument document = null;
            try
            {
                document = new PdfDocument();
                document.Info.Title = "Corte de Caja - " + fechaSeleccionada.ToString("dd/MM/yyyy");

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Colores
                XColor colorFondoTitulo = XColor.FromArgb(21, 57, 93);    // Azul oscuro
                XColor colorTextoTitulo = XColors.White;
                XColor colorLinea = XColor.FromArgb(21, 57, 93);
                XColor colorFondoEncabezado = XColor.FromArgb(230, 230, 230); // Gris claro

                // Fuentes
                XFont fontTitulo = new XFont("Arial", 16, XFontStyleEx.Bold);
                XFont fontSubtitulo = new XFont("Arial", 12, XFontStyleEx.Bold);
                XFont fontNormal = new XFont("Arial", 10, XFontStyleEx.Regular);
                XFont fontNegrita = new XFont("Arial", 10, XFontStyleEx.Bold);

                double yPoint;
                double margenIzquierdo = 40;

                // Cargar y dibujar logo arriba, centrado
                string rutaLogo = @"C:\LaRoss\larospi.png";
                if (File.Exists(rutaLogo))
                {
                    XImage logo = XImage.FromFile(rutaLogo);
                    // Ajustar tamaño proporcional, ancho máximo 120
                    double maxAnchoLogo = 120;
                    double anchoLogo = logo.PixelWidth;
                    double altoLogo = logo.PixelHeight;
                    double escala = maxAnchoLogo / anchoLogo;
                    double altoAjustado = altoLogo * escala;

                    // Posición centrada horizontal y margen superior (y=5)
                    double xLogo = (page.Width.Point - maxAnchoLogo) / 2;
                    double yLogo = 5;

                    gfx.DrawImage(logo, xLogo, yLogo, maxAnchoLogo, altoAjustado);

                    yPoint = yLogo + altoAjustado + 35; // espacio debajo del logo
                }
                else
                {
                    yPoint = 40; // Si no existe logo, usar posición anterior
                }

                // Fondo del título (40 de alto) justo debajo del logo
                gfx.DrawRectangle(new XSolidBrush(colorFondoTitulo), 0, yPoint - 40, page.Width, 40);
                gfx.DrawString("CORTE DE CAJA", fontTitulo, new XSolidBrush(colorTextoTitulo), new XRect(0, yPoint - 40, page.Width, 40), XStringFormats.Center);

                yPoint += 50;

                // Fecha debajo del título
                gfx.DrawString("Fecha: " + fechaSeleccionada.ToString("dd/MM/yyyy"), fontSubtitulo, XBrushes.Black, new XRect(margenIzquierdo, yPoint, page.Width, 20), XStringFormats.TopLeft);
                yPoint += 30;

                gfx.DrawString("Hora de impresión:", fontNegrita, XBrushes.Black, margenIzquierdo, yPoint);
                gfx.DrawString(DateTime.Now.ToString("hh:mm tt"), fontNegrita, XBrushes.Black, margenIzquierdo + 100, yPoint);
                yPoint += 30;

                // Encabezado columnas con fondo gris
                gfx.DrawRectangle(new XSolidBrush(colorFondoEncabezado), margenIzquierdo, yPoint, page.Width - 2 * margenIzquierdo, 25);
                gfx.DrawString("Producto", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 5, yPoint + 5, 250, 20), XStringFormats.TopLeft);
                gfx.DrawString("Cant.", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 260, yPoint + 5, 50, 20), XStringFormats.TopLeft);
                gfx.DrawString("Total", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 320, yPoint + 5, 100, 20), XStringFormats.TopLeft);
                yPoint += 35;

                // Línea divisoria
                gfx.DrawLine(new XPen(colorLinea, 1), margenIzquierdo, yPoint, page.Width - margenIzquierdo, yPoint);
                yPoint += 10;

                // Lista de productos
                foreach (var prod in productos)
                {
                    gfx.DrawString(prod.NombreProducto, fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 5, yPoint, 250, 20), XStringFormats.TopLeft);
                    gfx.DrawString(prod.CantidadVendida.ToString(), fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 260, yPoint, 50, 20), XStringFormats.TopLeft);
                    gfx.DrawString(prod.TotalProducto.ToString("C2", CultureInfo.CurrentCulture), fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 320, yPoint, 100, 20), XStringFormats.TopLeft);
                    yPoint += 20;

                    // Si se llena la página, agregar nueva
                    if (yPoint > page.Height - 100)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPoint = 40;
                    }
                }

                yPoint += 20;
                gfx.DrawLine(new XPen(colorLinea, 1), margenIzquierdo, yPoint, page.Width - margenIzquierdo, yPoint);
                yPoint += 20;

                // Totales con un fondo gris suave
                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(240, 240, 240 )), margenIzquierdo, yPoint, page.Width - 2 * margenIzquierdo, 110);

                gfx.DrawString("FONDO EN CAJA:", fontNegrita, XBrushes.Black, margenIzquierdo + 5, yPoint +10 );
                gfx.DrawString(resumen.FondoCajaFinal.ToString("C2", CultureInfo.CurrentCulture), fontNormal, XBrushes.Black, page.Width - margenIzquierdo - 100, yPoint +10 );

                gfx.DrawString("EFECTIVO:", fontNegrita, XBrushes.Black, margenIzquierdo + 5, yPoint + 30);
                gfx.DrawString(resumen.TotalEfectivo.ToString("C2", CultureInfo.CurrentCulture), fontNormal, XBrushes.Black, page.Width - margenIzquierdo - 100, yPoint + 30);

                gfx.DrawString("TARJETA:", fontNegrita, XBrushes.Black, margenIzquierdo + 5, yPoint + 50);
                gfx.DrawString(resumen.TotalTarjeta.ToString("C2", CultureInfo.CurrentCulture), fontNormal, XBrushes.Black, page.Width - margenIzquierdo - 100, yPoint + 50);

                gfx.DrawString("TRANSFERENCIA:", fontNegrita, XBrushes.Black, margenIzquierdo + 5, yPoint + 70);
                gfx.DrawString(resumen.TotalTransferencia.ToString("C2", CultureInfo.CurrentCulture), fontNormal, XBrushes.Black, page.Width - margenIzquierdo - 100, yPoint + 70);

                gfx.DrawString("TOTAL VENTA:", fontNegrita, XBrushes.Black, margenIzquierdo + 5, yPoint + 100);
                gfx.DrawString(resumen.MontoTotal.ToString("C2", CultureInfo.CurrentCulture), fontNegrita, XBrushes.Black, page.Width - margenIzquierdo - 100, yPoint + 100);

              
                // Guardar PDF
                string carpetaDocumentos = @"C:\LaRoss\Cortes de cajas";
                if (!Directory.Exists(carpetaDocumentos))
                    Directory.CreateDirectory(carpetaDocumentos);

                string archivo = Path.Combine(carpetaDocumentos, $"CorteCaja_{fechaSeleccionada:dd-MM-yyyy}.pdf");
                document.Save(archivo);

                // Abrir automáticamente
                Process.Start(new ProcessStartInfo(archivo) { UseShellExecute = true });
            }
            finally
            {
                document?.Dispose();
            }
        }

    }
}
