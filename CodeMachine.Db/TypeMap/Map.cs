using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMachine.Db.TypeMap
{
    public class Map : IMap
    {
        public Map()
        {
            ConfigNodes = new List<ConfigNode>();
        }

        public List<ConfigNode> ConfigNodes { get; set; }
        public ConfigNode Get(string dbType)
        {
            var node = ConfigNodes.FirstOrDefault(n => n.DbType.Equals(dbType,StringComparison.CurrentCultureIgnoreCase));

            if (node==null)
            {
                return new ConfigNode()
                {
                    ClrType = "object",
                    DbType = dbType
                };
            }

            return node;
        }
    }
}
