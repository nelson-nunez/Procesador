using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class B : _0FlipFlopBase
    {
        public B()
        {
        }

        public B Calcular_B(string fase, List<string> listSalAct, string entradagral)
        {
            string comp = "";
            comp = listSalAct.Letra(Salidas.CB);
            Calcular(fase, comp, entradagral);
            return this;
        }
    }
}
