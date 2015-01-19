using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.ExcelHelper;
using ChangingMajors.DAL.DAO.Excel;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.BLL.Infrastructure;



namespace ChangingMajors.BLL.Service
{

    public class ExcelService : IExcelService
    {
        ExcelDAO excelDao = new ExcelDAO();

        public ArgsHelp AddMajorByDataTable(String FilePath, String Year)
        {
            var dt = ExcelHelper.GetMajor(FilePath);
            return ExcelDAO.AddMajorByDataTable(dt, Year);
        }

        public ArgsHelp AddStuByDataTable(String FilePath, String Year)
        {
            var dt = ExcelHelper.GetUser(FilePath);
            return ExcelDAO.AddStuByDataTable(dt, Year);
        }

        public String CreateExcel(List<System_User> user,String path)
        {
            return ExcelDAO.CreateExcelFile(user,path);
        }

        public ArgsHelp UpdateUserDetail(String FilePath, String Year)
        {
            var dt = ExcelHelper.GetUpdateStu(FilePath);
            return ExcelDAO.AddDetailByDataTable(dt, Year);
        }

    }

}
