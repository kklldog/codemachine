using System.Collections.Generic;

namespace CodeMachine.Db.Models
{
    public class Database
    {
        public Database()
        {
            Tables = new List<Table>();
        }

        public string Name { get; set; }

        public List<Table> Tables { get; set; }
    }
}
