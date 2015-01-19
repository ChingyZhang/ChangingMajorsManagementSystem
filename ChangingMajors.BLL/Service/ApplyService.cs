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
    public class ApplyService : IApplyService
    {
        ApplyDAO applyDao = new ApplyDAO();

        public ArgsHelp AddApply()
        {
            return ApplyDAO.FindApplyToAdd();
        }

        public ArgsHelp AddNewApply(User_Entry userEntry)
        {
            return ApplyDAO.AddNewApply(userEntry);
        }

        public SelectList QueryAllMajors()
        {
            return MajorsDAO.QueryAllMajors();
        }

        public SelectList QueryAllMajors(String Year)
        {
            return MajorsDAO.QueryAllMajorsByYear(Year);
        }

        public SelectList QueryAllMajors(String Year, String Selected)
        {
            return MajorsDAO.QueryAllMajorsByYear(Year, Selected);
        }

        public SelectList Demotion(string selected)
        {
            return ApplyDAO.Demotion(selected);
        }

        public SelectList Demotion()
        {
            return ApplyDAO.Demotion();
        }

        public SelectList UserArtsScience(string selected)
        {
            return ApplyDAO.UserArtsScience(selected);
        }

        public SelectList UserArtsScience()
        {
            return ApplyDAO.UserArtsScience();
        }

        public SelectList FileClass(string selected)
        {
            return ApplyDAO.FileClass(selected);
        }

        public SelectList FileClass()
        {
            return ApplyDAO.FileClass();
        }

        public System_Majors QueryMajorIDByNameAndArts(String Name, int Arts)
        {
            return MajorsDAO.QueryMajorIDByNameAndArts(Name, Arts);
        }

        public System_Majors QueryNameByID(int? majorID)
        {
            return MajorsDAO.QueryNameByID(majorID);
        }

        public List<String[]> MajorDateAndStuDate()
        {
            return MajorsDAO.MajorDateAndStuDate();
        }

        public List<String[]> StuDateAndStuDate()
        {
            return MajorsDAO.StuDateAndStuDate();
        }

        public SelectList QueryNoAllMajors()
        {
            return CollectDAO.QueryNoAllMajors();
        }

        public SelectList QueryNoAllMajors(String selected)
        {
            return CollectDAO.QueryNoAllMajors(selected,null);
        }

        /// <summary>
        /// 获取专业
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="major_art_science">文理科</param>
        /// <returns></returns>
        public SelectList QueryNoAllMajors(String selected, String major_art_science)
        {
            return CollectDAO.QueryNoAllMajors(selected, major_art_science);
        }

        public ArgsHelp AddMajor(System_Majors majors)
        {
            return MajorsDAO.AddMajor(majors);
        }

        public ArgsHelp DelectMajor(String majorID)
        {
            return MajorsDAO.DelectMajor(majorID);
        }

        public List<System_Majors> QueryAllMajorInfo(String date)
        {
            return MajorsDAO.QueryAllMajorInfo(date);
        }

        public System_Majors QueryMajorInfo(String MajorID)
        {
            return MajorsDAO.QueryMajorInfo(MajorID);
        }

        public ArgsHelp UpdateMajorNum(String MajorID, String Num)
        {
            return MajorsDAO.UpdateMajorNum(MajorID, Num);
        }

    }
}
