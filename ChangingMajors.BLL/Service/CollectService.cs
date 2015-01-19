using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ChangingMajors.DAL.DAO.Base;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.BLL.Infrastructure;
using ChangingMajors.DAL.Entity;

namespace ChangingMajors.BLL.Service
{
    public class CollectService : ICollectService
    {
        CollectDAO collectDao = new CollectDAO();

        public List<System_User> QueryChangeMajorTable(String MajorName, String SysMajorName)
        {
            var aa = CollectDAO.QueryChangeMajorTable(MajorName, SysMajorName);
            return CollectDAO.MajorConvertUser(aa);
        }

        public SelectList QueryAllMajors()
        {
            return CollectDAO.QueryAllMajors();
        }

    }
}
