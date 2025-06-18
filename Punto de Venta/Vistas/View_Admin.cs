using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Admin : Form
    {
        private List<Articulos> productosOriginal;
        private readonly BindingSource bindingSource;
        private bool isEdit = false;
        private string codigo_aux = "";
        public bool LogoutSolicitado { get; private set; } = false;

        public View_Admin()
        {
            InitializeComponent();

            productosOriginal = new List<Articulos>();
            bindingSource = new BindingSource();
            dgv_productos.DoubleBuffered(true);

            ConfigurarDataGridView();
            dgv_productos.DataSource = bindingSource;
        }

        private async void View_Admin_Load(object sender, EventArgs e)
        {
            await LlenarProductos();
            LlenarCategorias();
        }

        private void ConfigurarDataGridView()
        {
            dgv_productos.Columns.Clear();
            dgv_productos.AutoGenerateColumns = false;

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "codigo",
                HeaderText = "Código",
                DataPropertyName = "codigo",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nombre",
                HeaderText = "Nombre",
                DataPropertyName = "nombre",
                Width = 200
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "medida",
                HeaderText = "Medida",
                DataPropertyName = "medida",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "precio",
                HeaderText = "Precio",
                DataPropertyName = "precio",
                Width = 80,
                DefaultCellStyle = { Format = "C2" }
            });

            dgv_productos.Columns["codigo"].Frozen = true;
        }

        private async Task LlenarProductos()
        {
            var controller = new ProductosController();
            productosOriginal = (await controller.GetProductosAsync()).ToList();
            bindingSource.DataSource = productosOriginal;
        }

        private void LlenarCategorias()
        {
            CategoriaController categoriaController = new CategoriaController();
            combo_categoria.DataSource = categoriaController.GetCategorias();
        }

        private void txt_producto_TextChanged(object sender, EventArgs e)
        {
            var filtro = txt_producto.Text.Trim().ToUpper();

            var filtrados = string.IsNullOrEmpty(filtro)
                ? productosOriginal
                : productosOriginal.Where(p => p.nombre.ToUpper().Contains(filtro)).ToList();

            bindingSource.DataSource = filtrados;
        }

        private void button_editar_Click(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow == null) return;

            var producto = (Articulos)dgv_productos.CurrentRow.DataBoundItem;
            txt_codigo.Text = producto.codigo_barras;
            codigo_aux = producto.codigo_barras;
            txt_nombre.Text = producto.nombre;
            txt_precio.Text = producto.precio_venta.ToString();
            combo_medida.SelectedItem = producto.id_categoria;
            combo_categoria.SelectedValue = producto.id_categoria;
            txt_id.Text = producto.id_producto.ToString();

            isEdit = true;
            btn_guardar.Text = "Actualizar";
            dgv_productos.Enabled = false;
            dgv_productos.EnableHeadersVisualStyles = false;
            lbl_modificando.Visible = true;
            btn_cancelar.Visible = true;
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow == null) return;

            var producto = (Articulos)dgv_productos.CurrentRow.DataBoundItem;
            var controller = new ProductosController();

            if (controller.DeleteProducto(producto.codigo_barras))
            {
                productosOriginal.Remove(producto);
                bindingSource.DataSource = productosOriginal.ToList();
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto", "Error");
            }
        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_codigo.Text) ||
                string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_precio.Text) ||
                string.IsNullOrWhiteSpace(combo_medida.Text) ||
                string.IsNullOrWhiteSpace(combo_categoria.Text))
            {
                MessageBox.Show("Favor de llenar todos los campos", "Mensaje");
                return;
            }

            var articulo = new Articulos
            {
                codigo_barras = txt_codigo.Text,
                nombre = txt_nombre.Text,
                precio_venta = Convert.ToDecimal(txt_precio.Text),
               // Categorias = combo_medida.Text,
                id_categoria = Convert.ToInt32(combo_categoria.SelectedValue),
                estatus = true,
                foto = null
            };

            if (isEdit)
                articulo.id_producto = Convert.ToInt32(txt_id.Text);

            var controller = new ProductosController();

            if (productosOriginal.Exists(x => x.codigo_barras == articulo.codigo_barras && articulo.codigo_barras != codigo_aux))
            {
                MessageBox.Show("La clave del producto ya existe, favor de ingresar una diferente.", "Mensaje");
                return;
            }

            if (isEdit)
            {
                if (controller.UpdateProducto(articulo))
                {
                    var p = productosOriginal.FirstOrDefault(x => x.id_producto == articulo.id_producto);
                    if (p != null)
                    {
                        p.codigo_barras = articulo.codigo_barras;
                        p.nombre = articulo.nombre;
                        p.precio_venta = articulo.precio_venta;
                        p.id_categoria = articulo.id_categoria;
                        await LlenarProductos();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el producto.", "ERROR");
                }
            }
            else
            {
                try
                {
                    int result = controller.InsertProducto(articulo);
                    if (result > 0)
                    {
                        articulo.id_producto = result;
                        productosOriginal.Add(articulo);
                        bindingSource.DataSource = productosOriginal.ToList();

                        var row = dgv_productos.Rows.Count - 1;
                        dgv_productos.Rows[row].Selected = true;
                        dgv_productos.Rows[row].Cells[0].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el nuevo producto.", "ERROR");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }

            Limpiar();
        }

        private void Limpiar()
        {
            txt_codigo.Clear();
            txt_nombre.Clear();
            txt_precio.Clear();
            txt_id.Clear();
            combo_medida.SelectedItem = null;
            combo_categoria.SelectedIndex = 0;

            isEdit = false;
            codigo_aux = "";
            btn_guardar.Text = "Guardar Nuevo Producto";
            dgv_productos.Enabled = true;
            dgv_productos.EnableHeadersVisualStyles = true;
            lbl_modificando.Visible = false;
            btn_cancelar.Visible = false;
            txt_codigo.Focus();
        }

        private void btn_cancelar_Click(object sender, EventArgs e) => Limpiar();

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == '.' && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void corteDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new View_Corte().ShowDialog();
            Show();
        }

        private void ventasPorSemanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new View_VentasMes().ShowDialog();
            Show();
        }
    }

}
