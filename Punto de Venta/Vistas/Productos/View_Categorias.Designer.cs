namespace Punto_de_Venta.Vistas.Productos
{
    partial class View_Categorias
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_eliminar_categoria = new System.Windows.Forms.Button();
            this.txt_nombre_categoria = new System.Windows.Forms.TextBox();
            this.dgv_categorias = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_agregar_categoria = new System.Windows.Forms.Button();
            this.btn_modificar_categoria = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_salir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_categorias)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel4.Controls.Add(this.btn_eliminar_categoria);
            this.panel4.Controls.Add(this.txt_nombre_categoria);
            this.panel4.Controls.Add(this.dgv_categorias);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btn_modificar_categoria);
            this.panel4.Controls.Add(this.btn_cancelar);
            this.panel4.Controls.Add(this.btn_salir);
            this.panel4.Controls.Add(this.btn_agregar_categoria);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 112);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(477, 338);
            this.panel4.TabIndex = 45;
            // 
            // btn_eliminar_categoria
            // 
            this.btn_eliminar_categoria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_eliminar_categoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_eliminar_categoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_eliminar_categoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eliminar_categoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_eliminar_categoria.Image = global::Punto_de_Venta.Properties.Resources.icons8_delete_25;
            this.btn_eliminar_categoria.Location = new System.Drawing.Point(347, 48);
            this.btn_eliminar_categoria.Name = "btn_eliminar_categoria";
            this.btn_eliminar_categoria.Size = new System.Drawing.Size(30, 30);
            this.btn_eliminar_categoria.TabIndex = 60;
            this.btn_eliminar_categoria.UseVisualStyleBackColor = true;
            this.btn_eliminar_categoria.Visible = false;
            this.btn_eliminar_categoria.Click += new System.EventHandler(this.btn_eliminar_categoria_Click);
            // 
            // txt_nombre_categoria
            // 
            this.txt_nombre_categoria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nombre_categoria.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombre_categoria.Location = new System.Drawing.Point(163, 51);
            this.txt_nombre_categoria.MaxLength = 50;
            this.txt_nombre_categoria.Name = "txt_nombre_categoria";
            this.txt_nombre_categoria.Size = new System.Drawing.Size(151, 26);
            this.txt_nombre_categoria.TabIndex = 45;
            this.txt_nombre_categoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nombre_categoria_KeyPress);
            // 
            // dgv_categorias
            // 
            this.dgv_categorias.AllowUserToAddRows = false;
            this.dgv_categorias.AllowUserToDeleteRows = false;
            this.dgv_categorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_categorias.Location = new System.Drawing.Point(82, 83);
            this.dgv_categorias.MultiSelect = false;
            this.dgv_categorias.Name = "dgv_categorias";
            this.dgv_categorias.ReadOnly = true;
            this.dgv_categorias.RowHeadersVisible = false;
            this.dgv_categorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_categorias.Size = new System.Drawing.Size(295, 159);
            this.dgv_categorias.TabIndex = 58;
            this.dgv_categorias.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_categorias_CellDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
            this.label3.Location = new System.Drawing.Point(78, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 19);
            this.label3.TabIndex = 50;
            this.label3.Text = "Nombre:";
            // 
            // btn_agregar_categoria
            // 
            this.btn_agregar_categoria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_agregar_categoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_agregar_categoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_agregar_categoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_agregar_categoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_agregar_categoria.Image = global::Punto_de_Venta.Properties.Resources.icons8_add_25;
            this.btn_agregar_categoria.Location = new System.Drawing.Point(320, 48);
            this.btn_agregar_categoria.Name = "btn_agregar_categoria";
            this.btn_agregar_categoria.Size = new System.Drawing.Size(30, 30);
            this.btn_agregar_categoria.TabIndex = 45;
            this.btn_agregar_categoria.UseVisualStyleBackColor = true;
            this.btn_agregar_categoria.Click += new System.EventHandler(this.btn_agregar_categoria_Click);
            // 
            // btn_modificar_categoria
            // 
            this.btn_modificar_categoria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_modificar_categoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_modificar_categoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_modificar_categoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_modificar_categoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_modificar_categoria.Image = global::Punto_de_Venta.Properties.Resources.icons8_edit_25_2;
            this.btn_modificar_categoria.Location = new System.Drawing.Point(320, 48);
            this.btn_modificar_categoria.Name = "btn_modificar_categoria";
            this.btn_modificar_categoria.Size = new System.Drawing.Size(30, 30);
            this.btn_modificar_categoria.TabIndex = 59;
            this.btn_modificar_categoria.UseVisualStyleBackColor = true;
            this.btn_modificar_categoria.Visible = false;
            this.btn_modificar_categoria.Click += new System.EventHandler(this.btn_modificar_categoria_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(21)))), ((int)(((byte)(34)))));
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatAppearance.BorderSize = 0;
            this.btn_cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(53)))), ((int)(((byte)(46)))));
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_cancelar.Location = new System.Drawing.Point(251, 248);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(126, 37);
            this.btn_cancelar.TabIndex = 61;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Visible = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_salir
            // 
            this.btn_salir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(21)))), ((int)(((byte)(34)))));
            this.btn_salir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_salir.FlatAppearance.BorderSize = 0;
            this.btn_salir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(53)))), ((int)(((byte)(46)))));
            this.btn_salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_salir.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_salir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_salir.Location = new System.Drawing.Point(251, 248);
            this.btn_salir.Name = "btn_salir";
            this.btn_salir.Size = new System.Drawing.Size(126, 37);
            this.btn_salir.TabIndex = 62;
            this.btn_salir.Text = "Salir";
            this.btn_salir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_salir.UseVisualStyleBackColor = false;
            this.btn_salir.Click += new System.EventHandler(this.btn_salir_Click);
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
            this.panel1.Size = new System.Drawing.Size(477, 112);
            this.panel1.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(293, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Alta de categoria";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Punto_de_Venta.Properties.Resources.laross_pi;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(287, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // View_Categorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 450);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "View_Categorias";
            this.Text = "Categorias";
            this.Load += new System.EventHandler(this.View_Categorias_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_Categorias_KeyDown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_categorias)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_salir;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_eliminar_categoria;
        private System.Windows.Forms.TextBox txt_nombre_categoria;
        private System.Windows.Forms.DataGridView dgv_categorias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_agregar_categoria;
        private System.Windows.Forms.Button btn_modificar_categoria;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}