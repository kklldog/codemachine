using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMachine.Db.Models
{
    class Util
    {
        public static string _ToCamel(string name)
        {
            if (!name.Contains("_"))
            {
                return ConvertFirstChartToUp(name);
            }

            var arr = name.Split('_');
            var newName = "";
            foreach (var s in arr)
            {
                newName += ConvertFirstChartToUp(s);
            }

            return newName;
        }

        public static string ConvertFirstChartToUp(string value)
        {
            var chars = value.ToCharArray();
            if (chars.Length > 0)
            {
                chars[0] = chars[0].ToString().ToUpper().ToCharArray()[0];
            }
            return string.Join("", chars);
        }
    }
}
