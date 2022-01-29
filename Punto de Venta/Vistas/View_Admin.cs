using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Punto_de_Venta.Modelo.Menu;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Admin : Form
    {
        List<Menu> productos;
        bool isEdit = false;
        public View_Admin()
        {
            InitializeComponent();
            productos = new List<Menu>();
        }

        private void View_Admin_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            LlenarProductos();
            LlenarCategorias();
        }


        private void LlenarProductos()
        {
            ProductosController productosController = new ProductosController(new chinahousedbEntities());
            productos = productosController.GetProductos().ToList();
            dgv_productos.DataSource = productos;
        }

        private void LlenarCategorias()
        {
            CategoriaController categoriaController = new CategoriaController(new chinahousedbEntities());
            combo_categoria.DataSource = categoriaController.GetCategorias();
        }

        private void combo_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = combo_categoria.SelectedValue.ToString();
        }

        private void button_editar_Click(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow != null)
            {
                Menu productoSelect = new Menu();
                productoSelect = (Menu)dgv_productos.CurrentRow.DataBoundItem;
                txt_codigo.Text = productoSelect.codigo;
                txt_nombre.Text= productoSelect.nombre;
                txt_precio.Text = productoSelect.precio.ToString();
                combo_medida.SelectedItem = productoSelect.medida;
                combo_categoria.SelectedValue = productoSelect.id_categoria;
                txt_id.Text = productoSelect.id_menu.ToString();
                isEdit = true;
            }
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow != null)
            {
                string message = "¿Estás seguro de eliminar este producto?";
                string title = "¡ALERTA!";

                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Menu producto = (Menu)dgv_productos.CurrentRow.DataBoundItem;
                    ProductosController productosController = new ProductosController(new chinahousedbEntities());
                    if (productosController.DeleteProducto(producto) > 0)
                    {
                        dgv_productos.DataSource = null;
                        productos.Remove(producto);
                        dgv_productos.DataSource = productos;
                    }
                    else
                    {
                        string message2 = "No se pudo eliminar el producto";
                        string title2 = "Error";
                        MessageBox.Show(message2, title2);

                    }

                  
                }
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_codigo.Text) || string.IsNullOrEmpty(txt_nombre.Text) || string.IsNullOrEmpty(txt_precio.Text)|| string.IsNullOrEmpty(combo_medida.Text) || string.IsNullOrEmpty(combo_categoria.Text))
            {
                string message = "Favor de llenar todos los campos";
                string title = "Mensaje";
                MessageBox.Show(message, title);
            }
            else
            {
                Menu menu = new Menu();
                if(isEdit)
                    menu.id_menu = Convert.ToInt32(txt_id.Text);

                menu.codigo = txt_codigo.Text;
                menu.nombre = txt_nombre.Text;
                menu.precio = Convert.ToDecimal(txt_precio.Text);
                menu.foto = null;
                menu.estatus = true;
                menu.medida = combo_medida.Text;
                menu.id_categoria = Convert.ToInt32(combo_categoria.SelectedValue);
                ProductosController productosController = new ProductosController(new chinahousedbEntities());
                if (productos.Exists(x => x.codigo == menu.codigo))
                {
                    string message = "La clave del producto ya existe, favor de ingresar una clave diferente.";
                    string title = "Mensaje";
                    MessageBox.Show(message, title);
                    return;
                }
                if (isEdit)
                {
                    int result = productosController.UpdateProducto(menu);
                    if(result >0)
                    {
                        Menu producto = productos.FirstOrDefault(x => x.id_menu.Equals(menu.id_menu));
                        producto.codigo = menu.codigo;
                        producto.id_categoria = menu.id_categoria;
                        producto.medida = menu.medida;
                        producto.nombre = menu.nombre;
                        producto.precio = menu.precio;
                        dgv_productos.DataSource = null;
                        dgv_productos.DataSource = productos;
                    }
                    else
                    {
                        string message = "No se pudo actualizar el producto.";
                        string title = "ERROR";
                        MessageBox.Show(message, title);
                    }
                }
                else
                {
                    int result = productosController.InsertProducto(menu);
                    if(result>0)
                    {
                        menu.id_menu = result;
                        productos.Add(menu);
                        dgv_productos.DataSource = null;
                        dgv_productos.DataSource = productos;
                        int row = dgv_productos.Rows.Count-1;
                        dgv_productos.Rows[row].Selected = true;
                        dgv_productos.Rows[row].Cells[0].Selected = true;
                    }
                    else
                    {
                        string message = "No se pudo agregar el nuevo producto.";
                        string title = "ERROR";
                        MessageBox.Show(message, title);
                    }
                }
                Limpiar();
            }
        }

        private void Limpiar()
        {
            txt_codigo.Text = "";
            txt_nombre.Text = "";
            txt_precio.Text = "";
            txt_id.Text = "";
            isEdit = false;
            txt_codigo.Focus();
        }

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void corteDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            View_Corte form = new View_Corte();
            form.ShowDialog();
            this.Show();
        }
    }
}
