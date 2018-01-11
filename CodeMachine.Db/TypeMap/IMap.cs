using System.Collections.Generic;

namespace CodeMachine.Db.TypeMap
{
    public interface IMap
    {
        List<ConfigNode> ConfigNodes { get; set; }

        ConfigNode Get(string dbType);
    }

   
}
