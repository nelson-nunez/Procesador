﻿using ModeloProcesador.Models;
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
                case var _ when instruccion.StartsWith("jnz") || instruccion.StartsWith("jmp"):
                    tipo = "Relativo";
                    break;
                case var _ when instruccion.Contains("[") && instruccion.Contains("]") && instruccion.Contains(","):
                    //Si tiene mas de un car entre corchetes es directo
                    if (ContarCaracteresDespuesDeComa(instruccion))
                        tipo = "Directo";
                    else
                        tipo = "Indirecto o Indexado";
                    break;
                case var _ when !instruccion.Contains("[") && instruccion.Contains(","):
                    if (ContarCaracteresDespuesDeComa(instruccion))
                        tipo = "Inmediato";
                    else
                        tipo = "Registro";
                    break;
                default:
                    tipo = "No encontrado";
                    break;
            }

            return "Direccionamiento " + tipo;
        }

        static bool ContarCaracteresDespuesDeComa(string instruccion)
        {
            int indiceComa = instruccion.IndexOf(',');
            // Ignorar los corchetes
            string instruccionSinCorchetes = instruccion.Replace("[", "").Replace("]", "");
            // Contar los caracteres posteriores a la coma
            int caracteresDespuesDeComa = instruccionSinCorchetes.Substring(indiceComa + 1).Length;
            return caracteresDespuesDeComa > 2;
        }
    }
}
