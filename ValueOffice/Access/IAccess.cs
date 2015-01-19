using System;

namespace ValueOffice.Access
{
    public interface IAccess
    {
        /// <summary>
        ///  创建数据库
        /// </summary>
        Boolean Create(String fileName);

        /// <summary>
        ///  创建数据库
        /// </summary>
        Boolean Create(String fileName, String password);

        /// <summary>
        ///  建表
        /// </summary>
        void AddTable(String fileName, String tableName, AccessColumn[] columns);

        /// <summary>
        ///  键表
        /// </summary>
        void AddTable(String fileName, String tableName, AccessColumn[] columns, String password);

        /// <summary>
        ///  删除表
        /// </summary>
        void DeleteTable(String fileName, String tableName);

        /// <summary>
        ///  执行SQL语句
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sql"></param>
        void ExecuteSQL(String fileName, String sql);

        /// <summary>
        ///  执行SQL语句
        /// </summary>
        void ExecuteSQL(String fileName, String sql, String password);

        /// <summary>
        ///  查询SQL语句
        /// </summary>
        System.Data.DataTable QuerySQL(String fileName, String sql);

        /// <summary>
        ///  查询SQL语句
        /// </summary>
        System.Data.DataTable QuerySQL(String fileName, String sql, String password);

    }
}
