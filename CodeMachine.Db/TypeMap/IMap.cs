using System.Collections.Generic;
using CodeMachine.Db.TypeMap;

namespace CodeMachine.Db
{
    public interface IMap
    {
        List<ConfigNode> ConfigNodes { get; set; }

        ConfigNode Get(string dbType);
    }

   
}
