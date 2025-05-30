using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Controles
{
    public partial class LoadingControl : UserControl
    {

        public LoadingControl()
        {
            InitializeComponent();
            this.Load += (s, e) =>
            {
                if (this.Parent != null)
                {
                    this.Location = new Point(
                        (this.Parent.ClientSize.Width - this.Width) / 2,
                        (this.Parent.ClientSize.Height - this.Height) / 2
                    );
                }
            };
            this.Visible = false; 
        }

        public void ShowOverlay()
        {
            this.Visible = true;
            this.BringToFront();
        }

        public void HideOverlay()
        {
            this.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
