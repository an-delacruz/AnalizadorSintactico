using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AnalizadorSintactico
{
    public partial class Form1 : Form
    {
        List<string> lstDerivaciones = new List<string>();
        List<string> lstErrores = new List<string>();
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
            rtxtDerivaciones.Clear();
            string instrucciones = rtxtArchivoTokens.Text.Trim();
            //Arreglo para almacenar cada linea en una casilla diferente del arreglo para hacer la busqueda por instrucción
            string[] arrTokens = instrucciones.Split('\n').ToArray();
            List<string> lstTokens = new List<string>();
            lstTokens = arrTokens.ToList();
            string[] arrResultado = new string[arrTokens.Length];
            for (int i = 0; i < arrTokens.Length; i++)
            {
                if (arrTokens[i].Contains("IDVA"))
                {
                    arrTokens[i] = ReemplazarIDVA(arrTokens[i]);

                }
                lstDerivaciones.Add(arrTokens[i]);
                if (arrTokens[i].Contains("PR21") || arrTokens[i].Contains("PR23") || arrTokens[i].Contains("PR05")
                    || arrTokens[i].Contains("PR19") || arrTokens[i].Contains("PR12"))
                {
                    if (arrTokens[i+1] == "CE07")
                    {
                        arrTokens[i] = arrTokens[i] + " " + arrTokens[i + 1];
                        //arrTokens[i+1] = "";
                        
                        
                        for (int z = i+1; z < arrTokens.Length; z++)
                        {
                            if (arrTokens[z].Contains("CE08"))
                            {
                                arrTokens[i] = arrTokens[i] + " " + arrTokens[z];
                                //arrTokens[z] = "";
                            }
                        }
                        if (!arrTokens[i].Contains("CE08"))
                        {
                            lstErrores.Add("Linea " + i + ": Error de sintaxis");
                        }
                    }
                    else
                    {
                        lstErrores.Add("Linea " + i + ": Error de sintaxis");
                    }
                }
                arrResultado[i] = BottomUp(arrTokens[i], 0, i+1);
                lstDerivaciones.Add(arrResultado[i]);
            }
            string resultado = "";
            for (int i = 0; i < arrResultado.Length; i++)
            {
                resultado = resultado +  arrResultado[i] + "\n";
            }
            rtxtResultado.Text = resultado;
            
            string derivaciones = ""; 
            foreach (string derivacion in lstDerivaciones)
            {
                derivaciones = derivaciones + derivacion + "\n";
            }
            rtxtDerivaciones.Text = derivaciones;
            string Errores = "";
            foreach (string error in lstErrores)
            {
                Errores = Errores + error + "\n";
            }
            rtxtErrores.Text = Errores;
            txtErrores.Text = lstErrores.Count().ToString();
            lstDerivaciones.Clear();
            lstErrores.Clear();
        }
        string BottomUp(string cadena, int posicionInicial, int numLinea)
        {
            string resultado = "";
            int LongitudCadena = cadena.Trim().Split(' ').ToArray().Length;
            string cadenaActual = "";
            string cadenaAuxiliar = "";
            //Arreglo con los tokens que contiene la cadena que se va a reducir.
            string[] TokensCadena = cadena.Trim().Split(' ').ToArray();
            int c = 0;
            if (cadena == "CE07" || cadena == "CE08")
            {
                return "";
            }
            while (c < TokensCadena.Length)
            {
                //Si la posición actual es mayor al número de tokens se devuelve la cadena (Aquí creo que también se debe manejar lo de errores)
                if (posicionInicial > TokensCadena.Length)
                {
                    lstErrores.Add("Linea " + numLinea + ": Error de sintaxis");
                    return cadena;
                }
                //Aquí se forma la cadena que se va a reducir, es decir se indica desde que número de token se va a comenzar en la cadena para cuando no se encuentre toda completa
                int numTokensABuscar = TokensCadena.Length - posicionInicial;
                for (int x = 0; x + numTokensABuscar <= TokensCadena.Length; x++)
                {
                    for (int i = x; i < numTokensABuscar + x; i++)
                    {
                        if (i == x)
                        {
                            cadenaAuxiliar = cadenaAuxiliar + TokensCadena[i];
                        }
                        else
                        {
                            cadenaAuxiliar = cadenaAuxiliar + " " + TokensCadena[i];
                        }
                    }
                    cadenaActual = cadenaAuxiliar;
                    cadenaAuxiliar = "";
                    resultado = BuscarGramatica(cadenaActual);
                    //Si la busqueda en la gramática devuelve S, entonces ya se término de reducir la cadena enviada.
                    if (resultado != cadenaActual)
                    {
                        break;
                    }
                }
                if (resultado == "S")
                {
                    return resultado;
                }
                else
                {
                    //Si el resultado regresado del metodo BuscarGramatica no es S, en el caso de que haya habido una reducción aquí se hace el reemplazo de la parte que se redujo
                    //y se vuelve a aplicar el método bottom up
                    if (resultado != cadenaActual)
                    {
                        cadena = ReemplazarCadena(cadena, cadenaActual, resultado);
                        TokensCadena = cadena.Trim().Split(' ').ToArray();
                        lstDerivaciones.Add(cadena);
                        posicionInicial = 0;
                    }
                    //En el caso de que la cadena no se haya reducido porque no se encoentro ninguna coincidencia, entonces se vuelve a buscar pero se
                    //mueve a la derecha la posición inicial para aplicar el método pero con un token menos.
                    else
                    {
                        posicionInicial = posicionInicial + 1;
                    }
                }

            }
            return "";
        }

        //Método para buscar la cadena actual en la parte derecha de la gramática, si se encuentra se devuelve la parte izquierda,
        //sino, se devuelve la misma cadena que se busco
        string BuscarGramatica(string cadena)
        {
            string resultado = "";
            string cadenacon = "Data Source=DESKTOP-ANTONIO\\SQLEXPRESS;Initial Catalog=AUTOMATAS;Integrated Security=True";
            using (SqlConnection cnn = new SqlConnection(cadenacon))
            {
                cnn.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT count(*) from Gramaticas where Gramatica ='" + cadena + "'", cnn);
                int coincidencias = int.Parse(sqlCommand.ExecuteScalar().ToString());
                if (coincidencias > 0)
                {
                    sqlCommand = new SqlCommand("SELECT Simbolo from Gramaticas where Gramatica ='" + cadena + "'", cnn);
                    resultado = sqlCommand.ExecuteScalar().ToString();
                }
                else
                {
                    resultado = cadena;
                }
            }
            //using (var reader = new StreamReader("./Resources/Eq.-1-Gramaticas-libres-de-contexto.csv"))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = line.Split(',');
            //        if(cadena == values[1])
            //        {
            //            resultado = values[0];
            //            reader.Dispose();
            //            return resultado;
            //        }
            //        else
            //        {
            //            resultado = cadena;
            //        }
            //    }

            //    reader.Dispose();
            //}
            return resultado;
        }
        //Método para reemplazar las partes de la cadena que ya se han encontrado en la gramatica.
        string ReemplazarCadena(string strcadenaActual, string subcadenaAnterior, string subcadenaNueva)
        {
            string Resultado = "";
            string subCadena1 = "";
            string subCadena2 = "";
            if (strcadenaActual.Contains(subcadenaAnterior) )
            {
                int posInicio;
                posInicio = strcadenaActual.IndexOf(subcadenaAnterior, 0);
                subCadena1 = strcadenaActual.Substring(0, posInicio);
                subCadena2 = strcadenaActual.Substring(posInicio + subcadenaAnterior.Length);
                Resultado = subCadena1  + subcadenaNueva  + subCadena2;
            }
            return Resultado;
        }
        string ReemplazarIDVA(string cadena)
        {
            string pattern = @"IDVA[0-9]";
            string replace = "IDVA";
            cadena = Regex.Replace(cadena, pattern, replace);
            return cadena;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            rtxtArchivoTokens.Enabled = true;
        }
    }
}
