using ModeloProcesador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador.Extensions
{
    public static class Extensions
    {
        public static string DeterminarModo(string instruccion)
        {
            instruccion = instruccion.ToLower();
            var tipo = "";

            // Buscar el modo de direccionamiento por cada tipo.
            switch (instruccion)
            {
                case var _ when instruccion.Contains("[") && instruccion.Contains("]"):
                    tipo = instruccion.Contains(",") ? "Indexado" : "Indirecto";
                    break;

                case var _ when instruccion.Contains(","):
                    tipo = "Inmediato";
                    break;

                case var _ when instruccion.StartsWith("mov") || instruccion.StartsWith("add") || instruccion.StartsWith("sub"):
                    if (instruccion.Contains("[") && instruccion.Contains("]"))
                        tipo = "Indirecto o Indexado";
                    else
                        tipo = "Registro";
                    break;

                case var _ when instruccion.StartsWith("jnz") || instruccion.StartsWith("jmp"):
                    tipo = "Relativo";
                    break;

                default:
                    tipo = "No encontrado";
                    break;
            }

            return "Direccionamiento " + tipo;
        }
    }
}
