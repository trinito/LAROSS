namespace Punto_de_Venta.Vistas
{
    partial class UserControl_Historial
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Código = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_stock = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbl_ventas = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgv_ventas = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button_copia = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.Cantidad.FillWeight = 50F;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Medida
            // 
            this.Medida.DataPropertyName = "medida";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Medida.DefaultCellStyle = dataGridViewCellStyle2;
            this.Medida.FillWeight = 50F;
            this.Medida.HeaderText = "Medida";
            this.Medida.Name = "Medida";
            this.Medida.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "nombre";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nombre.DefaultCellStyle = dataGridViewCellStyle3;
            this.Nombre.FillWeight = 160F;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Código
            // 
            this.Código.DataPropertyName = "codigo";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Código.DefaultCellStyle = dataGridViewCellStyle4;
            this.Código.FillWeight = 45F;
            this.Código.HeaderText = "Código";
            this.Código.Name = "Código";
            this.Código.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "precio";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle5;
            this.Precio.FillWeight = 50F;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 602);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1000, 100);
            this.panel4.TabIndex = 20;
            // 
            // lbl_stock
            // 
            this.lbl_stock.AutoSize = true;
            this.lbl_stock.Font = new System.Drawing.Font("Rockwell", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.lbl_stock.Location = new System.Drawing.Point(424, 37);
            this.lbl_stock.Name = "lbl_stock";
            this.lbl_stock.Size = new System.Drawing.Size(0, 36);
            this.lbl_stock.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(195)))), ((int)(((byte)(214)))));
            this.panel6.Controls.Add(this.lbl_ventas);
            this.panel6.Controls.Add(this.lbl_stock);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 502);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1000, 100);
            this.panel6.TabIndex = 21;
            // 
            // lbl_ventas
            // 
            this.lbl_ventas.AutoSize = true;
            this.lbl_ventas.Font = new System.Drawing.Font("Rockwell", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ventas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.lbl_ventas.Location = new System.Drawing.Point(377, 37);
            this.lbl_ventas.Name = "lbl_ventas";
            this.lbl_ventas.Size = new System.Drawing.Size(0, 36);
            this.lbl_ventas.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Rockwell", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.label9.Location = new System.Drawing.Point(12, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(370, 36);
            this.label9.TabIndex = 2;
            this.label9.Text = "Total de ventas en el día:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 702);
            this.panel1.TabIndex = 26;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgv_ventas);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1000, 580);
            this.panel3.TabIndex = 19;
            // 
            // dgv_ventas
            // 
            this.dgv_ventas.AllowUserToAddRows = false;
            this.dgv_ventas.AllowUserToDeleteRows = false;
            this.dgv_ventas.AllowUserToResizeColumns = false;
            this.dgv_ventas.AllowUserToResizeRows = false;
            this.dgv_ventas.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_ventas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_ventas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ventas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ventas.Location = new System.Drawing.Point(0, 0);
            this.dgv_ventas.MultiSelect = false;
            this.dgv_ventas.Name = "dgv_ventas";
            this.dgv_ventas.ReadOnly = true;
            this.dgv_ventas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ventas.Size = new System.Drawing.Size(1000, 580);
            this.dgv_ventas.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.dateTimePicker1);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1000, 122);
            this.panel5.TabIndex = 18;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(9, 73);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 33);
            this.label3.TabIndex = 14;
            this.label3.Text = "Historial de ventas";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.button_copia);
            this.panel8.Controls.Add(this.btn_cancelar);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 231);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(246, 406);
            this.panel8.TabIndex = 16;
            // 
            // button_copia
            // 
            this.button_copia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(59)))), ((int)(((byte)(97)))));
            this.button_copia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_copia.Enabled = false;
            this.button_copia.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_copia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_copia.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_copia.ForeColor = System.Drawing.Color.White;
            this.button_copia.Image = global::Punto_de_Venta.Properties.Resources.icons8_print_25__1_;
            this.button_copia.Location = new System.Drawing.Point(44, 139);
            this.button_copia.Name = "button_copia";
            this.button_copia.Size = new System.Drawing.Size(168, 78);
            this.button_copia.TabIndex = 21;
            this.button_copia.Text = "Imprimir copia de Ticket";
            this.button_copia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_copia.UseVisualStyleBackColor = false;
            this.button_copia.Visible = false;
            this.button_copia.Click += new System.EventHandler(this.button_copia_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(59)))), ((int)(((byte)(97)))));
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatAppearance.BorderSize = 0;
            this.btn_cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Image = global::Punto_de_Venta.Properties.Resources.icons8_delete_30;
            this.btn_cancelar.Location = new System.Drawing.Point(53, 26);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(144, 72);
            this.btn_cancelar.TabIndex = 13;
            this.btn_cancelar.Text = "Cancelar venta";
            this.btn_cancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1000, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 702);
            this.panel2.TabIndex = 25;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Punto_de_Venta.Properties.Resources._281850184_119981597370118_8427633085628640968_n1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(246, 231);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "precio";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Precio";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "codigo";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.FillWeight = 45F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Código";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "nombre";
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn3.FillWeight = 160F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "medida";
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.FillWeight = 50F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Medida";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "cantidad";
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn5.FillWeight = 50F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Cantidad";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // UserControl_Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "UserControl_Historial";
            this.Size = new System.Drawing.Size(1248, 702);
            this.Load += new System.EventHandler(this.UserControl_Historial_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Código;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbl_stock;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgv_ventas;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label lbl_ventas;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button_copia;
    }
}
