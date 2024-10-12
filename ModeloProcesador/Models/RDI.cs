using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class RDI : _0FlipFlopBase
    {
        public RDI()
        {
        }

        public RDI Calcular_RDI(string fase, List<string> listSalAct, string entradagral)
        {
            string comp = "";
            comp = listSalAct.Letra(Salidas.CRDI);
            Calcular(fase, comp, entradagral);
            return this;
        }
    }
}
