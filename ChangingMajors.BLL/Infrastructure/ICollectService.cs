using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Entity;
using System.Web.Mvc;

namespace ChangingMajors.BLL.Infrastructure
{
    public interface ICollectService
    {
        List<System_User> QueryChangeMajorTable(String MajorName, String SysMajorName);

        SelectList QueryAllMajors();
    }
}
