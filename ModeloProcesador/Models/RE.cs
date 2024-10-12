using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class RE : _0FlipFlopBase
    {
        public RE()
        {
        }

        public RE Calcular_RE(string fase, List<string> listSalAct, string szvc)
        {
            string comp = "";
            comp = listSalAct.Letra(Salidas.CRE);
            Calcular(fase, comp, szvc);
            return this;
        }
    }
}
