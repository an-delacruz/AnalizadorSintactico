using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorSintactico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargarPrograma_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos de texto (.txt)|*.txt|Todos los archivos (*.*)|*.*"; //se abre cualquier tipo.
            abrir.Title = "Seleccione archivo de tokens"; //titulo del mensaje de dialogo
            var resultado = abrir.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                StreamReader leer = new StreamReader(abrir.FileName);
                rtxtArchivoTokens.Text = leer.ReadToEnd();
                leer.Close();
            }
            rtxtArchivoTokens.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAnalizador_Click(object sender, EventArgs e)
        {
            int tamaño = rtxtArchivoTokens.Lines.Count();
            //Arreglo para almacenar los tokens que hay en cada linea del archivo de tokens, para así separar trabajar individualmente con cada fila.
            string[] arrArchivoTokens = new string[tamaño];
            arrArchivoTokens = rtxtArchivoTokens.Lines.ToArray();

        }
    }
}
