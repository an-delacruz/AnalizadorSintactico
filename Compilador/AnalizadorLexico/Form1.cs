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
        List<string> lstErroresSintacticosSemanticos = new List<string>();
        List<string> lsttipoDatos = new List<string>() { "CNEN","CNDE","CNEX","CADE","CARA","PR09","PR24"};
        
        public Form1()
        {
            InitializeComponent();

        }
        private void btnAnalizador_Click(object sender, EventArgs e)
        {
            
            try
            {
                Automata.lstOPAG.Clear();
                Automata.lstIdentificadores.Clear();
                Automata.lstCondiciones.Clear();
                Automata.lstSwitches.Clear();
                #region Léxico
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
                ActualizarTablaSimbolosIdentificadores(Automata.lstIdentificadores, miAutomata.lstConstantes);


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
                #region SintacticoSemántica
                rtxtDerivaciones.Clear();
                string instrucciones = rtxtArchivoTokens.Text.Trim();
                //Arreglo para almacenar cada linea en una casilla diferente del arreglo para hacer la busqueda por instrucción
                string[] arrTokens = instrucciones.Split('\n').ToArray();
                List<string> lstTokens = new List<string>();
                lstTokens = arrTokens.ToList();
                string[] arrResultado = new string[arrTokens.Length];
                List<string> lstCorchetes = new List<string>();

                //Verificar que la primera palabra sea INICIO y la ultima FIN y que solo haya una.
                if (!arrTokens[0].Equals("PR15")){lstErroresSintacticosSemanticos.Add("Linea " + 1 + ": Error de semántica - Se esperaba la instrucción INICIO");}
                if (!arrTokens[arrTokens.Length - 1].Equals("PR10")) { lstErroresSintacticosSemanticos.Add("Linea " + arrTokens.Length + ": Error de semántica - Se esperaba la instrucción FIN"); }
                
                //Verificar que todos los identificadores usados hayan sido declarados.
                foreach (Identificador identificador in Automata.lstIdentificadores)
                {
                    if (identificador.TipoDato == null || identificador.TipoDato == "")
                    {
                        for (int i = 0; i < arrTokens.Length; i++)
                        {
                            if (arrTokens[i].Contains("ID" + identificador.Numero.ToString().PadLeft(2, '0')))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + (i+1) + ": Error de semántica - Identificador no declarado");
                            }
                        }
                    }
                }

                for (int i = 0; i < arrTokens.Length; i++)
                {
                    if (arrTokens[i].Contains("TABU"))
                    {
                        arrTokens[i] = arrTokens[i].Replace("TABU", "").Trim();
                    }

                    if((arrTokens[i].Equals("PR10") || arrTokens[i].Equals("PR15")) && i != 0 && i != arrTokens.Length-1) { lstErroresSintacticosSemanticos.Add("Linea " + (i+1) + ": Error de semántica - Instrucción inesperada");}
                }

                for (int i = 0; i < arrTokens.Length; i++)
                {
                    string sino = "";
                    //lstDerivaciones.Add(arrTokens[i]);
                    if (arrTokens[i].Contains("PR21") || arrTokens[i].Contains("PR23") || arrTokens[i].Contains("PR05")
                        || arrTokens[i].Contains("PR19") || arrTokens[i].Contains("PR12"))
                    {
                        Switch miSwitch = new Switch();
                        if (arrTokens[i].Contains("PR23"))
                        {
                            miSwitch.Cadena = arrTokens[i];
                            miSwitch.NumLinea = i+1;
                        }
                        if (arrTokens[i + 1].Contains("CE07"))
                        //if (lstCorchetes.Contains("ABIERTO"))
                        {

                            arrTokens[i] = arrTokens[i] + " " + arrTokens[i + 1];
                            //arrTokens[i] = arrTokens[i] + " " + "CE03";
                            arrTokens[i+1] = "";
                            string ultimocaseevaluado = "";
                            string listacase = "";
                            for (int z = i + 1; z < arrTokens.Length; z++)
                            {

                                //Acomodo de los casos, fincasos y default
                                if (arrTokens[i].Contains("PR23") && (arrTokens[z].Contains("PR04") || arrTokens[z].Contains("PR06")) && !arrTokens[i].Contains("CE08"))
                                {

                                    listacase = ultimocaseevaluado;
                                    string caso = "";
                                    if (arrTokens[z].Contains("PR06"))
                                    {
                                        listacase = listacase + " " + arrTokens[z];

                                        listacase = listacase.Trim();
                                    }
                                    for (int y = z; y < arrTokens.Length; y++)
                                    {
                                        if (arrTokens[y] == "PR11" && arrTokens[z].Contains("PR04"))
                                        {
                                            int defaultIndex = Array.FindIndex(arrTokens, x => x.Contains("PR06"));
                                            if (defaultIndex > y || defaultIndex == -1)
                                            {
                                                caso = arrTokens[z] + " " + arrTokens[y];
                                                arrTokens[y] = "";
                                                arrTokens[z] = "";
                                                break;
                                            }
                                        }
                                        if (arrTokens[y] == "PR11" && arrTokens[z].Contains("PR06"))
                                        {
                                            listacase = listacase + " " + arrTokens[y];
                                            arrTokens[y] = "";
                                            arrTokens[z] = "";
                                            break;
                                        }
                                    }
                                    string rescaso = "";
                                    if (caso != "")
                                    {
                                        rescaso = BottomUp(caso, 0, z+1);
                                    }
                                    if (rescaso == "LISTACASE" || listacase.Contains("PR06"))
                                    {
                                        if(caso != "")
                                        {
                                            Caso miCaso = new Caso();
                                            miCaso.Cadena = caso;
                                            miCaso.NumLinea = z+1;
                                            miSwitch.lstCasos.Add(miCaso);
                                        }
                                        ultimocaseevaluado = rescaso;
                                    }
                                    else
                                    {
                                        if (rescaso != "LISTACASE" && !listacase.Contains("PR06"))
                                        {
                                            if (!caso.Contains("PR04"))
                                            {
                                                lstErroresSintacticosSemanticos.Add("Linea " + (z + 1) + ": Error de sintaxis - Se esperaba la instrucción CASE");
                                            }
                                            else if (!caso.Contains("CE16"))
                                            {
                                                lstErroresSintacticosSemanticos.Add("Linea " + (z + 1) + ": Error de sintaxis - Se esperaba :");
                                            }
                                            else if (!caso.Contains("PR11"))
                                            {
                                                lstErroresSintacticosSemanticos.Add("Linea " + (z + 1) + ": Error de sintaxis - Se esperaba la instrucción FINCASO");
                                            }
                                            else
                                            {
                                                lstErroresSintacticosSemanticos.Add("Linea " + (z + 1) + ": Error de sintaxis - Error en el CASO");
                                            }

                                        }
                                    }


                                }
                                if (arrTokens[z].Contains("CE08") && !arrTokens[i].Contains("CE08") && !arrTokens[i].Contains("PR12") && !arrTokens[z].Contains("PR19"))
                                {

                                    arrTokens[i] = arrTokens[i] + " " + listacase;
                                    arrTokens[i] = arrTokens[i].Trim();
                                    arrTokens[i] = arrTokens[i] + " " + arrTokens[z];
                                    arrTokens[z] = "";
                                    if (arrTokens[i].Contains("PR23"))
                                    {
                                        miSwitch.Cadena = arrTokens[i];
                                        Automata.lstSwitches.Add(miSwitch);
                                    }
                                    lstCorchetes.Add("PAR");
                                    if (!arrTokens[i].Contains("PR21"))
                                    {
                                        break;
                                    }
                                }
                                if (arrTokens[z].Contains("CE08")  &&  arrTokens[i].Contains("PR12") && arrTokens[z].Contains("PR19"))
                                {

                                    arrTokens[i] = arrTokens[i] + " " + listacase;
                                    arrTokens[i] = arrTokens[i].Trim();
                                    arrTokens[i] = arrTokens[i] + " " + arrTokens[z];
                                    arrTokens[z] = "";
                                    lstCorchetes.Add("PAR");
                                    if (!arrTokens[i].Contains("PR21"))
                                    {
                                        break;
                                    }
                                }
                                //Acomodo del sino para el sí y su verficación
                                if (arrTokens[i].Contains("PR21") && arrTokens[z].Contains("PR22"))
                                {
                                   
                                    sino = arrTokens[z];
                                    arrTokens[z] = "";
                                    if (arrTokens[z+1].Contains("CE07"))
                                    {
                                        sino = sino + " " + arrTokens[z+1];
                                        arrTokens[z + 1] = "";
                                        lstCorchetes.Add("IMPAR");
                                        for (int y = z+1; y < arrTokens.Length; y++)
                                        {
                                            if (arrTokens[y].Contains("CE08"))
                                            {
                                                sino = sino + " " + arrTokens[y];
                                                arrTokens[y] = "";
                                                lstCorchetes.Add("PAR");
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lstErroresSintacticosSemanticos.Add("Linea " + (z+1) + ": Error de semántica - Corchete no abierto");
                                    }
                                    if (!sino.Contains("CE08"))
                                    {
                                        lstErroresSintacticosSemanticos.Add("Linea " + (z+1) + ": Error de semántica - Corchete abierto pero no cerrado");
                                    }
                                    sino = BottomUp(sino,0,z);
                                }
                            }
                            if (!arrTokens[i].Contains("CE08"))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + (i+1) + ": Error de semántica - Corchete abierto pero no cerrado");
                            }
                        }
                        else
                        {
                            lstErroresSintacticosSemanticos.Add("Linea " + (i+1) + ": Error de semántica - Corchete no abierto");
                        }
                    }
                    if(!arrTokens[i].Contains("Error"))
                    {
                        if (arrTokens[i].Contains("PR21"))
                        {
                            string si = arrTokens[i] + " " + sino;
                            lstDerivaciones.Add(si);
                            
                            arrResultado[i] = BottomUp(si, 0, i + 1);
                        }
                        else
                        {
                            lstDerivaciones.Add(arrTokens[i]);
                            arrResultado[i] = BottomUp(arrTokens[i], 0, i + 1);
                        }

                        lstDerivaciones.Add(arrResultado[i]);
                    }
                }


                string derivaciones = "";
                foreach (string derivacion in lstDerivaciones)
                {
                    derivaciones = derivaciones + derivacion + "\n";
                }
                rtxtDerivaciones.Text = derivaciones;

                #region Balanceo parentesiscorchetesllaves
                //Balanceo de parentesis
                int balanceParentesis = 0;
                for (int i = 0; i < arrTokens.Length; i++)
                {
                    if (arrTokens[i].Contains("CE03"))
                    {
                        balanceParentesis = balanceParentesis + Regex.Matches(arrTokens[i], "CE03").Count;
                    }
                    if (arrTokens[i].Contains("CE04"))
                    {
                        balanceParentesis = balanceParentesis - Regex.Matches(arrTokens[i], "CE04").Count; 
                    }
                }

                if (balanceParentesis != 0)
                {
                    if (balanceParentesis > 0)
                    {
                        for (int i = arrTokens.Length-1; i >= 0; i--)
                        {
                            if (arrTokens[i].Contains("CE03") && (Regex.Matches(arrTokens[i], "CE03").Count > Regex.Matches(arrTokens[i], "CE04").Count))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + (i + 1) + ": Error de semántica - Parentesis abierto pero no cerrado");
                            }
                        }
                    }
                    else if (balanceParentesis < 0)
                    {
                        for (int i = arrTokens.Length-1; i >= 0; i--)
                        {
                            if (arrTokens[i].Contains("CE04") && (Regex.Matches(arrTokens[i], "CE04").Count > Regex.Matches(arrTokens[i], "CE03").Count))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + (i + 1) + ": Error de semántica - Parentesis no abierto");
                            }
                        }
                    }
                }
                //Balanceo de corchetes
                int balanceoCorchetes = 0;
                for (int i = 0; i < arrTokens.Length; i++)
                {
                    if (arrTokens[i].Contains("CE07"))
                    {
                        balanceoCorchetes = balanceoCorchetes + Regex.Matches(arrTokens[i], "CE07").Count; ;
                    }
                    if (arrTokens[i].Contains("CE08"))
                    {
                        balanceoCorchetes = balanceoCorchetes - Regex.Matches(arrTokens[i], "CE08").Count; ;
                    }
                }

                if (balanceoCorchetes != 0)
                {
                    if (balanceoCorchetes > 0)
                    {
                        lstErroresSintacticosSemanticos.Add("Linea " + (Array.LastIndexOf(arrTokens, "CE07") +1) +": Error de semántica - Corchete abierto pero no cerrado");
                    }

                    else if (balanceoCorchetes < 0)
                    {
                        lstErroresSintacticosSemanticos.Add("Linea " + (Array.LastIndexOf(arrTokens, "CE08") + 1) + ": Error de semántica - Corchete no abierto");
                    }
                }
                //Balanceo de parentesis
                int balanceoLlaves = 0;
                for (int i = 0; i < arrTokens.Length; i++)
                {
                    if (arrTokens[i].Contains("CE05"))
                    {
                        balanceoLlaves = balanceoLlaves + Regex.Matches(arrTokens[i],"CE05").Count;
                    }
                    if (arrTokens[i].Contains("CE06"))
                    {
                        balanceoLlaves = balanceoLlaves - Regex.Matches(arrTokens[i], "CE06").Count;
                    }
                }

                if (balanceoLlaves != 0)
                {
                    if (balanceoLlaves > 0)
                    {
                        for (int i = arrTokens.Length - 1; i >= 0; i--)
                        {
                            if (arrTokens[i].Contains("CE05") && (Regex.Matches(arrTokens[i], "CE05").Count > Regex.Matches(arrTokens[i], "CE06").Count))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + (i + 1) + ": Error de semántica - Llave abierta pero no cerrada");
                            }
                        }
                    }
                    else if (balanceoLlaves < 0)
                    {
                        for (int i = arrTokens.Length - 1; i >= 0; i--)
                        {
                            if (arrTokens[i].Contains("CE06") && (Regex.Matches(arrTokens[i], "CE06").Count > Regex.Matches(arrTokens[i], "CE05").Count))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + (i + 1) + ": Error de semántica - Llave no abierta");
                            }
                        }
                    }
                }
                #endregion

                #region Tipos de datos asignaciones
                foreach (Asignacion Asig in Automata.lstOPAG)
                {
                    string id = Asig.Cadena.Substring(0, 4);
                    int numID = int.Parse(id.Substring(2));
                    string[] tipoDatoAsignacion = new string[2];
                    foreach (Identificador identificador in Automata.lstIdentificadores)
                    {
                        if (identificador.Numero == numID && identificador.TipoDato != null)
                        {
                            tipoDatoAsignacion[0] = identificador.TipoDato;
                            if (identificador.TipoDato == "REAL")
                            {
                                tipoDatoAsignacion[1] = "ENTERO";
                            }
                            break;
                        }
                    }
                    string[] operandosAsignacion = Asig.Cadena.Substring(Asig.Cadena.IndexOf("OPAS")).Split(' ').ToArray();
                    if (tipoDatoAsignacion[0].Equals("BOOL") || tipoDatoAsignacion[0].Equals("CARACTER") && operandosAsignacion.Length > 1)
                    {
                        lstErroresSintacticosSemanticos.Add("Linea " + Asig.NumLinea + ": Error de semántica - Asignación invalida");
                    }
                    else if(tipoDatoAsignacion[0].Equals("CADENA") && (operandosAsignacion.Contains("OPA2") || operandosAsignacion.Contains("OPA3") || operandosAsignacion.Contains("OPA4")))
                    {
                        lstErroresSintacticosSemanticos.Add("Linea " + Asig.NumLinea + ": Error de semántica - Asignación invalida");
                    }
                    else
                    {
                        for (int j = 0; j < operandosAsignacion.Length; j++)
                        {
                            if (!operandosAsignacion[j].Contains("OPA") && !operandosAsignacion[j].Equals("CE05") && !operandosAsignacion[j].Equals("CE06"))
                            {
                                if (operandosAsignacion[j].Contains("ID"))
                                {
                                    Identificador idenActual = BuscarIdentificador(int.Parse(operandosAsignacion[j].Substring(3)));
                                    if (!tipoDatoAsignacion.Contains(idenActual.TipoDato))
                                    {

                                        lstErroresSintacticosSemanticos.Add("Linea " + Asig.NumLinea + ": Error de semántica - Se esperaba un tipo de dato " + tipoDatoAsignacion[0]);
                                    }
                                }
                                else
                                {
                                    if (lsttipoDatos.Contains(operandosAsignacion[j]))
                                    {
                                        string tipoDatoConstante = "";
                                        switch (operandosAsignacion[j])
                                        {
                                            case "CNEN":
                                                tipoDatoConstante = "ENTERO";
                                                break;
                                            case "CNDE":
                                                tipoDatoConstante = "REAL";
                                                break;
                                            case "CNEX":
                                                tipoDatoConstante = "REAL";
                                                break;
                                            case "PR09":
                                                tipoDatoConstante = "BOOL";
                                                break;
                                            case "PR24":
                                                tipoDatoConstante = "BOOL";
                                                break;
                                            case "CADE":
                                                tipoDatoConstante = "CADENA";
                                                break;
                                            case "CARA":
                                                tipoDatoConstante = "CARACTER";
                                                break;
                                        }
                                        if (!tipoDatoAsignacion.Contains(tipoDatoConstante))
                                        {
                                            lstErroresSintacticosSemanticos.Add("Linea " + Asig.NumLinea + ": Error de semántica - Se esperaba un tipo de dato " + tipoDatoAsignacion[0]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Tipos de datos condiciones
                foreach (Condicion cond in Automata.lstCondiciones)
                {
                    if (cond.Cadena.Contains("PR05"))
                    {
                        //Validación de asignación en DESDE
                        string[] desde = cond.Cadena.Substring(cond.Cadena.IndexOf("PR07") + 4, cond.Cadena.IndexOf("PR19") - cond.Cadena.IndexOf("PR07") - 4).Trim().Split(' ');
                        if (desde[0].Contains("ID"))
                        {
                            int numID = int.Parse(desde[0].Substring(2));
                            Identificador miIden = BuscarIdentificador(numID);
                            if (miIden.TipoDato != "ENTERO")
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Se esperaba un identificador ENTERO");
                            }
                        }
                        if(desde[2].Contains("ID"))
                        {
                            int numID = int.Parse(desde[0].Substring(2));
                            Identificador miIden = BuscarIdentificador(numID);
                            if (miIden.TipoDato != "ENTERO")
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Se esperaba un tipo de dato ENTERO");
                            }
                        }
                        else if(desde[2] != "CNEN")
                        {
                            lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Se esperaba un tipo de dato ENTERO");
                        }
                        //Validación de los tipos de datos en la condición
                        string[] mientras = cond.Cadena.Substring(cond.Cadena.IndexOf("PR19") + 4, cond.Cadena.IndexOf("PR14") - cond.Cadena.IndexOf("PR19") - 4).Trim().Split(' ');
                        for (int i = 0; i < mientras.Length; i++)
                        {
                            if (mientras[i].Contains("ID"))
                            {
                                int numID = int.Parse(mientras[i].Substring(2));
                                Identificador miIden = BuscarIdentificador(numID);
                                if (miIden.TipoDato != "ENTERO" && miIden.TipoDato != "REAL")
                                {
                                    lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Idenficador invalido en la condición");
                                }
                            }
                            if (mientras[i].Equals("CADENA") || mientras[i].Equals("BOOL") || mientras[i].Equals("CARACTER"))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Tipo de dato invalido en la condición");
                            }
                        }
                        //Validación de los tipos de datos en el incremento
                        string[] incremento = cond.Cadena.Substring(cond.Cadena.IndexOf("PR14") + 4, cond.Cadena.IndexOf("CE04") - cond.Cadena.IndexOf("PR14") - 4).Trim().Split(' ');
                        if (incremento[0].Contains("ID"))
                        {
                            int numID = int.Parse(incremento[0].Substring(2));
                            Identificador miIden = BuscarIdentificador(numID);
                            if (miIden.TipoDato != "ENTERO")
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Identificador invalido en el incremento");
                            }
                        }
                        if (incremento[2] != "CNEN")
                        {
                            lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Se esperaba un tipo de dato ENTERO");
                        }
                    }
                    else if(cond.Cadena.Contains("PR19") || cond.Cadena.Contains("PR21") && !cond.Cadena.Contains("PR05"))
                    {
                        string[] condic = cond.Cadena.Substring(cond.Cadena.IndexOf("CE03") + 4, cond.Cadena.IndexOf("CE04") - cond.Cadena.IndexOf("CE03") - 4).Trim().Split(' ');
                        for (int i = 0; i < condic.Length; i++)
                        {
                            if (condic[i].Contains("ID"))
                            {
                                int numID = int.Parse(condic[i].Substring(2));
                                Identificador miIden = BuscarIdentificador(numID);
                                if (miIden.TipoDato != "ENTERO" && miIden.TipoDato != "REAL")
                                {
                                    lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Idenficador invalido en la condición");
                                }
                            }
                            if (condic[i].Equals("CADENA") || condic[i].Equals("BOOL") || condic[i].Equals("CARACTER"))
                            {
                                lstErroresSintacticosSemanticos.Add("Linea " + cond.NumLinea + ": Error de semántica - Tipo de dato invalido en la condición");
                            }
                        }
                    }
                }
                #endregion

                #region TIpo de datos switch
                foreach (Switch @switch in Automata.lstSwitches)
                {
                    string condicSwitch = @switch.Cadena.Substring(@switch.Cadena.IndexOf("CE03") + 4, @switch.Cadena.IndexOf("CE04") - @switch.Cadena.IndexOf("CE03") - 4).Trim();
                    string tipoDatoSwitch = "";
                    if (condicSwitch.Contains("ID"))
                    {
                        int numID = int.Parse(condicSwitch.Substring(2));
                        Identificador miIden = BuscarIdentificador(numID);
                        tipoDatoSwitch = miIden.TipoDato;
                    }
                    foreach (Caso caso in @switch.lstCasos)
                    {
                        string[] condicCaso = caso.Cadena.Substring(caso.Cadena.IndexOf("PR04") + 4, caso.Cadena.IndexOf("CE16") - caso.Cadena.IndexOf("PR04") - 4).Trim().Split(' ');
                        if (condicCaso.Length > 1)
                        {
                            for (int i = 0; i < condicCaso.Length; i++)
                            {
                                if (condicCaso[i].Contains("ID"))
                                {
                                    int numID = int.Parse(condicCaso[i].Substring(2));
                                    Identificador miIden = BuscarIdentificador(numID);
                                    if (miIden.TipoDato != "ENTERO" && miIden.TipoDato != "REAL" && (tipoDatoSwitch == "ENTERO" || tipoDatoSwitch == "REAL"))
                                    {
                                        lstErroresSintacticosSemanticos.Add("Linea " + caso.NumLinea + ": Error de semántica - Idenficador invalido en la condición");
                                    }
                                }
                                if (condicCaso[i].Equals("CADENA") || condicCaso[i].Equals("BOOL") || condicCaso[i].Equals("CARACTER") && (tipoDatoSwitch == "ENTERO" || tipoDatoSwitch == "REAL"))
                                {
                                    lstErroresSintacticosSemanticos.Add("Linea " + caso.NumLinea + ": Error de semántica - Tipo de dato invalido en la condición");
                                }
                            }
                        }
                        else if(condicCaso.Length == 1)
                        {
                            if (condicCaso[0].Contains("ID"))
                            {
                                int numID = int.Parse(condicCaso[0].Substring(2));
                                Identificador miIden = BuscarIdentificador(numID);
                                if (miIden.TipoDato != tipoDatoSwitch)
                                {
                                    lstErroresSintacticosSemanticos.Add("Linea " + caso.NumLinea + ": Error de semántica - Tipo de dato invalido");
                                }
                            }
                            else
                            {
                                string tipoDatoConstante = "";
                                switch (condicCaso[0])
                                {
                                    case "CNEN":
                                        tipoDatoConstante = "ENTERO";
                                        break;
                                    case "CNDE":
                                        tipoDatoConstante = "REAL";
                                        break;
                                    case "CNEX":
                                        tipoDatoConstante = "REAL";
                                        break;
                                    case "PR09":
                                        tipoDatoConstante = "BOOL";
                                        break;
                                    case "PR24":
                                        tipoDatoConstante = "BOOL";
                                        break;
                                    case "CADE":
                                        tipoDatoConstante = "CADENA";
                                        break;
                                    case "CARA":
                                        tipoDatoConstante = "CARACTER";
                                        break;
                                }
                                if (tipoDatoConstante != tipoDatoSwitch)
                                {
                                    lstErroresSintacticosSemanticos.Add("Linea " + caso.NumLinea + ": Error de semántica - Tipo de dato invalido");
                                }
                            }
                        }
                    }
                }
                #endregion
                foreach (string error in lstErroresSintacticosSemanticos)
                {
                    Errores = Errores + error + "\n";
                }
                rtxtErrores.Text = Errores;
                txtErrores.Text = (miAutomata.lstErrores.Count + lstErroresSintacticosSemanticos.Count).ToString();

                lstDerivaciones.Clear();
                lstErroresSintacticosSemanticos.Clear();                
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
            string cadenaOriginal = cadena;
            string resultado = "";
            int LongitudCadena = cadena.Trim().Split(' ').Length;
            string cadenaActual = "";
            string cadenaAuxiliar = "";
            bool cadenaCompleta = true;
            //Arreglo con los tokens que contiene la cadena que se va a reducir.
            string[] TokensCadena = cadena.Trim().Split(' ');
            int c = 0;
            if (cadena == "")
            {
                return cadena;
            }
            while (c < TokensCadena.Length)
            {
                //Si la posición actual es mayor al número de tokens se devuelve la cadena (Aquí creo que también se debe manejar lo de errores)
                if (posicionInicial > TokensCadena.Length)
                {
                    if (cadena != "LISTACASE" && cadena != "SINO")
                    {
                        lstErroresSintacticosSemanticos.Add("Linea " + numLinea + ": Error de sintaxis");
                    }
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
                    if (cadenaActual.Trim().Split(' ').ToArray().Length == TokensCadena.Length)
                    {
                        cadenaCompleta = true;
                    }
                    else cadenaCompleta = false;
                    resultado = BuscarGramatica(cadenaActual, cadenaCompleta);
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
                        if (resultado == "OPAG")
                        {
                            Asignacion miAsig = new Asignacion();
                            miAsig.Cadena = cadenaOriginal;
                            miAsig.NumLinea = numLinea;
                            Automata.lstOPAG.Add(miAsig);
                        }
                        if (resultado == "COND")
                        {
                            Condicion miCond = new Condicion();
                            miCond.Cadena = cadenaOriginal;
                            miCond.NumLinea = numLinea;
                            Automata.lstCondiciones.Add(miCond);
                        }
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
        string BuscarGramatica(string cadenaOriginal, bool cadenaCompleta)
        {
            string cadena = cadenaOriginal;
            string resultado = "";
            string cadenacon = "Data Source=DESKTOP-ANTONIO\\SQLEXPRESS;Initial Catalog=AUTOMATAS;Integrated Security=True";
            if (cadena.Contains("ID"))
            {
                cadena = ReemplazarIDVA(cadena);

            }
            using (SqlConnection cnn = new SqlConnection(cadenacon))
            {
                cnn.Open();
                if (cadenaCompleta)
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT count(*) from Gramaticas where Gramatica ='" + cadena + "' AND (Simbolo = 'S' OR Simbolo = 'OPAG' OR Simbolo = 'SINO' OR Simbolo = 'CASE' OR Simbolo = 'LISTACASE')", cnn);
                    int coincidencias = int.Parse(sqlCommand.ExecuteScalar().ToString());
                    if (coincidencias > 0)
                    {
                        sqlCommand = new SqlCommand("SELECT Simbolo from Gramaticas where Gramatica ='" + cadena + "' AND (Simbolo = 'S' OR Simbolo = 'OPAG' OR Simbolo = 'SINO' OR Simbolo = 'CASE' OR Simbolo = 'LISTACASE')", cnn);
                        resultado = sqlCommand.ExecuteScalar().ToString();
                    }
                    else
                    {
                        resultado = cadena;
                    }
                }
                else if (!cadenaCompleta)
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT count(*) from Gramaticas where Gramatica ='" + cadena + "' AND Simbolo != 'S'  AND Simbolo !='OPAG'", cnn);
                    int coincidencias = int.Parse(sqlCommand.ExecuteScalar().ToString());
                    if (coincidencias > 0)
                    {
                        sqlCommand = new SqlCommand("SELECT Simbolo from Gramaticas where Gramatica ='" + cadena + "' AND Simbolo != 'S' AND Simbolo !='OPAG'", cnn);
                        resultado = sqlCommand.ExecuteScalar().ToString();
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

        Identificador BuscarIdentificador(int numIden)
        {
            Identificador miIden = Automata.lstIdentificadores.Where(iden => iden.Numero == numIden).FirstOrDefault();
            return miIden;
        }
    }
}
