using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangingMajorsManagementSystem.Models;
using ChangingMajors.BLL.Infrastructure;
using ChangingMajors.BLL.Service;
using System.IO;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.Infrastructure;

namespace ChangingMajorsManagementSystem.Controllers
{
    [HandleError]
    public class StudentController : Controller
    {
        public IUserService UserSerivce { get; set; }
        public IApplyService ApplyService { get; set; }
        public IExcelService ExcelService { get; set; }
        PageInfo pageInfo = new PageInfo();
        //
        // GET: /Student/



        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ApplyService == null)
            {
                ApplyService = new ApplyService();
            }
            if (UserSerivce == null)
            {
                UserSerivce = new UserService();
            }
            if (ExcelService == null)
            {
                ExcelService = new ExcelService();
            }
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }


        //public PartialViewResult GetYear()
        //{
        //    var years = DateTime.Now.Year;

        //    List<SelectListItem> items = new List<SelectListItem>();

        //    for (int i = years - 5; i < years + 5; i++)
        //    {
        //        items.Add(new SelectListItem
        //         {
        //             Text = i.ToString(),
        //             Value = i.ToString()
        //         });
        //    }
        //    ViewData["year"] = items;
        //    return PartialView();
        //}

        public PartialViewResult GetYear()
        {
            var haveYear = ApplyService.StuDateAndStuDate();
            var years = DateTime.Now.Year;

            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 0; i < haveYear.Count; i++)
            {
                var str = haveYear[i];
                //var manager = UserSerivce.QueryManagerByDate(haveYear[i][0]);
                //if (manager != null)
                //{
                //    str[1] = "(已有数据)";
                //}
                ////if (str[1] == "(未发起)")
                ////{
                ////    str[1] = "";
                ////}
                items.Add(new SelectListItem
                {
                    Text = str[0] + str[1],
                    Value = str[0]
                });

            }

            ViewData["year"] = items;
            return PartialView();
        }


        public PartialViewResult UpdateStu()
        {
            return PartialView();
        }

        public PartialViewResult DelectUploadStu()
        {
            ViewData["uploadTable"] = UserSerivce.QueryAllManager();
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult DelectUploadStu(String delectUploadID)
        {
            if (delectUploadID != null)
            {
                if (delectUploadID != "")
                {
                    UserSerivce.DelectUser1(delectUploadID);
                }
            }
            ViewData["uploadTable"] = UserSerivce.QueryAllManager();
            return PartialView();
        }

        /// <summary>
        /// 上传学生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult UpdateDetail(HttpPostedFileBase fileData, String UserID, String Year)
        {
            if (fileData != null)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");//获取时间
                string extension = Path.GetExtension(fileData.FileName);//获取扩展名
                string newFileName = time + extension;//重组成新的文件名
                string tempPath = Path.Combine("D:\\UserExcelFile", "adm");//组成存储路径
                //PayVoucherPath.Add(tempPath);

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                string payVoucherPath = Path.Combine(tempPath, newFileName);//完整路径
                fileData.SaveAs(payVoucherPath);
                var response = newFileName + ";" + payVoucherPath;


                //var result = ExcelService.AddStuByDataTable(Path.Combine(tempPath, newFileName).ToString(), Year);
                var result = ExcelService.UpdateUserDetail(Path.Combine(tempPath, newFileName).ToString(), Year);
                //插入更新方法

                return Content(response);
            }
            return Content("");
        }



        public PartialViewResult AddStu()
        {

            return PartialView();
        }

        [HttpPost]
        public PartialViewResult AddStu(UserQueryModel userQueryModel)
        {
            return PartialView();
        }

        /// <summary>
        /// 上传学生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult UploadStu(HttpPostedFileBase fileData, String Year)
        {
            if (fileData != null)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");//获取时间
                string extension = Path.GetExtension(fileData.FileName);//获取扩展名
                string newFileName = time + extension;//重组成新的文件名
                string tempPath = Path.Combine("D:\\UserExcelFile", "admin");//组成存储路径
                //PayVoucherPath.Add(tempPath);

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                string payVoucherPath = Path.Combine(tempPath, newFileName);//完整路径
                fileData.SaveAs(payVoucherPath);
                var response = newFileName + ";" + payVoucherPath;

                var result = ExcelService.AddStuByDataTable(Path.Combine(tempPath, newFileName).ToString(), Year);

                return Content(response);
            }
            return Content("");
        }


        public PartialViewResult AddStuTablePage(String page, String UserNum, String UserName, int? MajorName)
        {
            ViewData["majorname"] = MajorName;

            ViewData["major"] = ApplyService.QueryAllMajors();
            var userinfo = UserSerivce.QueryStuByIDNameMajor(UserNum, UserName, MajorName);

            if (UserNum == null)
            {
                UserNum = "";
            }
            if (UserName == null)
            {
                UserName = "";
            }
            ViewData["UserName"] = UserName;
            ViewData["UserNum"] = UserNum;
            if (page == null)
            {
                pageInfo.CurrentPage = 1;
            }
            else
            {
                pageInfo.CurrentPage = Convert.ToInt32(page);
            }
            pageInfo.TotalItems = userinfo.Count;
            pageInfo.ItemsPerPage = 10;
            ViewData["pageInfo"] = pageInfo;
            var skipNum = 10 * (pageInfo.CurrentPage - 1);
            var aa = userinfo.Skip(skipNum);
            var table = aa.Take(pageInfo.ItemsPerPage).ToList();
            ViewData["UserInfo"] = table;
            return PartialView();
        }


        [HttpPost]
        public PartialViewResult AddStuTable(UserQueryModel userQueryModel, String page)
        {
            ViewData["majorname"] = userQueryModel.MajorName;
            if (userQueryModel.UserName == null)
            {
                ViewData["UserName"] = "";
            }
            else
            {
                ViewData["UserName"] = userQueryModel.UserName;
            }
            if (userQueryModel.UserNum == null)
            {
                ViewData["UserNum"] = "";
            }
            else
            {
                ViewData["UserNum"] = userQueryModel.UserNum;
            }
            ViewData["major"] = ApplyService.QueryAllMajors();
            var userinfo = UserSerivce.QueryStuByIDNameMajor(userQueryModel.UserNum, userQueryModel.UserName, userQueryModel.MajorName);

            if (userinfo != null)
            {
                pageInfo.CurrentPage = 1;
                pageInfo.TotalItems = userinfo.Count;
                pageInfo.ItemsPerPage = 10;
                ViewData["pageInfo"] = pageInfo;
                if (userinfo.Count <= pageInfo.ItemsPerPage)
                {
                    var table = userinfo;
                    ViewData["UserInfo"] = table;
                }
                else
                {
                    var table = userinfo.Take(pageInfo.ItemsPerPage).ToList();
                    ViewData["UserInfo"] = table;
                }
            }
            else
            {
                pageInfo.CurrentPage = 1;
                pageInfo.TotalItems = 0;
                pageInfo.ItemsPerPage = 10;
                ViewData["pageInfo"] = null;
                ViewData["UserInfo"] = null;
            }

            //if (userinfo == null)
            //{
            //    ViewData["UserInfo"] = null;
            //}
            //else
            //{
            //    ViewData["UserInfo"] = userinfo;
            //}
            return PartialView();
        }

        public PartialViewResult StuDetail(UserInfoModel userInfoModel)
        {
            ViewData["major"] = ApplyService.QueryAllMajors();
            ViewData["demotion"] = ApplyService.Demotion();
            ViewData["ArtsScience"] = ApplyService.UserArtsScience();
            ViewData["FileClass"] = ApplyService.FileClass();
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult StuDetailTable(UserInfoModel userInfoModel, String UserNum)
        {
            ViewData["major"] = ApplyService.QueryNoAllMajors();
            ViewData["ArtsScience"] = ApplyService.UserArtsScience();


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

            if (!string.IsNullOrEmpty(UserNum))
            {
                var userInfo = UserSerivce.FindUserInfoByID(UserNum);
                if (userInfo == null)
                {
                    ModelState.Clear();
                    return PartialView(new UserInfoModel());
                }
                userInfoModel.UserNum = userInfo.user_num;
                userInfoModel.UserName = userInfo.user_name;
                userInfoModel.SysMajorName = userInfo.sys_major_name;
                userInfoModel.UserAddress = userInfo.user_address;
                userInfoModel.UserAddress = userInfo.user_address;
                userInfoModel.LongPhone = userInfo.user_long_phone;
                userInfoModel.ShortPhone = userInfo.user_short_phone;
                userInfoModel.Honour = userInfo.user_honour;
                userInfoModel.MajorRanking = userInfo.user_major_ranking;
                userInfoModel.UserCreditWeightedAverage = userInfo.user_credit_weighted_average;
                //userInfoModel.UserMajorRankingRatio = userInfo.user_major_ranking_ratio;
                userInfoModel.UserEnglishLevelFour = userInfo.user_english_level_four;
                userInfoModel.UserEnglishLevelSix = userInfo.user_english_level_six;
                ViewData["FileClass"] = new SelectList(items, "Value", "Text", userInfo.user_flied_class.ToString());
                ViewData["DisciplinaryAward"] = new SelectList(items, "Value", "Text", userInfo.user_disciplinary_award.ToString());
            }
            else
            {
                var userinfo = UserSerivce.QueryStuByIDNameMajor(userInfoModel.UserNum, userInfoModel.UserName, null);
                if (userinfo != null)
                {
                    if (userinfo.Count != 0)
                    {
                        userInfoModel.UserNum = userinfo[0].user_num;
                        userInfoModel.UserName = userinfo[0].user_name;
                        userInfoModel.SysMajorName = userinfo[0].sys_major_name;
                        userInfoModel.UserAddress = userinfo[0].user_address;
                        userInfoModel.UserAddress = userinfo[0].user_address;
                        userInfoModel.UserDemotion = userinfo[0].user_demotion.ToString();
                        userInfoModel.LongPhone = userinfo[0].user_long_phone;
                        userInfoModel.ShortPhone = userinfo[0].user_short_phone;
                        userInfoModel.Honour = userinfo[0].user_honour;
                        userInfoModel.MajorRanking = userinfo[0].user_major_ranking;
                        userInfoModel.UserCreditWeightedAverage = userinfo[0].user_credit_weighted_average;
                        //userInfoModel.UserMajorRankingRatio = userinfo[0].user_major_ranking_ratio;
                        userInfoModel.UserEnglishLevelFour = userinfo[0].user_english_level_four;
                        userInfoModel.UserEnglishLevelSix = userinfo[0].user_english_level_six;
                    }
                    ViewData["FileClass"] = new SelectList(items, "Value", "Text", userinfo[0].user_flied_class.ToString());
                    ViewData["DisciplinaryAward"] = new SelectList(items, "Value", "Text", userinfo[0].user_disciplinary_award.ToString());
                }
            }

            ModelState.Clear();//用于消除action里面的model方面的赋值
            return PartialView(userInfoModel);
        }

        [HttpPost]
        public PartialViewResult StuDetailTableSub(UserInfoModel userInfoModel, String UserID)
        {
            System_User user = new System_User()
            {
                user_num = userInfoModel.UserNum,
                user_name = userInfoModel.UserName,
                user_address = userInfoModel.UserAddress,
                user_disciplinary_award = userInfoModel.UserDisciplinaryAward == "1" ? 1 : 0,
                //user_demotion = Convert.ToInt32(userInfoModel.UserDemotion),不可改
                user_credit_weighted_average = userInfoModel.UserCreditWeightedAverage,
                user_major_ranking = userInfoModel.MajorRanking,
                user_flied_class = userInfoModel.FiledClass == "1" ? 1 : 0,
                user_long_phone = userInfoModel.LongPhone,
                user_short_phone = userInfoModel.ShortPhone,
                user_english_level_four = userInfoModel.UserEnglishLevelFour,
                user_english_level_six = userInfoModel.UserEnglishLevelSix,
                user_honour = userInfoModel.Honour
            };
            var aa = UserSerivce.UpdateUser1(user);

            //UserInfoModel bb = new UserInfoModel();
            //userInfoModel = bb;

            ViewData["Msg"] = aa.Msg;
            ModelState.Clear();
            return PartialView("StuDetailTable", new UserInfoModel());
        }

        public PartialViewResult StuQueryPassword(StuChangePasswordModel stuChangePasswordModel, String UserID)
        {
            if (UserID != null)
            {
                var userinfo = UserSerivce.QueryStuByIDNameMajor(UserID, null, null);
                stuChangePasswordModel.UserNum = userinfo[0].user_num;
                //stuChangePasswordModel.UserName = userinfo[0].user_name;
                stuChangePasswordModel.Password = userinfo[0].user_password;
                ModelState.Clear();

                return PartialView(stuChangePasswordModel);
            }
            return PartialView();
        }

        public PartialViewResult AdmChangePassword(StuChangePasswordModel stuChangePassword, String UserID)
        {
            var userinfo = UserSerivce.QueryStuByIDNameMajor(UserID, null, null);
            stuChangePassword.UserNum = userinfo[0].user_num;
            stuChangePassword.Password = userinfo[0].user_password;
            ModelState.Clear();
            return PartialView(stuChangePassword);
        }

        [HttpPost]
        public PartialViewResult AdmChangePassword(StuChangePasswordModel stuChangePassword)
        {
            if (stuChangePassword.UserNum != "" && stuChangePassword.UserNum != null)
            {
                System_User sysUser = new System_User()
                {
                    user_num = stuChangePassword.UserNum,
                    //user_name = stuChangePasswordModel.UserName,
                    user_password = stuChangePassword.NewPassword
                };
                UserSerivce.UpdateUserToAdm(sysUser);
            }
            return PartialView(stuChangePassword);
        }

        public PartialViewResult AdmAlterUserPsw(String userNum)
        {
            StuChangePasswordModel stuChangePasswordModel = new StuChangePasswordModel();
            var passwordModel = UserSerivce.QueryStuByIDNameMajor(userNum, null, null);
            if (passwordModel.Count != 0)
            {
                stuChangePasswordModel.UserNum = passwordModel[0].user_num;
                stuChangePasswordModel.UserName = passwordModel[0].user_name;
                stuChangePasswordModel.Password = passwordModel[0].user_password;
            }
            ModelState.Clear();

            return PartialView(stuChangePasswordModel);
        }

        [HttpPost]
        public PartialViewResult AdmAlterUserPsw(StuChangePasswordModel stuChangePasswordModel, String userNum)
        {
            System_User sysUser = new System_User()
            {
                user_num = stuChangePasswordModel.UserNum,
                user_name = stuChangePasswordModel.UserName,
                user_password = stuChangePasswordModel.NewPassword
            };
            UserSerivce.UpdateUserToAdm(sysUser);

            stuChangePasswordModel.UserNum = "";
            stuChangePasswordModel.UserName = "";
            stuChangePasswordModel.Password = "";
            stuChangePasswordModel.NewPassword = "";
            stuChangePasswordModel.ConfirmPassword = "";

            ViewData["Msg"] = "修改成功";
            ModelState.Clear();
            return PartialView(stuChangePasswordModel);

            //StuChangePasswordModel stuChangePasswordModel = new StuChangePasswordModel();
            //var passwordModel = UserSerivce.QueryStuByIDNameMajor(userNum, null, null);
            //if (passwordModel != null)
            //{
            //    stuChangePasswordModel.UserNum = passwordModel[0].user_num;
            //    stuChangePasswordModel.UserName = passwordModel[0].user_name;
            //    stuChangePasswordModel.Password = passwordModel[0].user_password;
            //}
            //ModelState.Clear();

            //return PartialView(stuChangePasswordModel);
        }

        public PartialViewResult StuQueryPasswordTable(StuChangePasswordModel stuChangePasswordModel, String UserID)
        {
            if (UserID != null)
            {
                var userinfo = UserSerivce.FindUserInfoByID(UserID);
                stuChangePasswordModel.UserNum = userinfo.user_num;
                stuChangePasswordModel.UserName = userinfo.user_name;
                stuChangePasswordModel.Password = userinfo.user_password;
                ModelState.Clear();

                return PartialView(stuChangePasswordModel);
            }
            var passwordModel = UserSerivce.QueryStuByIDNameMajor(stuChangePasswordModel.UserNum, stuChangePasswordModel.UserName, null);
            if (passwordModel != null)
            {
                stuChangePasswordModel.UserNum = passwordModel[0].user_num;
                stuChangePasswordModel.UserName = passwordModel[0].user_name;
                stuChangePasswordModel.Password = passwordModel[0].user_password;
            }
            ModelState.Clear();

            return PartialView(stuChangePasswordModel);
        }

        [HttpPost]
        public PartialViewResult StuQueryPasswordTable1(StuChangePasswordModel stuChangePasswordModel)
        {
            System_User sysUser = new System_User()
            {
                user_num = stuChangePasswordModel.UserNum,
                user_name = stuChangePasswordModel.UserName,
                user_password = stuChangePasswordModel.NewPassword
            };
            UserSerivce.UpdateUserToAdm(sysUser);

            stuChangePasswordModel.UserNum = "";
            stuChangePasswordModel.UserName = "";
            stuChangePasswordModel.Password = "";
            stuChangePasswordModel.NewPassword = "";
            stuChangePasswordModel.ConfirmPassword = "";

            ModelState.Clear();
            return PartialView(stuChangePasswordModel);
        }

        public PartialViewResult StuInfo()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult StuInfo(UserInfoModel userInfoModel, String UserID)
        {
            if (UserID != null)
            {
                var userInfo = UserSerivce.FindUserInfoByID(UserID);

                userInfoModel.UserNum = userInfo.user_num;
                userInfoModel.UserName = userInfo.user_name;
                if (userInfo.user_sex == 0)
                {
                    userInfoModel.UserSex = SexEnum.女.ToString();
                }
                else
                {
                    userInfoModel.UserSex = SexEnum.男.ToString();
                }
                userInfoModel.SysMajorName = userInfo.sys_major_name;
                userInfoModel.UserAddress = userInfo.user_address;
                if (userInfo.user_arts_science == 0)
                {
                    userInfoModel.UserArtsScience = ArtsScienceEnum.文科.ToString();
                }
                else
                {
                    userInfoModel.UserArtsScience = ArtsScienceEnum.理科.ToString();
                }
                userInfoModel.UserAddress = userInfo.user_address;
                userInfoModel.LongPhone = userInfo.user_long_phone;
                userInfoModel.ShortPhone = userInfo.user_short_phone;
                userInfoModel.Honour = userInfo.user_honour;


            }

            ModelState.Clear();//用于消除action里面的model方面的赋值
            return PartialView(userInfoModel);

            //return PartialView();
        }


        public PartialViewResult userInfo1(UserInfoModel userInfoModel, String UserID)
        {
            if (UserID != null)
            {
                var userInfo = UserSerivce.FindUserInfoByID(UserID);

                userInfoModel.UserNum = userInfo.user_num;
                userInfoModel.UserName = userInfo.user_name;
                userInfoModel.UserAddress = userInfo.user_address;
                userInfoModel.LongPhone = userInfo.user_long_phone;
                userInfoModel.ShortPhone = userInfo.user_short_phone;

                if (userInfo.user_sex == 0)
                {
                    userInfoModel.UserSex = SexEnum.女.ToString();
                }
                else
                {
                    userInfoModel.UserSex = SexEnum.男.ToString();
                }
                userInfoModel.MajorName = userInfo.major_name;
                userInfoModel.SysMajorName = userInfo.sys_major_name;
                userInfoModel.UserEnglishLevelFour = userInfo.user_english_level_four;
                userInfoModel.UserEnglishLevelSix = userInfo.user_english_level_six;

                if (userInfo.user_arts_science == 0)
                {
                    userInfoModel.UserArtsScience = ArtsScienceEnum.文科.ToString();
                }
                else
                {
                    userInfoModel.UserArtsScience = ArtsScienceEnum.理科.ToString();
                }
                userInfoModel.MajorRanking = userInfo.user_major_ranking;
                //userInfoModel.UserMajorRankingRatio = userInfo.user_major_ranking_ratio;
                userInfoModel.UserCreditWeightedAverage = userInfo.user_credit_weighted_average;
                userInfoModel.LastMajorNum = userInfo.user_last_major_num;
                if (userInfo.user_disciplinary_award == 0)
                {
                    userInfoModel.UserDisciplinaryAward = DisciplinaryAwardEnum.无.ToString();
                }
                else
                {
                    userInfoModel.UserDisciplinaryAward = DisciplinaryAwardEnum.有.ToString();
                }
                if (userInfo.user_demotion == 0)
                {
                    userInfoModel.UserDemotion = DemotionEnum.不同意.ToString();
                }
                else
                {
                    userInfoModel.UserDemotion = DemotionEnum.同意.ToString();
                }
                if (userInfo.user_flied_class == 0)
                {
                    userInfoModel.FiledClass = FliedEnum.无.ToString();
                }
                else
                {
                    userInfoModel.FiledClass = FliedEnum.有.ToString();
                }
                userInfoModel.Honour = userInfo.user_honour;
            }

            ModelState.Clear();//用于消除action里面的model方面的赋值
            return PartialView(userInfoModel);
        }





        [HttpPost]
        public PartialViewResult StuChangePassword1(String UserID)//StuChangePasswordModel stuChangePasswordModel,
        {
            StuChangePasswordModel stuChangePasswordModel = new StuChangePasswordModel();
            if (UserID != null)
            {
                var userInfo = UserSerivce.FindUserInfoByID(UserID);
                stuChangePasswordModel.UserNum = userInfo.user_num;
                stuChangePasswordModel.Password = userInfo.user_password;
                ModelState.Clear();
                return PartialView(stuChangePasswordModel);
            }
            return PartialView();
        }


        public PartialViewResult StuChangePassword(StuChangePasswordModel stuChangePasswordModel)
        {
            if (stuChangePasswordModel.NewPassword != null && stuChangePasswordModel.ConfirmPassword != null && stuChangePasswordModel.ConfirmPassword == stuChangePasswordModel.NewPassword)
            {
                var userInfo = UserSerivce.FindUserInfoByID(stuChangePasswordModel.UserNum);
                System_User sysUser = new System_User()
                {
                    user_num = userInfo.user_num,
                    user_password = stuChangePasswordModel.NewPassword
                };
                var args = UserSerivce.UpdateUserToAdm(sysUser);
                stuChangePasswordModel.Password = stuChangePasswordModel.NewPassword;
                stuChangePasswordModel.NewPassword = "";
                stuChangePasswordModel.ConfirmPassword = "";
                ViewData["Msg"] = args.Msg;
                return PartialView(stuChangePasswordModel);
            }
            return PartialView(stuChangePasswordModel);
        }











        /// <summary>
        /// 呈现学生管理的操作界面
        /// </summary>
        /// <returns></returns>
        public PartialViewResult StuManage()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "学生信息添加",
                Value = "0"
            });
            items.Add(new SelectListItem
            {
                Text = "学生信息删除",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "学生信息修改",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "学生转专业补报",
                Value = "3"
            });
            ViewData["selectList"] = new SelectList(items, "Value", "Text");
            return PartialView();
        }


        private void SetAddStuUI()
        {
            var years = DateTime.Now.Year;
            //学年
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = years - 5; i < years + 5; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            ViewData["year"] = new SelectList(items, "Value", "Text");

            //性别
            items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "女",
                Value = "0"
            });
            items.Add(new SelectListItem
            {
                Text = "男",
                Value = "1"
            });
            ViewData["sex"] = new SelectList(items, "Value", "Text");


            ViewData["demotion"] = ApplyService.Demotion();
            ViewData["ArtsScience"] = ApplyService.UserArtsScience();
            ViewData["FileClass"] = ApplyService.FileClass();

            ViewData["major"] = ApplyService.QueryAllMajors();
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <returns></returns>
        public ActionResult AdmAddStu()
        {
            SetAddStuUI();
            UserInfoModel model = new UserInfoModel()
            {
                ShortPhone = "无"
            };
            return PartialView(model);
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AdmAddStu(UserInfoModel model)
        {
            System_User user = new System_User()
            {
                user_num = model.UserNum,
                user_password = model.UserNum,
                user_name = model.UserName,
                user_sex = Convert.ToInt32(model.UserSex),
                user_class = model.UserClass,
                user_arts_science = Convert.ToInt32(model.UserArtsScience),
                user_address = model.UserAddress,
                user_demotion = Convert.ToInt32(model.Demotion),
                user_flied_class = Convert.ToInt32(model.FiledClass),
                user_disciplinary_award = Convert.ToInt32(model.UserDisciplinaryAward),
                user_long_phone = model.LongPhone,
                user_short_phone = model.ShortPhone,
                user_honour = model.Honour,
                user_date = model.UserDate,
                user_major_id = model.MajorName,
                user_sys_major_id = model.SysMajorName,
                user_english_level_four = model.UserEnglishLevelFour,
                user_english_level_six = model.UserEnglishLevelSix,
                user_last_major_num = model.LastMajorNum,
                user_credit_weighted_average = model.UserCreditWeightedAverage,
                user_major_ranking = model.MajorRanking,
                user_flag = 1,
                user_apply_flag = 0,
                user_power = 2
            };
            user.major_name = ApplyService.QueryNameByID(Convert.ToInt32(model.MajorName)).major_name;
            user.sys_major_name = ApplyService.QueryNameByID(Convert.ToInt32(model.MajorName)).major_name;
            var args = UserSerivce.AddUser(user);
            SetAddStuUI();
            ViewData["Msg"] = args.Msg;
            return PartialView();
        }

        /// <summary>
        /// 管理员删除用户
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AdmDeleteStu()
        {

            return PartialView();
        }

        [HttpPost]
        public JsonResult AdmDeleteStu(string userNum, string Date)
        {
            System_User user = new System_User()
            {
                user_num = userNum,
                user_date = Date
            };
            var args = UserSerivce.DelectUser(user);
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 待删除的学生列表
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AdmDeleteStuTable(string UserNum)
        {
            var userInfo = UserSerivce.FindUserInfoByID(UserNum);
            return PartialView(userInfo);
        }



    }
}
