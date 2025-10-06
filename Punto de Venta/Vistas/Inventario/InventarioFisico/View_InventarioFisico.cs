using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Inventario.InventarioFisico
{
    public partial class View_InventarioFisico : Form
    {
        private readonly BindingSource bindingSource;
        InventarioFisicoController inventarioFisicoController;
        private LoadingControl loadingOverlay;
        private int idInventarioActual;

        public View_InventarioFisico()
        {
            InitializeComponent();
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();
            bindingSource = new BindingSource();
            inventarioFisicoController = new InventarioFisicoController();
            GridViewHelper();
        }

        private async void View_InventarioFisico_Load(object sender, EventArgs e)
        {
            try
            {
                panel_main.Enabled = false;
                //await RevertirAjusteInventario(2);
               await InicializarInventarioFisicoAsync();
            }
            finally
            {
                loadingOverlay.HideOverlay();
                panel_main.Enabled = true;
            }
        }

        private async Task InicializarInventarioFisicoAsync()
        {
            try
            {
                int idUsuario = SesionUsuario.UsuarioActual.id;

                var inventarioExistente = await inventarioFisicoController.ObtenerInventarioActivo();

                if (inventarioExistente != null)
                {
                    idInventarioActual = inventarioExistente.id;
                    lbl_fecha.Text = inventarioExistente.fecha_inicio.ToString("dd/MM/yyyy");
                    lbl_fecha.Text = inventarioExistente.fecha_inicio.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
                }
                else
                {

                    DialogResult crearInventario = MessageBox.Show(
                    "¿Deseas crear un nuevo inventario físico?.",
                    "Nuevo Inventario",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                    if (crearInventario == DialogResult.No)
                    {
                        this.Close(); // cierra el formulario correctamente
                        return;
                    }
                        

                    // No mostrar overlay antes del diálogo para evitar bloquear UI
                    DialogResult result = MessageBox.Show(
                        "¿Deseas incluir productos con stock 0 en el inventario físico?",
                        "Opciones de Inventario",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    bool incluirStockCero = (result == DialogResult.Yes);

                    loadingOverlay.ShowOverlay(); // Mostrar overlay justo antes de operación async que tarda


                    idInventarioActual = await Task.Run(() => inventarioFisicoController.CrearInventarioFisico(idUsuario, incluirStockCero));
                    lbl_fecha.Text = DateTime.Today.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));

                }
                lbl_inventario.Text = "# " + idInventarioActual;


                // Obtener detalles del inventario (usando async)
                var detalles = await inventarioFisicoController.ObtenerDetalle(idInventarioActual);

                using (var context = new la_ross_dbEntities())
                {
                    var productos = await (from d in context.InventarioFisicoDetalle
                                           join a in context.Articulos on d.id_articulo equals a.id_producto
                                           join m in context.Marcas on a.id_marca equals m.id_marca
                                           join c in context.Colores on a.id_color equals c.id_color
                                           join t in context.Tallas on a.id_talla equals t.id_talla
                                           join s in context.Sexos on a.id_sexo equals s.id_sexo
                                           join cat in context.Categorias on a.id_categoria equals cat.id_categoria
                                           where d.id_inventario == idInventarioActual
                                           select new ProductoInventarioFisicoDTO
                                           {
                                               IdDetalle = d.id,
                                               CodigoBarras = a.codigo_barras,
                                               Nombre = a.nombre,
                                               Marca = m.nombre,
                                               Color = c.nombre,
                                               Talla = t.nombre,
                                               Sexo = s.nombre,
                                               Categoria = cat.nombre,
                                               CantidadContada = d.cantidad_contada,
                                               StockSistema = a.stock,
                                               Diferencia = d.cantidad_contada - a.stock
                                           }).ToListAsync();

                    bindingSource.DataSource = productos;
                }
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private void GridViewHelper()
        {
            dgv_productos.AutoGenerateColumns = false;
            dgv_productos.DataSource = bindingSource;

            dgv_productos.Columns.Clear();

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoBarras",
                DataPropertyName = "CodigoBarras",
                HeaderText = "Código de Barras",
                Width = 120
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 210
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Marca",
                DataPropertyName = "Marca",
                HeaderText = "Marca",
                Width = 120
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Color",
                DataPropertyName = "Color",
                HeaderText = "Color",
                Width = 120
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Talla",
                DataPropertyName = "Talla",
                HeaderText = "Talla",
                Width = 60
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sexo",
                DataPropertyName = "Sexo",
                HeaderText = "Sexo",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                DataPropertyName = "Categoria",
                HeaderText = "Categoría",
                Width = 135
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockSistema",
                DataPropertyName = "StockSistema",
                HeaderText = "Stock Sistema",
                Width = 90
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadContada",
                DataPropertyName = "CantidadContada",
                HeaderText = "Cantidad Contada",
                Width = 90
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Diferencia",
                DataPropertyName = "Diferencia",
                HeaderText = "Diferencia",
                Width = 90
            });

            dgv_productos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_productos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);

            if (dgv_productos.Columns.Contains("CodigoBarras"))
                dgv_productos.Columns["CodigoBarras"].Frozen = true;

            dgv_productos.DoubleBuffered(true);
        }

        private async void txt_producto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codigo = txt_producto.Text.Trim();
                if (!string.IsNullOrEmpty(codigo))
                {
                    loadingOverlay.ShowOverlay();
                    try
                    {
                        await ProcesarCodigoEscaneadoAsync(codigo);
                    }
                    finally
                    {
                        loadingOverlay.HideOverlay();
                    }
                    txt_producto.Clear();
                    txt_producto.Focus();
                }
                e.Handled = true;
            }
        }

        private async Task ProcesarCodigoEscaneadoAsync(string codigo)
        {
            loadingOverlay.ShowOverlay();
            bool disminuirStock = false;
            var listaProductos = bindingSource.DataSource as List<ProductoInventarioFisicoDTO>;
            if (listaProductos == null) return;


            if (codigo.StartsWith("-"))
            {
                disminuirStock = true;
                codigo = codigo.TrimStart('-');
            }

            var producto = listaProductos.FirstOrDefault(p => p.CodigoBarras == codigo);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado en el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nuevaCantidad = 0;

            if (disminuirStock)
                nuevaCantidad = producto.CantidadContada - 1;
            else
                nuevaCantidad = producto.CantidadContada + 1;

            bool exito = await inventarioFisicoController.RegistrarConteo(producto.IdDetalle, nuevaCantidad);

            if (exito)
            {
                producto.CantidadContada = nuevaCantidad;
                producto.Diferencia = nuevaCantidad - producto.StockSistema;

                bindingSource.ResetBindings(false);

                int index = listaProductos.IndexOf(producto);
                if (index >= 0 && index < dgv_productos.Rows.Count)
                {
                    dgv_productos.ClearSelection();
                    dgv_productos.Rows[index].Selected = true;
                    dgv_productos.CurrentCell = dgv_productos.Rows[index].Cells[0];
                }
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la cantidad contada en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadingOverlay.HideOverlay();
        }

        private async void btn_finalizar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Estás seguro que quieres finalizar y ajustar el inventario? Esto actualizará el stock real.",
                "Confirmar Finalización",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No) return;

            var listaProductos = bindingSource.DataSource as List<ProductoInventarioFisicoDTO>;
            if (listaProductos == null || listaProductos.Count == 0)
            {
                MessageBox.Show("No hay productos en el inventario para finalizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int productosConCero = listaProductos.Count(p => p.CantidadContada == 0);

            if (productosConCero > 0)
            {
                var resultado = MessageBox.Show(
                    $"Hay {productosConCero} productos con cantidad 0. ¿Deseas continuar y ajustar el inventario de todas formas?",
                    "Advertencia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No) return;
            }

            loadingOverlay.ShowOverlay();

            try
            {
                bool exito = await inventarioFisicoController.AjustarInventario(idInventarioActual);

                if (exito)
                {
                    var inventario = await inventarioFisicoController.ObtenerInventarioPorId(idInventarioActual); // necesitas este método
                    string observaciones = inventario.observaciones;
                    DateTime fechaAjuste = inventario.fecha_fin ?? DateTime.Now;

                    var productosConDiferencia = listaProductos
                        .Where(p => p.StockSistema != p.CantidadContada)
                        .ToList();

                    GenerarPdfInventarioFisico(idInventarioActual, fechaAjuste, observaciones, productosConDiferencia);

                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Inventario finalizado y ajustado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Error al finalizar el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async void btn_cancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Deseas cancelar este inventario sin guardar cambios?",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            loadingOverlay.ShowOverlay();

            try
            {
                bool exito = await inventarioFisicoController.CancelarInventario(idInventarioActual);

                if (exito)
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Inventario cancelado correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("No se pudo cancelar el inventario. Ya podría estar finalizado o no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
                MessageBox.Show($"Error al cancelar inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }


        private async Task RevertirAjusteInventario(int idInventario)
        {
            loadingOverlay.ShowOverlay();
            try
            {
                bool exito = await Task.Run(() => inventarioFisicoController.RevertirAjusteInventario(idInventario));
                // idInventarioActual = await Task.Run(() => inventarioFisicoController.CrearInventarioFisico(idUsuario, incluirStockCero));
                if (exito)
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Ajuste revertido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("No se pudo revertir el ajuste. Verifica que el inventario esté ajustado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        public void GenerarPdfInventarioFisico(int idInventario, DateTime fechaAjuste, string observaciones, List<ProductoInventarioFisicoDTO> productos)
        {
            PdfDocument document = null;
            try
            {
                document = new PdfDocument();
                document.Info.Title = $"REPORTE DE INVENTARIO FÍSICO #{idInventario} - {fechaAjuste:dd/MM/yyyy}";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);



                // Colores
                XColor colorFondoTitulo = XColor.FromArgb(21, 57, 93); // Azul oscuro
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

                // Título centrado
                gfx.DrawRectangle(new XSolidBrush(colorFondoTitulo), 0, yPoint - 40, page.Width, 40);
                gfx.DrawString($"REPORTE DE INVENTARIO FÍSICO #{idInventario}", fontTitulo, new XSolidBrush(colorTextoTitulo), new XRect(0, yPoint - 40, page.Width, 40), XStringFormats.Center);
                yPoint += 50;


                gfx.DrawString($"Fecha de ajuste: {fechaAjuste:dd MMMM yyyy}", fontSubtitulo, XBrushes.Black, new XRect(margenIzquierdo, yPoint, page.Width, 20), XStringFormats.TopLeft);
                yPoint += 30;

                // Observaciones con salto de línea y margen extra
                gfx.DrawString("Observaciones:", fontNegrita, XBrushes.Black, margenIzquierdo, yPoint);
                yPoint += 20;

                // Para evitar que se empalmen, dividir texto en líneas y dibujarlas
                var obsLines = observaciones?.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
                foreach (var line in obsLines)
                {
                    gfx.DrawString(line.Trim(), fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 10, yPoint, page.Width - 2 * margenIzquierdo, 20), XStringFormats.TopLeft);
                    yPoint += 18;
                    if (yPoint > page.Height - 100)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPoint = 40;
                    }
                }

                yPoint += 15;

                // Línea divisoria antes del listado
                gfx.DrawLine(new XPen(colorLinea, 1), margenIzquierdo, yPoint, page.Width - margenIzquierdo, yPoint);
                yPoint += 10;

                // Encabezado columnas con fondo gris
                gfx.DrawRectangle(new XSolidBrush(colorFondoEncabezado), margenIzquierdo, yPoint, page.Width - 2 * margenIzquierdo, 25);

                gfx.DrawString("Código Barras", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 5, yPoint + 5, 120, 20), XStringFormats.TopLeft);
                gfx.DrawString("Producto", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 130, yPoint + 5, 200, 20), XStringFormats.TopLeft);
                gfx.DrawString("Stock Sistema", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 300, yPoint + 5, 90, 20), XStringFormats.TopLeft);
                gfx.DrawString("Cantidad Contada", fontNegrita, XBrushes.Black, new XRect(margenIzquierdo + 400, yPoint + 5, 90, 20), XStringFormats.TopLeft);

                yPoint += 35;

                // Lista de productos
                foreach (var prod in productos)
                {
                    if (yPoint > page.Height - 50)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPoint = 40;
                    }

                    gfx.DrawString(prod.CodigoBarras, fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 5, yPoint, 120, 20), XStringFormats.TopLeft);
                    gfx.DrawString(prod.Nombre, fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 130, yPoint, 200, 20), XStringFormats.TopLeft);

                    // Alinear números a la derecha
                    gfx.DrawString(prod.StockSistema.ToString(), fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 250, yPoint, 90, 20), XStringFormats.TopRight);
                    gfx.DrawString(prod.CantidadContada.ToString(), fontNormal, XBrushes.Black, new XRect(margenIzquierdo + 360, yPoint, 90, 20), XStringFormats.TopRight);

                    yPoint += 20;
                }

                // Guardar PDF
                string carpetaDocumentos = @"C:\LaRoss\ReportesInventarioFisico";
                if (!Directory.Exists(carpetaDocumentos))
                    Directory.CreateDirectory(carpetaDocumentos);

                string archivo = Path.Combine(carpetaDocumentos, $"InventarioFisico_{idInventario}_{fechaAjuste:dd-MM-yyyy}.pdf");
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
