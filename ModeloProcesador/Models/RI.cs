using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class RI_Izquierdo : _0FlipFlopBase
    {
        public RI_Izquierdo()
        {
        }

        public RI_Izquierdo Calcular_RI_Izq(string fase, List<string> listSalAct, BusDir_Memoria bus)
        {
            var entradagral = bus.RDA2;
            string comp = "";
            comp = listSalAct.Letra(Salidas.CRI);
            Calcular(fase, comp, entradagral);
            return this;
        }
    }

    public class RI_Derecho : _0FlipFlopBase
    {
        public RI_Derecho()
        {
        }

        public RI_Derecho Calcular_RI_Der(string fase, List<string> listSalAct, BusDir_Memoria bus)
        {
            var entradagral = bus.RDA1;
            string comp = "";
            comp = listSalAct.Letra(Salidas.CRI);
            Calcular(fase, comp, entradagral);
            return this;
        }
    }
}

