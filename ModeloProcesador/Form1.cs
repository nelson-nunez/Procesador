using ModeloProcesador.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ModeloProcesador
{
    public partial class Form1 : Form
    {
        #region Variables

        A A = new A();
        B B = new B();
        IP IP = new IP();
        RDI RDI = new RDI();
        RE RE = new RE();
        RI_Izquierdo RI_Izq = new RI_Izquierdo();
        RI_Derecho RI_Der = new RI_Derecho();
        RL RL = new RL();
        UAL UAL = new UAL();
        BusDir_Memoria BusMemoria = new BusDir_Memoria();

        List<string> listSalAct = new List<string>();
        InstruccionSet direccioncompleta = new InstruccionSet(null, null, null, null);

        private void InicializarTodo()
        {
            A = new A();
            B = new B();
            IP = new IP();
            RDI = new RDI();
            RE = new RE();
            RI_Izq = new RI_Izquierdo();
            RI_Der = new RI_Derecho();
            RL = new RL();
            UAL = new UAL();
            BusMemoria = new BusDir_Memoria();
            listSalAct = new List<string>();
            direccioncompleta = new InstruccionSet(null, null, null, null);
        }

        #endregion
       
        public Form1()
        {
            InitializeComponent();
            InicializarTodo();
        }

        #region Botones Fases

        //Ciclo 1 Fase Pedido
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InicializarTodo();
                TieneValoresPrecargados();

                #region Clock 0
                //POR LO VISTO SIEMPRE EMPIEZA EN 0
                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);
                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);
                //RE
                RE = RE.Calcular_RE(Const.FP_C1_CK0, listSalAct, UAL.Valor_Salida_SZVC);
                //Ahora todos tienen q copiar la salida de la UAL
                A = A.Calcular_A(Const.FP_C1_CK0, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FP_C1_CK0, listSalAct, UAL.Salida_general);
                IP = IP.Calcular_IP(Const.FP_C1_CK0, listSalAct, UAL.Salida_general, IP.Salida_general);
                RDI = RDI.Calcular_RDI(Const.FP_C1_CK0, listSalAct, UAL.Salida_general);

                //TODO: RDA dice q es indeterminado pero nose porque
                RI_Der = RI_Der.Calcular_RI_Der(Const.FP_C1_CK0, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FP_C1_CK0, listSalAct, BusMemoria);

                #endregion

                #region Clock 1

                RL = RL.Calcular_RL(Const.FP_C1_CK1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                A = A.Calcular_A(Const.FP_C1_CK1, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FP_C1_CK1, listSalAct, UAL.Salida_general);

                IP = IP.Calcular_IP(Const.FP_C1_CK1, listSalAct, UAL.Salida_general, IP.Salida_general);
                RE = RE.Calcular_RE(Const.FP_C1_CK1, listSalAct, UAL.Valor_Salida_SZVC);

                RDI = RDI.Calcular_RDI(Const.FP_C1_CK1, listSalAct, UAL.Salida_general);

                RI_Der = RI_Der.Calcular_RI_Der(Const.FP_C1_CK1, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FP_C1_CK1, listSalAct, BusMemoria);

                //No lo calculo al bus de memoria tdvia para no alterar el RDA tdvia

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                label_EstadoActual.Text = " Primer Ciclo \n Fase Pedido \n " + textBox_instruccion.Text; ;
                MostrarLabels();
            }
        }

        //Ciclo 2 Fase Pedido
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                #region Clock 0

                RL = RL.Calcular_RL(Const.FP_C2_CK0, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                //La salida de RL va ala UC
                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);

                BusMemoria = BusMemoria.Calcular_BusDir_Memoria(RDI.Salida_general, listSalAct, Const.FP_C2_CK0);

                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);

                IP = IP.Calcular_IP(Const.FP_C2_CK0, listSalAct, UAL.Salida_general, IP.Salida_general);
                RDI = RDI.Calcular_RDI(Const.FP_C2_CK0, listSalAct, UAL.Salida_general);

                RI_Der = RI_Der.Calcular_RI_Der(Const.FP_C2_CK0, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FP_C2_CK0, listSalAct, BusMemoria);

                B = B.Calcular_B(Const.FP_C2_CK0, listSalAct, UAL.Salida_general);
                A = A.Calcular_A(Const.FP_C2_CK0, listSalAct, UAL.Salida_general);
                RE = RE.Calcular_RE(Const.FP_C2_CK0, listSalAct, UAL.Valor_Salida_SZVC);

                #endregion

                #region Clock 1

                RI_Der = RI_Der.Calcular_RI_Der(Const.FP_C2_CK1, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FP_C2_CK1, listSalAct, BusMemoria);

                RL = RL.Calcular_RL(Const.FP_C2_CK1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                A = A.Calcular_A(Const.FP_C2_CK1, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FP_C2_CK1, listSalAct, UAL.Salida_general);
                RE = RE.Calcular_RE(Const.FP_C2_CK1, listSalAct, UAL.Valor_Salida_SZVC);
                RDI = RDI.Calcular_RDI(Const.FP_C2_CK1, listSalAct, UAL.Salida_general);
                IP = IP.Calcular_IP(Const.FP_C2_CK1, listSalAct, UAL.Salida_general, IP.Salida_general);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                label_EstadoActual.Text = " Segundo Ciclo \n Fase Pedido \n " + textBox_instruccion.Text; ;
                MostrarLabels();
            }
        }

        //Ciclo 1 Fase Ejecucion
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                #region Clock 0

                //Va primero porq necesita cambiar con el clock 0 antes de mandar a la UC
                RL = RL.Calcular_RL(Const.FE_C1_CK0, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);

                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);

                RE = RE.Calcular_RE(Const.FE_C1_CK0, listSalAct, UAL.Valor_Salida_SZVC);
                A = A.Calcular_A(Const.FE_C1_CK0, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FE_C1_CK0, listSalAct, UAL.Salida_general);

                //Osea si es relativo llegado a la ejecucion el IP va a Sumar con RI
                //En el caso del parcial creo que es IP + JNZ 0110 
                string sumatemporal = UAL.Salida_general;
                if (textBox_instruccion.Text.DeterminarModo() == "Direccionamiento Relativo")
                {
                    int ual = UAL.Salida_general == "XXXX" ? 0 : Convert.ToInt32(UAL.Salida_general, 2);
                    int dataDir = Convert.ToInt32("0110", 2);
                    int suma = ual + dataDir;
                    sumatemporal = Convert.ToString(suma, 2).PadLeft(4, '0'); 
                }

                IP = IP.Calcular_IP(Const.FE_C1_CK0, listSalAct, sumatemporal, IP.Salida_general);
                RDI = RDI.Calcular_RDI(Const.FE_C1_CK0, listSalAct, UAL.Salida_general);
                BusMemoria = BusMemoria.Calcular_BusDir_Memoria(RDI.Salida_general, listSalAct, Const.FE_C1_CK0);
                RI_Der = RI_Der.Calcular_RI_Der(Const.FE_C1_CK0, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FE_C1_CK0, listSalAct, BusMemoria);

                #endregion

                #region Clock 1

                RL = RL.Calcular_RL(Const.FE_C1_CK1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                RE = RE.Calcular_RE(Const.FE_C1_CK1, listSalAct, UAL.Valor_Salida_SZVC);
                A = A.Calcular_A(Const.FE_C1_CK1, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FE_C1_CK1, listSalAct, UAL.Salida_general);
                IP = IP.Calcular_IP(Const.FE_C1_CK1, listSalAct, UAL.Salida_general, IP.Salida_general);
                RDI = RDI.Calcular_RDI(Const.FE_C1_CK1, listSalAct, UAL.Salida_general);
                RI_Der = RI_Der.Calcular_RI_Der(Const.FE_C1_CK1, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FE_C1_CK1, listSalAct, BusMemoria);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                label_EstadoActual.Text = " Primer Ciclo \n Fase Ejecución \n " + textBox_instruccion.Text; ;
                MostrarLabels();
            }
        }

        //Ciclo 2 Fase Ejecucion
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                #region Clock 0

                //Va primero porq necesita cambiar con el clock 0 antes de mandar a la UC
                RL = RL.Calcular_RL(Const.FE_C2_CK0, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                direccioncompleta = BuscarDireccionBinariaXCodigo(RL.Salida_general);
                listSalAct = Salidas.GenerarListaSalida(direccioncompleta.Binario);
                BusMemoria = BusMemoria.Calcular_BusDir_Memoria(RDI.Salida_general, listSalAct, Const.FE_C2_CK0);

                UAL = UAL.Calcular_UAL(listSalAct, A, B, IP, RI_Der, BusMemoria);

                RE = RE.Calcular_RE(Const.FE_C2_CK0, listSalAct, UAL.Valor_Salida_SZVC);
                A = A.Calcular_A(Const.FE_C2_CK0, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FE_C2_CK0, listSalAct, UAL.Salida_general);
                IP = IP.Calcular_IP(Const.FE_C2_CK0, listSalAct, UAL.Salida_general, IP.Salida_general);
                RDI = RDI.Calcular_RDI(Const.FE_C2_CK0, listSalAct, UAL.Salida_general);
                RI_Der = RI_Der.Calcular_RI_Der(Const.FE_C2_CK0, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FE_C2_CK0, listSalAct, BusMemoria);

                #endregion

                #region Clock 1

                RL = RL.Calcular_RL(Const.FE_C2_CK1, listSalAct, direccioncompleta.Direccion, RI_Izq.Salida_general);
                RE = RE.Calcular_RE(Const.FE_C2_CK1, listSalAct, UAL.Valor_Salida_SZVC);
                A = A.Calcular_A(Const.FE_C2_CK1, listSalAct, UAL.Salida_general);
                B = B.Calcular_B(Const.FE_C2_CK1, listSalAct, UAL.Salida_general);
                IP = IP.Calcular_IP(Const.FE_C2_CK1, listSalAct, UAL.Salida_general, IP.Salida_general);
                RDI = RDI.Calcular_RDI(Const.FE_C2_CK1, listSalAct, UAL.Salida_general);
                RI_Der = RI_Der.Calcular_RI_Der(Const.FE_C2_CK1, listSalAct, BusMemoria);
                RI_Izq = RI_Izq.Calcular_RI_Izq(Const.FE_C2_CK1, listSalAct, BusMemoria);

                #endregion 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                label_EstadoActual.Text = " Segundo Ciclo \n Fase Ejecución \n " + textBox_instruccion.Text; ;
                MostrarLabels();
            }

        }

        #endregion

        #region Buscar Codigos e Instrucciones

        public InstruccionSet BuscarCodigo_ProgramaEjecutar(string instruccion)
        {
            if (string.IsNullOrEmpty(instruccion))
                throw new Exception("Ingrese la instrucción para continuar");

            string instruccionNormalizada = instruccion.Trim().Replace(" ","").ToUpper();
            var resultado = InstruccionSetList.ProgramaAEjecutar.FirstOrDefault(ins => ins.Instruccion != null && ins.Instruccion.Trim().Replace(" ", "").ToUpper() == instruccionNormalizada);

            if (resultado == null)
                throw new Exception("No se encontró el código");
            else
                return resultado;
        }
        
        public InstruccionSet BuscarDireccionBinaria(string instruccion)
        {
            if (string.IsNullOrEmpty(instruccion))
                throw new Exception("Ingrese la instrucción para continuar");

            string instruccionNormalizada = instruccion.Trim().Replace(" ", "").ToUpper();
            var resultado = InstruccionSetList.ProgramaAEjecutar.FirstOrDefault(ins => ins.Instruccion != null && ins.Instruccion.Trim().Replace(" ", "").ToUpper() == instruccionNormalizada);

            if (resultado == null)
                throw new Exception("No se encontró el código");

            var binario = InstruccionSetList.DireccionesDelModelo.FirstOrDefault(ins => ins.Codigo != null && ins.Codigo == resultado.Codigo);

            if (binario == null)
                throw new Exception("No se encontró la dir binaria");

            return binario;
        }

        public InstruccionSet BuscarDireccionBinariaXCodigo(string codigo)
        {
            var binario = InstruccionSetList.DireccionesDelModelo.FirstOrDefault(ins => ins.Codigo != null && ins.Codigo == codigo);

            if (binario == null)
                throw new Exception("No se encontró la dir binaria");

            return binario;
        }

        #endregion

        #region Mostrar Labels     
        private void textBox_instruccion_TextChanged(object sender, EventArgs e)
        {
            label_ModoDireccionamiento.Text = textBox_instruccion.Text.DeterminarModo();
        }

        public void MostrarLabels()
        {
            //Limpio los 3 checklistbox
            checked_UC_Entrada.CheckedIndices.Cast<int>().ToList().ForEach(i => checked_UC_Entrada.SetItemChecked(i, false));
            checked_salidas.CheckedIndices.Cast<int>().ToList().ForEach(i => checked_salidas.SetItemChecked(i, false));
            checked_salidas_instr.CheckedIndices.Cast<int>().ToList().ForEach(i => checked_salidas_instr.SetItemChecked(i, false));


            //Selecciono el check. Los ceros del RL inf izq van a la entrada de la UC
            checked_UC_Entrada.SetItemChecked(checked_UC_Entrada.FindString(RL.Salida_general), true);

            //Marco las 3 salidas de la UC en base a la selección
            textBox_Salida_UC.Text = direccioncompleta.Codigo;
            textBox_DireccionSelecc_UC.Text = direccioncompleta.Binario;
            textBox_Codsalida_UC.Text = direccioncompleta.Direccion;

            //Comparo la salida para remarcar las letras que correspondan
            label_SalidasResaltadas.Text = Salidas.GenerarSalida(direccioncompleta.Binario);
            
            //RI IZQ
            text_RI_IZQ_Entrada_0.Text = RI_Izq.Valor_Entrada_0;
            text_RI_IZQ_Entrada_1.Text = RI_Izq.Valor_Entrada_1;
            text_RI_IZQ_Salida_0.Text = RI_Izq.Valor_Salida_0;
            text_RI_IZQ_Salida_1.Text = RI_Izq.Valor_Salida_1;

            //RI DER
            text_RI_DER_Entrada_0.Text = RI_Der.Valor_Entrada_0;
            text_RI_DER_Entrada_1.Text = RI_Der.Valor_Entrada_1;
            text_RI_DER_Salida_0.Text = RI_Der.Valor_Salida_0;
            text_RI_DER_Salida_1.Text = RI_Der.Valor_Salida_1;

            //RL
            text_RL_Entrada_0.Text = RL.Valor_Entrada_0;
            text_RL_Entrada_1.Text = RL.Valor_Entrada_1;
            text_RL_Salida_0.Text = RL.Valor_Salida_0;
            text_RL_Salida_1.Text = RL.Valor_Salida_1;

            //RE (Registro de Estado)
            text_RE_Entrada_0.Text = RE.Valor_Entrada_0;
            text_RE_Entrada_1.Text = RE.Valor_Entrada_1;
            text_RE_Salida_0.Text = RE.Valor_Salida_0;
            text_RE_Salida_1.Text = RE.Valor_Salida_1;

            //RDI (Registro de Datos de Instrucción)
            text_RDI_Entrada_0.Text = RDI.Valor_Entrada_0;
            text_RDI_Entrada_1.Text = RDI.Valor_Entrada_1;
            text_RDI_Salida_0.Text = RDI.Valor_Salida_0;
            text_RDI_Salida_1.Text = RDI.Valor_Salida_1;

            //A (Registro A)
            text_A_Entrada_0.Text = A.Valor_Entrada_0;
            text_A_Entrada_1.Text = A.Valor_Entrada_1;
            text_A_Salida_0.Text = A.Valor_Salida_0;
            text_A_Salida_1.Text = A.Valor_Salida_1;

            //B (Registro B)
            text_B_Entrada_0.Text = B.Valor_Entrada_0;
            text_B_Entrada_1.Text = B.Valor_Entrada_1;
            text_B_Salida_0.Text = B.Valor_Salida_0;
            text_B_Salida_1.Text = B.Valor_Salida_1;

            //IP (Registro de Instrucción del Procesador)
            text_IP_Entrada_0.Text = IP.Valor_Entrada_0;
            text_IP_Entrada_1.Text = IP.Valor_Entrada_1;
            text_IP_Salida_0.Text = IP.Valor_Salida_0;
            text_IP_Salida_1.Text = IP.Valor_Salida_1;

            //UAL
            text_UAL_X.Text = UAL.Valor_Entrada_X;
            text_UAL_Y.Text = UAL.Valor_Entrada_Y;

            //Zona bus salidas
            lbl_AM.Text = "AM: " + BusMemoria.AM;
            lbl_LE.Text = "LE: " + BusMemoria.LE;
            lbl_BusDireccion.Text = RDI.Salida_general;
            lbl_RDA1.Text = BusMemoria.RDA1;
            lbl_RDA2.Text = BusMemoria.RDA2;

            //Los ceros del RL inf izq van a la entrada de la UC
            if (!string.IsNullOrEmpty(RDI.Salida_general) & RDI.Salida_general != "XXXX")
                checked_salidas.SetItemChecked(checked_salidas.FindStringExact(RDI.Salida_general), true);
            if (!string.IsNullOrEmpty(BusMemoria.RDA1) & BusMemoria.RDA1 != "XXXX")
                checked_salidas_instr.SetItemChecked(checked_salidas_instr.FindStringExact(BusMemoria.RDA1), true);
            if (!string.IsNullOrEmpty(BusMemoria.RDA2) & BusMemoria.RDA2 != "XXXX")
                checked_salidas_instr.SetItemChecked(checked_salidas_instr.FindStringExact(BusMemoria.RDA2), true);

        }

        #endregion

        private void TieneValoresPrecargados()
        {
            ////MOVA,[1101]
            //textBox_instruccion.Text = "MOV A,[1101]";
            //text_RL_Entrada_1.Text = "0000";
            //text_RE_Salida_1.Text = "XXXX";
            ////Es la siguiente en este caso MOVA[]
            //text_IP_Salida_1.Text = "0000";
            //text_RDI_Salida_1.Text = "XXXX";
            //text_A_Salida_1.Text = "XXXX";
            //text_B_Salida_1.Text = "XXXX";
            //text_RI_IZQ_Salida_1.Text = "XXXX";
            //text_RI_DER_Salida_1.Text = "XXXX";

            ////SUBA;[1110]
            //textBox_instruccion.Text = "SUB A,[1110]";
            //text_RL_Entrada_1.Text = "0000";
            //text_RE_Salida_1.Text = "XXXX";
            //text_RDI_Salida_1.Text = "1101";
            //text_A_Salida_1.Text = "0110";
            //text_B_Salida_1.Text = "XXXX";
            ////Es la siguiente en este caso SUBA[]
            //text_IP_Salida_1.Text = "0010";
            //text_RI_IZQ_Salida_1.Text = "0111";
            //text_RI_DER_Salida_1.Text = "1101";

            //////Parcial = JNZ 0110
            //textBox_instruccion.Text = "JNZ 0110";
            //text_RL_Entrada_1.Text = "0000";
            //text_RE_Salida_1.Text = "0000";
            //text_RDI_Salida_1.Text = "1110";
            //text_A_Salida_1.Text = "0010";
            //text_B_Salida_1.Text = "1110";
            ////Es la siguiente en este caso JNZ[]
            //text_IP_Salida_1.Text = "1000";
            //text_RI_IZQ_Salida_1.Text = "1100";
            //text_RI_DER_Salida_1.Text = "0000";

            //ADD A;[0011]
            //textBox_instruccion.Text = "ADD A,[0011]";
            //text_RL_Entrada_1.Text = "0000";
            //text_RE_Salida_1.Text = "XXXX";
            //text_RDI_Salida_1.Text = "XXXX";
            //text_A_Salida_1.Text = "1000";
            //text_B_Salida_1.Text = "XXXX";
            ////Es la siguiente en este caso ADD A,[]
            //text_IP_Salida_1.Text = "1010";
            //text_RI_IZQ_Salida_1.Text = "XXXX";
            //text_RI_DER_Salida_1.Text = "XXXX";

            //////SUB A, B
            //textBox_instruccion.Text = "SUB A, B";
            //text_RL_Entrada_1.Text = "0000";
            //text_RE_Salida_1.Text = "XXXX";
            //text_RDI_Salida_1.Text = "0111";
            //text_A_Salida_1.Text = "0101";
            //text_B_Salida_1.Text = "0110";
            ////Es la siguiente en este caso JNZ[]
            //text_IP_Salida_1.Text = "0111";
            //text_RI_IZQ_Salida_1.Text = "XXXX";
            //text_RI_DER_Salida_1.Text = "XXXX";

            //////MOV A,[1100]
            //textBox_instruccion.Text = "MOV A,[1100]";
            //text_RL_Entrada_1.Text = "0000";
            //text_RE_Salida_1.Text = "XXXX";
            //text_RDI_Salida_1.Text = "XXXX";
            //text_A_Salida_1.Text = "XXXX";
            //text_B_Salida_1.Text = "XXXX";
            ////Es la siguiente en este caso JNZ[]
            //text_IP_Salida_1.Text = "0001";
            //text_RI_IZQ_Salida_1.Text = "XXXX";
            //text_RI_DER_Salida_1.Text = "XXXX";



            // RL
            RL.Valor_Entrada_0 = text_RL_Entrada_1.Text;
            RL.Valor_Salida_0 = RL.Valor_Entrada_0;
            RL.Salida_general = RL.Valor_Salida_0;

            // RE (Registro de Estado)
            RE.Valor_Salida_0 = text_RE_Salida_1.Text;
            RE.Salida_general = RE.Valor_Salida_0;

            // RDI (Registro de Datos de Instrucción)
            RDI.Valor_Salida_0 = text_RDI_Salida_1.Text;
            RDI.Salida_general = RDI.Valor_Salida_0;

            // A (Registro A)
            A.Valor_Salida_0 = text_A_Salida_1.Text;
            A.Salida_general = A.Valor_Salida_0;

            // B (Registro B)
            B.Valor_Salida_0 = text_B_Salida_1.Text;
            B.Salida_general = B.Valor_Salida_0;

            // IP (Registro de Instrucción del Procesador)
            IP.Valor_Salida_0 = text_IP_Salida_1.Text;
            IP.Salida_general = IP.Valor_Salida_0;

            // RI IZQ
            RI_Izq.Valor_Salida_0 = text_RI_IZQ_Salida_1.Text;
            RI_Izq.Salida_general = RI_Izq.Valor_Salida_0;

            // RI DER
            RI_Der.Valor_Salida_0 = text_RI_DER_Salida_1.Text;
            RI_Der.Salida_general = RI_Der.Valor_Salida_0;
        }

    }
}