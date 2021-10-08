using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Identificador 
    {
        private int _intNumero;

        public int Numero
        {
            get { return _intNumero; }
            set { _intNumero = value; }
        }
        private string _strNombre;

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        private string _strtipoDato;

        public string TipoDato
        {
            get { return _strtipoDato; }
            set { _strtipoDato = value; }
        }
        private string _strValor;

        public string Valor
        {
            get { return _strValor; }
            set { _strValor = value; }
        }
    }
}
