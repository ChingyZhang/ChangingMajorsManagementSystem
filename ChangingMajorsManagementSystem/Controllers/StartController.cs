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

namespace ChangingMajorsManagementSystem.Controllers
{
    public class StartController : Controller
    {
        //
        // GET: /Start/
        IStartService StartService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (StartService == null)
            {
                StartService = new StartService();
            }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult StartApply()
        {
            var aa = StartService.QueryAllDate();
            ViewData["applyYear"] = aa;
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult StartApply(String Year,String UserID)
        {
            if (Year != null && Year != "undefind")
            {
                User_ChangingMajors_Start start=new User_ChangingMajors_Start(){
                user_num=UserID,
                start_flag=1,
                start_date=Year
                };
                StartService.Start(start);
            }
            var aa = StartService.QueryAllDate();
            ViewData["applyYear"] = aa;
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult StartApplyTable()
        {
            var aa = StartService.QueryAllCloseStart();
            ViewData["table"] = aa;
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult EndApply(String EndID)
        {
            if (EndID != null && EndID != "undefind")
            {
                var start = StartService.QueryStartByID(EndID);
                StartService.End(start);
            }
            var aa = StartService.QueryAllCloseStart();
            ViewData["table"] = aa;
            //return RedirectToAction("StartApply", "Start");
            return PartialView();
        }


    }
}
