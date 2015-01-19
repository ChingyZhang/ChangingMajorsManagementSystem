/*
  >>>----- Copyright (c) 2012 zformular -----> 
 |                                            |
 |              Author: zformular             |
 |          E-mail: zformular@163.com         |
 |               Date: 05.16.2013             |
 |                                            |
 ╰==========================================╯
 
 */

using System;
using System.Reflection;
using System.Data.OleDb;

namespace ValueOffice.Excel
{
    public class ValueExcel : IExcel
    {
        private ValueExcel() { }

        private static ValueExcel instance;
        public static ValueExcel Instance
        {
            get
            {
                if (instance == null)
                    instance = new ValueExcel();
                return instance;
            }
        }

        #region IExcel 成员

        public bool Create(string fileName)
        {
            return this.Create(fileName, null);
        }

        public bool Create(string fileName, string password)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                    return false;

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbooks workbooks = excel.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(true);
                if (!String.IsNullOrEmpty(password))
                    workbook.Password = password;
                workbook.SaveAs(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value);
                workbook.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ChangeSheet(string fileName, string oldName, string newName)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("文件不存在");

                workbook = excel.Workbooks.Open(fileName, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;

                for (int i = 1; i <= sheets.Count; i++)
                {
                    if (((Microsoft.Office.Interop.Excel.Worksheet)sheets[i]).Name == oldName)
                        ((Microsoft.Office.Interop.Excel.Worksheet)sheets[i]).Name = newName;
                }
                workbook.Save();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                workbook.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
            }
        }


        public void DeleteSheet(string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("文件不存在");

                workbook = excel.Workbooks.Open(fileName, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                Int32 index = workbook.Sheets.Count;
                workbook.Sheets.Add(Missing.Value, workbook.Sheets[index], 1, Missing.Value);
                //workbook.Sheets[index + 1].Name = sheetName;

                workbook.Sheets.Delete();
                workbook.Sheets.Delete();
                workbook.Save();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                workbook.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
            }
        }


        public void AddSheet(string fileName, string sheetName)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("文件不存在");

                workbook = excel.Workbooks.Open(fileName, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                Int32 index = workbook.Sheets.Count;
                workbook.Sheets.Add(Missing.Value, workbook.Sheets[index], 1, Missing.Value);
                workbook.Sheets[index + 1].Name = sheetName;
                workbook.Save();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                workbook.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
            }
        }

        public void AddData(string fileName, System.Data.DataTable dataTable)
        {
            this.AddData(fileName, 0, 0, dataTable);
        }

        public void AddData(string fileName, string sheetName, System.Data.DataTable dataTable)
        {
            this.AddData(fileName, 0, 0, sheetName, dataTable);
        }

        public void AddData(string fileName, int rowIndex, int colIndex, System.Data.DataTable dataTable)
        {
            this.AddData(fileName, rowIndex, colIndex, 1, dataTable);
        }

        public void AddData(string fileName, int rowIndex, int colIndex, Object sheet, System.Data.DataTable dataTable)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            rowIndex++;
            colIndex++;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("文件不存在");

                workbook = excel.Workbooks.Open(fileName, Missing.Value, Missing.Value,
                  Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                  Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                if (sheet.GetType() == typeof(Int32))
                    worksheet = workbook.Sheets[sheet];
                else
                {
                    Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                    for (int i = 1; i <= sheets.Count; i++)
                    {
                        if (((Microsoft.Office.Interop.Excel.Worksheet)sheets[i]).Name.Equals(sheet))
                            worksheet = sheets[i];
                    }
                }

                if (worksheet == null) return;

                for (int j = colIndex; j < colIndex + dataTable.Columns.Count; j++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[rowIndex, j]).Value2 = dataTable.Columns[j - colIndex].ColumnName;
                }

                for (int i = rowIndex + 1; i < rowIndex + dataTable.Rows.Count + 1; i++)
                {
                    for (int j = colIndex; j < colIndex + dataTable.Columns.Count; j++)
                    {
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, j]).Value2 = dataTable.Rows[i - rowIndex - 1][j - colIndex];
                    }
                }

                workbook.Save();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                workbook.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
            }
        }

        public void ExecuteSQL(string fileName, string sql)
        {
            this.ExecuteSQL(fileName, sql, false);
        }

        public void ExecuteSQL(string fileName, string sql, bool HDR)
        {
            // HDR NO 第一行还是解析为数据
            // HDR YES 第一行解析为列头
            String hdr = HDR ? "YES" : "NO";

            String connectionString = String.Concat("Provider=Microsoft.Ace.OleDb.12.0;Data Source=", fileName, ";Extended Properties='Excel 12.0; HDR=", hdr, "; IMEX=0'");
            OleDbCommand command = null;
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connectionString);
                command = new OleDbCommand(sql, conn);
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;
                conn.Open();
                var asd = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                command.Dispose();
            }

        }

        public System.Data.DataTable QuerySQL(string fileName, string sql)
        {
            return this.QuerySQL(fileName, sql, false);
        }

        public System.Data.DataTable QuerySQL(string fileName, string sql, bool HDR)
        {
            // HDR NO 第一行还是解析为数据
            // HDR YES 第一行解析为列头
            String hdr = HDR ? "YES" : "NO";
            String connectionString = String.Concat("Provider=Microsoft.Ace.OleDb.12.0;Data Source=", fileName, ";Extended Properties='Excel 12.0; HDR=", hdr, "; IMEX=1'");
            OleDbDataAdapter adapter = null;
            OleDbCommand command = null;
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connectionString);
                adapter = new OleDbDataAdapter();
                command = new OleDbCommand(sql, conn);
                adapter.SelectCommand = command;
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                command.Dispose();
                adapter.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        public System.Data.DataSet QueryAllData(string fileName)
        {
            #region 查询所有Sheet名称

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            String[] sheetNames = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("文件不存在");

                workbook = excel.Workbooks.Open(fileName, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                sheetNames = new String[workbook.Sheets.Count];
                for (int i = 1; i <= workbook.Sheets.Count; i++)
                {
                    sheetNames[i - 1] = workbook.Sheets[i].Name;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                workbook.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
            }
            if (sheetNames == null || sheetNames.Length == 0) return null;

            #endregion

            try
            {
                System.Data.DataSet dataSet = new System.Data.DataSet();
                System.Data.DataTable dataTable = null;
                String sql = String.Empty;
                for (int i = 0; i < sheetNames.Length; i++)
                {
                    sql = String.Concat("SELECT * FROM [", sheetNames[i], "$]");
                    dataTable = this.QuerySQL(fileName, sql);
                    if (dataTable != null)
                        dataSet.Tables.Add(dataTable);
                }
                return dataSet;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
