using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Entity;

namespace ChangingMajors.BLL.Infrastructure
{
    public interface IExcelService
    {
        ArgsHelp AddMajorByDataTable(String FilePath, String Year);

        ArgsHelp AddStuByDataTable(String FilePath, String Year);

        String CreateExcel(List<System_User> user,String path);

        ArgsHelp UpdateUserDetail(String FilePath, String Year);

    }
}
