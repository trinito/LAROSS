using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Inventario
{
    public partial class View_Stock : Form
    {
        private string codigoProducto;
        private int stockProducto;
        private string nombreProducto;
        public bool result = false;
        private LoadingControl loadingOverlay;

        ProductosController productosController;
        public View_Stock(string nombre, string codigo, int stock)
        {
            InitializeComponent();
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            codigoProducto = codigo;
            stockProducto = stock;
            nombreProducto = nombre;
            productosController = new ProductosController();
            txt_stock_actual.Text = stockProducto.ToString();

        }

        private void View_Stock_Load(object sender, EventArgs e)
        {

        }

        private async Task AgregarStock()
        {
            try
            {
                string codigo = codigoProducto;


                // Obtener valores de stock actual y cantidad a agregar
                if (!int.TryParse(txt_stock_actual.Text.Trim(), out int stockActual))
                {
                    MessageBox.Show("El stock actual no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txt_agregar.Text.Trim(), out int stockAgregar) || stockAgregar <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida para agregar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_agregar.Focus();
                    return;
                }

                loadingOverlay.ShowOverlay();

                int nuevoStock = stockActual + stockAgregar;

                bool actualizado = await productosController.AgregarStockAsync(codigo, stockAgregar);

                if (actualizado)
                {
                    loadingOverlay.HideOverlay();
                    result = true;
                    MessageBox.Show("Stock actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ImprimirCodigosDeBarras.ImprimirCodigoAsync(nombreProducto, codigoProducto, stockProducto);
                    this.Close();
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("No se encontró el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
                MessageBox.Show("Error al actualizar stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private async void btn_aceptar_Click(object sender, EventArgs e)
        {
            await AgregarStock();
        }

        private void txt_agregar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_agregar.Text))
                txt_stock_total.Text = "";
            else
                txt_stock_total.Text = (stockProducto + Convert.ToInt32(txt_agregar.Text)).ToString();
        }

        private void txt_agregar_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite solo dígitos y control (como Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void View_Stock_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    await AgregarStock();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;

                default:
                    break;
            }
        }
    }
}
