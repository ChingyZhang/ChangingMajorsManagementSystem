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
    public class CollectDAO
    {
        public static List<User_Entry> QueryChangeMajorTable(String MajorName, String SysMajorName)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (MajorName == "全部" && SysMajorName != "全部")
                {
                    var cmmsdata = cmms.User_Entry.Where(x => x.sys_major_id == SysMajorName && x.entry_flag == 1).ToList();
                    return cmmsdata;
                }
                else if (MajorName != "全部" && SysMajorName == "全部")
                {
                    var cmmsdata = cmms.User_Entry.Where(x => x.major_id == MajorName && x.sys_major_name != null && x.entry_flag == 1).ToList();
                    return cmmsdata;
                }
                else if (MajorName == "全部" && SysMajorName == "全部")
                {
                    var cmmsdata = cmms.User_Entry.Where(x => x.entry_flag == 1 && x.sys_major_name != null).ToList();
                    return cmmsdata;
                }
                else
                {
                    var cmmsdata = cmms.User_Entry.Where(x => x.major_id == MajorName && x.sys_major_id == SysMajorName && x.entry_flag == 1).ToList();
                    return cmmsdata;
                }

            }

        }

        public static List<System_User> MajorConvertUser(List<User_Entry> userEntry)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (userEntry != null)
                {
                    List<System_User> user = new List<System_User>();
                    for (int i = 0; i < userEntry.Count; i++)
                    {
                        var cmmsdata = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == userEntry[i].user_num);
                        user.Add(cmmsdata);
                    }
                    return user;
                }
                return null;
            }
        }

        public static SelectList QueryNoAllMajors()
        {
            return QueryNoAllMajors(null, null);
        }

        /// <summary>
        /// 用于专业下拉框的输出
        /// </summary>
        /// <returns></returns>
        public static SelectList QueryNoAllMajors(string selected, string major_art_science)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_Majors.Where(x => x.major_flag == 1 && x.major_start_flag == 1).ToList();
                if (!string.IsNullOrEmpty(major_art_science))
                {
                    cmmsData = cmmsData.Where(x => x.major_art_science == Convert.ToInt32(major_art_science)).ToList();
                }
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
                    return new SelectList(items, "Value", "Text", selected);
                }
                return null;
            }
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
                    items.Add(new SelectListItem
                    {
                        Text = "全部",
                        Value = "全部",
                    });

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
