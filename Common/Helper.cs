using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Helper
    {
        public static string Repeat(this string s, int number)
        {
            StringBuilder result = new();
            for (int i = 0; i < number; i++) result.Append(s);
            return result.ToString();
        }
    }
}
