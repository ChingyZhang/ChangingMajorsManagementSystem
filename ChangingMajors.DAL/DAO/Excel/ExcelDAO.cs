using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Model;
using System.Data;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.DAO.Base;

namespace ChangingMajors.DAL.DAO.Excel
{
    public class ExcelDAO
    {
        ChangingMajors.DAL.ExcelHelper.ExcelHelper excelHelper = new ExcelHelper.ExcelHelper();


        public static ArgsHelp UpdateUserDetail(DataTable dataTable, String Year)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    //var manager = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == Year);
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var lastData = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == dataTable.Rows[0].ToString() && x.detail_date == Year);
                        if (lastData != null)
                        {
                            lastData.user_credit_weighted_average = dataTable.Rows[1].ToString();
                            lastData.user_major_ranking = dataTable.Rows[2].ToString();
                            lastData.user_major_num = dataTable.Rows[3].ToString();
                        }
                        else
                        {
                            system_user_detail detail = new system_user_detail()
                            {
                                user_num = dataTable.Rows[0].ToString(),
                                user_credit_weighted_average = dataTable.Rows[1].ToString(),
                                user_major_ranking = dataTable.Rows[2].ToString(),
                                user_major_num = dataTable.Rows[3].ToString(),
                                detail_date = Year
                            };
                            cmms.AddTosystem_user_detail(detail);
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
                    return new ArgsHelp("添加学生成功", true);
                }
            }
        }


        public static ArgsHelp AddStuByDataTable(DataTable dataTable, String Year)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var upload2 = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == Year);
                        if (dataTable.Rows[i].IsNull(0))
                        {
                            break;
                        }
                        var detail1 = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == dataTable.Rows[i][0].ToString());
                        var user1 = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == dataTable.Rows[i][0].ToString());
                        if (user1 != null)
                        {
                            user1.user_flag = 1;
                            user1.user_apply_flag = 0;
                            user1.user_date = Year;
                            user1.major_name = dataTable.Rows[i][3].ToString();//更新专业
                            var art_science = dataTable.Rows[i][8].ToString() == "理" ? 1 : 0;//获取文理科
                            var majorid = cmms.System_Majors.FirstOrDefault(x => x.major_name == user1.major_name && x.major_art_science == art_science && x.major_date == Year).major_id;
                            user1.user_major_id = majorid.ToString();//在这里也要讲专业id更新
                            if (upload2 != null)
                            {
                                var uploadID = upload2.upload_id;
                                var lastUploadID = user1.upload_id;
                                if (user1.upload_id != uploadID)
                                {
                                    user1.upload_id = uploadID;
                                    var upload1 = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_id == uploadID);
                                    var upload3 = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_id == lastUploadID);
                                    var aa = Convert.ToInt32(upload1.upload_num);
                                    var bb = aa + 1;
                                    upload1.upload_num = bb.ToString();
                                    var cc = Convert.ToInt32(upload3.upload_num);
                                    var dd = cc - 1;
                                    upload3.upload_num = dd.ToString();
                                    if (upload3.upload_num == "0")
                                    {
                                        cmms.DeleteObject(upload3);
                                    }
                                    cmms.SaveChanges();
                                }
                            }
                            else
                            {
                                var lastuploadID = user1.upload_id;
                                system_user_manager manager = new system_user_manager()
                                {
                                    upload_date = Year,
                                    upload_state = "0",
                                    upload_num = "1"
                                };
                                cmms.AddTosystem_user_manager(manager);
                                var upload4 = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_id == lastuploadID);
                                var num = Convert.ToInt32(upload4.upload_num);
                                var newnum = num - 1;
                                upload4.upload_num = newnum.ToString();
                                if (upload4.upload_num == "0")
                                {
                                    cmms.DeleteObject(upload4);
                                }
                                cmms.SaveChanges();
                            }
                            if (detail1 != null)
                            {
                                detail1.detail_date = Year;
                                cmms.SaveChanges();
                            }

                        }
                        else
                        {

                            var user = PackPageToUser(dataTable.Rows[i], Year);
                            cmms.AddToSystem_User(user);
                            system_user_detail detail = new system_user_detail()
                            {
                                user_major_num = user.user_last_major_num,
                                user_major_ranking = user.user_major_ranking,
                                user_num = user.user_num,
                                user_credit_weighted_average = user.user_credit_weighted_average,
                                detail_date = Year
                            };
                            cmms.AddTosystem_user_detail(detail);
                            cmms.SaveChanges();
                        }

                    }
                    var managerdata = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == Year);
                    var studata = cmms.System_User.Where(x => x.user_date == Year && x.user_flag == 1).ToList();
                    if (managerdata == null)
                    {
                        system_user_manager manager = new system_user_manager()
                        {
                            upload_date = Year,
                            upload_state = "0",
                            upload_num = studata.Count.ToString()
                        };
                        cmms.AddTosystem_user_manager(manager);
                        cmms.SaveChanges();
                    }

                    for (int j = 0; j < studata.Count; j++)
                    {
                        var userdata = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == studata[j].user_num && x.user_date == studata[j].user_date);
                        var upload = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == Year);
                        userdata.upload_id = upload.upload_id;
                        cmms.SaveChanges();
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
                return new ArgsHelp("添加学生成功", true);
            }

        }

        public static String CreateExcelFile(List<System_User> user, String path)
        {
            return ExcelHelper.ExcelHelper.CreateExcel(user, path);
        }

        public static ArgsHelp AddDetailByDataTable(DataTable dataTable, String Year)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (dataTable == null)
                {
                    return null;
                }
                else
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var detail = PackPageToDetail(dataTable.Rows[i], Year);
                        var lastDetail = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == detail.user_num);
                        if (lastDetail != null)
                        {
                            lastDetail.user_major_num = detail.user_major_num;
                            lastDetail.user_credit_weighted_average = detail.user_credit_weighted_average;
                            lastDetail.user_major_ranking = detail.user_major_ranking;
                            lastDetail.detail_date = Year;
                            cmms.SaveChanges();
                        }
                        else
                        {
                            cmms.AddTosystem_user_detail(detail);
                        }
                    }
                    try
                    {
                        cmms.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ArgsHelp(ex.ToString());
                    }
                    return new ArgsHelp("添加专业成功", true);
                }
            }
        }

        /// <summary>
        /// 增加专业
        /// </summary>
        /// <param name="dataTable">Helper传出DataTable</param>
        /// <returns></returns>
        public static ArgsHelp AddMajorByDataTable(DataTable dataTable, String Year)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (dataTable == null)
                {
                    return null;
                }
                else
                {
                    var stuInfo = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_date == Year);
                    if (stuInfo != null)
                    {
                        var stuInfoList = cmms.System_User.Where(x => x.user_date == Year).ToList();
                        for (int i = 0; i < stuInfoList.Count; i++)
                        {
                            var UpdateStuInfo = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == stuInfoList[i].user_num);
                            UpdateStuInfo.user_date = Year;
                            cmms.SaveChanges();
                        }
                        var stuManager = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_id == stuInfoList[0].upload_id);
                        if (stuManager != null)
                        {
                            stuManager.upload_date = Year;
                            stuManager.upload_num = stuInfoList.Count.ToString();
                            stuManager.upload_state = "0";
                            cmms.SaveChanges();
                        }
                        else
                        {
                            system_user_manager manager = new system_user_manager()
                            {
                                upload_num = stuInfoList.Count.ToString(),
                                upload_date = Year,
                                upload_state = "0",
                            };
                            cmms.AddTosystem_user_manager(manager);
                            cmms.SaveChanges();
                            var updateStuList = cmms.System_User.Where(x => x.user_date == Year).ToList();
                            var NewManager = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == Year);
                            for (int i = 0; i < updateStuList.Count; i++)
                            {
                                var updateStu = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_date == Year);
                                updateStu.upload_id = NewManager.upload_id;
                                cmms.SaveChanges();
                            }
                        }

                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var major = PackPageToMajor(dataTable.Rows[i], Year);
                        var lastMajor = cmms.System_Majors.AsParallel().FirstOrDefault(x => x.major_name == major.major_name && x.major_art_science == major.major_art_science && x.major_date == Year);
                        if (lastMajor != null)
                        {
                            lastMajor.major_flag = 1;
                            lastMajor.major_start_flag = 0;
                        }
                        else
                        {
                            cmms.AddToSystem_Majors(major);
                        }
                    }
                    try
                    {
                        cmms.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ArgsHelp(ex.ToString());
                    }
                    return new ArgsHelp("添加专业成功", true);
                }
            }
        }

        /// <summary>
        /// 将用户信息打包后放入
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static System_User PackPageToUser(DataRow dataRow, String Year)
        {
            //for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            //{
            //    if (dataRow.IsNull(i))
            //    {
            //        dataRow[i] = "NULL";
            //    }
            //}

            if (dataRow[2].ToString() == "男")
            {
                dataRow[2] = 1;
            }
            else
            {
                dataRow[2] = 0;
            }
            if (dataRow[8].ToString() == "理科" || dataRow[8].ToString() == "理")
            {
                dataRow[8] = 1;
            }
            else
            {
                dataRow[8] = 0;
            }

            if (dataRow[11].ToString() == "无")
            {
                dataRow[11] = 0;
            }
            else
            {
                dataRow[11] = 1;
            }

            System_User sysUser = new System_User()
            {
                user_num = dataRow[0].ToString(),
                user_password = dataRow[0].ToString(),
                user_name = dataRow[1].ToString(),
                user_sex = Convert.ToInt32(dataRow[2]),
                major_name = dataRow[3].ToString(),
                user_major_ranking = dataRow[6].ToString(),
                //user_major_ranking_ratio = dataRow[8].ToString(),
                user_arts_science = Convert.ToInt32(dataRow[8]),
                //user_english_level_four = Convert.ToInt32(dataRow[9]),
                //user_english_level_six = Convert.ToInt32(dataRow[10]),
                //user_address = dataRow[12].ToString(),
                //sys_major_name = dataRow[13].ToString(),
                //user_demotion = Convert.ToInt32(dataRow[14]),
                user_flied_class = Convert.ToInt32(dataRow[11]),
                //user_disciplinary_award = Convert.ToInt32(dataRow[16]),
                //user_long_phone = dataRow[17].ToString(),
                //user_short_phone = dataRow[18].ToString(),
                user_flag = 1,
                user_power = 2,
                //user_honour = "",
                user_apply_flag = 0,
                user_date = Year
            };
            if (!dataRow.IsNull(3) && !dataRow.IsNull(8))
            {
                sysUser.user_major_id = MajorsDAO.QueryMajorIDByNameAndArts(dataRow[3].ToString(), Convert.ToInt32(dataRow[8])).major_id.ToString();
            }
            if (!dataRow.IsNull(9))
            {
                sysUser.user_english_level_four = dataRow[9].ToString();
            }
            if (!dataRow.IsNull(10))
            {
                sysUser.user_english_level_six = dataRow[10].ToString();
            }
            if (!dataRow.IsNull(4))
            {
                sysUser.user_class = dataRow[4].ToString();
            }
            if (!dataRow.IsNull(5))
            {
                sysUser.user_credit_weighted_average = dataRow[5].ToString();
            }
            if (!dataRow.IsNull(7))
            {
                sysUser.user_last_major_num = dataRow[7].ToString();
            }

            return sysUser;
        }

        private static system_user_detail PackPageToDetail(DataRow dataRow, String Year)
        {
            system_user_detail detail = new system_user_detail()
            {
                user_num = dataRow[0].ToString(),
                user_credit_weighted_average = dataRow[1].ToString(),
                user_major_ranking = dataRow[2].ToString(),
                user_major_num = dataRow[3].ToString(),
                detail_date = Year
            };
            return detail;
        }

        /// <summary>
        /// 把专业信息通过行打包为可存储格式
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static System_Majors PackPageToMajor(DataRow dataRow, String Year)
        {
            if (dataRow[0].ToString() == "理科" || dataRow[0].ToString() == "理")
            {
                dataRow[0] = 1;
            }
            else
            {
                dataRow[0] = 0;
            }
            System_Majors sysMajors = new System_Majors()
            {
                major_name = dataRow[1].ToString(),
                major_art_science = Convert.ToInt32(dataRow[0].ToString()),
                major_flag = 1,
                major_date = Year,
                major_start_flag = 0
            };
            if (!dataRow.IsNull(2))
            {
                sysMajors.major_num = Convert.ToInt32(dataRow[2].ToString());
            }

            return sysMajors;
        }
    }
}
