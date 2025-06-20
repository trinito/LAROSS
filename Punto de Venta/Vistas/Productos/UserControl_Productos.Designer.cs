namespace Punto_de_Venta.Vistas
{
    partial class UserControl_Productos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_Productos));
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Código = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_quitar = new System.Windows.Forms.Button();
            this.button_buscar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.cb_color = new System.Windows.Forms.ComboBox();
            this.btn_color = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.pb_imagen = new System.Windows.Forms.PictureBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.cb_sexo = new System.Windows.Forms.ComboBox();
            this.btn_add_sexo = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btn_add_img = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cb_tallas = new System.Windows.Forms.ComboBox();
            this.btn_add_talla = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.cb_categoria = new System.Windows.Forms.ComboBox();
            this.btn_add_categoria = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.cb_marcas = new System.Windows.Forms.ComboBox();
            this.btn_add_marca = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_costo = new Punto_de_Venta.Controles.TextBoxConEsquinas();
            this.txt_nombre = new Punto_de_Venta.Controles.TextBoxConEsquinas();
            this.txt_venta = new Punto_de_Venta.Controles.TextBoxConEsquinas();
            this.txt_stock = new Punto_de_Venta.Controles.TextBoxConEsquinas();
            this.botonConEsquinas5 = new Punto_de_Venta.Controles.BotonConEsquinas();
            this.botonConEsquinas6 = new Punto_de_Venta.Controles.BotonConEsquinas();
            this.botonConEsquinas7 = new Punto_de_Venta.Controles.BotonConEsquinas();
            this.botonConEsquinas4 = new Punto_de_Venta.Controles.BotonConEsquinas();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_imagen)).BeginInit();
            this.panel14.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel7.SuspendLayout();
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(57)))), ((int)(((byte)(93)))));
            this.panel2.Controls.Add(this.botonConEsquinas4);
            this.panel2.Controls.Add(this.button_quitar);
            this.panel2.Controls.Add(this.button_buscar);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1004, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 811);
            this.panel2.TabIndex = 21;
            // 
            // button_quitar
            // 
            this.button_quitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(21)))), ((int)(((byte)(34)))));
            this.button_quitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_quitar.FlatAppearance.BorderSize = 0;
            this.button_quitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(53)))), ((int)(((byte)(46)))));
            this.button_quitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_quitar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_quitar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_quitar.Image = global::Punto_de_Venta.Properties.Resources.cruz_quitar_signo;
            this.button_quitar.Location = new System.Drawing.Point(73, 484);
            this.button_quitar.Name = "button_quitar";
            this.button_quitar.Size = new System.Drawing.Size(126, 70);
            this.button_quitar.TabIndex = 14;
            this.button_quitar.Text = "Eliminar(Supr)";
            this.button_quitar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_quitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_quitar.UseVisualStyleBackColor = false;
            // 
            // button_buscar
            // 
            this.button_buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(44)))), ((int)(((byte)(61)))));
            this.button_buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_buscar.FlatAppearance.BorderSize = 0;
            this.button_buscar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_buscar.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_buscar.ForeColor = System.Drawing.Color.White;
            this.button_buscar.Image = global::Punto_de_Venta.Properties.Resources.icons8_search_more_25;
            this.button_buscar.Location = new System.Drawing.Point(73, 339);
            this.button_buscar.Name = "button_buscar";
            this.button_buscar.Size = new System.Drawing.Size(124, 70);
            this.button_buscar.TabIndex = 13;
            this.button_buscar.Text = "Buscar(F1)";
            this.button_buscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_buscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_buscar.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Punto_de_Venta.Properties.Resources._281850184_119981597370118_8427633085628640968_n1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(248, 231);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(57)))), ((int)(((byte)(93)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 791);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1004, 20);
            this.panel4.TabIndex = 23;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1004, 66);
            this.panel3.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label5.Location = new System.Drawing.Point(517, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Código de barras";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label3.Location = new System.Drawing.Point(22, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 33);
            this.label3.TabIndex = 14;
            this.label3.Text = "Productos";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel1.Controls.Add(this.botonConEsquinas5);
            this.panel1.Controls.Add(this.botonConEsquinas6);
            this.panel1.Controls.Add(this.botonConEsquinas7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 691);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 100);
            this.panel1.TabIndex = 26;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 66);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1004, 625);
            this.panel5.TabIndex = 27;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel16, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel13, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel14, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel10, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel9, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel15, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel12, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel11, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1004, 625);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel16.Controls.Add(this.cb_color);
            this.panel16.Controls.Add(this.btn_color);
            this.panel16.Controls.Add(this.label12);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(3, 203);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(496, 419);
            this.panel16.TabIndex = 28;
            // 
            // cb_color
            // 
            this.cb_color.FormattingEnabled = true;
            this.cb_color.Location = new System.Drawing.Point(102, 7);
            this.cb_color.Name = "cb_color";
            this.cb_color.Size = new System.Drawing.Size(307, 21);
            this.cb_color.TabIndex = 6;
            // 
            // btn_color
            // 
            this.btn_color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_color.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_color.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_color.Image = global::Punto_de_Venta.Properties.Resources.icons8_add_25;
            this.btn_color.Location = new System.Drawing.Point(415, 3);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(30, 30);
            this.btn_color.TabIndex = 0;
            this.btn_color.TabStop = false;
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label12.Location = new System.Drawing.Point(9, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 19);
            this.label12.TabIndex = 0;
            this.label12.Text = "Color";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.pb_imagen);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(505, 163);
            this.panel13.Name = "panel13";
            this.tableLayoutPanel1.SetRowSpan(this.panel13, 2);
            this.panel13.Size = new System.Drawing.Size(496, 459);
            this.panel13.TabIndex = 27;
            // 
            // pb_imagen
            // 
            this.pb_imagen.Image = ((System.Drawing.Image)(resources.GetObject("pb_imagen.Image")));
            this.pb_imagen.Location = new System.Drawing.Point(11, 11);
            this.pb_imagen.Name = "pb_imagen";
            this.pb_imagen.Size = new System.Drawing.Size(350, 350);
            this.pb_imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_imagen.TabIndex = 0;
            this.pb_imagen.TabStop = false;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.panel14.Controls.Add(this.cb_sexo);
            this.panel14.Controls.Add(this.btn_add_sexo);
            this.panel14.Controls.Add(this.label9);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(3, 163);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(496, 34);
            this.panel14.TabIndex = 25;
            // 
            // cb_sexo
            // 
            this.cb_sexo.FormattingEnabled = true;
            this.cb_sexo.Location = new System.Drawing.Point(102, 7);
            this.cb_sexo.Name = "cb_sexo";
            this.cb_sexo.Size = new System.Drawing.Size(307, 21);
            this.cb_sexo.TabIndex = 5;
            // 
            // btn_add_sexo
            // 
            this.btn_add_sexo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_sexo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_sexo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_sexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_sexo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_add_sexo.Image = global::Punto_de_Venta.Properties.Resources.icons8_add_25;
            this.btn_add_sexo.Location = new System.Drawing.Point(415, 3);
            this.btn_add_sexo.Name = "btn_add_sexo";
            this.btn_add_sexo.Size = new System.Drawing.Size(30, 30);
            this.btn_add_sexo.TabIndex = 0;
            this.btn_add_sexo.TabStop = false;
            this.btn_add_sexo.UseVisualStyleBackColor = true;
            this.btn_add_sexo.Click += new System.EventHandler(this.btn_add_sexo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label9.Location = new System.Drawing.Point(9, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 19);
            this.label9.TabIndex = 0;
            this.label9.Text = "Género";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btn_add_img);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(505, 123);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(496, 34);
            this.panel10.TabIndex = 23;
            // 
            // btn_add_img
            // 
            this.btn_add_img.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_img.Image = global::Punto_de_Venta.Properties.Resources.icons8_image_25;
            this.btn_add_img.Location = new System.Drawing.Point(135, 3);
            this.btn_add_img.Name = "btn_add_img";
            this.btn_add_img.Size = new System.Drawing.Size(35, 30);
            this.btn_add_img.TabIndex = 10;
            this.btn_add_img.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_add_img.UseVisualStyleBackColor = false;
            this.btn_add_img.Click += new System.EventHandler(this.btn_add_img_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label4.Location = new System.Drawing.Point(7, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Adjuntar foto";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cb_tallas);
            this.panel6.Controls.Add(this.btn_add_talla);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 123);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(496, 34);
            this.panel6.TabIndex = 22;
            // 
            // cb_tallas
            // 
            this.cb_tallas.FormattingEnabled = true;
            this.cb_tallas.Location = new System.Drawing.Point(102, 7);
            this.cb_tallas.Name = "cb_tallas";
            this.cb_tallas.Size = new System.Drawing.Size(307, 21);
            this.cb_tallas.TabIndex = 4;
            // 
            // btn_add_talla
            // 
            this.btn_add_talla.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_talla.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_talla.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_talla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_talla.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_add_talla.Image = global::Punto_de_Venta.Properties.Resources.icons8_add_25;
            this.btn_add_talla.Location = new System.Drawing.Point(415, 2);
            this.btn_add_talla.Name = "btn_add_talla";
            this.btn_add_talla.Size = new System.Drawing.Size(30, 30);
            this.btn_add_talla.TabIndex = 0;
            this.btn_add_talla.TabStop = false;
            this.btn_add_talla.UseVisualStyleBackColor = true;
            this.btn_add_talla.Click += new System.EventHandler(this.btn_add_talla_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Talla";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.txt_costo);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(505, 43);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(496, 34);
            this.panel9.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label8.Location = new System.Drawing.Point(9, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 19);
            this.label8.TabIndex = 0;
            this.label8.Text = "Precio costo";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.cb_categoria);
            this.panel15.Controls.Add(this.btn_add_categoria);
            this.panel15.Controls.Add(this.label14);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(3, 83);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(496, 34);
            this.panel15.TabIndex = 18;
            // 
            // cb_categoria
            // 
            this.cb_categoria.FormattingEnabled = true;
            this.cb_categoria.Location = new System.Drawing.Point(102, 9);
            this.cb_categoria.Name = "cb_categoria";
            this.cb_categoria.Size = new System.Drawing.Size(307, 21);
            this.cb_categoria.TabIndex = 3;
            // 
            // btn_add_categoria
            // 
            this.btn_add_categoria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_categoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_categoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_categoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_categoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_add_categoria.Image = global::Punto_de_Venta.Properties.Resources.icons8_add_25;
            this.btn_add_categoria.Location = new System.Drawing.Point(415, 1);
            this.btn_add_categoria.Name = "btn_add_categoria";
            this.btn_add_categoria.Size = new System.Drawing.Size(30, 30);
            this.btn_add_categoria.TabIndex = 0;
            this.btn_add_categoria.TabStop = false;
            this.btn_add_categoria.UseVisualStyleBackColor = true;
            this.btn_add_categoria.Click += new System.EventHandler(this.btn_add_categoria_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label14.Location = new System.Drawing.Point(9, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 19);
            this.label14.TabIndex = 0;
            this.label14.Text = "Categoria";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.cb_marcas);
            this.panel12.Controls.Add(this.btn_add_marca);
            this.panel12.Controls.Add(this.label11);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(3, 43);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(496, 34);
            this.panel12.TabIndex = 14;
            // 
            // cb_marcas
            // 
            this.cb_marcas.FormattingEnabled = true;
            this.cb_marcas.Location = new System.Drawing.Point(102, 6);
            this.cb_marcas.Name = "cb_marcas";
            this.cb_marcas.Size = new System.Drawing.Size(307, 21);
            this.cb_marcas.TabIndex = 2;
            // 
            // btn_add_marca
            // 
            this.btn_add_marca.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_marca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_marca.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.btn_add_marca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_marca.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_add_marca.Image = global::Punto_de_Venta.Properties.Resources.icons8_add_25;
            this.btn_add_marca.Location = new System.Drawing.Point(415, 1);
            this.btn_add_marca.Name = "btn_add_marca";
            this.btn_add_marca.Size = new System.Drawing.Size(30, 30);
            this.btn_add_marca.TabIndex = 0;
            this.btn_add_marca.TabStop = false;
            this.btn_add_marca.UseVisualStyleBackColor = true;
            this.btn_add_marca.Click += new System.EventHandler(this.btn_add_marca_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label11.Location = new System.Drawing.Point(9, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 19);
            this.label11.TabIndex = 0;
            this.label11.Text = "Marca";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txt_nombre);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(496, 34);
            this.panel8.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label7.Location = new System.Drawing.Point(9, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Nombre";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.txt_venta);
            this.panel11.Controls.Add(this.label10);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(505, 83);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(496, 34);
            this.panel11.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label10.Location = new System.Drawing.Point(7, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "Precio venta";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txt_stock);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(505, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(496, 34);
            this.panel7.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(46)))), ((int)(((byte)(87)))));
            this.label6.Location = new System.Drawing.Point(9, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Stock";
            // 
            // txt_costo
            // 
            this.txt_costo.BackColor = System.Drawing.Color.Transparent;
            this.txt_costo.BordeColor = System.Drawing.Color.Gray;
            this.txt_costo.FondoColor = System.Drawing.Color.White;
            this.txt_costo.Location = new System.Drawing.Point(135, 4);
            this.txt_costo.Name = "txt_costo";
            this.txt_costo.Radio = 10;
            this.txt_costo.Size = new System.Drawing.Size(307, 25);
            this.txt_costo.TabIndex = 8;
            // 
            // txt_nombre
            // 
            this.txt_nombre.BackColor = System.Drawing.Color.Transparent;
            this.txt_nombre.BordeColor = System.Drawing.Color.Gray;
            this.txt_nombre.FondoColor = System.Drawing.Color.White;
            this.txt_nombre.Location = new System.Drawing.Point(102, 4);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Radio = 10;
            this.txt_nombre.Size = new System.Drawing.Size(307, 25);
            this.txt_nombre.TabIndex = 1;
            // 
            // txt_venta
            // 
            this.txt_venta.BackColor = System.Drawing.Color.Transparent;
            this.txt_venta.BordeColor = System.Drawing.Color.Gray;
            this.txt_venta.FondoColor = System.Drawing.Color.White;
            this.txt_venta.Location = new System.Drawing.Point(135, 4);
            this.txt_venta.Name = "txt_venta";
            this.txt_venta.Radio = 10;
            this.txt_venta.Size = new System.Drawing.Size(307, 25);
            this.txt_venta.TabIndex = 9;
            // 
            // txt_stock
            // 
            this.txt_stock.BackColor = System.Drawing.Color.Transparent;
            this.txt_stock.BordeColor = System.Drawing.Color.Gray;
            this.txt_stock.FondoColor = System.Drawing.Color.White;
            this.txt_stock.Location = new System.Drawing.Point(135, 4);
            this.txt_stock.Name = "txt_stock";
            this.txt_stock.Radio = 10;
            this.txt_stock.Size = new System.Drawing.Size(307, 25);
            this.txt_stock.TabIndex = 7;
            // 
            // botonConEsquinas5
            // 
            this.botonConEsquinas5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(59)))), ((int)(((byte)(97)))));
            this.botonConEsquinas5.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.botonConEsquinas5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonConEsquinas5.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold);
            this.botonConEsquinas5.ForeColor = System.Drawing.Color.White;
            this.botonConEsquinas5.Location = new System.Drawing.Point(443, 15);
            this.botonConEsquinas5.Name = "botonConEsquinas5";
            this.botonConEsquinas5.Radio = 25;
            this.botonConEsquinas5.Size = new System.Drawing.Size(177, 70);
            this.botonConEsquinas5.TabIndex = 27;
            this.botonConEsquinas5.Text = "Eliminar";
            this.botonConEsquinas5.UseVisualStyleBackColor = false;
            // 
            // botonConEsquinas6
            // 
            this.botonConEsquinas6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(59)))), ((int)(((byte)(97)))));
            this.botonConEsquinas6.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.botonConEsquinas6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonConEsquinas6.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold);
            this.botonConEsquinas6.ForeColor = System.Drawing.Color.White;
            this.botonConEsquinas6.Location = new System.Drawing.Point(235, 15);
            this.botonConEsquinas6.Name = "botonConEsquinas6";
            this.botonConEsquinas6.Radio = 25;
            this.botonConEsquinas6.Size = new System.Drawing.Size(177, 70);
            this.botonConEsquinas6.TabIndex = 26;
            this.botonConEsquinas6.Text = "Modificar";
            this.botonConEsquinas6.UseVisualStyleBackColor = false;
            // 
            // botonConEsquinas7
            // 
            this.botonConEsquinas7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(59)))), ((int)(((byte)(97)))));
            this.botonConEsquinas7.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.botonConEsquinas7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonConEsquinas7.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold);
            this.botonConEsquinas7.ForeColor = System.Drawing.Color.White;
            this.botonConEsquinas7.Location = new System.Drawing.Point(39, 15);
            this.botonConEsquinas7.Name = "botonConEsquinas7";
            this.botonConEsquinas7.Radio = 25;
            this.botonConEsquinas7.Size = new System.Drawing.Size(177, 70);
            this.botonConEsquinas7.TabIndex = 25;
            this.botonConEsquinas7.Text = "Agregar";
            this.botonConEsquinas7.UseVisualStyleBackColor = false;
            // 
            // botonConEsquinas4
            // 
            this.botonConEsquinas4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(44)))), ((int)(((byte)(61)))));
            this.botonConEsquinas4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(44)))), ((int)(((byte)(61)))));
            this.botonConEsquinas4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonConEsquinas4.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold);
            this.botonConEsquinas4.ForeColor = System.Drawing.Color.White;
            this.botonConEsquinas4.Location = new System.Drawing.Point(50, 263);
            this.botonConEsquinas4.Name = "botonConEsquinas4";
            this.botonConEsquinas4.Radio = 25;
            this.botonConEsquinas4.Size = new System.Drawing.Size(177, 70);
            this.botonConEsquinas4.TabIndex = 28;
            this.botonConEsquinas4.Text = "Eliminar";
            this.botonConEsquinas4.UseVisualStyleBackColor = false;
            // 
            // UserControl_Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Name = "UserControl_Productos";
            this.Size = new System.Drawing.Size(1252, 811);
            this.Load += new System.EventHandler(this.UserControl_Productos_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_imagen)).EndInit();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Código;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_buscar;
        private System.Windows.Forms.Button button_quitar;
        private System.Windows.Forms.Panel panel2;
        private Controles.BotonConEsquinas botonConEsquinas4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private Controles.BotonConEsquinas botonConEsquinas5;
        private Controles.BotonConEsquinas botonConEsquinas6;
        private Controles.BotonConEsquinas botonConEsquinas7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btn_add_img;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel9;
        private Controles.TextBoxConEsquinas txt_costo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel8;
        private Controles.TextBoxConEsquinas txt_nombre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel11;
        private Controles.TextBoxConEsquinas txt_venta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel7;
        private Controles.TextBoxConEsquinas txt_stock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_add_marca;
        private System.Windows.Forms.Button btn_add_sexo;
        private System.Windows.Forms.Button btn_add_talla;
        private System.Windows.Forms.Button btn_add_categoria;
        private System.Windows.Forms.ComboBox cb_marcas;
        private System.Windows.Forms.ComboBox cb_categoria;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.PictureBox pb_imagen;
        private System.Windows.Forms.ComboBox cb_color;
        private System.Windows.Forms.ComboBox cb_sexo;
        private System.Windows.Forms.ComboBox cb_tallas;
    }
}
