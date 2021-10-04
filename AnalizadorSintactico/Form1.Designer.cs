
namespace AnalizadorSintactico
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtErrores = new System.Windows.Forms.TextBox();
            this.lblErrores = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxtErrores = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtResultado = new System.Windows.Forms.RichTextBox();
            this.btnAnalizador = new System.Windows.Forms.Button();
            this.btnCargarPrograma = new System.Windows.Forms.Button();
            this.rtxtArchivoTokens = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxtDerivaciones = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.lineNumbers_For_RichTextBox2 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.lineNumbers_For_RichTextBox3 = new LineNumbers.LineNumbers_For_RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1211, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtErrores
            // 
            this.txtErrores.Location = new System.Drawing.Point(1232, 388);
            this.txtErrores.Name = "txtErrores";
            this.txtErrores.ReadOnly = true;
            this.txtErrores.Size = new System.Drawing.Size(47, 20);
            this.txtErrores.TabIndex = 37;
            // 
            // lblErrores
            // 
            this.lblErrores.AutoSize = true;
            this.lblErrores.Location = new System.Drawing.Point(1195, 372);
            this.lblErrores.Name = "lblErrores";
            this.lblErrores.Size = new System.Drawing.Size(84, 13);
            this.lblErrores.TabIndex = 36;
            this.lblErrores.Text = "Cantidad errores";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1075, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 29);
            this.label3.TabIndex = 35;
            this.label3.Text = "ERRORES";
            // 
            // rtxtErrores
            // 
            this.rtxtErrores.Location = new System.Drawing.Point(1013, 47);
            this.rtxtErrores.Name = "rtxtErrores";
            this.rtxtErrores.Size = new System.Drawing.Size(266, 319);
            this.rtxtErrores.TabIndex = 34;
            this.rtxtErrores.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(685, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 29);
            this.label2.TabIndex = 33;
            this.label2.Text = "RESULTADO";
            // 
            // rtxtResultado
            // 
            this.rtxtResultado.Location = new System.Drawing.Point(588, 50);
            this.rtxtResultado.Name = "rtxtResultado";
            this.rtxtResultado.Size = new System.Drawing.Size(368, 316);
            this.rtxtResultado.TabIndex = 32;
            this.rtxtResultado.Text = "";
            // 
            // btnAnalizador
            // 
            this.btnAnalizador.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnAnalizador.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnalizador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalizador.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizador.Location = new System.Drawing.Point(406, 175);
            this.btnAnalizador.Name = "btnAnalizador";
            this.btnAnalizador.Size = new System.Drawing.Size(140, 71);
            this.btnAnalizador.TabIndex = 31;
            this.btnAnalizador.Text = "Ejecutar analizador";
            this.btnAnalizador.UseVisualStyleBackColor = false;
            this.btnAnalizador.Click += new System.EventHandler(this.btnAnalizador_Click);
            // 
            // btnCargarPrograma
            // 
            this.btnCargarPrograma.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCargarPrograma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCargarPrograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarPrograma.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarPrograma.Location = new System.Drawing.Point(172, 372);
            this.btnCargarPrograma.Name = "btnCargarPrograma";
            this.btnCargarPrograma.Size = new System.Drawing.Size(79, 41);
            this.btnCargarPrograma.TabIndex = 30;
            this.btnCargarPrograma.Text = "Cargar";
            this.btnCargarPrograma.UseVisualStyleBackColor = false;
            this.btnCargarPrograma.Click += new System.EventHandler(this.btnCargarPrograma_Click);
            // 
            // rtxtArchivoTokens
            // 
            this.rtxtArchivoTokens.Enabled = false;
            this.rtxtArchivoTokens.Location = new System.Drawing.Point(30, 50);
            this.rtxtArchivoTokens.Name = "rtxtArchivoTokens";
            this.rtxtArchivoTokens.Size = new System.Drawing.Size(368, 316);
            this.rtxtArchivoTokens.TabIndex = 29;
            this.rtxtArchivoTokens.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 29);
            this.label1.TabIndex = 28;
            this.label1.Text = "ARCHIVO TOKENS";
            // 
            // rtxtDerivaciones
            // 
            this.rtxtDerivaciones.Location = new System.Drawing.Point(30, 470);
            this.rtxtDerivaciones.Name = "rtxtDerivaciones";
            this.rtxtDerivaciones.Size = new System.Drawing.Size(656, 232);
            this.rtxtDerivaciones.TabIndex = 39;
            this.rtxtDerivaciones.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(265, 438);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 29);
            this.label4.TabIndex = 40;
            this.label4.Text = "DERIVACIÓN";
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
            this.lineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(12, 50);
            this.lineNumbers_For_RichTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox1.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox1.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox1.Name = "lineNumbers_For_RichTextBox1";
            this.lineNumbers_For_RichTextBox1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox1.ParentRichTextBox = this.rtxtArchivoTokens;
            this.lineNumbers_For_RichTextBox1.Show_BackgroundGradient = true;
            this.lineNumbers_For_RichTextBox1.Show_BorderLines = true;
            this.lineNumbers_For_RichTextBox1.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox1.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox1.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(17, 316);
            this.lineNumbers_For_RichTextBox1.TabIndex = 41;
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
            this.lineNumbers_For_RichTextBox2.Location = new System.Drawing.Point(570, 50);
            this.lineNumbers_For_RichTextBox2.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox2.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox2.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox2.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox2.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox2.Name = "lineNumbers_For_RichTextBox2";
            this.lineNumbers_For_RichTextBox2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox2.ParentRichTextBox = this.rtxtResultado;
            this.lineNumbers_For_RichTextBox2.Show_BackgroundGradient = true;
            this.lineNumbers_For_RichTextBox2.Show_BorderLines = true;
            this.lineNumbers_For_RichTextBox2.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox2.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox2.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox2.Size = new System.Drawing.Size(17, 316);
            this.lineNumbers_For_RichTextBox2.TabIndex = 42;
            // 
            // lineNumbers_For_RichTextBox3
            // 
            this.lineNumbers_For_RichTextBox3._SeeThroughMode_ = false;
            this.lineNumbers_For_RichTextBox3.AutoSizing = true;
            this.lineNumbers_For_RichTextBox3.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineNumbers_For_RichTextBox3.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbers_For_RichTextBox3.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbers_For_RichTextBox3.BorderLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox3.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox3.BorderLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox3.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
            this.lineNumbers_For_RichTextBox3.GridLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox3.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_RichTextBox3.GridLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox3.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbers_For_RichTextBox3.LineNrs_AntiAlias = true;
            this.lineNumbers_For_RichTextBox3.LineNrs_AsHexadecimal = false;
            this.lineNumbers_For_RichTextBox3.LineNrs_ClippedByItemRectangle = true;
            this.lineNumbers_For_RichTextBox3.LineNrs_LeadingZeroes = true;
            this.lineNumbers_For_RichTextBox3.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.lineNumbers_For_RichTextBox3.Location = new System.Drawing.Point(995, 47);
            this.lineNumbers_For_RichTextBox3.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_RichTextBox3.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_RichTextBox3.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
            this.lineNumbers_For_RichTextBox3.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_RichTextBox3.MarginLines_Thickness = 1F;
            this.lineNumbers_For_RichTextBox3.Name = "lineNumbers_For_RichTextBox3";
            this.lineNumbers_For_RichTextBox3.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_RichTextBox3.ParentRichTextBox = this.rtxtErrores;
            this.lineNumbers_For_RichTextBox3.Show_BackgroundGradient = true;
            this.lineNumbers_For_RichTextBox3.Show_BorderLines = true;
            this.lineNumbers_For_RichTextBox3.Show_GridLines = true;
            this.lineNumbers_For_RichTextBox3.Show_LineNrs = true;
            this.lineNumbers_For_RichTextBox3.Show_MarginLines = true;
            this.lineNumbers_For_RichTextBox3.Size = new System.Drawing.Size(17, 319);
            this.lineNumbers_For_RichTextBox3.TabIndex = 43;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 714);
            this.Controls.Add(this.lineNumbers_For_RichTextBox3);
            this.Controls.Add(this.lineNumbers_For_RichTextBox2);
            this.Controls.Add(this.lineNumbers_For_RichTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtxtDerivaciones);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtErrores);
            this.Controls.Add(this.lblErrores);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtxtErrores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtxtResultado);
            this.Controls.Add(this.btnAnalizador);
            this.Controls.Add(this.btnCargarPrograma);
            this.Controls.Add(this.rtxtArchivoTokens);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Analizador sintactico";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtErrores;
        private System.Windows.Forms.Label lblErrores;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxtErrores;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtxtResultado;
        private System.Windows.Forms.Button btnAnalizador;
        private System.Windows.Forms.Button btnCargarPrograma;
        private System.Windows.Forms.RichTextBox rtxtArchivoTokens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxtDerivaciones;
        private System.Windows.Forms.Label label4;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox2;
        private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox3;
    }
}

