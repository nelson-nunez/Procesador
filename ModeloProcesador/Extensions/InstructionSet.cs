using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador
{
    public static class InstructionSet
    {
        private static readonly Dictionary<string, string> OpCodeToInstruction = new Dictionary<string, string>
        {
            { "0010", "ADD A,[dddd]" },
            { "0100", "SUB A,B" },
            { "0101", "SUB A,[dddd]" },
            { "0111", "MOV A,[dddd]" },
            { "1001", "MOV B,cccc" },
            { "1010", "MOV [dddd],A" },
            { "1100", "SUB A,[B]" },
            { "1110", "ADD A,B" },
            { "1111", "JNZ rrrr" }
        };

        // Método para obtener la instrucción asociada a un código de operación
        public static string GetInstruction(this string opCode)
        {
            if (opCode.Length < 4)
                return string.Empty;
           if (OpCodeToInstruction.TryGetValue(opCode, out string instruction))
                return instruction;
            else
                return "Código inválido";
        }
    }
}
