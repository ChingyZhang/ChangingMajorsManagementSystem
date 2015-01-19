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
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data;
using Microsoft.Office.Interop.Access.Dao;

namespace ValueOffice.Access
{
    public class ValueAccess : IAccess
    {
        private ValueAccess() { }

        private static ValueAccess instance = null;
        public static ValueAccess Instance
        {
            get
            {
                if (instance == null)
                    instance = new ValueAccess();
                return instance;
            }
        }

        #region IAccess 成员

        public bool Create(string fileName)
        {
            return this.Create(fileName, null);
        }

        public bool Create(string fileName, string password)
        {
            if (System.IO.File.Exists(fileName))
                return false;

            Microsoft.Office.Interop.Access.Application access = new Microsoft.Office.Interop.Access.Application();
            try
            {
                access.NewCurrentDatabase(fileName);
                access.Visible = false;
                if (!String.IsNullOrEmpty(password))
                {
                    Database database = access.CurrentDb();
                    database.NewPassword(String.Empty, password);
                    database.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(database);
                }
                access.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveAll);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                access = null;
                GC.Collect();
            }
        }

        public void AddTable(string fileName, string tableName, AccessColumn[] columns)
        {
            this.AddTable(fileName, tableName, columns, null);
        }

        public void AddTable(string fileName, string tableName, AccessColumn[] columns, string password)
        {
            Microsoft.Office.Interop.Access.Application access = new Microsoft.Office.Interop.Access.Application();
            Database database = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("数据库不存在");

                if (String.IsNullOrEmpty(password))
                    access.OpenCurrentDatabase(fileName);
                else
                    access.OpenCurrentDatabase(fileName, false, password);
                access.Visible = false;

                #region 生成sql语句

                String cols = String.Empty;
                String constraint = String.Empty;
                for (int i = 0; i < columns.Length; i++)
                {
                    cols += columns[i].ToString();
                    if (i != columns.Length - 1)
                        cols += ",";
                }
                String sql = String.Concat("CREATE TABLE [", tableName, "] (", cols, ")");

                #endregion

                database = access.CurrentDb();
                database.Execute(sql, Type.Missing);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                database.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(database);
                access.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveAll);
                access = null;
                GC.Collect();
            }
        }

        public void DeleteTable(string fileName, string tableName)
        {
            Microsoft.Office.Interop.Access.Application access = new Microsoft.Office.Interop.Access.Application();
            Database database = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("数据库不存在");

                access.OpenCurrentDatabase(fileName);
                access.Visible = false;
                String sql = String.Concat("DROP TABLE [", tableName, "]");
                database = access.CurrentDb();
                database.Execute(sql, Type.Missing);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                database.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(database);
                access.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
                access = null;
                GC.Collect();
            }
        }

        public void ExecuteSQL(string fileName, string sql)
        {
            this.ExecuteSQL(fileName, sql, null);
        }

        public void ExecuteSQL(string fileName, string sql, string password)
        {
            Microsoft.Office.Interop.Access.Application access = new Microsoft.Office.Interop.Access.Application();
            Database database = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new InvalidOperationException("数据库不存在");

                if (String.IsNullOrEmpty(password))
                    access.OpenCurrentDatabase(fileName);
                else
                    access.OpenCurrentDatabase(fileName, false, password);
                access.Visible = false;
                database = access.CurrentDb();
                database.Execute(sql, Type.Missing);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                database.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(database);
                access.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
                access = null;
                GC.Collect();
            }
        }

        public System.Data.DataTable QuerySQL(string fileName, string sql)
        {
            return this.QuerySQL(fileName, sql, null);
        }

        public DataTable QuerySQL(string fileName, string sql, string password)
        {
            String pwd = String.Empty;
            if (!String.IsNullOrEmpty(password))
                pwd = String.Concat("Jet OLEDB:Database Password=", password, ";");

            String connectionString = String.Concat("Provider=Microsoft.ACE.OLEDB.12.0;", pwd, "Data Source=", fileName);
            OleDbConnection conn = null;
            OleDbDataAdapter adapter = null;
            OleDbCommand command = null;
            try
            {
                conn = new OleDbConnection(connectionString);
                adapter = new OleDbDataAdapter();
                command = new OleDbCommand(sql, conn);
                adapter.SelectCommand = command;
                DataTable dt = new DataTable();
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

        #endregion
    }
}
