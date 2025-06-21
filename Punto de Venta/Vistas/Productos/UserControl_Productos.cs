using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Punto_de_Venta.Properties;
using Punto_de_Venta.Vistas.Productos;
using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using System.IO;
using System.Drawing.Imaging;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Productos : UserControl
    {
        private LoadingControl loadingOverlay;

        private Timer animTimer;
        private int animPaso = 0;
        private Size originalSize;
        private Point originalLocation;
        private Button currentButton;
        private const int scale = 10;

        private MarcasController marcasController;
        private CategoriasController categoriasController;
        private TallasController tallasController;
        private SexoController sexoController;
        private ColorController colorController;
        private ProductosController productosController;
        

        private string rutaImagenGuardada = null; // Variable para guardar ruta imagen en el form
        private string rutaBaseImagenes = @"C:\LaRoss\imagenes_productos";
        private string nombreArchivoImagenGuardada = null; // Guardará solo el nombre del archivo

        public UserControl_Productos()
        {
            InitializeComponent();


            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            marcasController = new MarcasController();
            categoriasController = new CategoriasController();
            tallasController = new TallasController();
            sexoController = new SexoController();
            colorController = new ColorController();
            productosController = new ProductosController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            ConfigurarBoton(btn_add_marca, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_add_categoria, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_add_talla, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_add_sexo, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_color, Resources.icons8_add_25, Resources.icons8_add_25_2);
            txt_nombre.Focus();


        }

        private void ConfigurarBoton(Button boton, Image imgNormal, Image imgHover)
        {
            boton.MouseEnter += (s, e) => { boton.Image = imgHover; };
            boton.MouseLeave += (s, e) => { boton.Image = imgNormal; };
        }

        private void AnimarBoton(Button boton)
        {
            if (animTimer.Enabled) return;

            currentButton = boton;
            originalSize = boton.Size;
            originalLocation = boton.Location;
            animPaso = 0;
            animTimer.Start();
        }

        private void AnimacionClic(object sender, EventArgs e)
        {
            if (currentButton == null) return;

            if (animPaso == 0)
            {
                currentButton.Size = new Size(originalSize.Width - scale, originalSize.Height - scale);
                currentButton.Location = new Point(originalLocation.X + scale / 2, originalLocation.Y + scale / 2);
                animPaso++;
            }
            else
            {
                currentButton.Size = originalSize;
                currentButton.Location = originalLocation;
                animTimer.Stop();
                currentButton = null;
            }
        }

        private async void btn_add_marca_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_marca);
            using (var form = new View_Marcas())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
                await CargarMarcasEnComboBoxAsync();
            }
        }

        private async void btn_add_categoria_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_categoria);
            using (var form = new View_Categorias())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
                await CargarCategoriasEnComboBoxAsync();
            }
        }

        private async void btn_add_talla_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_talla);
            using (var form = new View_Tallas())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
                await CargarTallasEnComboBoxAsync();
            }
        }

        private async void btn_add_sexo_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_sexo);
            using (var form = new View_Sexos())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
                await CargarSexosEnComboBoxAsync();
            }
        }

        private async void btn_color_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_color);
            using (var form = new View_Colores())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
                await CargarColoresEnComboBoxAsync();
            }
        }

        private async void UserControl_Productos_Load(object sender, EventArgs e)
        {
            await CargarMarcasEnComboBoxAsync();
            await CargarCategoriasEnComboBoxAsync();
            await CargarTallasEnComboBoxAsync();
            await CargarSexosEnComboBoxAsync();
            await CargarColoresEnComboBoxAsync();
        }

        private async Task CargarMarcasEnComboBoxAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                var listaMarcas = await marcasController.ObtenerTodasLasMarcasAsync();
                cb_marcas.DataSource = null;
                cb_marcas.Items.Clear();

                cb_marcas.DataSource = listaMarcas;
                cb_marcas.DisplayMember = "nombre";
                cb_marcas.ValueMember = "id_marca";
                cb_marcas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async Task CargarCategoriasEnComboBoxAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                var listaCategorias = await categoriasController.ObtenerTodasLasCategoriasAsync();
                cb_categoria.DataSource = null;
                cb_categoria.Items.Clear();

                cb_categoria.DataSource = listaCategorias;
                cb_categoria.DisplayMember = "nombre";
                cb_categoria.ValueMember = "id_categoria";
                cb_categoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async Task CargarTallasEnComboBoxAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                var listaTallas = await tallasController.ObtenerTodasLasTallasAsync();
                cb_tallas.DataSource = null;
                cb_tallas.Items.Clear();

                cb_tallas.DataSource = listaTallas;
                cb_tallas.DisplayMember = "nombre";
                cb_tallas.ValueMember = "id_talla";
                cb_tallas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tallas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async Task CargarSexosEnComboBoxAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                var listaSexos = await sexoController.ObtenerTodosLosSexosAsync();
                cb_sexo.DataSource = null;
                cb_sexo.Items.Clear();

                cb_sexo.DataSource = listaSexos;
                cb_sexo.DisplayMember = "nombre";
                cb_sexo.ValueMember = "id_sexo";
                cb_sexo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar sexos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async Task CargarColoresEnComboBoxAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                var listaColores = await colorController.ObtenerTodosLosColoresAsync();
                cb_color.DataSource = null;
                cb_color.Items.Clear();

                cb_color.DataSource = listaColores;
                cb_color.DisplayMember = "nombre";
                cb_color.ValueMember = "id_color";
                cb_color.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar colores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }



        private void btn_add_img_Click(object sender, EventArgs e)
        {
            btn_add_img.Enabled = false;
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Seleccionar imagen";
                    openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaOriginal = openFileDialog.FileName;

                        // Cargar imagen en memoria sin bloquear archivo
                        Image original;
                        using (FileStream stream = new FileStream(rutaOriginal, FileMode.Open, FileAccess.Read))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            ms.Position = 0;
                            original = Image.FromStream(ms);
                        }

                        using (original)
                        {
                            Size maxSize = new Size(600, 600);
                            Size nuevoTamaño = ObtenerTamañoEscalado(original.Size, maxSize);
                            using (Bitmap redimensionada = RedimensionarImagenConCalidad(original, nuevoTamaño))
                            {
                                if (!Directory.Exists(rutaBaseImagenes))
                                    Directory.CreateDirectory(rutaBaseImagenes);

                                // Generar solo el nombre del archivo
                                string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                                string rutaFinal = Path.Combine(rutaBaseImagenes, nombreArchivo);

                                // Guardar con calidad JPEG
                                var encoderParams = new EncoderParameters(1);
                                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);
                                var jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                                redimensionada.Save(rutaFinal, jpgEncoder, encoderParams);

                                // Guardar solo el nombre para la BD
                                nombreArchivoImagenGuardada = nombreArchivo;

                                // Mostrar imagen redimensionada en picturebox
                                pb_imagen.Image = new Bitmap(redimensionada);
                            }
                        }
                    }
                }
            }
            finally
            {
                btn_add_img.Enabled = true;
            }
        }

        // Método para cargar la imagen después, ejemplo para usar en tu formulario o donde necesites:
        private void CargarImagenDesdeNombre(string nombreArchivo)
        {
            string rutaCompleta = Path.Combine(rutaBaseImagenes, nombreArchivo);
            if (File.Exists(rutaCompleta))
            {
                pb_imagen.Image = Image.FromFile(rutaCompleta);
            }
            else
            {
                pb_imagen.Image = null; // o alguna imagen por defecto
            }
        }


        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }


        private Size ObtenerTamañoEscalado(Size original, Size maximo)
        {
            float ratioX = (float)maximo.Width / original.Width;
            float ratioY = (float)maximo.Height / original.Height;
            float ratio = Math.Min(ratioX, ratioY); // Para mantener la proporción

            int nuevoAncho = (int)(original.Width * ratio);
            int nuevaAltura = (int)(original.Height * ratio);

            return new Size(nuevoAncho, nuevaAltura);
        }

        private Bitmap RedimensionarImagenConCalidad(Image imagenOriginal, Size nuevoTamaño)
        {
            Bitmap imagenRedimensionada = new Bitmap(nuevoTamaño.Width, nuevoTamaño.Height);
            using (Graphics g = Graphics.FromImage(imagenRedimensionada))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                g.DrawImage(imagenOriginal, 0, 0, nuevoTamaño.Width, nuevoTamaño.Height);
            }
            return imagenRedimensionada;
        }

        private async void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txt_nombre.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_nombre.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_stock.Text) || !int.TryParse(txt_stock.Text, out int stock) || stock < 0)
                {
                    MessageBox.Show("Ingrese un stock válido (número entero mayor o igual a 0).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_stock.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_costo.Text) || !decimal.TryParse(txt_costo.Text, out decimal precioCosto) || precioCosto < 0)
                {
                    MessageBox.Show("Ingrese un precio de costo válido (número decimal mayor o igual a 0).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_costo.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_venta.Text) || !decimal.TryParse(txt_venta.Text, out decimal precioVenta) || precioVenta <= precioCosto)
                {
                    MessageBox.Show("El precio de venta debe ser un número decimal mayor que el precio de costo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_venta.Focus();
                    return;
                }
                if (cb_marcas.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una marca.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_marcas.Focus();
                    return;
                }
                if (cb_categoria.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_categoria.Focus();
                    return;
                }
                if (cb_tallas.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una talla.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_tallas.Focus();
                    return;
                }
                if (cb_sexo.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_sexo.Focus();
                    return;
                }
                if (cb_color.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un color.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_color.Focus();
                    return;
                }
                if (pb_imagen.Image == null)
                {
                    MessageBox.Show("Debe adjuntar una imagen para el producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear objeto producto
                var nuevoProducto = new Articulos
                {
                    codigo_barras = "", //GenerarCodigoBarrasInterno(), // Aquí implementa o llama la función para generar tu código interno
                    codigo_barras_original = ObtenerCodigoBarrasOriginal(), // Aquí obtén el código original, puede ser un textbox o variable
                    nombre = txt_nombre.Text.Trim(),
                    id_marca = (int)cb_marcas.SelectedValue,
                    id_categoria = (int)cb_categoria.SelectedValue,
                    id_talla = (int)cb_tallas.SelectedValue,
                    id_sexo = (int)cb_sexo.SelectedValue,
                    id_color = (int)cb_color.SelectedValue,
                    stock = stock,
                    precio_costo = precioCosto,
                    precio_venta = precioVenta,
                    estatus = true,
                    foto = nombreArchivoImagenGuardada
                };

                int idNuevo = await productosController.InsertProductoAsync(nuevoProducto);

                // Genera el código de barras: 8 dígitos con ceros a la izquierda
                string codigoGenerado = idNuevo.ToString("D8");  // Ej: 25 => "00000025"

                // Actualiza el producto con el código generado
                nuevoProducto.id_producto = idNuevo;
                nuevoProducto.codigo_barras = codigoGenerado;

                bool actualizado = await productosController.UpdateProductoAsync(nuevoProducto);

                if (idNuevo > 0 && actualizado)
                {
                    MessageBox.Show("Producto "+ txt_nombre.Text.Trim() + " agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ImprimirCodigosDeBarras codigos = new ImprimirCodigosDeBarras();
                    LimpiarFormulario();
                    codigos.ImprimirCodigo(codigoGenerado, stock);
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarCodigoBarrasInterno()
        {
            // Ejemplo simple: generar un GUID sin guiones como código interno
            return Guid.NewGuid().ToString("N").ToUpper();
        }

        private string ObtenerCodigoBarrasOriginal()
        {
            return txt_original_codigo_barras.Text.Trim();
        }


        private void LimpiarFormulario()
        {
            txt_nombre.Text = string.Empty;
            txt_stock.Text = string.Empty;
            txt_costo.Text = string.Empty;
            txt_venta.Text = string.Empty;
            txt_original_codigo_barras.Text = string.Empty;

            // Si tienes textbox para código original, limpia también aquí
            // txt_codigo_barras_original.Clear();

            cb_marcas.SelectedIndex = -1;
            cb_categoria.SelectedIndex = -1;
            cb_tallas.SelectedIndex = -1;
            cb_sexo.SelectedIndex = -1;
            cb_color.SelectedIndex = -1;

            

            pb_imagen.Image = null;
            nombreArchivoImagenGuardada = null; // Limpiamos la variable para la próxima vez
        }

    }
}
