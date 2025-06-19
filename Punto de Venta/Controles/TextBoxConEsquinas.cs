using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Punto_de_Venta.Controles
{
    public class TextBoxConEsquinas : UserControl
    {
        private TextBox txt = new TextBox();
        private int _radio = 15;

        [Category("Diseño")]
        public int Radio
        {
            get => _radio;
            set { _radio = value; this.Invalidate(); }
        }

        [Category("Diseño")]
        public Color BordeColor { get; set; } = Color.Gray;

        [Category("Diseño")]
        public Color FondoColor { get; set; } = Color.White;

        public override string Text
        {
            get => txt.Text;
            set => txt.Text = value;
        }

        public TextBoxConEsquinas()
        {
            this.DoubleBuffered = true;
            txt.BorderStyle = BorderStyle.None;
            txt.BackColor = FondoColor;
            txt.Location = new Point(10, 6);
            txt.Width = this.Width - 20;
            txt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.Controls.Add(txt);
            this.BackColor = Color.Transparent;
            this.Size = new Size(200, 30);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(BordeColor, 1.5f))
            using (SolidBrush brush = new SolidBrush(FondoColor))
            {
                Rectangle rect = this.ClientRectangle;
                rect.Inflate(-1, -1);
                int r = Radio;

                path.AddArc(rect.X, rect.Y, r, r, 180, 90);
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
                path.CloseFigure();

                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            txt.Width = this.Width - 20;
        }
    }
}
