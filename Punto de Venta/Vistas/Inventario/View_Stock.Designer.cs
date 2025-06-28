namespace Punto_de_Venta.Vistas.Inventario
{
    partial class View_Stock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_no = new System.Windows.Forms.RadioButton();
            this.radio_si = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.txt_agregar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.txt_stock_actual = new System.Windows.Forms.TextBox();
            this.txt_stock_total = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.radio_no);
            this.groupBox1.Controls.Add(this.radio_si);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 73);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "¿Desea imprimir los tickets de código de barra a ingresar?";
            // 
            // radio_no
            // 
            this.radio_no.AutoSize = true;
            this.radio_no.Location = new System.Drawing.Point(404, 36);
            this.radio_no.Name = "radio_no";
            this.radio_no.Size = new System.Drawing.Size(61, 31);
            this.radio_no.TabIndex = 1;
            this.radio_no.Text = "No";
            this.radio_no.UseVisualStyleBackColor = true;
            // 
            // radio_si
            // 
            this.radio_si.AutoSize = true;
            this.radio_si.Checked = true;
            this.radio_si.Location = new System.Drawing.Point(327, 35);
            this.radio_si.Name = "radio_si";
            this.radio_si.Size = new System.Drawing.Size(50, 31);
            this.radio_si.TabIndex = 0;
            this.radio_si.TabStop = true;
            this.radio_si.Text = "Si";
            this.radio_si.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(57)))), ((int)(((byte)(93)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 112);
            this.panel1.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(361, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Agregar stock";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Punto_de_Venta.Properties.Resources.laross_pi;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(344, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(21)))), ((int)(((byte)(34)))));
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(53)))), ((int)(((byte)(46)))));
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_cancelar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_cancelar.Location = new System.Drawing.Point(367, 368);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(136, 57);
            this.btn_cancelar.TabIndex = 53;
            this.btn_cancelar.Text = "Cancelar (ESC)";
            this.btn_cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // txt_agregar
            // 
            this.txt_agregar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_agregar.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_agregar.Location = new System.Drawing.Point(216, 340);
            this.txt_agregar.MaxLength = 5;
            this.txt_agregar.Name = "txt_agregar";
            this.txt_agregar.Size = new System.Drawing.Size(121, 39);
            this.txt_agregar.TabIndex = 45;
            this.txt_agregar.TextChanged += new System.EventHandler(this.txt_agregar_TextChanged);
            this.txt_agregar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_agregar_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.label3.Location = new System.Drawing.Point(23, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 33);
            this.label3.TabIndex = 50;
            this.label3.Text = "Agregar:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.label10.Location = new System.Drawing.Point(23, 288);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(187, 33);
            this.label10.TabIndex = 49;
            this.label10.Text = "Stock actual:";
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(59)))), ((int)(((byte)(97)))));
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(367, 286);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(136, 57);
            this.btn_aceptar.TabIndex = 48;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_aceptar.UseVisualStyleBackColor = false;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // txt_stock_actual
            // 
            this.txt_stock_actual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_stock_actual.Enabled = false;
            this.txt_stock_actual.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_stock_actual.Location = new System.Drawing.Point(216, 286);
            this.txt_stock_actual.MaxLength = 5;
            this.txt_stock_actual.Name = "txt_stock_actual";
            this.txt_stock_actual.ReadOnly = true;
            this.txt_stock_actual.Size = new System.Drawing.Size(121, 39);
            this.txt_stock_actual.TabIndex = 47;
            // 
            // txt_stock_total
            // 
            this.txt_stock_total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_stock_total.Enabled = false;
            this.txt_stock_total.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_stock_total.Location = new System.Drawing.Point(216, 390);
            this.txt_stock_total.MaxLength = 5;
            this.txt_stock_total.Name = "txt_stock_total";
            this.txt_stock_total.ReadOnly = true;
            this.txt_stock_total.Size = new System.Drawing.Size(121, 39);
            this.txt_stock_total.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.label4.Location = new System.Drawing.Point(23, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 33);
            this.label4.TabIndex = 51;
            this.label4.Text = "Stock total";
            // 
            // View_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 467);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.txt_stock_total);
            this.Controls.Add(this.txt_agregar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.txt_stock_actual);
            this.KeyPreview = true;
            this.Name = "View_Stock";
            this.Text = "Agregar stock";
            this.Load += new System.EventHandler(this.View_Stock_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_Stock_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_no;
        private System.Windows.Forms.RadioButton radio_si;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.TextBox txt_agregar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.TextBox txt_stock_actual;
        private System.Windows.Forms.TextBox txt_stock_total;
        private System.Windows.Forms.Label label4;
    }
}