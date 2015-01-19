using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Entity;
using System.Web.Mvc;

namespace ChangingMajors.BLL.Infrastructure
{
    public interface IApplyService
    {
        ArgsHelp AddApply();

        ArgsHelp AddNewApply(User_Entry userEntry);

        System_Majors QueryMajorIDByNameAndArts(String Name, int Arts);

        System_Majors QueryNameByID(int? majorID);

        List<String[]> MajorDateAndStuDate();

        List<String[]> StuDateAndStuDate();

        SelectList QueryAllMajors();

        SelectList QueryAllMajors(String Year);

        SelectList QueryAllMajors(String Year, String Selected);

        SelectList QueryNoAllMajors();

        SelectList QueryNoAllMajors(String selected);

        SelectList QueryNoAllMajors(String selected, String major_art_science);

        SelectList Demotion();

        SelectList Demotion(String selected);

        SelectList UserArtsScience();

        SelectList UserArtsScience(String selected);

        SelectList FileClass();

        SelectList FileClass(String selected);

        ArgsHelp AddMajor(System_Majors majors);

        ArgsHelp DelectMajor(String majorID);

        List<System_Majors> QueryAllMajorInfo(String date);

        System_Majors QueryMajorInfo(String MajorID);

        ArgsHelp UpdateMajorNum(String MajorID, String Num);
    }
}
