using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Productos
{
    public partial class View_Tallas : Form
    {
        private TallasController tallasController;
        private Timer animTimer;
        private int animPaso = 0;
        private Size originalSize;
        private Point originalLocation;
        private Button currentButton;
        private const int scale = 10;

        private int idTallaSeleccionada = -1;

        public View_Tallas()
        {
            InitializeComponent();
            tallasController = new TallasController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            ConfigurarBoton(btn_agregar_talla, Properties.Resources.icons8_add_25, Properties.Resources.icons8_add_25_2);
            ConfigurarBoton(btn_modificar_talla, Properties.Resources.icons8_edit_25_2, Properties.Resources.icons8_edit_25);
            ConfigurarBoton(btn_eliminar_talla, Properties.Resources.icons8_delete_25, Properties.Resources.icons8_delete_25_2);
        }

        private async void View_Tallas_Load(object sender, EventArgs e)
        {
            await CargarTallasEnGridAsync();
            CambiarEstadoBotones(false);
        }

        private void ConfigurarBoton(Button boton, Image imgNormal, Image imgHover)
        {
            boton.MouseEnter += (s, e) => boton.Image = imgHover;
            boton.MouseLeave += (s, e) => boton.Image = imgNormal;
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

        private void AnimarBoton(Button boton)
        {
            if (animTimer.Enabled) return;

            currentButton = boton;
            originalSize = boton.Size;
            originalLocation = boton.Location;
            animPaso = 0;
            animTimer.Start();
        }

        private async Task CargarTallasEnGridAsync()
        {
            var lista = await tallasController.ObtenerTodasLasTallasAsync();
            dgv_tallas.DataSource = null;
            dgv_tallas.DataSource = lista;

            if (dgv_tallas.Columns.Contains("id_talla"))
                dgv_tallas.Columns["id_talla"].Visible = false;

            if (dgv_tallas.Columns.Contains("estatus"))
                dgv_tallas.Columns["estatus"].Visible = false;

            dgv_tallas.Columns["nombre"].HeaderText = "Nombre";

            dgv_tallas.ReadOnly = true;
            dgv_tallas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_tallas.MultiSelect = false;
            dgv_tallas.AllowUserToAddRows = false;
            dgv_tallas.AllowUserToDeleteRows = false;
            dgv_tallas.AllowUserToResizeRows = false;
            dgv_tallas.AllowUserToResizeColumns = false;
            dgv_tallas.RowHeadersVisible = false;

            dgv_tallas.EnableHeadersVisualStyles = false;
            dgv_tallas.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_tallas.ColumnHeadersDefaultCellStyle.BackColor;
            dgv_tallas.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv_tallas.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv_tallas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_tallas.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_tallas.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
        }

        private void dgv_tallas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            CambiarEstadoBotones(true);

            var fila = dgv_tallas.Rows[e.RowIndex];
            idTallaSeleccionada = Convert.ToInt32(fila.Cells["id_talla"].Value);
            txt_nombre_talla.Text = fila.Cells["nombre"].Value.ToString();
            txt_nombre_talla.Focus();
        }

        private async void btn_agregar_talla_Click(object sender, EventArgs e)
        {
            await Agregar();
        }

        public async Task Agregar()
        {
            AnimarBoton(btn_agregar_talla);

            string nombre = txt_nombre_talla.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre para la talla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await tallasController.InsertarTallaAsync(nombre);
                MessageBox.Show("Talla agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarTallasEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_modificar_talla_Click(object sender, EventArgs e)
        {
            await Modificar();
        }

        public async Task Modificar()
        {
            AnimarBoton(btn_modificar_talla);

            if (idTallaSeleccionada == -1)
            {
                MessageBox.Show("Selecciona una talla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txt_nombre_talla.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await tallasController.ModificarTallaAsync(idTallaSeleccionada, nombre);
                MessageBox.Show("Talla modificada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarTallasEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_eliminar_talla_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_eliminar_talla);

            if (idTallaSeleccionada == -1)
            {
                MessageBox.Show("Selecciona una talla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar esta talla?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await tallasController.EliminarTallaAsync(idTallaSeleccionada);
                    MessageBox.Show("Talla eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarTallasEnGridAsync();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarFormulario()
        {
            txt_nombre_talla.Clear();
            idTallaSeleccionada = -1;
            CambiarEstadoBotones(false);
            txt_nombre_talla.Focus();
        }

        private void CambiarEstadoBotones(bool edicion)
        {
            btn_modificar_talla.Visible = edicion;
            btn_eliminar_talla.Visible = edicion;
            btn_agregar_talla.Visible = !edicion;

            btn_cancelar.Visible = edicion;
            btn_salir.Visible = !edicion;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_tallas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (btn_cancelar.Visible)
                    LimpiarFormulario();
                else
                    this.Close();
            }
        }

        private async void txt_nombre_talla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter
            {
                if (btn_agregar_talla.Visible)
                    await Agregar();
                else
                    await Modificar();

                e.Handled = true;
            }
        }

        private void View_Tallas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (btn_cancelar.Visible)
                    LimpiarFormulario();
                else
                    this.Close();
            }
        }
    }
}
