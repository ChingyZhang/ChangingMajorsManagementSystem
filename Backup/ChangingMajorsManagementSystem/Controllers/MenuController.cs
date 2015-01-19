using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangingMajorsManagementSystem.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        /// <summary>
        /// 此乃重中之重----骨折
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Pour"></param>
        /// <returns></returns>
        public ActionResult Index(String UserID, String Pour)
        {
            ViewData["UserID"] = UserID;
            ViewData["Pour"] = Pour;
            return View();
        }

        //弯路
        public ActionResult Menu(String UserID, String Pour)
        {
            ViewData["UserID"] = UserID;
            ViewData["Pour"] = Pour;
            return View();
        }

        public PartialViewResult MenuStu(String UserID)
        {
            ViewData["UserID"] = UserID;
            return PartialView();
        }


        public PartialViewResult MenuAdm(String UserID)
        {
            ViewData["UserID"] = UserID;
            return PartialView();
        }

    }
}
