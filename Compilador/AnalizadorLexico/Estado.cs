using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Estado
    {
        private string _intnumeroEstado;
        public string NumeroEstado
        {
            get { return _intnumeroEstado; }
            set { _intnumeroEstado = value; }
        }
    }
}
