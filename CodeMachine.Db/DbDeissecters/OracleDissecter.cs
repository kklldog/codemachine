using System.Collections.Generic;
using CodeMachine.Db.Models;
using CodeMachine.Db.TypeMap;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace CodeMachine.Db.DbDeissecters
{
    public class OracleDissecter : IDbDissecter
    {
        private string _connection;

        public IMap TypeMap { get; set; }

        public OracleDissecter(string connection)
        {
            _connection = connection;
            TypeMap = TypeMapLoader.GetMap(DbProviders.Oracle);
        }

        public Database GetDb()
        {
            var db = new Database();
            var tableNames = GetTableNames();
            foreach (var tableName in tableNames)
            {
                var tb = new Table();
                tb.Name = tableName;
                tb.Columns = GetColumns(tableName);

                db.Tables.Add(tb);
            }

            return db;
        }

        public List<string> GetTableNames()
        {
            var tables = new List<string>();
            const string sql = "SELECT TABLE_NAME FROM USER_TABLES ";
            using (var conn = new OracleConnection(_connection))
            {
                var result = conn.Query(sql);

                foreach (var o in result)
                {
                    tables.Add(o.TABLE_NAME);
                }
            }

            return tables;
        }

        public List<Column> GetColumns(string tableName)
        {
            var columns = new List<Column>();
            const string sql = @"select t.*,c.COMMENTS from user_tab_columns t,user_col_comments c 
                                where t.table_name = c.table_name 
                                    and t.column_name = c.column_name 
                                    and t.table_name =:tableName";
            using (var conn = new OracleConnection(_connection))
            {
                var result = conn.Query(sql, new { tableName });

                foreach (var o in result)
                {
                    var col = new Column();
                    col.Name = o.COLUMN_NAME;
                    col.DbType = o.DATA_TYPE;
                    col.ClrType = TypeMap.Get(col.DbType).ClrType;
                    col.Length = (int)o.DATA_LENGTH;
                    col.Nullable = o.NULLABLE == "Y";

                    columns.Add(col);
                }
            }

            return columns;

        }
    }
}
