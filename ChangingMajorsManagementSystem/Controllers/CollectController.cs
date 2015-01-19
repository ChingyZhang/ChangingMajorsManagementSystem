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
using System.Data;
using ValueOffice.Excel;

namespace ChangingMajorsManagementSystem.Controllers
{
    public class CollectController : Controller
    {
        public ICollectService CollectService { get; set; }
        public IApplyService ApplyService { get; set; }
        public IExcelService ExcelService { get; set; }
        PageInfo pageInfo = new PageInfo();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ExcelService == null)
            {
                ExcelService = new ExcelService();
            }
            if (CollectService == null)
            {
                CollectService = new CollectService();
            }
            if (ApplyService == null)
            {
                ApplyService = new ApplyService();
            }

            base.Initialize(requestContext);
        }
        //
        // GET: /Collect/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Collect()
        {
            ViewData["Major"] = CollectService.QueryAllMajors();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Collect(CollectModel collectModel)
        {
            //问题
            ViewData["Major"] = CollectService.QueryAllMajors();
            return RedirectToAction("CollectTable", new { MajorName = collectModel.MajorName, SysMajorName = collectModel.SysMajorName });
        }


        public PartialViewResult CollectTablePageInfo(String MajorName, String SysMajorName, String page)
        {
            ViewData["majorname"] = MajorName;
            ViewData["SysMajorName"] = SysMajorName;
            var collecttable = CollectService.QueryChangeMajorTable(MajorName, SysMajorName);
            if (page == null)
            {
                pageInfo.CurrentPage = 1;
            }
            else
            {
                pageInfo.CurrentPage = Convert.ToInt32(page);
            }
            pageInfo.TotalItems = collecttable.Count;
            pageInfo.ItemsPerPage = 10;
            ViewData["pageInfo"] = pageInfo;
            var skipNum = 10 * (pageInfo.CurrentPage - 1);
            var aa = collecttable.Skip(skipNum);
            var table = aa.Take(pageInfo.ItemsPerPage).ToList();
            ViewData["collectTable"] = table;
            return PartialView();
            //List<System_User> table = new List<System_User>();
            //if (collecttable.Count != 0)
            //{
            //    if (collecttable.Count >= 10)
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            var j = i + (pageInfo.CurrentPage - 1) * 10;
            //            table.Add(collecttable[j]);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < collecttable.Count; i++)
            //        {
            //            var j = i + (pageInfo.CurrentPage - 1) * 10;
            //            table.Add(collecttable[j]);
            //        }
            //    }
            //}

        }


        public PartialViewResult CollectTable(String MajorName, String SysMajorName, String page)
        {
            ViewData["majorname"] = MajorName;
            ViewData["SysMajorName"] = SysMajorName;

            var collecttable = CollectService.QueryChangeMajorTable(MajorName, SysMajorName);
            pageInfo.CurrentPage = 1;
            pageInfo.TotalItems = collecttable.Count;
            pageInfo.ItemsPerPage = 10;
            ViewData["pageInfo"] = pageInfo;
            if (collecttable.Count != 0)
            {
                if (collecttable.Count <= pageInfo.ItemsPerPage)
                {
                    var table = collecttable;
                    ViewData["collectTable"] = table;
                }
                else
                {
                    var table = collecttable.Take(pageInfo.ItemsPerPage).ToList();
                    ViewData["collectTable"] = table;
                }
            }
            return PartialView();
        }

        public FileResult CollectDownload(string MajorName, string SysMajorName)
        {
            var fileData = CollectService.QueryChangeMajorTable(MajorName, SysMajorName);
            string path = Server.MapPath("~/FileData/Collect.xls");
            var filePath = ExcelService.CreateExcel(fileData, path);
            return File(path, "application/-excel", "Collect.xls");
        }




    }
}
