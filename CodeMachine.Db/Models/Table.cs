using System.Collections.Generic;
using System.Data.Common;

namespace CodeMachine.Db.Models
{
    public class Table
    {
        public string Name { get; set; }

        public string _ToCamel => Util._ToCamel(Name);

        public List<Column> Columns { get; set; }
    }
}
