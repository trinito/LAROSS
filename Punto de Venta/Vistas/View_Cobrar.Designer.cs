namespace Punto_de_Venta.Vistas
{
    partial class View_Cobrar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_tarjeta = new System.Windows.Forms.RadioButton();
            this.radio_efectivo = new System.Windows.Forms.RadioButton();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.btn_cobrar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_pago = new System.Windows.Forms.TextBox();
            this.txt_cambio = new System.Windows.Forms.TextBox();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 102);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(335, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cobrar venta";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Punto_de_Venta.Properties.Resources.LOGOCHINAHOUSE;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Pristina", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(109, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "CHINA HOUSE";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_tarjeta);
            this.groupBox1.Controls.Add(this.radio_efectivo);
            this.groupBox1.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(90, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forma de pago";
            // 
            // radio_tarjeta
            // 
            this.radio_tarjeta.AutoSize = true;
            this.radio_tarjeta.Location = new System.Drawing.Point(167, 34);
            this.radio_tarjeta.Name = "radio_tarjeta";
            this.radio_tarjeta.Size = new System.Drawing.Size(108, 31);
            this.radio_tarjeta.TabIndex = 1;
            this.radio_tarjeta.Text = "Tarjeta";
            this.radio_tarjeta.UseVisualStyleBackColor = true;
            this.radio_tarjeta.CheckedChanged += new System.EventHandler(this.radio_tarjeta_CheckedChanged);
            // 
            // radio_efectivo
            // 
            this.radio_efectivo.AutoSize = true;
            this.radio_efectivo.Checked = true;
            this.radio_efectivo.Location = new System.Drawing.Point(27, 34);
            this.radio_efectivo.Name = "radio_efectivo";
            this.radio_efectivo.Size = new System.Drawing.Size(120, 31);
            this.radio_efectivo.TabIndex = 0;
            this.radio_efectivo.TabStop = true;
            this.radio_efectivo.Text = "Efectivo";
            this.radio_efectivo.UseVisualStyleBackColor = true;
            this.radio_efectivo.CheckedChanged += new System.EventHandler(this.radio_efectivo_CheckedChanged);
            // 
            // txt_total
            // 
            this.txt_total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_total.Enabled = false;
            this.txt_total.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.Location = new System.Drawing.Point(160, 265);
            this.txt_total.MaxLength = 5;
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(121, 39);
            this.txt_total.TabIndex = 36;
            // 
            // btn_cobrar
            // 
            this.btn_cobrar.BackColor = System.Drawing.Color.Black;
            this.btn_cobrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cobrar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_cobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cobrar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cobrar.ForeColor = System.Drawing.Color.White;
            this.btn_cobrar.Location = new System.Drawing.Point(329, 265);
            this.btn_cobrar.Name = "btn_cobrar";
            this.btn_cobrar.Size = new System.Drawing.Size(136, 57);
            this.btn_cobrar.TabIndex = 37;
            this.btn_cobrar.Text = "Cobrar (ENTER)";
            this.btn_cobrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cobrar.UseVisualStyleBackColor = false;
            this.btn_cobrar.Click += new System.EventHandler(this.btn_cobrar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(23, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 33);
            this.label10.TabIndex = 38;
            this.label10.Text = "Total:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 33);
            this.label3.TabIndex = 39;
            this.label3.Text = "Pago:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 33);
            this.label4.TabIndex = 40;
            this.label4.Text = "Cambio:";
            // 
            // txt_pago
            // 
            this.txt_pago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pago.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pago.Location = new System.Drawing.Point(160, 319);
            this.txt_pago.MaxLength = 5;
            this.txt_pago.Name = "txt_pago";
            this.txt_pago.Size = new System.Drawing.Size(121, 39);
            this.txt_pago.TabIndex = 1;
            this.txt_pago.TextChanged += new System.EventHandler(this.txt_pago_TextChanged);
            this.txt_pago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pago_KeyPress);
            // 
            // txt_cambio
            // 
            this.txt_cambio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cambio.Enabled = false;
            this.txt_cambio.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cambio.Location = new System.Drawing.Point(160, 369);
            this.txt_cambio.MaxLength = 5;
            this.txt_cambio.Name = "txt_cambio";
            this.txt_cambio.ReadOnly = true;
            this.txt_cambio.Size = new System.Drawing.Size(121, 39);
            this.txt_cambio.TabIndex = 42;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(43)))), ((int)(((byte)(33)))));
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(53)))), ((int)(((byte)(46)))));
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_cancelar.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_cancelar.Location = new System.Drawing.Point(329, 340);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(136, 57);
            this.btn_cancelar.TabIndex = 43;
            this.btn_cancelar.Text = "Cancelar (ESC)";
            this.btn_cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // View_Cobrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(492, 450);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.txt_cambio);
            this.Controls.Add(this.txt_pago);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn_cobrar);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "View_Cobrar";
            this.ShowIcon = false;
            this.Text = "Cobrar";
            this.Load += new System.EventHandler(this.View_Cobrar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_Cobrar_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_cobrar;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.RadioButton radio_tarjeta;
        private System.Windows.Forms.RadioButton radio_efectivo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_cambio;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.TextBox txt_pago;
    }
}