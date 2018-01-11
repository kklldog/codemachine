using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CodeMachine.Db.TypeMap
{
    /// <summary>
    /// 加载类型映射的配置
    /// </summary>
    public class TypeMapLoader
    {
        private static Dictionary<string, IMap> _mappers = new Dictionary<string, IMap>();

        static TypeMapLoader()
        {
            Load();
        }


        public static void Load()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "/typemap.json";
            var text = File.ReadAllText(path);

            var config = JsonConvert.DeserializeObject<dynamic>(text);

            foreach (var jsonmap in config.mappers)
            {
                string name = jsonmap.provider;
                var mapconfig = jsonmap.mapconfig;
                IMap map = null;
                if (_mappers.ContainsKey(name))
                {
                    map = _mappers[name];
                }
                if (map == null)
                {
                    map = new Map();
                    _mappers.Add(name, map);
                }

                foreach (var node in mapconfig)
                {
                    var clrType = node.clrType;
                    var dbType = node.dbType;

                    map.ConfigNodes.Add(new ConfigNode()
                    {
                        ClrType = clrType,
                        DbType = dbType
                    });
                }
            }

        }

        /// <summary>
        /// 获取类型映射map
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IMap GetMap(string name)
        {
            IMap map = null;
            _mappers.TryGetValue(name, out map);

            return map;
        }
    }
}
