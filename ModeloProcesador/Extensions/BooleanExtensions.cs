using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloProcesador
{
    public static class BooleanExtensions
    {
        public static bool And(this bool a, bool b)
        {
            return a && b;
        }

        public static bool Or(this bool a, bool b)
        {
            return a || b;
        }

        public static bool Not(this bool a)
        {
            return !a;
        }

        public static bool Xor(this bool a, bool b)
        {
            return a != b;
        }
    }
}
