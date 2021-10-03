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
            string instrucciones = rtxtArchivoTokens.Text.Trim();
            //Arreglo para almacenar cada linea en una casilla diferente del arreglo para hacer la busqueda por instrucción
            string[] arrTokens = instrucciones.Split('\n').ToArray();
            string[] arrResultado = new string[arrTokens.Length];
            for (int i = 0; i < arrTokens.Length; i++)
            {
                arrResultado[i] = BottomUp(arrTokens[i], 0);
            }
            string resultado = "";
            for (int i = 0; i < arrResultado.Length; i++)
            {
                resultado = resultado +  arrResultado[i] + "\n";
            }
            rtxtResultado.Text = resultado;
        }
        string BottomUp(string cadena, int posicionInicial)
        {
            int LongitudCadena = cadena.Trim().Split(' ').ToArray().Length;
            string cadenaActual = "";
            //Arreglo con los tokens que contiene la cadena que se va a reducir.
            string[] TokensCadena = cadena.Trim().Split(' ').ToArray();
            //Si la posición actual es mayor al número de tokens se devuelve la cadena (Aquí creo que también se debe manejar lo de errores)
            if(posicionInicial > TokensCadena.Length)
            {
                return cadena;
            }
            //Aquí se forma la cadena que se va a reducir, es decir se indica desde que número de token se va a comenzar en la cadena para cuando no se encuentre toda completa
            for (int i = posicionInicial; i < TokensCadena.Length; i++)
            {
                if(i == posicionInicial)
                {
                    cadenaActual = cadenaActual + TokensCadena[i];
                }
                else
                {
                    cadenaActual = cadenaActual + " " + TokensCadena[i];
                }
            }
            string resultado;
            resultado = BuscarGramatica(cadenaActual);
            //Si la busqueda en la gramática devuelve S, entonces ya se término de reducir la cadena enviada.
            if (resultado == "S")
            {
                return resultado;
            }
            else
            {
                //Si el resultado regresado del metodo BuscarGramatica no es S, en el caso de que haya habido una reducción aquí se hace el reemplazo de la parte que se redujo
                //y se vuelve a aplicar el método bottom up
                if (resultado != cadena)
                {
                    cadena = ReemplazarCadena(cadena.Split(' ').ToArray(), posicionInicial, TokensCadena.Length, resultado);
                    return (BottomUp(cadena, 0));
                }
                //En el caso de que la cadena no se haya reducido porque no se encoentro ninguna coincidencia, entonces se vuelve a buscar pero se
                //mueve a la derecha la posición inicial para aplicar el método pero con un token menos.
                else
                {
                    return (BottomUp(cadena, posicionInicial + 1));
                }
            }
        }
        //Método para buscar la cadena actual en la parte derecha de la gramática, si se encuentra se devuelve la parte izquierda,
        //sino, se devuelve la misma cadena que se busco
        string BuscarGramatica(string cadena)
        {
            string resultado = "";
            using (var reader = new StreamReader("./Resources/Eq.-1-Gramáticas-libres-de-contexto.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if(cadena == values[1])
                    {
                        resultado = values[0];
                        return resultado;
                    }
                    else
                    {
                        resultado = cadena;
                    }
                }
            }
            return resultado;
        }
        //Método para reemplazar las partes de la cadena que ya se han encontrado en la gramatica.
        string ReemplazarCadena(string[] cadenaActual, int posicionInicial, int posicionFinal, string subcadena)
        {
            string nvaCadena;
            string subCadena1 = "";
            string subCadena2 = "";         
            for (int i = 0; i < posicionInicial; i++)
            {
                subCadena1 = subCadena1 + cadenaActual[i] + " ";
            }
            for (int i = posicionFinal; i < cadenaActual.Length; i++)
            {
                subCadena2 = subCadena2 + cadenaActual[i] + " ";
            }

            nvaCadena = subCadena1.Trim() + " " + subcadena + " " + subCadena2.Trim();
            return nvaCadena.Trim();
        }
    }
}
