using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQL_com_C_
{

    public class Sessao
    {
        public int IdSessao { get; set; }
        public int IdFilme { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
    }

}