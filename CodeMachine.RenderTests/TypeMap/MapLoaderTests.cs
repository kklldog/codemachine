using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeMachine.Db.TypeMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMachine.Db.Models;

namespace CodeMachine.Db.TypeMap.Tests
{
    [TestClass()]
    public class MapLoaderTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            TypeMapLoader.Load();
            var map = TypeMapLoader.GetMap(DbProviders.Sqlserver);

            Assert.IsNotNull(map);

            var confignodes = map.ConfigNodes;
            Assert.IsNotNull(confignodes);

            var confignode = map.Get("varchar");

            Assert.IsNotNull(confignode);

            Assert.AreEqual(confignode.ClrType,"string");
        }
    }
}