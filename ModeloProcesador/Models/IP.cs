using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class IP: _0FlipFlopBase
    {
        public IP()
        {
        }

        public IP Calcular_IP(string fase, List<string> listSalAct, string salidaUal, string salidadeIp)
        {
            var cip = listSalAct.Letra(Salidas.CIP);
            var M = listSalAct.Letra(Salidas.M);
            //Agrego el funci del sumador
            int sumaTemporal = Convert.ToInt32(salidadeIp, 2) + Convert.ToInt32("0010", 2);
            string retro = Convert.ToString(sumaTemporal, 2).PadLeft(4, '0');


            //Agrego el func del multi a la entrada
            string entradagral = (M == "1") ? salidaUal : retro;
            
            //Esto en el caso que sea indirecto
            //if (!string.IsNullOrEmpty(indirecto))
            //    entradagral = Convert.ToInt32(entradagral, 2) + Convert.ToInt32(indirecto, 2).ToString();

            Calcular(fase, cip, entradagral);
            return this;
        }
    }
}
