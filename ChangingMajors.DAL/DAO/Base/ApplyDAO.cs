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
    public class ApplyDAO
    {

        public static SelectList Demotion()
        {
            return Demotion(null);
        }

        public static SelectList Demotion(String select)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "同意",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "不同意",
                Value = "0"
            });
            return new SelectList(items, "Value", "Text", select);
        }

        public static SelectList UserArtsScience()
        {
            return UserArtsScience(null);
        }

        public static SelectList UserArtsScience(String select)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "理科",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "文科",
                Value = "0"
            });
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList FileClass()
        {
            return FileClass(null);
        }

        public static SelectList FileClass(String select)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "有",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "无",
                Value = "0"
            });
            return new SelectList(items, "Value", "Text", select);
        }

        /// <summary>
        /// 添加报名
        /// </summary>
        /// <param name="userEntry"></param>
        /// <returns></returns>
        public static ArgsHelp AddNewApply(User_Entry userEntry)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var start = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_flag == 1);
                if (start != null)
                {
                    userEntry.start_id = start.start_id;
                    var entry = cmms.User_Entry.AsParallel().FirstOrDefault(x => x.user_num == userEntry.user_num && x.start_id == userEntry.start_id && x.entry_flag == 1);
                    if (entry != null)
                    {
                        entry.sys_major_id = userEntry.sys_major_id;
                        entry.sys_major_name = userEntry.sys_major_name;
                    }
                    else
                    {
                        cmms.AddToUser_Entry(userEntry);
                    }
                    try
                    {
                        cmms.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ArgsHelp(ex.ToString());
                    }
                }

                return new ArgsHelp("添加报名成功", true);
            }
        }

        public static ArgsHelp CloseApply(User_Entry userEntry)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.User_Entry.AsParallel().FirstOrDefault(x => x.entry_id == userEntry.entry_id);
                cmmsdata.entry_flag = 0;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("删除报名成功", true);
            }

        }

        public static ArgsHelp FindApplyToClose()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.User_Entry.Where(x => x.entry_flag == 1).ToList();
                if (cmmsdata != null)
                {
                    for (int i = 0; i < cmmsdata.Count; i++)
                    {
                        CloseApply(cmmsdata[i]);
                    }
                    return new ArgsHelp("删除报名成功", true);
                }
                return new ArgsHelp("null");
            }
        }

        public static ArgsHelp FindApplyToAdd()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var start = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_flag == 1);
                var cmmsData = cmms.System_User.Where(x => x.user_apply_flag == 1 && x.user_flag == 1).ToList();
                if (start != null)
                {
                    if (cmmsData != null)
                    {
                        for (int i = 0; i < cmmsData.Count(); i++)
                        {
                            User_Entry entry = new User_Entry()
                            {
                                user_num = cmmsData[i].user_num,
                                start_id = start.start_id,
                                major_name = cmmsData[i].major_name,
                                major_id = cmmsData[i].user_major_id,
                                sys_major_id = cmmsData[i].user_sys_major_id,
                                sys_major_name = cmmsData[i].sys_major_name,
                                entry_flag = 1,
                                entry_state = 0
                            };
                            AddNewApply(entry);
                        }
                        return new ArgsHelp("添加报名成功", true);
                    }
                }
                return new ArgsHelp("null");
            }
        }





    }
}
