using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO; //se agrego la libreria
using System.Xml;
using System.Text.RegularExpressions;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        EjecutorQuerys miConexion = new EjecutorQuerys();
        List<string> lstDerivaciones = new List<string>();
        List<string> lstErroresSintacticos = new List<string>();
        public Form1()
        {
            InitializeComponent();

        }
        private void btnAnalizador_Click(object sender, EventArgs e)
        {
            #region Léxico
            try
            {

                miConexion.Conectar();
                //archivoTOken
                int iToken = 0;       //Iterador para el recorrido de los token en el foreach
                int acomodoTokenAnterior = 0;       //Guarda si es espacio en blanco o salto de linea para el acomodo del archivo token
                                                    //Errores                           //Linea en la que nos encontramos
                int noLinea = 1;    //Iterador que lleva la cuenta de las lineas de codigo(Salto de linea)
                int iError = 0;     //Iterador para el recorrido de los errores en el foreach
                int numPalabra = 1;
                Cadena todosCaracteres = new Cadena();
                Automata miAutomata = new Automata();

                todosCaracteres.Caracteres = rtxtProgramaFuente.Text.ToCharArray();

                string[] cadenas = rtxtProgramaFuente.Text.Split(';');
                foreach (char caracterActual in todosCaracteres.Caracteres)
                {
                    if (caracterActual == ';')
                    {
                        numPalabra++; //Conteo del número de palabra
                    }
                    if (caracterActual != char.Parse("\n"))
                    {
                        if (char.IsWhiteSpace(caracterActual))
                        {
                            //Hice el cambio de blanco a punto y coma para poder aceptar espacios en blanco tal cual estaba en el interfaz que mostro la Ing.
                            miAutomata.BuscarSiguienteEstado("Δ", miConexion, 1, noLinea, cadenas, numPalabra);
                            //miAutomata.BuscarSiguienteEstado(";", miConexion, 1, noLinea, cadenas, numPalabra);   //El uno representa espacio en blanco para el archivo token mientras que el cero representa salto de linea. 
                        }                                                                   //noLinea es un acumulador que lleva el conteo de la linea en la que nos encontramos actualmente.
                        else if (char.IsUpper(caracterActual))
                        {
                            miAutomata.BuscarSiguienteEstado(caracterActual.ToString() + "1", miConexion, 0, noLinea, cadenas, numPalabra);   //Aqui represento el salto de linea con un cero para el acomodo del archivo token.
                        }
                        else if (caracterActual == '"')
                        {
                            miAutomata.BuscarSiguienteEstado("C2", miConexion, 0, noLinea, cadenas, numPalabra); 
                        }
                        else
                        {
                            miAutomata.BuscarSiguienteEstado(caracterActual.ToString(), miConexion, 0, noLinea, cadenas, numPalabra);
                        }
                    }
                    else
                    {
                        noLinea++;
                    }
                }

                string ArchivoToken = "";
                foreach (Token token in miAutomata.lstTokens)
                {
                    if (iToken == 0)     //Este if es utilizado para verificar si es el primer token de la lista.
                    {
                        ArchivoToken = token.CAT;       //Se guarda el primer token
                        acomodoTokenAnterior = token.Acomodo;   //Se guarda su acomodo para que no se pierda al cambiar de token.
                        iToken++;        //iterador que me auxilia para saber en que token estoy
                    }
                    else
                    {
                        if (cadenas[iToken].Contains("\n"))
                        {
                            ArchivoToken = ArchivoToken + "\n" + token.CAT;
                            acomodoTokenAnterior = token.Acomodo;
                            iToken++;
                        }
                        else
                        {
                            ArchivoToken = ArchivoToken + " " + token.CAT;
                            acomodoTokenAnterior = token.Acomodo;
                            iToken++;
                        }
                    }
                }

                rtxtArchivoTokens.Text = ArchivoToken;

                string Errores = "";
                foreach (Error error in miAutomata.lstErrores)
                {
                    if (iError == 0)        //of que compara si el el orimer error de la lista.
                    {
                        Errores = error.CatError + " en linea " + error.NoLinea + "\n";
                        iError++;
                    }
                    else
                    {
                        Errores = Errores + "\n" + error.CatError + "en linea " + error.NoLinea;
                    }
                }
                rtxtErrores.Text = Errores;
                ActualizarTablaSimbolosIdentificadores(miAutomata.lstIdentificadores, miAutomata.lstConstantes);


                if(miAutomata.lstErrores.Count > 0) { btnGuardarArchivo.Enabled = false;
                    btnGuardarArchivo.ForeColor = Color.Red;
                }
                else
                {
                    btnGuardarArchivo.Enabled = true;
                    btnGuardarArchivo.ForeColor = Color.Green;
                    btnGuardarTabla.Enabled = true;
                    btnGuardarTabla.ForeColor = Color.Green;
                }
                #endregion


                #region Sintactico
                rtxtDerivaciones.Clear();
                string instrucciones = rtxtArchivoTokens.Text.Trim();
                //Arreglo para almacenar cada linea en una casilla diferente del arreglo para hacer la busqueda por instrucción
                string[] arrTokens = instrucciones.Split('\n').ToArray();
                List<string> lstTokens = new List<string>();
                lstTokens = arrTokens.ToList();
                string[] arrResultado = new string[arrTokens.Length];
                List<string> lstCorchetes = new List<string>();
                for (int i = 0; i < arrTokens.Length; i++)
                {
                    if (arrTokens[i].Contains("ID"))
                    {
                        arrTokens[i] = ReemplazarIDVA(arrTokens[i]);

                    }
                    lstDerivaciones.Add(arrTokens[i]);
                    if (arrTokens[i].Contains("PR21") || arrTokens[i].Contains("PR23") || arrTokens[i].Contains("PR05")
                        || arrTokens[i].Contains("PR19") || arrTokens[i].Contains("PR12") || arrTokens[i].Contains("PR22"))
                    {
                        if (arrTokens[i + 1] == "CE07")
                        {
                            arrTokens[i] = arrTokens[i] + " " + arrTokens[i + 1];
                            //arrTokens[i+1] = "";
                            lstCorchetes.Add("IMPAR");

                            for (int z = i + 1; z < arrTokens.Length; z++)
                            {
                                if (arrTokens[z].Contains("CE08"))
                                {
                                    arrTokens[i] = arrTokens[i] + " " + arrTokens[z];
                                    //arrTokens[z] = "";
                                    lstCorchetes.Add("PAR");
                                }
                            }
                            if (!arrTokens[i].Contains("CE08"))
                            {
                                lstErroresSintacticos.Add("Linea " + i + ": Error de sintaxis - Corchete abierto pero no cerrado");
                            }
                        }
                        else
                        {
                            lstErroresSintacticos.Add("Linea " + i + ": Error de sintaxis - Corchete no abierto");
                        }
                    }
                    if(!arrTokens[i].Contains("Error"))
                    {
                        arrResultado[i] = BottomUp(arrTokens[i], 0, i + 1);
                        lstDerivaciones.Add(arrResultado[i]);
                    }
                }
                //string resultado = "";
                //for (int i = 0; i < arrResultado.Length; i++)
                //{
                //    resultado = resultado + arrResultado[i] + "\n";
                //}
                //rtxtResultado.Text = resultado;

                string derivaciones = "";
                foreach (string derivacion in lstDerivaciones)
                {
                    derivaciones = derivaciones + derivacion + "\n";
                }
                rtxtDerivaciones.Text = derivaciones;
                foreach (string error in lstErroresSintacticos)
                {
                    Errores = Errores + error + "\n";
                }
                rtxtErrores.Text = Errores;
                txtErrores.Text = (miAutomata.lstErrores.Count + lstErroresSintacticos.Count).ToString();

                lstDerivaciones.Clear();
                lstErroresSintacticos.Clear();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            miConexion.Desconectar();

            

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
                    lstErroresSintacticos.Add("Linea " + numLinea + ": Error de sintaxis");
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
            return resultado;
        }
        //Método para reemplazar las partes de la cadena que ya se han encontrado en la gramatica.
        string ReemplazarCadena(string strcadenaActual, string subcadenaAnterior, string subcadenaNueva)
        {
            string Resultado = "";
            string subCadena1 = "";
            string subCadena2 = "";
            if (strcadenaActual.Contains(subcadenaAnterior))
            {
                int posInicio;
                posInicio = strcadenaActual.IndexOf(subcadenaAnterior, 0);
                subCadena1 = strcadenaActual.Substring(0, posInicio);
                subCadena2 = strcadenaActual.Substring(posInicio + subcadenaAnterior.Length);
                Resultado = subCadena1 + subcadenaNueva + subCadena2;
            }
            return Resultado;
        }
        string ReemplazarIDVA(string cadena)
        {
            string pattern = @"ID[0-9][0-9]";
            string replace = "IDVA";
            cadena = Regex.Replace(cadena, pattern, replace);
            return cadena;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            miConexion.Conectar();
            rtxtProgramaFuente.Select();
            dgvTSIdentificador.Rows.Clear();
            dgvTSIdentificador.Columns.Add("# Identificador", "# Identificador");
            dgvTSIdentificador.Columns.Add("Nombre", "Nombre");
            dgvTSIdentificador.Columns.Add("Tipo de dato", "Tipo de dato");
            dgvTSIdentificador.Columns.Add("Valor", "Valor");


            miConexion.CargarSimbolos(dgvSimbolos);

            dgvTSIdentificador.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTSIdentificador.ReadOnly = true;

            dgvTSIdentificador.ReadOnly = true;
            miConexion.Desconectar();
        }

        private void btnCargarPrograma_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos de texto (.txt)|*.txt|Todos los archivos (*.*)|*.*"; //se abre cualquier tipo.
            abrir.Title = "Guardar como..."; //titulo del mensaje de dialogo
            abrir.FileName = "Sin Titulo";
            var resultado = abrir.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                StreamReader leer = new StreamReader(abrir.FileName);
                rtxtProgramaFuente.Text = leer.ReadToEnd();
                leer.Close();
            }
            rtxtProgramaFuente.Enabled = false;
        }
        private void btnEditarPrograma_Click(object sender, EventArgs e)
        {
            rtxtProgramaFuente.Enabled = true;
        }

        private void btnGuardarPrograma_Click(object sender, EventArgs e)
        {
            SaveFileDialog Guardar = new SaveFileDialog();
            Guardar.Filter = "Archivos de texto (.txt)|*.txt|Todos los archivos (*.*)|*.*"; //se abre cualquier tipo.
            Guardar.Title = "Guardar como..."; //titulo del mensaje de dialogo
            Guardar.FileName = "Sin Titulo";
            var resultado = Guardar.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                StreamWriter escribir = new StreamWriter(Guardar.FileName);
                foreach (object line in rtxtProgramaFuente.Lines)
                {
                    escribir.WriteLine(line);
                }
                escribir.Close();
            }
        }
        private void btnGuardarArchivo_Click(object sender, EventArgs e)
        {
            SaveFileDialog Guardar = new SaveFileDialog();
            Guardar.Filter = "Archivos de texto (.txt)|*.txt|Todos los archivos (*.*)|*.*"; //se abre cualquier tipo.
            Guardar.Title = "Guardar como..."; //titulo del mensaje de dialogo
            Guardar.FileName = "ArchivoTokens";
            var resultado = Guardar.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                StreamWriter escribir = new StreamWriter(Guardar.FileName);
                foreach (object line in rtxtArchivoTokens.Lines)
                {
                    escribir.WriteLine(line);
                }
                escribir.Close();
            }
        }
        void ActualizarTablaSimbolosIdentificadores(List<Identificador> lstIden, List<Constante> lstConst)
        {
            dgvTSIdentificador.Rows.Clear();
            foreach (Identificador identificador in lstIden)
            {
                dgvTSIdentificador.Rows.Add(identificador.Numero, identificador.Nombre, identificador.TipoDato, identificador.Valor);
            }
            foreach (Constante constante in lstConst)
            {
                dgvTSIdentificador.Rows.Add("", constante.Nombre, constante.TipoDato, "");
            }
        }

        private void btnGuardarTabla_Click(object sender, EventArgs e)
        {


            SaveFileDialog Guardar = new SaveFileDialog();
            Guardar.Filter = "Archivos de texto (.txt)|*.txt|Todos los archivos (*.*)|*.*"; //se abre cualquier tipo.
            Guardar.Title = "Guardar como..."; //titulo del mensaje de dialogo
            Guardar.FileName = "TablaSimbolos";
            var resultado = Guardar.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                StreamWriter escribir = new StreamWriter(Guardar.FileName);
                for (int i = 0; i < dgvTSIdentificador.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvTSIdentificador.Columns.Count; j++)
                    {
                        escribir.Write("\t" + dgvTSIdentificador.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    escribir.WriteLine("");
                    escribir.WriteLine("-----------------------------------------------------");
                }
                escribir.Close();
            }
            MessageBox.Show("Guardado correctamente");        
        }


    }
}
