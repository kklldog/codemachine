using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeMachine.Db.DbDeissecters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMachine.Db.DbDeissecters.Tests
{
    [TestClass()]
    public class SqlserverDissecterTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var conn = "Persist Security Info = False; User ID =dev; Password =dev@123; Initial Catalog =CCPCDB; Server =.";
            var dissecter = new SqlserverDissecter(conn);
            Assert.IsNotNull(dissecter);

            var db = dissecter.GetDb();
            Assert.IsNotNull(db);

            var tables = db.Tables;
            Assert.IsNotNull(tables);

            foreach (var table in tables)
            {
                Console.WriteLine(table.Name);
                Assert.IsNotNull(table.Columns);

                foreach (var column in table.Columns)
                {
                    Assert.IsNotNull(column);
                    Console.WriteLine("{0}=>{1}=>{2}", column.Name, column.DbType, column.ClrType);
                }
            }
        }
    }
}