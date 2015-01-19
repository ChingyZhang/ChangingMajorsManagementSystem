using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;


namespace ChangingMajors.DAL.DAO.Base
{
    public class StartDAO
    {
        public static ArgsHelp clossManager(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == start.start_date);
                cmmsdata.upload_state = "2";
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("修改状态成功", true);
            }
        }

        public static ArgsHelp startManager(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == start.start_date);
                cmmsdata.upload_state = "1";
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("修改状态成功", true);
            }
        }

        /// <summary>
        /// 查询所有的可以发起专业的时间
        /// </summary>
        /// <returns></returns>
        public static SelectList QueryAllDate()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_User.Where(x => x.user_flag == 1 && x.user_apply_flag == 0);
                var cmmsDate = (from s in cmmsData select s.user_date).Distinct().ToList();
                if (cmmsDate.Count != 0)
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    for (int i = 0; i < cmmsDate.Count; i++)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = cmmsDate[i].ToString(),
                            Value = cmmsDate[i].ToString()
                        });
                    }
                    return new SelectList(items, "Value", "Text");
                }
                return null;
            }
        }

        public static User_ChangingMajors_Start QueryStartByID(String StartID)
        {
            User_ChangingMajors_Start start = new User_ChangingMajors_Start();
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_id == Convert.ToInt32(StartID));
                start.start_id = cmmsdata.start_id;
                start.user_num = cmmsdata.user_num;
                start.start_date = cmmsdata.start_date;
                start.start_flag = cmmsdata.start_flag;
            }
            return start;
        }

        public static List<User_ChangingMajors_Start> QueryAllCloseStart()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.User_ChangingMajors_Start.Where(x => x.start_flag == 1).ToList();
                return cmmsdata;
            }
        }


        /// <summary>
        /// 发起新的一次报名
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp AddStart(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                cmms.AddToUser_ChangingMajors_Start(start);
                try
                {
                    cmms.SaveChanges();
                }

                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("发起报名成功", true);
            }

        }
        /// <summary>
        /// 开始学生
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp AddStartStu(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsStuData = cmms.System_User.Where(x => x.user_date == start.start_date).ToList();
                if (cmmsStuData != null)
                {
                    for (int i = 0; i < cmmsStuData.Count(); i++)
                    {
                        //cmmsStuData[i].user_apply_flag = 1;
                        var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_id == cmmsStuData[i].user_id);
                        cmmsData.user_apply_flag = 1;
                        try
                        {
                            cmms.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return new ArgsHelp(ex.ToString());
                        }
                    }
                    return new ArgsHelp("发起学生成功", true);
                }
                return new ArgsHelp("null");
            }
        }
        /// <summary>
        /// 开始专业
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp AddStartMaj(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsMajData = cmms.System_Majors.Where(x => x.major_date == start.start_date).ToList();
                if (cmmsMajData != null)
                {
                    for (int i = 0; i < cmmsMajData.Count(); i++)
                    {
                        //cmmsMajData[i].major_flag = 1;
                        var cmmsData = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_id == cmmsMajData[i].major_id);
                        cmmsData.major_start_flag = 1;
                        cmmsData.major_flag = 1;
                        try
                        {
                            cmms.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return new ArgsHelp(ex.ToString());
                        }
                    }
                    return new ArgsHelp("发起专业成功", true);
                }
                return new ArgsHelp("null");
            }
        }
        /// <summary>
        /// 结束某次报名
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp EndApply(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_id == start.start_id);
                cmmsData.start_flag = 2;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("结束报名成功", true);
            }
        }
        /// <summary>
        /// 结束学生
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp EndStartStu(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsStuData = cmms.System_User.Where(x => x.user_date == start.start_date).ToList();
                if (cmmsStuData != null)
                {
                    for (int i = 0; i < cmmsStuData.Count(); i++)
                    {
                        //cmmsStuData[i].user_apply_flag = 0;
                        var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_id == cmmsStuData[i].user_id);
                        cmmsData.user_apply_flag = 0;
                        try
                        {
                            cmms.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return new ArgsHelp(ex.ToString());
                        }
                    }
                    return new ArgsHelp("结束学生成功", true);
                }
                return new ArgsHelp("null");
            }
        }
        /// <summary>
        /// 结束专业
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp EndStartMaj(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsMajData = cmms.System_Majors.Where(x => x.major_date == start.start_date).ToList();
                if (cmmsMajData != null)
                {
                    for (int i = 0; i < cmmsMajData.Count(); i++)
                    {
                        //cmmsMajData[i].major_flag = 0;
                        var cmmsData = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_id == cmmsMajData[i].major_id);
                        cmmsData.major_start_flag = 0;
                        cmmsData.major_flag = 0;
                        try
                        {
                            cmms.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return new ArgsHelp(ex.ToString());
                        }
                    }
                    return new ArgsHelp("结束专业成功", true);
                }
                return new ArgsHelp("null");
            }
        }
        /// <summary>
        /// 结束专业
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ArgsHelp EndStartApply(User_ChangingMajors_Start start)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsDataEntry = cmms.User_Entry.Where(x => x.start_id == start.start_id).ToList();
                if (cmmsDataEntry != null)
                {
                    for (int i = 0; i < cmmsDataEntry.Count(); i++)
                    {
                        //cmmsDataEntry[i].entry_flag = 0;
                        var cmmsData = cmms.User_Entry.AsParallel().FirstOrDefault(x => x.entry_id == cmmsDataEntry[i].entry_id);
                        cmmsData.entry_flag = 0;
                        try
                        {
                            cmms.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return new ArgsHelp("结束报名者成功", true);
                        }
                    }
                }
                return new ArgsHelp("null");
            }
        }


    }
}
