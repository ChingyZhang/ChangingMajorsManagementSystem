using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.DAO.Base;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.BLL.Infrastructure;
using System.Web.Mvc;

namespace ChangingMajors.BLL.Service
{
    public class StartService : IStartService
    {
        StartDAO start = new StartDAO();
        ApplyDAO apply = new ApplyDAO();

        public void Start(User_ChangingMajors_Start start)
        {
            StartDAO.AddStart(start);
            StartDAO.AddStartStu(start);
            StartDAO.AddStartMaj(start);
            //ApplyDAO.FindApplyToAdd();
            StartDAO.startManager(start);
        }

        public void End(User_ChangingMajors_Start start)
        {
            StartDAO.EndApply(start);
            StartDAO.EndStartApply(start);
            StartDAO.EndStartMaj(start);
            StartDAO.EndStartStu(start);
            ApplyDAO.FindApplyToClose();
            StartDAO.clossManager(start);
        }

        public SelectList QueryAllDate()
        {
            return StartDAO.QueryAllDate();
        }

        public User_ChangingMajors_Start QueryStartByID(String StartID)
        {
            return StartDAO.QueryStartByID(StartID);
        }

        public List<User_ChangingMajors_Start> QueryAllCloseStart()
        {
            return StartDAO.QueryAllCloseStart();
        }
    }
}
