using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AnalizadorLexico
{
    class Automata
    {
        public List<Token> lstTokens = new List<Token>();
        public List<Error> lstErrores = new List<Error>();
        public static List<Identificador> lstIdentificadores = new List<Identificador>();
        public List<Constante> lstConstantes = new List<Constante>();
        List<string> lsttipoDatos = new List<string>(new string[] { "ENTERO", "CADENA", "CARACTER", "BOOL", "REAL" });
        public static List<Asignacion> lstOPAG = new List<Asignacion>();
        public static List<Condicion> lstCondiciones = new List<Condicion>();
        public static List<Switch> lstSwitches = new List<Switch>();

        private Estado _EstadoActual;
        public Estado EstadoActual
        {
            get { return _EstadoActual; }
            set { _EstadoActual = value; }
        }

        private string  _strFDC;

        public string  FDC
        {
            get { return _strFDC; }
            set { _strFDC = value; }
        }


        public Automata()
        {
            EstadoActual = new Estado();
            EstadoActual.NumeroEstado = 0.ToString();
            FDC = ";";
        }
        public void BuscarSiguienteEstado(string caracterActual, EjecutorQuerys conexion, int acomodo, int noLinea, string[] cadenas, int numCadena)  //int acomodo es un auxiliar que ocupo para poder acomodar el archivo de tokens.
        {

            Token miToken = new Token();
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            da = new SqlDataAdapter("SELECT \"" + caracterActual + "\", CAT FROM Matriz WHERE ESTADOS = " + EstadoActual.NumeroEstado, conexion.Conectar());
            conexion.Desconectar();
            da.Fill(dt);
            EstadoActual.NumeroEstado = dt.Rows[0][caracterActual].ToString();
            miToken.CAT = dt.Rows[0]["CAT"].ToString();
            if (EstadoActual.NumeroEstado.Contains("Error"))
            {
                Error miError = new Error();
                miError.CatError = EstadoActual.NumeroEstado;
                miError.NoLinea = noLinea;
                lstErrores.Add(miError);

                //Si existe error se guarda un token denominado ERROR
                miToken.CAT = EstadoActual.NumeroEstado;
                miToken.Acomodo = acomodo;  //Cree una propiedad acomodo que me sirve para acomodar el archivo token.
                lstTokens.Add(miToken);

                EstadoActual.NumeroEstado = 0.ToString();
            }
            else
            {
                if (caracterActual == ";")
                {
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2;
                    da2 = new SqlDataAdapter("SELECT \"" + caracterActual + "\" FROM Matriz WHERE ESTADOS = " + EstadoActual.NumeroEstado, conexion.Conectar());
                    conexion.Desconectar();
                    da2.Fill(dt2);
                    FDC = dt2.Rows[0][";"].ToString();
                    if (FDC.Contains("Error"))
                    {
                        Error miError = new Error();
                        miError.CatError = FDC;
                        miError.NoLinea = noLinea;
                        lstErrores.Add(miError);

                        //Si existe error se guarda un token denominado ERROR
                        miToken.CAT = FDC;
                        miToken.Acomodo = acomodo;  //Cree una propiedad acomodo que me sirve para acomodar el archivo token.
                        lstTokens.Add(miToken);

                        EstadoActual.NumeroEstado = 0.ToString();
                    }
                }
                if (FDC == "Acepta")
                {
                    DataTable dataTable1 = new DataTable();
                    SqlDataAdapter da1;
                    da1 = new SqlDataAdapter("SELECT CAT FROM Matriz WHERE ESTADOS = " + EstadoActual.NumeroEstado, conexion.Conectar());
                    conexion.Desconectar();
                    da1.Fill(dataTable1);
                    miToken.CAT = dataTable1.Rows[0]["CAT"].ToString();
                    if (miToken.CAT == "IDVA")
                    {
                        Identificador miIden = new Identificador();
                        miIden.Numero = lstIdentificadores.Count + 1;
                        miIden.Nombre = cadenas[numCadena - 2];
                        if (miIden.Nombre.Contains("\n"))
                        {
                            miIden.Nombre = miIden.Nombre.Replace("\n", "");
                        }
                        if (!existeIdentificador(miIden.Nombre))
                        {
                            miIden.Valor = "";
                            string cadenaAnterior = cadenas[numCadena - 3];
                            if (cadenaAnterior.Contains("\n")) { cadenaAnterior = cadenaAnterior.Replace("\n", ""); }
                            foreach (var tipoDato in lsttipoDatos)
                            {
                                if (tipoDato.Equals(cadenaAnterior, StringComparison.OrdinalIgnoreCase))
                                {
                                    miIden.TipoDato = cadenaAnterior.ToUpper();
                                    break;
                                }
                            }


                            lstIdentificadores.Add(miIden);
                        }
                        foreach (Identificador identificador in lstIdentificadores)
                        {
                            if (identificador.Nombre == miIden.Nombre)
                            {
                                miIden.Numero = identificador.Numero;
                            }
                        }
                        miToken.CAT = "ID" + miIden.Numero.ToString().PadLeft(2,'0');
                    }
                    if(miToken.CAT == "CADE" || miToken.CAT == "CNEN" || miToken.CAT == "CNEX" || miToken.CAT == "CNDE" || miToken.CAT == "PR09" || miToken.CAT == "PR24" || miToken.CAT == "CARA")
                    {
                        Constante miConstante = new Constante();
                        miConstante.Nombre = cadenas[numCadena - 2];
                        if(miConstante.Nombre.Contains("\n"))
                        {
                            miConstante.Nombre = miConstante.Nombre.Replace("\n", "");
                        }
                        switch (miToken.CAT)
                        {
                            case "CNEN":
                                miConstante.TipoDato = "Const. Núm. Entera";
                                break;
                            case "CNDE":
                                miConstante.TipoDato = "Const. Núm. Decimal";
                                break;
                            case "CNEX":
                                miConstante.TipoDato = "Const. núm. Exponencial";
                                break;
                            case "PR09":
                                miConstante.TipoDato = "BOOL";
                                break;
                            case "PR24":
                                miConstante.TipoDato = "BOOL";
                                break;
                            case "CADE":
                                miConstante.TipoDato = "CADENA";
                                break;
                            case "CARA":
                                miConstante.TipoDato = "CARACTER";
                                break;
                            default:
                                break;
                        }
                        lstConstantes.Add(miConstante);
                    }
                    miToken.Acomodo = acomodo;  //Aqui esta de nuevo la propiedad que cree
                    lstTokens.Add(miToken);
                    EstadoActual.NumeroEstado = 0.ToString();
                    FDC = ";";
                }
            }

        }

        public bool existeIdentificador(string nombre)
        {
            bool existe = false;
            if (lstIdentificadores.Count > 0)
            {
                foreach (Identificador ide in lstIdentificadores)
                {
                    if (ide.Nombre == nombre)
                    {
                        existe = true;
                    }
                }
            }
            return existe;
        }
    }
}
