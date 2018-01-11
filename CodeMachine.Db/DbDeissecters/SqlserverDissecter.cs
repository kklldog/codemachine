using System.Collections.Generic;
using System.Data.SqlClient;
using CodeMachine.Db.Models;
using CodeMachine.Db.TypeMap;
using Dapper;

namespace CodeMachine.Db.DbDeissecters
{
    public class SqlserverDissecter : IDbDissecter
    {
        private string _connection;

        public IMap TypeMap { get; set; }


        public SqlserverDissecter(string connection)
        {
            _connection = connection;
            TypeMap = TypeMapLoader.GetMap(DbProviders.Sqlserver);

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

        /// <summary>
        /// 获取表名列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableNames()
        {
            var tables = new List<string>();
            const string sql = "SELECT NAME FROM SYSOBJECTS WHERE XTYPE='U' ORDER BY NAME ";
            using (var conn = new SqlConnection(_connection))
            {
                var result = conn.Query(sql);

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
            const string sql = @"SELECT 
            SYSCOLUMNS.NAME,
            SYSTYPES.NAME 'TYPE',
            SYSCOLUMNS.ISNULLABLE,
            SYSCOLUMNS.LENGTH
            FROM SYSCOLUMNS, SYSTYPES
            WHERE SYSCOLUMNS.XUSERTYPE = SYSTYPES.XUSERTYPE AND SYSCOLUMNS.ID = OBJECT_ID('{0}')";
            using (var conn = new SqlConnection(_connection))
            {
                var result = conn.Query(string.Format(sql, tableName));

                foreach (var o in result)
                {
                    var col = new Column();
                    col.Name = o.NAME;
                    col.DbType = o.TYPE;
                    col.ClrType = TypeMap.Get(col.DbType).ClrType;
                    col.Length = o.LENGTH;
                    col.Nullable = (int)o.ISNULLABLE == 1;

                    columns.Add(col);
                }
            }

            return columns;

        }
    }
}
