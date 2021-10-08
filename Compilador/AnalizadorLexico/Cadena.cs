using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Cadena
    {
        private char[] _intCaracteres;
        public char[] Caracteres
        {
            get { return _intCaracteres; }
            set { _intCaracteres = value; }
        }
    }
}
