using System.Collections.Generic;
using CodeMachine.Db.Models;
using CodeMachine.Db.TypeMap;

namespace CodeMachine.Db.DbDeissecters
{
    public interface IDbDissecter
    {
        /// <summary>
        /// 类型映射
        /// </summary>
        IMap TypeMap { get; set; }
        /// <summary>
        /// 获取数据库描述
        /// </summary>
        /// <returns></returns>
        Database GetDb();

        /// <summary>
        /// 获取表名列表
        /// </summary>
        /// <returns></returns>
        List<string> GetTableNames();

        /// <summary>
        /// 获取列
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<Column> GetColumns(string tableName);
    }
}
