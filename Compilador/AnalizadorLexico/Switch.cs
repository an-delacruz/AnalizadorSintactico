using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Switch
    {
        public List<Caso> lstCasos = new List<Caso>();
        private string _strCadena;

        public string Cadena
        {
            get { return _strCadena; }
            set { _strCadena = value; }
        }
        private int _intnumLinea;

        public int NumLinea
        {
            get { return _intnumLinea; }
            set { _intnumLinea = value; }
        }

    }
}
