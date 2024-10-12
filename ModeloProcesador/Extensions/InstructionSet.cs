using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador
{
    public static class Const
    {
        public const string FP_C1_CK0 = "0";
        public const string FP_C1_CK1 = "1";
        public const string FP_C2_CK0 = "2";
        public const string FP_C2_CK1 = "3";
        public const string FE_C1_CK0 = "4";
        public const string FE_C1_CK1 = "5";
        public const string FE_C2_CK0 = "6";
        public const string FE_C2_CK1 = "7";
    }

    // Clase que representa una instrucción
    public class InstruccionSet
    {
        public string Codigo { get; set; }
        public string Binario { get; set; }
        public string Direccion { get; set; }
        public string Instruccion { get; set; }

        public InstruccionSet(string codigo,string binario, string direccion, string instruccion)
        {
            Codigo = codigo;
            Binario = binario;
            Direccion = direccion;
            Instruccion = instruccion;
        }
    }

    public static class InstruccionSetList
    {
        // Lista que contiene las direcciones del modelo con sus respectivas instrucciones
        public static readonly List<InstruccionSet> DireccionesDelModelo = new List<InstruccionSet>()
        {
            new InstruccionSet("0000", "010110000000000000000000", "0001", null),
            new InstruccionSet("0001", "001000100000000000000001", "XXXX", null),//Este es el cod del RI pero se pone XXXX
            new InstruccionSet("0010", "000101010000000000100010", "0011", "ADD A,[dddd]"),
            new InstruccionSet("0011", "000000000011101000100010", "0000", null),
            new InstruccionSet("0100", "100000000001101001000010", "0000", "SUB A,B"),
            new InstruccionSet("0101", "000101010000000000100010", "0110", "SUB A,[dddd]"),
            new InstruccionSet("0110", "100000000011101000100010", "0000", null),
            new InstruccionSet("0111", "000101010000000000100010", "1000", "MOV A,[dddd]"),
            new InstruccionSet("1000", "000001000101000000100010", "0000", null),
            new InstruccionSet("1001", "000001010000000100000010", "0000", "MOV B,cccc"),
            new InstruccionSet("1010", "000101010000000000100010", "1011", "MOV [dddd],A"),
            new InstruccionSet("1011", "000000000000100000111010", "0000", null),
            new InstruccionSet("1100", "000101000000000010100010", "1101", "SUB A,[B]"),
            new InstruccionSet("1101", "100000000011101000100010", "0000", null),
            new InstruccionSet("1110", "000000000001101001000010", "0000", "ADD A,B"),
            new InstruccionSet("1111", "000000000000000000000010", "0000", null),
            new InstruccionSet("1111", "011000010000000000000010", "0000", "JNZ rrrr")
        };

        // Lista que contiene el programa a ejecutar
        public static readonly List<InstruccionSet> ProgramaAEjecutar = new List<InstruccionSet>()
        {
            new InstruccionSet("0000", null, "0111", "MOV A,[1101]"),
            new InstruccionSet("0001", null, "1101", null),
            new InstruccionSet("0010", null, "0101", "SUB A,[1110]"),
            new InstruccionSet("0011", null, "1110", null),
            new InstruccionSet("0100", null, "1001", "MOV B,1110"),
            new InstruccionSet("0101", null, "1110", null),
            new InstruccionSet("0110", null, "1100", "SUB A,[B]"),
            new InstruccionSet("0111", null, "0000", null),
            new InstruccionSet("1000", null, "1111", "JNZ 0110"),
            new InstruccionSet("1001", null, "1100", null),
            new InstruccionSet("1010", null, "1010", "MOV [1111],A"),
            new InstruccionSet("1011", null, "1111", null),
            new InstruccionSet("1100", null, "    ", null),
            new InstruccionSet("1101", null, "0110", null),
            new InstruccionSet("1110", null, "0010", null),
            new InstruccionSet("1111", null, "    ", null)
        };
    }
    
    public static class Salidas
    {
        // Definir constantes individuales
        public const string SumaResta = "-/+";
        public const string HIp = "HIp";
        public const string CIP = "CIP";
        public const string CRDI = "CRDI";
        public const string H0X = "H0X";
        public const string H0Y = "H0Y";
        public const string CRI = "CRI";
        public const string HRIX = "HRIX";
        public const string HRIY = "HRIY";
        public const string HX1 = "HX1";
        public const string HY1 = "HY1";
        public const string CA = "CA";
        public const string HAX = "HAX";
        public const string HAY = "HAY";
        public const string CRE = "CRE";
        public const string CB = "CB";
        public const string HBX = "HBX";
        public const string HBY = "HBY";
        public const string AM = "AM";
        public const string LE = "L/E";
        public const string HX2 = "HX2";
        public const string HY2 = "HY2";
        public const string M = "M";
        public const string E = "E";

        // Diccionario estático para mapear cada posición a su constante
        public static readonly Dictionary<int, string> Constantes = new Dictionary<int, string>
        {
            { 0, SumaResta }, { 1, HIp }, { 2, CIP }, { 3, CRDI }, { 4, H0X },
            { 5, H0Y }, { 6, CRI }, { 7, HRIX }, { 8, HRIY }, { 9, HX1 },
            { 10, HY1 }, { 11, CA }, { 12, HAX }, { 13, HAY }, { 14, CRE },
            { 15, CB }, { 16, HBX }, { 17, HBY }, { 18, AM }, { 19, LE },
            { 20, HX2 }, { 21, HY2 }, { 22, M }, { 23, E }
        };

        // Método para generar una cadena de salidas basada en la dirección
        public static string GenerarSalida(string direccion)
        {
            if (direccion.Length != Constantes.Count)
                throw new ArgumentException("La longitud de la dirección no coincide con el número de constantes");

            var lista = string.Join(" ", direccion.Select((c, i) => c == '1' ? Constantes[i] : "_"));
            return "Salidas: " + lista;
        }

        // Método para generar una lista de salidas activas
        public static List<string> GenerarListaSalida(string direccion)
        {
            if (direccion.Length != Constantes.Count)
                throw new ArgumentException("La longitud de la dirección no coincide con el número de constantes");

            var lista = direccion.Select((c, i) => c == '1' ? Constantes[i] : "_").Where(x => x != "_").ToList();
            return lista;
        }

        public static string Letra(this List<string> listaSalidasActivas, string letra)
        {
            return listaSalidasActivas.Contains(letra) ? "1" : "0";
        }
    }
}

