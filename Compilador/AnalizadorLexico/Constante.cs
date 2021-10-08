using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Constante
    {
        private string _strNombre;

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        private string _strTipoDato;

        public string TipoDato
        {
            get { return _strTipoDato; }
            set { _strTipoDato = value; }
        }

    }
}
