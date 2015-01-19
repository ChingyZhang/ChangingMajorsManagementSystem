using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ChangingMajorsManagementSystem.Models;
using ChangingMajors.BLL.Infrastructure;
using ChangingMajors.BLL.Service;
using ChangingMajors.DAL.Entity;



namespace ChangingMajorsManagementSystem.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public IUserService UserSerivce { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (UserSerivce == null)
            {
                UserSerivce = new UserService();
            }
            base.Initialize(requestContext);
        }


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登陆(伪)
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return PartialView();
        }

        /// <summary>
        /// 登陆处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="subModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LogOnModel model, SubModel subModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserSerivce.Logon(model.UserName, model.Password, model.Pour);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "账号名或密码错误");
                    return PartialView("Login");
                }
                subModel.User = user;
                //if (model.Pour == "0")
                //{
                //    return RedirectToAction("MenuAdm", "Menu", new { UserID = user.UserNum });
                //}
                //else
                //{
                //    return RedirectToAction("MenuStu", "Menu", new { UserID = user.UserNum });
                //}
                return RedirectToAction("Menu", "Menu", new { UserID = user.UserNum, Pour = model.Pour });
            }
            return PartialView("Login");

        }
    }
}
