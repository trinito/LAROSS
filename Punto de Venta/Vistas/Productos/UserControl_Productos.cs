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

        public UserControl_Productos()
        {
            InitializeComponent();

            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            marcasController = new MarcasController();
            categoriasController = new CategoriasController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            ConfigurarBoton(btn_add_marca, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_add_categoria, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_add_talla, Resources.icons8_add_25, Resources.icons8_add_25_2);
            ConfigurarBoton(btn_add_sexo, Resources.icons8_add_25, Resources.icons8_add_25_2);
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
            // lógica adicional aquí (por ejemplo abrir formulario de marca)
            using (var form = new View_Marcas())
            {
                form.ShowDialog();
                // Espera que recargue la lista antes de continuar
                await CargarMarcasEnComboBoxAsync();
            }
        }

        private async void btn_add_categoria_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_categoria);
            using (var form = new View_Categorias())
            {
                form.ShowDialog();
                // Espera que recargue la lista antes de continuar
               await CargarCategoriasEnComboBoxAsync();
            }
        }

        private void btn_add_talla_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_talla);
            // lógica adicional aquí
        }

        private void btn_add_sexo_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_add_sexo);
            // lógica adicional aquí
        }

        private async void UserControl_Productos_Load(object sender, EventArgs e)
        {
            await CargarMarcasEnComboBoxAsync();
            await CargarCategoriasEnComboBoxAsync();
        }

        private async Task CargarMarcasEnComboBoxAsync()
        {
            try
            {
                loadingOverlay.ShowOverlay();

                var listaMarcas = await marcasController.ObtenerTodasLasMarcasAsync();

                // Limpia la fuente antes de asignar nueva lista
                cb_marcas.DataSource = null;
                cb_marcas.Items.Clear();

                cb_marcas.DataSource = listaMarcas;
                cb_marcas.DisplayMember = "nombre";  // Lo que el usuario verá
                cb_marcas.ValueMember = "id_marca";   // El valor interno (id)
                cb_marcas.SelectedIndex = -1;         // Opcional: dejar sin selección inicial
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
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

                // Limpia la fuente antes de asignar nueva lista
                cb_categoria.DataSource = null;
                cb_categoria.Items.Clear();

                cb_categoria.DataSource = listaCategorias;
                cb_categoria.DisplayMember = "nombre";  // Lo que el usuario verá
                cb_categoria.ValueMember = "id_categoria";   // El valor interno (id)
                cb_categoria.SelectedIndex = -1;         // Opcional: dejar sin selección inicial
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
                MessageBox.Show("Error al cargar marcas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }
    }
}
