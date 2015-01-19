using System;
using Microsoft.Office.Interop.Excel;

namespace ValueOffice.Excel
{
    public interface IExcel
    {
        Boolean Create(String fileName);

        Boolean Create(String fileName, String password);

        void ChangeSheet(String fileName, String oldName, String newName);

        void AddSheet(String fileName, String sheetName);

        void DeleteSheet(string fileName);

        void AddData(String fileName, System.Data.DataTable dataTable);

        void AddData(String fileName, String sheetName, System.Data.DataTable dataTable);

        void AddData(String fileName, Int32 rowIndex, Int32 colIndex, System.Data.DataTable dataTable);

        void AddData(String fileName, Int32 rowIndex, Int32 colIndex, Object sheet, System.Data.DataTable dataTable);

        void ExecuteSQL(String fileName, String sql);

        void ExecuteSQL(String fileName, String sql, Boolean HDR);

        System.Data.DataTable QuerySQL(String fileName, String sql);

        /// <summary>
        ///  查询SQL语句
        /// </summary>
        /// <param name="HDR">是否将第一行解析为列头</param>
        System.Data.DataTable QuerySQL(String fileName, String sql, Boolean HDR);

        System.Data.DataSet QueryAllData(String fileName);
    }
}
