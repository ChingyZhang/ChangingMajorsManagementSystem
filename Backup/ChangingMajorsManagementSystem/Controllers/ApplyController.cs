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
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Infrastructure;


namespace ChangingMajorsManagementSystem.Controllers
{
    public class ApplyController : Controller
    {
        public ICollectService CollectService { get; set; }
        public IApplyService ApplyService { get; set; }
        public IUserService UserSerivce { get; set; }
        //
        // GET: /Apply/


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (CollectService == null)
            {
                CollectService = new CollectService();
            }
            if (UserSerivce == null)
            {
                UserSerivce = new UserService();
            }
            if (ApplyService == null)
            {
                ApplyService = new ApplyService();
            }
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Print()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Print1(String UserID)
        {
            var userInfo = UserSerivce.FindUserInfoByID(UserID);
            SysUserModel1 userModel = new SysUserModel1();
            userModel.UserNum = userInfo.user_num;
            userModel.UserName = userInfo.user_name;
            userModel.UserClass = userInfo.user_class;
            if (userInfo.user_arts_science == 0)
            {
                userModel.UserArtsScience = ArtsScienceEnum.文科.ToString();
            }
            else
            {
                userModel.UserArtsScience = ArtsScienceEnum.理科.ToString();
            }
            userModel.MajorName = userInfo.major_name;
            userModel.UserCreditWeightedAverage = userInfo.user_credit_weighted_average;
            userModel.UserMajorRanking = userInfo.user_major_ranking;
            userModel.UserEnglishFour = userInfo.user_english_level_four;
            userModel.UserEnglishSix = userInfo.user_english_level_six;
            //if (userInfo.user_english_level == 0)
            //{
            //    userModel.UserEnglishPower = EnglishPowerEnum.无.ToString();
            //}
            //else if (userInfo.user_english_level == 4)
            //{
            //    userModel.UserEnglishPower = EnglishPowerEnum.四级.ToString();
            //}
            //else if (userInfo.user_english_level == 6)
            //{
            //    userModel.UserEnglishPower = EnglishPowerEnum.六级.ToString();
            //}
            //else if (userInfo.user_english_level == 8)
            //{
            //    userModel.UserEnglishPower = EnglishPowerEnum.专八.ToString();
            //}
            userModel.SysMajorName = userInfo.sys_major_name;
            userModel.UserLastMajorNum = userInfo.user_last_major_num;
            if (userInfo.user_demotion == 0)
            {
                userModel.UserDemotion = DemotionEnum.不同意.ToString();
            }
            else
            {
                userModel.UserDemotion = DemotionEnum.同意.ToString();
            }
            userModel.UserAddress = userInfo.user_address;
            if (userInfo.user_flied_class == 0)
            {
                userModel.UserFiledClass = FliedEnum.无.ToString();
            }
            else
            {
                userModel.UserFiledClass = FliedEnum.有.ToString();
            }
            if (userInfo.user_disciplinary_award == 0)
            {
                userModel.UserDisciplinaryAward = DisciplinaryAwardEnum.无.ToString();
            }
            else
            {
                userModel.UserDisciplinaryAward = DisciplinaryAwardEnum.有.ToString();
            }
            userModel.UserLongPhone = userInfo.user_long_phone;
            userModel.UserShortPhone = userInfo.user_short_phone;
            userModel.UserHonour = userInfo.user_honour;


            ViewData["printInfo"] = userModel;
            return PartialView();
        }

        public PartialViewResult StuApply()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult StuApply1(UserInfoModel userInfoModel, String UserID, String SysMajorName1, String UserDisciplinaryAward1, String UserDemotion1)
        {

            if (userInfoModel.UserNum == null)
            {
                var sysUser = UserSerivce.QueryStuByIDNameMajor(UserID, null, null);
                if (sysUser.Count != 0)
                {
                    userInfoModel.UserNum = sysUser[0].user_num;
                    userInfoModel.UserName = sysUser[0].user_name;
                    userInfoModel.MajorID = sysUser[0].user_major_id;
                    userInfoModel.MajorName = sysUser[0].major_name;
                    userInfoModel.SysMajorID = sysUser[0].user_sys_major_id;
                    userInfoModel.SysMajorName = sysUser[0].sys_major_name;
                    userInfoModel.UserAddress = sysUser[0].user_address;
                    userInfoModel.LongPhone = sysUser[0].user_long_phone;
                    userInfoModel.ShortPhone = sysUser[0].user_short_phone;
                    userInfoModel.Honour = sysUser[0].user_honour;
                    userInfoModel.MajorRanking = sysUser[0].user_major_ranking;
                    userInfoModel.UserCreditWeightedAverage = sysUser[0].user_credit_weighted_average;
                    userInfoModel.UserEnglishLevelFour = sysUser[0].user_english_level_four;
                    userInfoModel.UserEnglishLevelSix = sysUser[0].user_english_level_six;
                    userInfoModel.UserDisciplinaryAward = sysUser[0].user_disciplinary_award.ToString();
                    userInfoModel.UserDemotion = sysUser[0].user_demotion.ToString();
                    userInfoModel.UserArtsScience = sysUser[0].user_arts_science.ToString();
                }
                else
                {
                    return PartialView();
                }
            }
            else
            {
                userInfoModel.MajorName = UserSerivce.FindUserInfoByID(userInfoModel.UserNum).major_name;
                userInfoModel.MajorID = UserSerivce.FindUserInfoByID(userInfoModel.UserNum).user_major_id.ToString();
                userInfoModel.SysMajorName = ApplyService.QueryNameByID(Convert.ToInt32(SysMajorName1)).major_name;
                System_User sysUser = new System_User()
                {
                    user_num = userInfoModel.UserNum,
                    user_name = userInfoModel.UserName,
                    //user_major_id = userInfoModel.MajorID,
                    //major_name = userInfoModel.MajorName,
                    sys_major_name = userInfoModel.SysMajorName,
                    user_sys_major_id = SysMajorName1,
                    //user_credit_weighted_average = userInfoModel.UserCreditWeightedAverage,
                    //user_english_level_four = userInfoModel.UserEnglishLevelFour,
                    //user_english_level_six = userInfoModel.UserEnglishLevelSix,
                    //user_major_ranking = userInfoModel.MajorRanking,
                    //user_major_ranking_ratio = userInfoModel.UserMajorRankingRatio,
                    user_address = userInfoModel.UserAddress,
                    user_disciplinary_award = Convert.ToInt32(UserDisciplinaryAward1),
                    user_demotion = Convert.ToInt32(UserDemotion1),
                    //user_arts_science = Convert.ToInt32(userInfoModel.UserArtsScience),
                    user_long_phone = userInfoModel.LongPhone,
                    user_short_phone = userInfoModel.ShortPhone,
                    //user_flied_class = Convert.ToInt32(userInfoModel.FiledClass),
                    user_honour = userInfoModel.Honour
                };
                UserSerivce.UpdateUser(sysUser);

                User_Entry entry = new User_Entry()
                {
                    entry_flag = 1,
                    major_id = userInfoModel.MajorID,
                    major_name = userInfoModel.MajorName,
                    sys_major_id = SysMajorName1,
                    sys_major_name = userInfoModel.SysMajorName,
                    user_num = userInfoModel.UserNum,
                    entry_state = 0,
                };

                ApplyService.AddNewApply(entry);
                ModelState.Clear();
                ViewData["major"] = ApplyService.QueryNoAllMajors(sysUser.user_sys_major_id, sysUser.user_arts_science.ToString());
                ViewData["FileClass"] = ApplyService.FileClass(sysUser.user_disciplinary_award.ToString());
                ViewData["demotion"] = ApplyService.Demotion(sysUser.user_demotion.ToString());
                ViewData["Msg"] = "修改成功";
                return PartialView(userInfoModel);
            }
            ViewData["major"] = ApplyService.QueryNoAllMajors(userInfoModel.SysMajorID, userInfoModel.UserArtsScience);
            ViewData["FileClass"] = ApplyService.FileClass(userInfoModel.UserDisciplinaryAward);
            ViewData["demotion"] = ApplyService.Demotion(userInfoModel.UserDemotion);
            ModelState.Clear();
            return PartialView(userInfoModel);
        }

        private void AllYear()
        {
            var years = DateTime.Now.Year;
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
        }

        /// <summary>
        /// 查询学生
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SearchStu()
        {
            var haveYear = ApplyService.MajorDateAndStuDate();
            var years = DateTime.Now.Year;

            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 0; i < haveYear.Count; i++)
            {
                var str = haveYear[i];
                if (str[1] == "(未发起)")
                {
                    str[1] = "";
                }
                items.Add(new SelectListItem
                {
                    Text = str[0] + str[1],
                    Value = str[0]
                });

            }

            ViewData["year"] = new SelectList(items, "Value", "Text");
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchUserNum"></param>
        /// <param name="SearchYear"></param>
        /// <returns></returns>
        public PartialViewResult StuReSignUp(String SearchUserNum, String SearchYear)
        {
            var result = UserSerivce.QueryStuByNumAndDate(SearchUserNum, SearchYear);
            if (result == null)
            {
                ViewData["Msg"] = "无此学生,请添加";
                return PartialView();
            }
            else
            {
                UserInfoModel model = new UserInfoModel()
                {
                    UserNum = result.user_num,
                    UserName = result.user_name,
                    UserAddress = result.user_address,
                    LongPhone = result.user_long_phone,
                    ShortPhone = result.user_short_phone,
                    SysMajorID = result.user_sys_major_id,
                    SysMajorName = result.sys_major_name,
                    UserDisciplinaryAward = result.user_disciplinary_award.ToString(),
                    FiledClass = result.user_flied_class.ToString(),
                    UserDemotion = result.user_demotion.ToString(),
                    Honour = result.user_honour,
                    UserEnglishLevelFour = result.user_english_level_four
                };
                ViewData["major"] = ApplyService.QueryAllMajors(SearchYear, model.SysMajorID);
                ViewData["UserDisciplinaryAward"] = ApplyService.FileClass(model.UserDisciplinaryAward);
                ViewData["demotion"] = ApplyService.Demotion(model.UserDemotion);
                //ModelState.Clear();
                return PartialView(model);
            }

        }

        [HttpPost]
        public PartialViewResult StuReSignUp(UserInfoModel userInfoModel, String MajorName, String DisciplinaryAward)
        {
            userInfoModel.MajorName = UserSerivce.FindUserInfoByID(userInfoModel.UserNum).major_name;
            userInfoModel.MajorID = UserSerivce.FindUserInfoByID(userInfoModel.UserNum).user_major_id.ToString();
            userInfoModel.SysMajorName = ApplyService.QueryNameByID(Convert.ToInt32(MajorName)).major_name;
            System_User sysUser = new System_User()
            {
                user_num = userInfoModel.UserNum,
                user_name = userInfoModel.UserName,
                user_major_id = userInfoModel.MajorID,
                major_name = userInfoModel.MajorName,
                sys_major_name = userInfoModel.SysMajorName,
                user_sys_major_id = MajorName,
                user_address = userInfoModel.UserAddress,
                user_disciplinary_award = Convert.ToInt32(DisciplinaryAward),
                user_demotion = Convert.ToInt32(userInfoModel.UserDemotion),
                user_arts_science = Convert.ToInt32(userInfoModel.UserArtsScience),
                user_long_phone = userInfoModel.LongPhone,
                user_short_phone = userInfoModel.ShortPhone,
                user_honour = userInfoModel.Honour
            };
            UserSerivce.UpdateUser(sysUser);

            User_Entry entry = new User_Entry()
            {
                entry_flag = 1,
                major_id = userInfoModel.MajorID,
                major_name = userInfoModel.MajorName,
                sys_major_id = MajorName,
                sys_major_name = userInfoModel.SysMajorName,
                user_num = userInfoModel.UserNum,
                entry_state = 0,
            };

            ApplyService.AddNewApply(entry);

            ViewData["major"] = ApplyService.QueryNoAllMajors(userInfoModel.SysMajorName);
            ViewData["UserDisciplinaryAward"] = ApplyService.FileClass(userInfoModel.FiledClass);
            ViewData["demotion"] = ApplyService.Demotion(userInfoModel.UserDemotion);
            ViewData["Msg"] = "修改成功";
            return PartialView(userInfoModel);
        }

    }
}
