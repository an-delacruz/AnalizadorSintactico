
namespace AnalizadorLexico
{
    partial class Form1
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCargarPrograma = new System.Windows.Forms.Button();
            this.btnEditarPrograma = new System.Windows.Forms.Button();
            this.btnGuardarPrograma = new System.Windows.Forms.Button();
            this.btnGuardarArchivo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAnalizador = new System.Windows.Forms.Button();
            this.dgvSimbolos = new System.Windows.Forms.DataGridView();
            this.rtxtProgramaFuente = new System.Windows.Forms.RichTextBox();
            this.numberLabel = new System.Windows.Forms.Label();
            this.rtxtArchivoTokens = new System.Windows.Forms.RichTextBox();
            this.rtxtErrores = new System.Windows.Forms.RichTextBox();
            this.lblErrores = new System.Windows.Forms.Label();
            this.txtErrores = new System.Windows.Forms.TextBox();
            this.dgvTSIdentificador = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGuardarTabla = new System.Windows.Forms.Button();
            this.lineNumbers_For_RichTextBox2 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.rtxtDerivaciones = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimbolos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTSIdentificador)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "PROGRAMA FUENTE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(582, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "ARCHIVO DE TOKENS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1394, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "ERRORES";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1585, 50);
            this.panel1.TabIndex = 6;
            // 
            // btnCargarPrograma
            // 
            this.btnCargarPrograma.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCargarPrograma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCargarPrograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarPrograma.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarPrograma.Location = new System.Drawing.Point(94, 433);
            this.btnCargarPrograma.Name = "btnCargarPrograma";
            this.btnCargarPrograma.Size = new System.Drawing.Size(79, 41);
            this.btnCargarPrograma.TabIndex = 7;
            this.btnCargarPrograma.Text = "Cargar";
            this.btnCargarPrograma.UseVisualStyleBackColor = false;
            this.btnCargarPrograma.Click += new System.EventHandler(this.btnCargarPrograma_Click);
            // 
            // btnEditarPrograma
            // 
            this.btnEditarPrograma.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEditarPrograma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditarPrograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarPrograma.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarPrograma.Location = new System.Drawing.Point(179, 433);
            this.btnEditarPrograma.Name = "btnEditarPrograma";
            this.btnEditarPrograma.Size = new System.Drawing.Size(79, 41);
            this.btnEditarPrograma.TabIndex = 9;
            this.btnEditarPrograma.Text = "Editar";
            this.btnEditarPrograma.UseVisualStyleBackColor = false;
            this.btnEditarPrograma.Click += new System.EventHandler(this.btnEditarPrograma_Click);
            // 
            // btnGuardarPrograma
            // 
            this.btnGuardarPrograma.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGuardarPrograma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardarPrograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarPrograma.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPrograma.Location = new System.Drawing.Point(264, 433);
            this.btnGuardarPrograma.Name = "btnGuardarPrograma";
            this.btnGuardarPrograma.Size = new System.Drawing.Size(93, 41);
            this.btnGuardarPrograma.TabIndex = 10;
            this.btnGuardarPrograma.Text = "Guardar";
            this.btnGuardarPrograma.UseVisualStyleBackColor = false;
            this.btnGuardarPrograma.Click += new System.EventHandler(this.btnGuardarPrograma_Click);
            // 
            // btnGuardarArchivo
            // 
            this.btnGuardarArchivo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGuardarArchivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardarArchivo.Enabled = false;
            this.btnGuardarArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarArchivo.Font = new System.Drawing.Font("Century", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarArchivo.ForeColor = System.Drawing.Color.Red;
            this.btnGuardarArchivo.Location = new System.Drawing.Point(622, 433);
            this.btnGuardarArchivo.Name = "btnGuardarArchivo";
            this.btnGuardarArchivo.Size = new System.Drawing.Size(234, 41);
            this.btnGuardarArchivo.TabIndex = 11;
            this.btnGuardarArchivo.Text = "Guardar";
            this.btnGuardarArchivo.UseVisualStyleBackColor = false;
            this.btnGuardarArchivo.Click += new System.EventHandler(this.btnGuardarArchivo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1498, 472);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAnalizador
            // 
            this.btnAnalizador.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnAnalizador.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnalizador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalizador.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizador.Location = new System.Drawing.Point(385, 226);
            this.btnAnalizador.Name = "btnAnalizador";
            this.btnAnalizador.Size = new System.Drawing.Size(140, 71);
            this.btnAnalizador.TabIndex = 13;
            this.btnAnalizador.Text = "Ejecutar analizador";
            this.btnAnalizador.UseVisualStyleBackColor = false;
            this.btnAnalizador.Click += new System.EventHandler(this.btnAnalizador_Click);
            // 
            // dgvSimbolos
            // 
            this.dgvSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimbolos.Location = new System.Drawing.Point(12, 522);
            this.dgvSimbolos.Name = "dgvSimbolos";
            this.dgvSimbolos.RowHeadersWidth = 51;
            this.dgvSimbolos.Size = new System.Drawing.Size(787, 311);
            this.dgvSimbolos.TabIndex = 15;
            // 
            // rtxtProgramaFuente
            // 
            this.rtxtProgramaFuente.Location = new System.Drawing.Point(77, 111);
            this.rtxtProgramaFuente.Name = "rtxtProgramaFuente";
            this.rtxtProgramaFuente.Size = new System.Drawing.Size(302, 316);
            this.rtxtProgramaFuente.TabIndex = 16;
            this.rtxtProgramaFuente.Text = "";
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Location = new System.Drawing.Point(14, 111);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(0, 13);
            this.numberLabel.TabIndex = 17;
            // 
            // rtxtArchivoTokens
            // 
            this.rtxtArchivoTokens.Location = new System.Drawing.Point(552, 111);
            this.rtxtArchivoTokens.Name = "rtxtArchivoTokens";
            this.rtxtArchivoTokens.Size = new System.Drawing.Size(368, 316);
            this.rtxtArchivoTokens.TabIndex = 20;
            this.rtxtArchivoTokens.Text = "";
            // 
            // rtxtErrores
            // 
            this.rtxtErrores.Location = new System.Drawing.Point(1307, 108);
            this.rtxtErrores.Name = "rtxtErrores";
            this.rtxtErrores.Size = new System.Drawing.Size(266, 319);
            this.rtxtErrores.TabIndex = 21;
            this.rtxtErrores.Text = "";
            // 
            // lblErrores
            // 
            this.lblErrores.AutoSize = true;
            this.lblErrores.Location = new System.Drawing.Point(1484, 430);
            this.lblErrores.Name = "lblErrores";
            this.lblErrores.Size = new System.Drawing.Size(84, 13);
            this.lblErrores.TabIndex = 22;
            this.lblErrores.Text = "Cantidad errores";
            // 
            // txtErrores
            // 
            this.txtErrores.Location = new System.Drawing.Point(1521, 445);
            this.txtErrores.Name = "txtErrores";
            this.txtErrores.ReadOnly = true;
            this.txtErrores.Size = new System.Drawing.Size(47, 20);
            this.txtErrores.TabIndex = 23;
            // 
            // dgvTSIdentificador
            // 
            this.dgvTSIdentificador.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTSIdentificador.Location = new System.Drawing.Point(805, 521);
            this.dgvTSIdentificador.Name = "dgvTSIdentificador";
            this.dgvTSIdentificador.RowHeadersWidth = 51;
            this.dgvTSIdentificador.Size = new System.Drawing.Size(624, 265);
            this.dgvTSIdentificador.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1052, 498);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 21);
            this.label4.TabIndex = 26;
            this.label4.Text = "Tabla de símbolos ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(308, 498);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 21);
            this.label6.TabIndex = 28;
            this.label6.Text = "Matriz de transición";
            // 
            // btnGuardarTabla
            // 
            this.btnGuardarTabla.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGuardarTabla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardarTabla.Enabled = false;
            this.btnGuardarTabla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarTabla.Font = new System.Drawing.Font("Century", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarTabla.ForeColor = System.Drawing.Color.Red;
            this.btnGuardarTabla.Location = new System.Drawing.Point(1017, 792);
            this.btnGuardarTabla.Name = "btnGuardarTabla";
            this.btnGuardarTabla.Size = new System.Drawing.Size(234, 41);
            this.btnGuardarTabla.TabIndex = 31;
            this.btnGuardarTabla.Text = "Guardar";
            this.btnGuardarTabla.UseVisualStyleBackColor = false;
            this.btnGuardarTabla.Click += new System.EventHandler(this.btnGuardarTabla_Click);
            // 
            // lineNumbers_For_RichTextBox2
            // 
            this.lineNumbers_For_RichTextBox2._SeeThroughMode_ = false;
            this.lineNumbers_For_RichTextBox2.AutoSizing = true;
            this.lineNumbers_For_RichTextBox2.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineNumbers_For_RichTextBox2.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbers_For_RichTextBox2.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbers_For_RichTextBox2.BorderLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox2.BorderLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
            this.lineNumbers_For_RichTextBox2.GridLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox2.GridLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbers_For_RichTextBox2.LineNrs_AntiAlias = true;
            this.lineNumbers_For_RichTextBox2.LineNrs_AsHexadecimal = false;
            this.lineNumbers_For_RichTextBox2.LineNrs_ClippedByItemRectangle = true;
            this.lineNumbers_For_RichTextBox2.LineNrs_LeadingZeroes = true;
            this.lineNumbers_For_RichTextBox2.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.lineNumbers_For_RichTextBox2.Location = new System.Drawing.Point(534, 111);
            this.lineNumbers_For_RichTextBox2.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox2.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox2.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox2.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.Name = "lineNumbers_For_RichTextBox2";
            this.lineNumbers_For_RichTextBox2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox2.ParentRichTextBox = this.rtxtArchivoTokens;
            this.lineNumbers_For_RichTextBox2.Show_BackgroundGradient = false;
            this.lineNumbers_For_RichTextBox2.Show_BorderLines = false;
            this.lineNumbers_For_RichTextBox2.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox2.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox2.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox2.Size = new System.Drawing.Size(17, 316);
            this.lineNumbers_For_RichTextBox2.TabIndex = 30;
            // 
            // lineNumbers_For_RichTextBox1
            // 
            this.lineNumbers_For_RichTextBox1._SeeThroughMode_ = false;
            this.lineNumbers_For_RichTextBox1.AutoSizing = true;
            this.lineNumbers_For_RichTextBox1.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineNumbers_For_RichTextBox1.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbers_For_RichTextBox1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbers_For_RichTextBox1.BorderLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox1.BorderLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
            this.lineNumbers_For_RichTextBox1.GridLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox1.GridLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbers_For_RichTextBox1.LineNrs_AntiAlias = true;
            this.lineNumbers_For_RichTextBox1.LineNrs_AsHexadecimal = false;
            this.lineNumbers_For_RichTextBox1.LineNrs_ClippedByItemRectangle = true;
            this.lineNumbers_For_RichTextBox1.LineNrs_LeadingZeroes = true;
            this.lineNumbers_For_RichTextBox1.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.lineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(59, 111);
            this.lineNumbers_For_RichTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox1.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.Name = "lineNumbers_For_RichTextBox1";
            this.lineNumbers_For_RichTextBox1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox1.ParentRichTextBox = this.rtxtProgramaFuente;
            this.lineNumbers_For_RichTextBox1.Show_BackgroundGradient = false;
            this.lineNumbers_For_RichTextBox1.Show_BorderLines = false;
            this.lineNumbers_For_RichTextBox1.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox1.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox1.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(17, 316);
            this.lineNumbers_For_RichTextBox1.TabIndex = 29;
            // 
            // rtxtDerivaciones
            // 
            this.rtxtDerivaciones.Location = new System.Drawing.Point(926, 108);
            this.rtxtDerivaciones.Name = "rtxtDerivaciones";
            this.rtxtDerivaciones.Size = new System.Drawing.Size(375, 319);
            this.rtxtDerivaciones.TabIndex = 40;
            this.rtxtDerivaciones.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1012, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 29);
            this.label5.TabIndex = 41;
            this.label5.Text = "DERIVACIÓN";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1585, 845);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtxtDerivaciones);
            this.Controls.Add(this.btnGuardarTabla);
            this.Controls.Add(this.lineNumbers_For_RichTextBox2);
            this.Controls.Add(this.lineNumbers_For_RichTextBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvTSIdentificador);
            this.Controls.Add(this.txtErrores);
            this.Controls.Add(this.lblErrores);
            this.Controls.Add(this.rtxtErrores);
            this.Controls.Add(this.rtxtArchivoTokens);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.rtxtProgramaFuente);
            this.Controls.Add(this.dgvSimbolos);
            this.Controls.Add(this.btnAnalizador);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGuardarArchivo);
            this.Controls.Add(this.btnGuardarPrograma);
            this.Controls.Add(this.btnEditarPrograma);
            this.Controls.Add(this.btnCargarPrograma);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimbolos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTSIdentificador)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCargarPrograma;
        private System.Windows.Forms.Button btnEditarPrograma;
        private System.Windows.Forms.Button btnGuardarPrograma;
        private System.Windows.Forms.Button btnGuardarArchivo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAnalizador;
        private System.Windows.Forms.DataGridView dgvSimbolos;
        private System.Windows.Forms.RichTextBox rtxtProgramaFuente;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.RichTextBox rtxtArchivoTokens;
        private System.Windows.Forms.RichTextBox rtxtErrores;
        private System.Windows.Forms.Label lblErrores;
        private System.Windows.Forms.TextBox txtErrores;
        private System.Windows.Forms.DataGridView dgvTSIdentificador;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox2;
        private System.Windows.Forms.Button btnGuardarTabla;
        private System.Windows.Forms.RichTextBox rtxtDerivaciones;
        private System.Windows.Forms.Label label5;
    }
}

