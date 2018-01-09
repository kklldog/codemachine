using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeMachine.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMachine.Db.Models;

namespace CodeMachine.Render.Tests
{
    [TestClass()]
    public class RazorRenderTests
    {
        [TestMethod()]
        public void RenderTest()
        {
            var render = new RazorRender();
            var path = AppDomain.CurrentDomain.BaseDirectory + "/test.cshtml";
            var result = render.Render("x",path,new Table
            {
                Name = "User",
                Columns = new List<Column>()
                {
                    new Column()
                    {
                        ClrType = "string",
                        Name = "Id"
                    },
                    new Column()
                    {
                        ClrType = "bool",
                        Name = "Enabled"
                    }
                }
            });

            Console.WriteLine(result);

            Assert.IsNotNull(result);
        }
    }
}