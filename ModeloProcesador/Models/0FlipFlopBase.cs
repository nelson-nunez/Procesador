using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public abstract class _0FlipFlopBase
    {
        public string Valor_Entrada_0 { get; set; }
        public string Valor_Salida_0 { get; set; }

        public string Valor_Entrada_1 { get; set; }
        public string Valor_Salida_1 { get; set; }

        public string Salida_general { get; set; }

        public _0FlipFlopBase(string entrada_0 = null, string salida_0 = null, string entrada_1 = null, string salida_1 = null) 
        {
            Valor_Entrada_0 = entrada_0;
            Valor_Salida_0 = salida_0;
            Valor_Entrada_1 = entrada_1;
            Valor_Salida_1 = salida_1;
        }

        public _0FlipFlopBase Calcular(string fase, string Comp, string Entradagral)
        {
            switch (fase)
            {
                #region Fase Pedido

                case Const.FP_C1_CK0 when Comp == "0":          
                case Const.FP_C1_CK0 when Comp == "0":
                    // 0+0=!0=1 Copia en el estado de entrada 0
                    // 0+0=0 retiene en salida 0
                    Valor_Entrada_0 = Entradagral;
                    Valor_Salida_0 = Valor_Salida_0.Vacio();
                    Salida_general = Valor_Salida_0;
                    break;
                case Const.FP_C2_CK0 when Comp == "0":
                    // 0+0=!0=1 Copia en el estado de entrada 0
                    // 0+0=0 retiene en salida 0
                    Valor_Entrada_0 = Entradagral;
                    Valor_Salida_0 = Valor_Salida_1.Vacio();
                    Salida_general = Valor_Salida_0;
                    break;

                case Const.FP_C1_CK0 when Comp == "1":
                case Const.FP_C2_CK0 when Comp == "1":
                    // 0+0=!0=1 Copia en el estado de entrada 0
                    // 0+0=0 retiene en salida 0
                    if (string.IsNullOrEmpty(Entradagral))
                    {
                        Valor_Entrada_0 = Valor_Entrada_1;
                        Valor_Salida_0 = Valor_Salida_0;
                    }
                    else
                    {
                        Valor_Entrada_0 = Entradagral;
                        Valor_Salida_0 = Valor_Salida_0.Vacio();
                    }
                    Salida_general = Valor_Salida_0;
                    break;

                case Const.FP_C1_CK1 when Comp == "0":
                case Const.FP_C2_CK1 when Comp == "0":
                    // 1+0=!0=1 Copia 
                    // 1+0=0 retiene
                    Valor_Entrada_1 = Entradagral;
                    Valor_Salida_1 = Valor_Salida_0.Vacio();
                    Salida_general = Valor_Salida_1;
                    break;

                case Const.FP_C1_CK1 when Comp == "1":
                case Const.FP_C2_CK1 when Comp == "1":
                    // 1+1=!1=0 retiene
                    // 1+1=1 copia
                    Valor_Entrada_1 = Valor_Entrada_0.Vacio(); 
                    Valor_Salida_1 = Valor_Entrada_1;
                    Salida_general = Valor_Salida_1;
                    break;

                #endregion

                #region Fase Ejecucion

                case Const.FE_C1_CK0 when Comp == "0":
                case Const.FE_C2_CK0 when Comp == "0":
                    // 0+0=!0=1 Copia 
                    // 0+0=0 retiene
                    Valor_Entrada_0 = Entradagral;
                    Valor_Salida_0 = Valor_Salida_1.Vacio(); 
                    Salida_general = Valor_Salida_0;
                    break;

                case Const.FE_C1_CK0 when Comp == "1":
                case Const.FE_C2_CK0 when Comp == "1":
                    // 0+1=!0=1 copia 
                    // 0+1=0 retiene
                    Valor_Entrada_0 = Entradagral;
                    Valor_Salida_0 = Valor_Salida_1.Vacio();
                    Salida_general = Valor_Salida_0;
                    break;

                case Const.FE_C1_CK1 when Comp == "0":
                case Const.FE_C2_CK1 when Comp == "0":
                    // 1+0=!0=1 copia 
                    // 1+0=0 retiene
                    Valor_Entrada_1 = Entradagral;
                    Valor_Salida_1 = Valor_Salida_0.Vacio();
                    Salida_general = Valor_Salida_1;
                    break;

                case Const.FE_C1_CK1 when Comp == "1":
                case Const.FE_C2_CK1 when Comp == "1":
                    // 1+1=!1=0 retiene 
                    // 1+1=1 copia
                    Valor_Entrada_1 = Valor_Entrada_0.Vacio();
                    Valor_Salida_1 = Valor_Entrada_1;
                    Salida_general = Valor_Salida_1;
                    break;

                #endregion

                // Excepción para casos no contemplados
                default:
                    throw new Exception("Caso no contemplado");
            }

            return this;
        }
    }
}
