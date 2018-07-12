using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMachine.Db.Models;

namespace CodeMachine.Db
{
    public class DbConnection
    {
        public static IDbConnection CreateConnection(string provider)
        {
           var conn =  DbProviderFactories.GetFactory(provider).CreateConnection();

            return conn;
        }
    }
}
