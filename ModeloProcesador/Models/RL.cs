using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    //Flip Flop Maestro-Esclavo
    public class RL : _0FlipFlopBase
    {
        public RL()
        {
        }

        //Agrego la logica del multi porq no esta visible
        public RL Calcular_RL(string fase, List<string> listSalAct, string direccion, string RI)
        {
            string E="";
            E = listSalAct.Letra(Salidas.E);
            var entradagral = (E == "0") ? direccion : RI;


            switch (fase)
            {
                case Const.FP_C1_CK0:
                case Const.FP_C2_CK0:
                    // Ck=0 Retiene 
                    // !Ck=1 copia
                    Valor_Entrada_0 = Valor_Entrada_1;
                    Valor_Salida_0 = Valor_Entrada_0;
                    Salida_general = Valor_Salida_0;
                    break;

                case Const.FP_C1_CK1:
                case Const.FP_C2_CK1:
                    // Ck=1 copia 
                    // !Ck=0 retiene
                    Valor_Entrada_1 = entradagral;
                    Valor_Salida_1 = Valor_Salida_0;
                    Salida_general = Valor_Salida_1;
                    break;

                case Const.FE_C1_CK0:
                case Const.FE_C2_CK0:
                    // Ck=0 Retiene 
                    // !Ck=1 copia
                    Valor_Entrada_0 = Valor_Entrada_1;
                    Valor_Salida_0 = Valor_Entrada_0;
                    Salida_general = Valor_Salida_0;
                    break;

                case Const.FE_C1_CK1:
                case Const.FE_C2_CK1:
                    // Ck=1 copia 
                    // !Ck=0 retiene
                    Valor_Entrada_1 = entradagral;
                    Valor_Salida_1 = Valor_Salida_0;
                    Salida_general = Valor_Salida_1;
                    break;

                // Excepción para casos no contemplados
                default:
                    throw new Exception("Caso no contemplado");
            }

            return this;
        }
    }
}
