using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class A: _0FlipFlopBase
    {
        public A()
        {
        }

        public A Calcular_A(string fase, List<string> listSalAct, string entradagral)
        {
            string comp = "";
            comp = listSalAct.Letra(Salidas.CA);
            Calcular(fase, comp, entradagral);
            return this;
        }
    }
}
