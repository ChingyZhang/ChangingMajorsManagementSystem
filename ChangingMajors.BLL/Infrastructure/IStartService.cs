using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Entity;
using System.Web.Mvc;

namespace ChangingMajors.BLL.Infrastructure
{
    public interface IStartService
    {
        void Start(User_ChangingMajors_Start start);

        void End(User_ChangingMajors_Start start);

        SelectList QueryAllDate();

        User_ChangingMajors_Start QueryStartByID(String StartID);
  
        List<User_ChangingMajors_Start> QueryAllCloseStart();
    }
}
