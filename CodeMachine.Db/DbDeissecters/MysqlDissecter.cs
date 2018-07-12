using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using CodeMachine.Db.Models;
using CodeMachine.Db.TypeMap;
using Dapper;

namespace CodeMachine.Db.DbDeissecters
{
    public class MysqlDissecter : IDbDissecter
    {
        private string _connection;

        public IMap TypeMap { get; set; }


        public MysqlDissecter(string connection)
        {
            _connection = connection;
            TypeMap = TypeMapLoader.GetMap(DbProviders.Mysql);

        }

        /// <summary>
        /// 获取数据库描述
        /// </summary>
        /// <returns></returns>
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

        private string FindDbName(string connectionString)
        {
            var dbName = connectionString.Split(';').First(n => n.StartsWith("Database=")).Split('=')[1];
            return dbName;
        }



        /// <summary>
        /// 获取表名列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableNames()
        {
            var tables = new List<string>();
            const string sql = "select table_name as NAME from information_schema.tables where table_schema='{0}' ";
            using (var conn = DbConnection.CreateConnection(DbProviders.Mysql))
            {
                conn.ConnectionString = _connection;

                var dbName = FindDbName(_connection);
                var result = conn.Query(string.Format(sql, dbName));

                foreach (var o in result)
                {
                    tables.Add(o.NAME);
                }
            }

            return tables;
        }

        /// <summary>
        /// 根据表面获取列
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<Column> GetColumns(string tableName)
        {
            var columns = new List<Column>();
            const string sql = @"SELECT COLUMN_NAME as NAME,
                                IS_NULLABLE as ISNULLABLE,
                                DATA_TYPE as TYPE,
                                CHARACTER_MAXIMUM_LENGTH as LENGTH
                                FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{0}' AND TABLE_NAME='{1}'";
            using (var conn = DbConnection.CreateConnection(DbProviders.Mysql))
            {
                conn.ConnectionString = _connection;

                var dbName = FindDbName(_connection);
                var result = conn.Query(string.Format(sql, dbName, tableName));

                foreach (var o in result)
                {
                    var col = new Column();
                    col.Name = o.NAME;
                    col.DbType = o.TYPE;
                    col.ClrType = TypeMap.Get(col.DbType).ClrType;
                    col.Length = o.LENGTH ==null? 0 : int.Parse(o.LENGTH.ToString());
                    col.Nullable = (string)o.ISNULLABLE == "YES";

                    columns.Add(col);
                }
            }

            return columns;

        }
    }
}
