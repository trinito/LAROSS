using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Productos
{
    public partial class View_Categorias : Form
    {
        private CategoriasController categoriasController;

        private Timer animTimer;
        private int animPaso = 0;
        private Size originalSize;
        private Point originalLocation;
        private Button currentButton;
        private const int scale = 10;

        public View_Categorias()
        {
            InitializeComponent();
            categoriasController = new CategoriasController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            ConfigurarBoton(btn_agregar_categoria, Properties.Resources.icons8_add_25, Properties.Resources.icons8_add_25_2);
            ConfigurarBoton(btn_modificar_categoria, Properties.Resources.icons8_edit_25_2, Properties.Resources.icons8_edit_25);
            ConfigurarBoton(btn_eliminar_categoria, Properties.Resources.icons8_delete_25, Properties.Resources.icons8_delete_25_2);
        }

        private async void View_Categorias_Load(object sender, EventArgs e)
        {
            await CargarCategoriasEnGridAsync();
            CambiarEstadoBotones(false);
        }

        private void ConfigurarBoton(Button boton, Image imgNormal, Image imgHover)
        {
            boton.MouseEnter += (s, e) => { boton.Image = imgHover; };
            boton.MouseLeave += (s, e) => { boton.Image = imgNormal; };
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

        private async Task CargarCategoriasEnGridAsync()
        {
            var lista = await categoriasController.ObtenerTodasLasCategoriasAsync();
            dgv_categorias.DataSource = null;
            dgv_categorias.DataSource = lista;

            if (dgv_categorias.Columns.Contains("id_categoria"))
                dgv_categorias.Columns["id_categoria"].Visible = false;

            if (dgv_categorias.Columns.Contains("estatus"))
                dgv_categorias.Columns["estatus"].Visible = false;

            dgv_categorias.Columns["nombre"].HeaderText = "Nombre";

            dgv_categorias.ReadOnly = true;
            dgv_categorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_categorias.MultiSelect = false;
            dgv_categorias.AllowUserToAddRows = false;
            dgv_categorias.AllowUserToDeleteRows = false;
            dgv_categorias.AllowUserToResizeRows = false;
            dgv_categorias.AllowUserToResizeColumns = false;
            dgv_categorias.RowHeadersVisible = false;

            dgv_categorias.EnableHeadersVisualStyles = false;
            dgv_categorias.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_categorias.ColumnHeadersDefaultCellStyle.BackColor;
            dgv_categorias.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv_categorias.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv_categorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_categorias.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_categorias.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
        }

        private void dgv_categorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            CambiarEstadoBotones(true);

            var fila = dgv_categorias.Rows[e.RowIndex];
            txt_nombre_categoria.Tag = fila.Cells["id_categoria"].Value;
            txt_nombre_categoria.Text = fila.Cells["nombre"].Value.ToString();
        }

        private async void btn_agregar_categoria_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_agregar_categoria);

            string nombre = txt_nombre_categoria.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre para la categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await categoriasController.InsertarCategoriaAsync(nombre);
                MessageBox.Show("Categoría agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                await CargarCategoriasEnGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_modificar_categoria_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_modificar_categoria);

            if (string.IsNullOrWhiteSpace(txt_nombre_categoria.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = (int)txt_nombre_categoria.Tag;
                string nuevoNombre = txt_nombre_categoria.Text.Trim();

                await categoriasController.ModificarCategoriaAsync(id, nuevoNombre);
                MessageBox.Show("Categoría modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                await CargarCategoriasEnGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_eliminar_categoria_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_eliminar_categoria);

            try
            {
                int id = (int)txt_nombre_categoria.Tag;

                var confirm = MessageBox.Show("¿Estás seguro que deseas eliminar esta categoría?",
                                              "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    await categoriasController.EliminarCategoriaAsync(id);
                    MessageBox.Show("Categoría eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarCategoriasEnGridAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txt_nombre_categoria.Clear();
            txt_nombre_categoria.Tag = null;
            CambiarEstadoBotones(false);
            txt_nombre_categoria.Focus();
        }

        private void CambiarEstadoBotones(bool editarVisible)
        {
            btn_modificar_categoria.Visible = editarVisible;
            btn_eliminar_categoria.Visible = editarVisible;
            btn_cancelar.Visible = editarVisible;
            btn_agregar_categoria.Visible = !editarVisible;
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
