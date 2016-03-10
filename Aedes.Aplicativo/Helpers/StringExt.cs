using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aedes.Aplicativo.Helpers
{
    public class StringExt
    {
        public static bool IsNullOrWhiteSpace(params string[] values)
        {
            foreach (string value in values)
                if (String.IsNullOrWhiteSpace(value))
                    return true;
            return false;
        }
    }
}
