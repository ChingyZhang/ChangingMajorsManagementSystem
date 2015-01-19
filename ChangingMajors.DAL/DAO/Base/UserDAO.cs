using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;

namespace ChangingMajors.DAL.DAO.Base
{
    public class UserDAO
    {
        /// <summary>
        /// 登陆方法
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userPsw"></param>
        /// <returns></returns>
        public static UserModel Login(String userID, String userPsw, String pour)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (pour == "0")
                {
                    var user = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == userID && x.user_password == userPsw && x.user_flag == 1 && x.user_power != 2);
                    return packageUserModel(user);
                }
                else if (pour == "1")
                {
                    var user = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == userID && x.user_password == userPsw && x.user_flag == 1 && x.user_power == 2);
                    return packageUserModel(user);
                }
                else
                {
                    return null;
                }
            }
        }



        public static System_User QueryStuByNumAndDate(String StuNum, String Date)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == StuNum && x.user_date == Date);
                return cmmsData;
            }
        }

        public static system_user_manager QueryManagerByDate(String Date)
        {
            using (ChangingMajorsManagementSystemEntities cmms=new ChangingMajorsManagementSystemEntities())
            {
                var manager = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == Date);
                return manager;
            }
        }

        public static ArgsHelp DelectUser(System_User sysUser)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num && x.user_date == sysUser.user_date);
                if (cmmsdata == null)
                {
                    return new ArgsHelp("该用户不存在", false);
                }
                else
                {
                    var userdetaildata = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num);
                    if (userdetaildata != null)
                    {
                        cmms.DeleteObject(userdetaildata);
                    }
                    var cmmsData = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_id == cmmsdata.upload_id && x.upload_state == "0" && x.upload_date == cmmsdata.user_date);
                    if (cmmsData != null)
                    {
                        var aa = Convert.ToInt32(cmmsData.upload_num);
                        var bb = aa - 1;
                        if (bb == 0)
                        {
                            cmms.DeleteObject(cmmsData);
                        }
                        else
                        {
                            cmmsData.upload_num = bb.ToString();
                        }
                        cmms.DeleteObject(cmmsdata);
                    }
                    else
                    {
                        return new ArgsHelp("该用户处于发起态无法删除", false);
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
                return new ArgsHelp("删除用户成功", true);
            }
        }


        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public static ArgsHelp AddUser(System_User sysUser)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num);
                //var cmmsData=cmms
                if (cmmsdata != null)
                {
                    cmmsdata.user_flag = 1;
                    cmmsdata.user_apply_flag = 0;
                    cmmsdata.user_date = sysUser.user_date;
                }
                else
                {
                    cmms.AddToSystem_User(sysUser);
                    cmms.SaveChanges();
                }
                system_user_detail detail = new system_user_detail()
                {
                    user_major_num = sysUser.user_last_major_num,
                    user_num = sysUser.user_num,
                    user_major_ranking = sysUser.user_major_ranking,
                    user_credit_weighted_average = sysUser.user_credit_weighted_average,
                    detail_date = sysUser.user_date,
                };
                cmms.AddTosystem_user_detail(detail);
                var upload = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == sysUser.user_date);
                if (upload == null)
                {
                    system_user_manager DBupload = new system_user_manager()
                    {
                        upload_num = "1",
                        upload_date = sysUser.user_date,
                        upload_state = "0"
                    };
                    cmms.AddTosystem_user_manager(DBupload);
                    cmms.SaveChanges();
                }
                else
                {
                    var aa = Convert.ToInt32(upload.upload_num);
                    var bb = aa + 1;
                    upload.upload_num = bb.ToString();
                }

                var id = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_date == sysUser.user_date);
                var cmmsdata1 = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num);
                cmmsdata1.upload_id = id.upload_id;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("添加新用户成功", true);
            }
        }


        public static ArgsHelp UpdateUser1(System_User sysUser)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (sysUser.user_num != null)
                {
                    var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num && x.user_flag == 1);
                    cmmsData.user_num = sysUser.user_num;
                    cmmsData.user_name = sysUser.user_name;
                    cmmsData.user_credit_weighted_average = sysUser.user_credit_weighted_average;
                    cmmsData.user_english_level_four = sysUser.user_english_level_four;
                    cmmsData.user_english_level_six = sysUser.user_english_level_six;
                    cmmsData.user_major_ranking = sysUser.user_major_ranking;
                    cmmsData.user_address = sysUser.user_address;
                    cmmsData.user_disciplinary_award = sysUser.user_disciplinary_award;
                    cmmsData.user_long_phone = sysUser.user_long_phone;
                    cmmsData.user_short_phone = sysUser.user_short_phone;
                    cmmsData.user_flied_class = sysUser.user_flied_class;
                    cmmsData.user_honour = sysUser.user_honour;
                    try
                    {
                        cmms.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ArgsHelp(ex.ToString());
                    }
                    return new ArgsHelp("修改学生资料成功", true);
                }
                else
                {
                    return new ArgsHelp("");
                }
            }
        }



        /// <summary>
        /// 学生信息修改,针对管理员修改
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public static ArgsHelp UpdateUser(System_User sysUser)
        {
            //int i;
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (sysUser.user_num != null)
                {
                    var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num && x.user_flag == 1);
                    cmmsData.user_num = sysUser.user_num;
                    cmmsData.user_name = sysUser.user_name;
                    //cmmsData.user_credit_weighted_average = sysUser.user_credit_weighted_average;
                    cmmsData.user_address = sysUser.user_address;
                    cmmsData.user_disciplinary_award = sysUser.user_disciplinary_award;
                    cmmsData.user_long_phone = sysUser.user_long_phone;
                    cmmsData.user_short_phone = sysUser.user_short_phone;
                    //cmmsData.user_flied_class = sysUser.user_flied_class;
                    cmmsData.user_honour = sysUser.user_honour;
                    cmmsData.sys_major_name = sysUser.sys_major_name;
                    cmmsData.user_sys_major_id = sysUser.user_sys_major_id;
                    cmmsData.user_demotion = sysUser.user_demotion;
                    try
                    {
                        cmms.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ArgsHelp(ex.ToString());
                    }
                    return new ArgsHelp("修改学生资料成功", true);
                }
                else
                {
                    return new ArgsHelp("");
                }
            }
        }


        /// <summary>
        /// 学生密码修改,通用
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public static ArgsHelp UpdateUserToAdm(System_User sysUser)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num && x.user_flag == 1);
                cmmsData.user_password = sysUser.user_password;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("修改学生密码成功", true);
            }
        }

        /// <summary>
        /// 学生信息修改,针对学生修改
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public static ArgsHelp UpdateUserToStu(System_User sysUser)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == sysUser.user_num && x.user_flag == 1);
                cmmsData.user_sys_major_id = sysUser.user_sys_major_id;
                cmmsData.sys_major_name = sysUser.sys_major_name;
                cmmsData.user_demotion = sysUser.user_demotion;
                cmmsData.user_address = sysUser.user_address;
                cmmsData.user_disciplinary_award = sysUser.user_disciplinary_award;
                cmmsData.user_long_phone = sysUser.user_long_phone;
                cmmsData.user_short_phone = sysUser.user_short_phone;
                cmmsData.user_honour = sysUser.user_honour;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("修改资料成功", true);
            }
        }

        /// <summary>
        /// 通过id,姓名,专业,来查学生,管理员方法
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<System_User> QueryStuByIDNameMajor(String userID, String userName, int? userMajor)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                if (userID != null)
                {
                    var user = cmms.System_User.Where(x => x.user_num == userID && x.user_flag == 1).ToList();
                    if (user == null)
                    {
                        return null;
                    }
                    else
                    {
                        for (int i = 0; i < user.Count; i++)
                        {
                            var detail = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == user[i].user_num);
                            if (detail != null)
                            {
                                user[i].user_credit_weighted_average = detail.user_credit_weighted_average;
                                user[i].user_last_major_num = detail.user_major_num;
                                user[i].user_major_ranking = detail.user_major_ranking;
                            }
                        }
                        return user;
                    }
                }
                else
                {
                    if (userName != null && userMajor == null)
                    {
                        var user = cmms.System_User.Where(x => x.user_name == userName && x.user_flag == 1).ToList();
                        if (user == null)
                        {
                            return null;
                        }
                        else
                        {
                            for (int i = 0; i < user.Count; i++)
                            {
                                var detail = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == user[i].user_num);
                                if (detail != null)
                                {
                                    user[i].user_credit_weighted_average = detail.user_credit_weighted_average;
                                    user[i].user_last_major_num = detail.user_major_num;
                                    user[i].user_major_ranking = detail.user_major_ranking;
                                }
                            }
                            return user;
                        }
                    }
                    else
                    {
                        if (userName == null && userMajor != null)
                        {
                            var user = cmms.System_User.Where(x => x.user_major_id == userMajor.ToString() && x.user_flag == 1).ToList();
                            if (user == null)
                            {
                                return null;
                            }
                            else
                            {
                                for (int i = 0; i < user.Count; i++)
                                {
                                    var detail = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == user[i].user_num);
                                    if (detail != null)
                                    {
                                        user[i].user_credit_weighted_average = detail.user_credit_weighted_average;
                                        user[i].user_last_major_num = detail.user_major_num;
                                        user[i].user_major_ranking = detail.user_major_ranking;
                                    }
                                }
                                return user;
                            }
                        }
                        else if (userName != null && userMajor != null && userID != null)
                        {
                            var user = cmms.System_User.Where(x => x.user_name == userName && x.user_flag == 1).Where(x => x.user_major_id == userMajor.ToString()).ToList();
                            if (user == null)
                            {
                                return null;
                            }
                            else
                            {
                                for (int i = 0; i < user.Count; i++)
                                {
                                    var detail = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == user[i].user_num);
                                    if (detail != null)
                                    {
                                        user[i].user_credit_weighted_average = detail.user_credit_weighted_average;
                                        user[i].user_last_major_num = detail.user_major_num;
                                        user[i].user_major_ranking = detail.user_major_ranking;
                                    }
                                }
                                return user;
                            }
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 根据ID查询此人全部详细值
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static System_User FindUserInfoByID(String UserID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == UserID && x.user_flag == 1);
                if (cmmsData == null)
                {
                    return null;
                }
                var detail = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == UserID);
                if (detail==null)
                {
                    return null;
                }
                cmmsData.user_credit_weighted_average = detail.user_credit_weighted_average;
                cmmsData.user_major_ranking = detail.user_major_ranking;
                cmmsData.user_last_major_num = detail.user_major_num;
                return cmmsData;
            }
        }

        /// <summary>
        /// 用于包装userModel
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserModel packageUserModel(System_User user)
        {
            if (user == null)
            {
                return null;
            }
            var detail = QueryDetailByNum(user.user_num);
            UserModel model = new UserModel
            {
                UserID = user.user_id,
                UserNum = user.user_num,
                UserPassword = user.user_password,
                UserName = user.user_name,
                UserSex = user.user_sex,
                UserClass = user.user_class,
                UserMajorRanking = detail.user_major_ranking,
                UserLastMajorNum = detail.user_major_num,
                UserCreditWeightedAverage = detail.user_credit_weighted_average,
                //UserMajorRankingRatio = detail.user_major_ranking,
                UserArtsScience = user.user_arts_science,
                //UserEnglishPower = user.user_english_level,
                UserEnglishFour = user.user_english_level_four,
                UserEnglishSix = user.user_english_level_six,
                UserAddress = user.user_address,
                UserDemotion = user.user_demotion,
                UserFiledClass = user.user_flied_class,
                UserDisciplinaryAward = user.user_disciplinary_award,
                UserLongPhone = user.user_long_phone,
                UserShortPhone = user.user_short_phone,
                UserHonour = user.user_honour,
                UserPower = user.user_power,
                UserFlag = user.user_flag,
                MajorName = user.major_name,
                MajorID = user.user_major_id,
                SysMajorID = user.user_sys_major_id,
                SysMajorName = user.sys_major_name,
                UsserDate = user.user_date
            };
            return model;
        }


        public static List<system_user_manager> QueryAllUpload()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var manager = cmms.system_user_manager.Where(x => x.upload_state == "0").ToList();
                return manager;
            }
        }

        public static ArgsHelp AddDetail(system_user_detail userDetail)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                cmms.AddTosystem_user_detail(userDetail);
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("添加学生成功", true);
            }
        }

        public static ArgsHelp DelectDetail(system_user_detail userDetail)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == userDetail.user_num);
                cmms.DeleteObject(cmmsdata);
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

        public static ArgsHelp DelectUser(system_user_manager userManager)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.System_User.Where(x => x.user_date == userManager.upload_date).ToList();
                for (int i = 0; i < cmmsdata.Count; i++)
                {
                    var cmmsDelectData = cmms.System_User.AsParallel().FirstOrDefault(x => x.user_num == cmmsdata[i].user_num && x.user_date == cmmsdata[i].user_date);
                    cmms.DeleteObject(cmmsDelectData);
                }
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


        public static system_user_detail QueryDetailByNum(String userNum)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsdata = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == userNum);
                system_user_detail userDetail;
                if (cmmsdata != null)
                {
                    userDetail = new system_user_detail()
                    {
                        user_num = cmmsdata.user_num,
                        user_major_num = cmmsdata.user_major_num,
                        user_credit_weighted_average = cmmsdata.user_credit_weighted_average,
                        user_major_ranking = cmmsdata.user_major_ranking,
                        detail_date = cmmsdata.detail_date
                    };
                }
                else
                {
                    userDetail = new system_user_detail()
                    {
                        user_num = null,
                        user_major_num = null,
                        user_credit_weighted_average = null,
                        user_major_ranking = null,
                        detail_date = null
                    };
                }
                return userDetail;
            }
        }


        public static List<system_user_manager> QueryAllManager()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.system_user_manager.Where(x => x.upload_state !="1").ToList();
                return cmmsData;
            }
        }


        public static ArgsHelp DelectUser1(String uploadID)
        {

            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var id = Convert.ToInt32(uploadID);
                var countdata = cmms.System_User.Where(x => x.upload_id == id).ToList();
                for (int i = 0; i < countdata.Count; i++)
                {
                    var cmmsData = cmms.System_User.AsParallel().FirstOrDefault(x => x.upload_id == Convert.ToInt32(uploadID));
                    DelectDetail(cmmsData.user_num, cmmsData.user_date);
                    cmms.DeleteObject(cmmsData);
                    try
                    {
                        cmms.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ArgsHelp(ex.ToString());
                    }
                }
                //DelectStart(countdata[0].user_date);
                return DelectManager(uploadID);
            }
        }

        private static ArgsHelp DelectStart(String Date)
        {
            using (ChangingMajorsManagementSystemEntities cmms=new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.User_ChangingMajors_Start.AsParallel().FirstOrDefault(x => x.start_date == Date);
                cmms.DeleteObject(cmmsData);
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

        private static ArgsHelp DelectDetail(String userNum, String detailDate)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var cmmsData = cmms.system_user_detail.AsParallel().FirstOrDefault(x => x.user_num == userNum && x.detail_date == detailDate);
                cmms.DeleteObject(cmmsData);
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

        private static ArgsHelp DelectManager(String uploadID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var DelectData = cmms.system_user_manager.AsParallel().FirstOrDefault(x => x.upload_id == Convert.ToInt32(uploadID));
                cmms.DeleteObject(DelectData);
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




    }
}
