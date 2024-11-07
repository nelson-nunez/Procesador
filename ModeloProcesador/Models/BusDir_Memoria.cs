using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModeloProcesador.Models
{
    public class BusDir_Memoria
    {
        public string LE { get; set; }
        public string AM { get; set; }
        public string RDA1 { get; set; }
        public string RDA2 { get; set; }

        public BusDir_Memoria()
        {
            RDA1 = "XXXX";
            RDA2 = "XXXX";
        }

        public static readonly Dictionary<string, string> Constantes = new Dictionary<string, string>
        {
            { "0000", "0111"},
            { "0001", "1101"},
            { "0010", "0101"},
            { "0011", "1110"},
            { "0100", "1001"},
            { "0101", "1110"},
            { "0110", "1100"},
            { "0111", "0000"},
            { "1000", "1111"},
            { "1001", "1100"},
            { "1010", "1010"},
            { "1011", "1111"},
            { "1100", "----"},
            { "1101", "0110"},
            { "1110", "0010"},
            { "1111", "----"},
        };

        public BusDir_Memoria Calcular_BusDir_Memoria(string salida_RDI, List<string> listSalAct,string fase)
        {
            var le = listSalAct.Letra(Salidas.LE);
            var am = listSalAct.Letra(Salidas.AM);

            RDA1 = string.IsNullOrEmpty(RDA1) ? "XXXX": RDA1;
            RDA2 = string.IsNullOrEmpty(RDA2) ? "XXXX": RDA2;
            AM = am;

            if (le=="1" & (fase == Const.FE_C1_CK0 | fase == Const.FE_C2_CK0 | fase == Const.FP_C1_CK0| fase == Const.FP_C2_CK0))
                LE = "0";
            else
                LE = "1";

            // Si LE es 0, no se permite acceso a memoria.
            if (LE == "0") return this;

            //Verificar luego del CRDI en 1 la instruccion siguiente eleva los datos a RDA

            if (Constantes.TryGetValue(salida_RDI, out string resultado))
            {
                if (
                    string.IsNullOrEmpty(resultado) || 
                    (am == "1" & (fase == Const.FP_C1_CK0 | fase == Const.FE_C1_CK0)) ||
                    (fase == Const.FE_C1_CK0)
                   )
                {
                    RDA2 = "XXXX";
                    RDA1 = "XXXX";
                }
                else if (am == "1")
                {
                    RDA2 = resultado;
                    RDA1 = "XXXX";
                }
                else
                {
                    RDA2 = resultado;
                    RDA1 = ObtenerSiguienteConstante(salida_RDI);
                }
            }

            return this;
        }

        private string ObtenerSiguienteConstante(string claveActual)
        {
            var keys = Constantes.Keys.ToList();
            int indiceActual = keys.IndexOf(claveActual);

            if (indiceActual >= 0 && indiceActual < keys.Count - 1)
            {
                string siguienteClave = keys[indiceActual + 1];
                return Constantes[siguienteClave];
            }

            // Si es la última clave o no existe, devuelve "XXXX".
            return "XXXX";
        }
    }
}
