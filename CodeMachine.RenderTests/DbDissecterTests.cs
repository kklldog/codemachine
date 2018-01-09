using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeMachine.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMachine.Db.DbDeissecters;

namespace CodeMachine.Db.Tests
{
    [TestClass()]
    public class DbDissecterTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var dissecter = DbDissecter.Get("defaultConn");
            Assert.IsNotNull(dissecter);
            Assert.IsInstanceOfType(dissecter,typeof(SqlserverDissecter));

            dissecter = DbDissecter.Get("oracleConnectionString");
            Assert.IsNotNull(dissecter);
            Assert.IsInstanceOfType(dissecter, typeof(OracleDissecter));
        }
    }
}