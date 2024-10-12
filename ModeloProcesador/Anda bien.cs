using ModeloProcesador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModeloProcesador
{
    internal class Anda_bien
    {
//        //Ciclo 1 Fase Pedido
//        private void button1_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                #region Situacion de Inicio

//                //Acá va la direccion de memoria de la siguiente instrucción!!!!
//                IP.Valor_Salida_0 = "0000"; //nose porq cero
//                IP.Salida_general = IP.Valor_Salida_0;

//                //Cargo inicial RL
//                RL.Valor_Entrada_0 = "0000";
//                RL.Valor_Salida_0 = "0000";
//                RL.Salida_general = "0000";

//                //Carga inicial de 
//                A.Valor_Salida_0 = "XXXX";
//                A.Salida_general = "XXXX";

//                B.Valor_Salida_0 = "XXXX";
//                B.Salida_general = "XXXX";

//                RE.Valor_Salida_0 = "XXXX";
//                RE.Salida_general = "XXXX";


//                RDI.Valor_Salida_0 = "XXXX";
//                RDI.Salida_general = "XXXX";

//                RI_Izq.Valor_Salida_0 = "XXXX";
//                RI_Izq.Salida_general = "XXXX";

//                RI_Der.Valor_Salida_0 = "XXXX";
//                RI_Der.Salida_general = "XXXX";

//                #endregion

//                #region Clock 0

//                direccioncompleta = BuscarDireccionBinaria(textBox_instruccion.Text);
//                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);
//                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);
//                //RE
//                RE = RE.Calcular_RE(Const.Fase_1, Const.Clock_0, listSalAct, UAL.Valor_Salida_SZVC);
//                //Ahora todos tienen q copiar la salida de la UAL
//                A = A.Calcular_A(Const.Fase_1, Const.Clock_0, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_1, Const.Clock_0, listSalAct, UAL.Salida_general);
//                IP = IP.Calcular_IP(Const.Fase_1, Const.Clock_0, listSalAct, UAL.Salida_general, IP.Salida_general);
//                RDI = RDI.Calcular_RDI(Const.Fase_1, Const.Clock_0, listSalAct, UAL.Salida_general);

//                //TODO: RDA dice q es indeterminado pero nose porque
//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_1, Const.Clock_0, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_1, Const.Clock_0, listSalAct, BusMemoria);

//                #endregion

//                #region Clock 1

//                RL = RL.Calcular_RL(Const.Fase_1, Const.Clock_1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                A = A.Calcular_A(Const.Fase_1, Const.Clock_1, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_1, Const.Clock_1, listSalAct, UAL.Salida_general);

//                IP = IP.Calcular_IP(Const.Fase_1, Const.Clock_1, listSalAct, UAL.Salida_general, IP.Salida_general);
//                RE = RE.Calcular_RE(Const.Fase_1, Const.Clock_1, listSalAct, UAL.Valor_Salida_SZVC);

//                RDI = RDI.Calcular_RDI(Const.Fase_1, Const.Clock_1, listSalAct, UAL.Salida_general);

//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_1, Const.Clock_1, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_1, Const.Clock_1, listSalAct, BusMemoria);

//                //No lo calculo al bus de memoria tdvia para no alterar el RDA tdvia

//                #endregion
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                MostrarLabels();
//            }
//        }

//        //Ciclo 2 Fase Pedido
//        private void button2_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                #region Clock 0

//                RL = RL.Calcular_RL(Const.Fase_2, Const.Clock_0, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                //La salida de RL va ala UC
//                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
//                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);

//                BusMemoria = BusMemoria.Calcular_BusDir_Memoria(RDI.Salida_general, listSalAct, Const.Fase_2, Const.Clock_0);

//                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);

//                IP = IP.Calcular_IP(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general, IP.Salida_general);
//                RDI = RDI.Calcular_RDI(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);

//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_2, Const.Clock_0, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_2, Const.Clock_0, listSalAct, BusMemoria);

//                B = B.Calcular_B(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                A = A.Calcular_A(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                RE = RE.Calcular_RE(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Valor_Salida_SZVC);

//                #endregion

//                #region Clock 1

//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_2, Const.Clock_1, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_2, Const.Clock_1, listSalAct, BusMemoria);

//                RL = RL.Calcular_RL(Const.Fase_2, Const.Clock_1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                A = A.Calcular_A(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                RE = RE.Calcular_RE(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Valor_Salida_SZVC);
//                RDI = RDI.Calcular_RDI(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                IP = IP.Calcular_IP(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general, IP.Salida_general);

//                #endregion
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                MostrarLabels();
//            }
//        }

//        //Ciclo 1 Fase Ejecucion
//        private void button3_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                #region Clock 0

//                //Va primero porq necesita cambiar con el clock 0 antes de mandar a la UC
//                RL = RL.Calcular_RL(Const.Fase_2, Const.Clock_0, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
//                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);

//                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);

//                RE = RE.Calcular_RE(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Valor_Salida_SZVC);
//                A = A.Calcular_A(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);

//                IP = IP.Calcular_IP(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general, IP.Salida_general);

//                RDI = RDI.Calcular_RDI(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                BusMemoria = BusMemoria.Calcular_BusDir_Memoria(RDI.Salida_general, listSalAct, Const.Fase_1, Const.Clock_0);

//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_2, Const.Clock_0, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_2, Const.Clock_0, listSalAct, BusMemoria);

//                #endregion

//                #region Clock 1

//                RL = RL.Calcular_RL(Const.Fase_2, Const.Clock_1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                RE = RE.Calcular_RE(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Valor_Salida_SZVC);
//                A = A.Calcular_A(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                IP = IP.Calcular_IP(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general, IP.Salida_general);
//                RDI = RDI.Calcular_RDI(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_2, Const.Clock_1, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_2, Const.Clock_1, listSalAct, BusMemoria);

//                #endregion
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                MostrarLabels();
//            }
//        }

//        //Ciclo 2 Fase Ejecucion
//        private void button4_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                #region Clock 0

//                //Va primero porq necesita cambiar con el clock 0 antes de mandar a la UC
//                RL = RL.Calcular_RL(Const.Fase_2, Const.Clock_0, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
//                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);
//                BusMemoria = BusMemoria.Calcular_BusDir_Memoria(RDI.Salida_general, listSalAct, Const.Fase_2, Const.Clock_0);

//                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);

//                RE = RE.Calcular_RE(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Valor_Salida_SZVC);
//                A = A.Calcular_A(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                IP = IP.Calcular_IP(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general, IP.Salida_general);
//                RDI = RDI.Calcular_RDI(Const.Fase_2, Const.Clock_0, listSalAct, UAL.Salida_general);
//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_2, Const.Clock_0, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_2, Const.Clock_0, listSalAct, BusMemoria);

//                #endregion

//                #region Clock 1

//                RL = RL.Calcular_RL(Const.Fase_2, Const.Clock_1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
//                RE = RE.Calcular_RE(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Valor_Salida_SZVC);
//                A = A.Calcular_A(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                B = B.Calcular_B(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                IP = IP.Calcular_IP(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general, IP.Salida_general);
//                RDI = RDI.Calcular_RDI(Const.Fase_2, Const.Clock_1, listSalAct, UAL.Salida_general);
//                RI_Der = RI_Der.Calcular_RI_Der(Const.Fase_2, Const.Clock_1, listSalAct, BusMemoria);
//                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.Fase_2, Const.Clock_1, listSalAct, BusMemoria);

//                #endregion 
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                MostrarLabels();
//            }

//        }

//#endregion
    }
}
