using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;
using System.Web.Mvc;

namespace ChangingMajors.DAL.DAO.Base
{
    public class MajorsDAO
    {

        public static ArgsHelp AddMajor(System_Majors majors)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var data = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_name == majors.major_name && x.major_date == majors.major_date);
                if (data != null)
                {
                    data.major_date = majors.major_date;
                    data.major_flag = 1;
                    data.major_art_science = majors.major_art_science;
                    data.major_num = majors.major_num;
                    data.major_start_flag = 0;
                }
                else
                {
                    cmms.AddToSystem_Majors(majors);
                }
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("添加成功", true);
            }
        }

        public static ArgsHelp DelectMajor(String majorID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var delectError = cmms.User_Entry.AsParallel().FirstOrDefault(x => x.major_id == majorID || x.sys_major_id == majorID);
                if (delectError != null)
                {
                    return new ArgsHelp("无法删除,存在关联项", false);
                }
                var delectData = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_id == Convert.ToInt32(majorID));
                cmms.DeleteObject(delectData);
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("删除成功", true);

            }
        }

        /// <summary>
        /// 查询全部信息
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<System_Majors> QueryAllMajorInfo(String date)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var dateData = cmms.System_Majors.Where(x => x.major_date == date).ToList();
                return dateData;
            }
        }

        public static System_Majors QueryMajorInfo(String MajorID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var data = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_id == Convert.ToInt32(MajorID));
                return data;
            }
        }

        public static ArgsHelp UpdateMajorNum(String MajorID, String Num)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var data = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_id == Convert.ToInt32(MajorID));
                data.major_num = Convert.ToInt32(Num);
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("修改成功", true);
            }
        }

        /// <summary>
        /// 所有已经发起的时间
        /// </summary>
        /// <returns></returns>
        public static List<String[]> MajorDateAndStuDate()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.System_Majors.Where(x => x.major_date != null && x.major_flag == 1);
                var cmmsDate = (from m in cmmsdata select m.major_date).Distinct().ToList();
                List<String[]> result = new List<String[]>();
                for (int i = 0; i < cmmsDate.Count; i++)
                {
                    var startState = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_date == cmmsDate[i].ToString());
                    var str = new String[2];
                    if (startState != null)
                    {

                        if (startState.start_flag == 0)
                        {
                            str[0] = startState.start_date;
                            str[1] = "(未发起)";
                        }
                        if (startState.start_flag == 1)
                        {
                            str[0] = startState.start_date;
                            str[1] = "(已发起)";
                        }
                        if (startState.start_flag == 2)
                        {
                            str[0] = startState.start_date;
                            str[1] = "(已结束)";
                        }
                        result.Add(str);
                    }
                    else
                    {
                        str[0] = cmmsDate[i];
                        str[1] = "";
                        result.Add(str);
                    }

                }
                return result;
            }
        }

        /// <summary>
        /// 所有已经发起的时间
        /// </summary>
        /// <returns></returns>
        public static List<String[]> StuDateAndStuDate()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                List<String[]> result = new List<String[]>();
                //var StartDateData = cmms.User_ChangingMajors_Start.Where(x => x.start_flag == 0).ToList();
                //if (StartDateData.Count != 0)
                //{
                //    for (int i = 0; i < StartDateData.Count; i++)
                //    {
                //        String[] str = new String[2];
                //        str[0] = StartDateData[i].start_date;
                //        str[1] = "(已结束)";
                //        result.Add(str);
                //    }
                //}
                var uploadDateData = cmms.system_user_manager.Where(x => x.upload_state == "0").ToList();
                if (uploadDateData.Count != 0)
                {
                    for (int i = 0; i < uploadDateData.Count; i++)
                    {
                        String[] str = new String[2];
                        str[0] = uploadDateData[i].upload_date;
                        str[1] = "(已存在数据)";
                        result.Add(str);
                    }
                }
                else
                {
                    var cmmsdata = cmms.System_Majors.Where(x => x.major_date != null && x.major_flag == 1);
                    var cmmsDate = (from m in cmmsdata select m.major_date).Distinct().ToList();
                    for (int i = 0; i < cmmsDate.Count; i++)
                    {
                        var startState = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_date == cmmsDate[i].ToString());
                        var str = new String[2];
                        if (startState != null)
                        {

                            if (startState.start_flag == 0)
                            {
                                str[0] = startState.start_date;
                                str[1] = "(未发起)";
                            }
                            if (startState.start_flag == 1)
                            {
                                str[0] = startState.start_date;
                                str[1] = "(已发起)";
                            }
                            if (startState.start_flag == 2)
                            {
                                str[0] = startState.start_date;
                                str[1] = "(已结束)";
                            }
                            result.Add(str);
                        }
                        else
                        {
                            str[0] = cmmsDate[i];
                            str[1] = "";
                            result.Add(str);
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 通过ID来查专业名称
        /// </summary>
        /// <param name="majorID"></param>
        /// <returns></returns>
        public static System_Majors QueryNameByID(int? majorID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var nameRow = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_id == majorID && x.major_flag == 1);
                return nameRow;
            }
        }

        /// <summary>
        /// 废弃
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int? QueryIDByName(String Name, String date)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var idRow = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_name == Name && x.major_date == date);
                var majorID = idRow.major_id;
                return majorID;
            }
        }

        /// <summary>
        /// 通过名字和文理来查现在可用的专业的ID
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Arts"></param>
        /// <returns></returns>
        public static System_Majors QueryMajorIDByNameAndArts(String Name, int Arts)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var ID = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_name == Name && x.major_art_science == Arts && x.major_flag == 1);
                return ID;
            }
        }

        /// <summary>
        /// 用于专业下拉框的输出
        /// </summary>
        /// <returns></returns>
        public static SelectList QueryAllMajorsByYear(String Year, String select)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_Majors.Where(x => x.major_flag == 1 && x.major_date == Year).ToList();
                if (cmmsData.Count != 0)
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    for (int i = 0; i < cmmsData.Count(); i++)
                    {
                        String text;
                        if (cmmsData[i].major_art_science == 0)
                        {
                            text = cmmsData[i].major_name + "(文)" + cmmsData[i].major_num;
                        }
                        else
                        {
                            text = cmmsData[i].major_name + "(理)" + cmmsData[i].major_num;
                        }
                        items.Add(new SelectListItem
                        {
                            Text = text,
                            Value = cmmsData[i].major_id.ToString()
                        });
                    }
                    return new SelectList(items, "Value", "Text", select);
                }
                return null;
            }
        }

        public static SelectList QueryAllMajorsByYear(String Year)
        {
            return QueryAllMajorsByYear(Year, null);
        }

        /// <summary>
        /// 用于专业下拉框的输出
        /// </summary>
        /// <returns></returns>
        public static SelectList QueryAllMajors()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_Majors.Where(x => x.major_flag == 1 && x.major_start_flag == 1).ToList();
                if (cmmsData.Count != 0)
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    for (int i = 0; i < cmmsData.Count(); i++)
                    {
                        String text;
                        if (cmmsData[i].major_art_science == 0)
                        {
                            text = cmmsData[i].major_name + "(文)" + cmmsData[i].major_num;
                        }
                        else
                        {
                            text = cmmsData[i].major_name + "(理)" + cmmsData[i].major_num;
                        }
                        items.Add(new SelectListItem
                        {
                            Text = text,
                            Value = cmmsData[i].major_id.ToString()
                        });
                    }
                    return new SelectList(items, "Value", "Text");
                }
                return null;
            }
        }
    }
}
