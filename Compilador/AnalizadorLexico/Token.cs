using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Token
    {
        private string _strcat;

        public string CAT
        {
            get { return _strcat; }
            set { _strcat = value; }
        }

        private int _intAcomodo;        //Se crea esta propiedad para axuliar al acomodo del archivo token
        public int Acomodo
        {
            get { return _intAcomodo; }
            set { _intAcomodo = value; }
        }
    }
}
