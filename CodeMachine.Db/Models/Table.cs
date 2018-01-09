using System.Collections.Generic;

namespace CodeMachine.Db.Models
{
    public class Table
    {
        public string Name { get; set; }

        public List<Column> Columns { get; set; }
    }
}
