using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Models
{
    public class UAL
    {
        public string Valor_Entrada_X { get; set; }
        public string Valor_Entrada_Y { get; set; }
        // 0 para suma, 1 para resta
        public string Linea_Control { get; set; }

        // Flags SZVC (Sign, Zero, Overflow, Carry)
        public string Valor_Salida_SZVC { get; set; }
        public string Salida_general { get; set; }

        public UAL() { }

        public UAL Calcular_UAL(List<string> listaSalidasActivas, A A, B B, IP IP, RI_Derecho RI_Derecho, BusDir_Memoria busDir_Memoria)
        {
            Valor_Entrada_X = null;
            Valor_Entrada_Y = null;
            Linea_Control = "0";

            if (listaSalidasActivas.Contains(Salidas.SumaResta))
                Linea_Control = "1";

            // Calcular la entrada X
            if (listaSalidasActivas.Contains(Salidas.H0X))
                Valor_Entrada_X = "0000";
            if (listaSalidasActivas.Contains(Salidas.HX1))
                Valor_Entrada_X = busDir_Memoria.RDA2;
            else if (listaSalidasActivas.Contains(Salidas.HBX))
                Valor_Entrada_X = B.Salida_general;
            else if (listaSalidasActivas.Contains(Salidas.HRIX))
                Valor_Entrada_X = RI_Derecho.Salida_general;
            else if (listaSalidasActivas.Contains(Salidas.HAX))
                Valor_Entrada_X = A.Salida_general;

            // Calcular la entrada Y
            if (listaSalidasActivas.Contains(Salidas.HRIY))
                Valor_Entrada_Y = IP.Salida_general;
            else if (listaSalidasActivas.Contains(Salidas.HIp))
                Valor_Entrada_Y = IP.Salida_general;
            else if (listaSalidasActivas.Contains(Salidas.H0Y))
                Valor_Entrada_Y = "0000";
            else if (listaSalidasActivas.Contains(Salidas.HBY))
                Valor_Entrada_Y = B.Salida_general;
            else if (listaSalidasActivas.Contains(Salidas.HAY))
                Valor_Entrada_Y = A.Salida_general;

            // Si ninguna H está habilitada, ambas entradas se establecen en "XXXX"
            if (string.IsNullOrEmpty(Valor_Entrada_X))
                Valor_Entrada_X = "XXXX";
            if (string.IsNullOrEmpty(Valor_Entrada_Y))
                Valor_Entrada_Y = "XXXX";

            // Verificamos si las entradas son válidas (no XXXX), para proceder con la operación
            if (Valor_Entrada_X != "XXXX" && Valor_Entrada_Y != "XXXX")
            {
                int x = Convert.ToInt32(Valor_Entrada_X, 2);
                int y = Convert.ToInt32(Valor_Entrada_Y, 2);
                int resultado = 0;
                bool carry = false;
                bool overflow = false;

                // Operación (0 = Suma, 1 = Resta)
                if (Linea_Control == "0")
                {
                    resultado = x + y;
                    carry = resultado > 15;  
                    // Si el resultado excede el rango de 4 bits
                    overflow = ((x & 8) == (y & 8)) && ((resultado & 8) != (x & 8)); 
                    // Verifica overflow en suma
                }
                else
                {
                    resultado = x - y;
                    carry = x < y;  
                    // Si x es menor que y, hubo "carry" en la resta
                    overflow = ((x & 8) != (y & 8)) && ((resultado & 8) != (x & 8)); 
                    // Verifica overflow en resta
                }

                // Convertimos el resultado a un string de 4 bits
                Salida_general = Convert.ToString(resultado & 0xF, 2).PadLeft(4, '0');
                // Calculamos las flags SZVC (Sign, Zero, Overflow, Carry)
                Valor_Salida_SZVC = CalcularFlags(resultado, carry, overflow);
            }
            else
            {
                Salida_general = "XXXX";
                Valor_Salida_SZVC = "XXXX"; // No hay flags válidas si las entradas no lo son
            }

            return this;
        }

        private string CalcularFlags(int resultado, bool carry, bool overflow)
        {
            string S = (resultado & 8) != 0 ? "1" : "0";  // Flag de signo
            string Z = (resultado & 0xF) == 0 ? "1" : "0"; // Flag de cero
            string V = overflow ? "1" : "0";  // Flag de overflow
            string C = carry ? "1" : "0";  // Flag de carry
            return S + Z + V + C;
        }
    }
}


