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
    [HandleError]
    public class MajorController : Controller
    {
        PageInfo pageInfo = new PageInfo();

        public IExcelService ExcelService { get; set; }
        public IApplyService ApplyService { get; set; }
        //
        // GET: /Major/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ApplyService == null)
            {
                ApplyService = new ApplyService();
            }
            if (ExcelService == null)
            {
                ExcelService = new ExcelService();
            }
            base.Initialize(requestContext);
        }




        public PartialViewResult GetYear()
        {
            var haveYear = ApplyService.MajorDateAndStuDate();
            var years = DateTime.Now.Year;
            List<int> year = new List<int>();
            for (int m = 0; m < 10; m++)
            {
                year.Add(years - 5 + m);
            }

            List<SelectListItem> items = new List<SelectListItem>();
            String text, value;
            for (int i = years - 5; i < years + 5; i++)
            {
                if (haveYear.Count != 0)
                {
                    for (int j = 0; j < haveYear.Count; j++)
                    {
                        for (int h = 0; h < 10; h++)
                        {
                            if (year[h] == Convert.ToInt32(haveYear[j][0]))
                            {
                                year[h] = 0;
                            }
                        }
                        var str = haveYear[j];
                        if (str[0] == i.ToString())
                        {
                            if (str[1] == "")
                            {
                                str[1] = "(已有数据)";
                            }
                            text = str[0] + str[1];
                            value = str[0];
                            items.Add(new SelectListItem
                            {
                                Text = text,
                                Value = value
                            });
                            break;
                        }
                    }

                    //else
                    //{
                    //    text = i.ToString();
                    //    value = i.ToString();
                    //    items.Add(new SelectListItem
                    //    {
                    //        Text = text,
                    //        Value = value
                    //    });
                    //}

                    //}
                }

                else
                {
                    text = i.ToString();
                    value = i.ToString();

                    items.Add(new SelectListItem
                    {
                        Text = text,
                        Value = value
                    });
                }

            }
            for (int j = 0; j < 10; j++)
            {
                if (year[j] != 0)
                {
                    text = year[j].ToString();
                    value = year[j].ToString();
                    items.Add(new SelectListItem
                    {
                        Text = text,
                        Value = value
                    });
                }
            }

            ViewData["year"] = items;
            return PartialView();
        }

        /// <summary>
        /// 上传专业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult UploadMajor(HttpPostedFileBase fileData, String Year)
        {
            if (fileData != null)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");//获取时间
                string extension = Path.GetExtension(fileData.FileName);//获取扩展名
                string newFileName = time + extension;//重组成新的文件名
                string tempPath = Path.Combine("D:\\ExcelFile", "admin");//组成存储路径
                //PayVoucherPath.Add(tempPath);

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                string payVoucherPath = Path.Combine(tempPath, newFileName);//完整路径
                fileData.SaveAs(payVoucherPath);
                var response = newFileName + ";" + payVoucherPath;

                var result = ExcelService.AddMajorByDataTable(Path.Combine(tempPath, newFileName).ToString(), Year);

                return Content(response);
            }
            return Content("");
        }



        /// <summary>
        /// 增加专业
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddMajor()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 专业管理
        /// </summary>
        /// <returns></returns>
        public PartialViewResult MajorManage(string year)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem
            //{
            //    Text = "专业信息添加",
            //    Value = "0"
            //});
            //items.Add(new SelectListItem
            //{
            //    Text = "专业信息删除",
            //    Value = "1"
            //});
            items.Add(new SelectListItem
            {
                Text = "专业信息修改",
                Value = "2"
            });
            ViewData["MajorselectList"] = new SelectList(items, "Value", "Text");
            AllYear();
            //var asd = ApplyService.QueryAllMajors(year);
            //ViewData["MajorList"] = ApplyService.QueryAllMajors(year);
            return PartialView();
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
            ViewData["year"] = items;
        }

        /// <summary>
        /// 添加专业
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddMajors(string year)
        {
            ViewData["UserArtsScience"] = ApplyService.UserArtsScience();
            ViewData["year"] = year;
            return PartialView();
        }


        /// <summary>
        /// 添加专业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AddMajors(string MajorName, string MajorArtOrScience, string MajorNumOfPeople, string Year)
        {
            System_Majors major = new System_Majors()
            {
                major_name = MajorName,
                major_art_science = Convert.ToInt32(MajorArtOrScience),
                major_num = Convert.ToInt32(MajorNumOfPeople),
                major_date = Year,
                major_flag = 1,
                major_start_flag = 0
            };
            var args = ApplyService.AddMajor(major);
            ViewData["UserArtsScience"] = ApplyService.UserArtsScience();
            ViewData["Msg"] = args.Msg;
            return PartialView();
        }



        /// 删除专业(查询)
        /// </summary>
        /// <returns></returns>
        public PartialViewResult MajorsTable(string year)
        {
            var result = ApplyService.QueryAllMajorInfo(year);
            pageInfo.CurrentPage = 1;
            pageInfo.ItemsPerPage = 10;
            pageInfo.TotalItems = result.Count;
            ViewData["MajorsList"] = result.Take(pageInfo.ItemsPerPage).ToList();
            ViewData["pageInfo"] = pageInfo;
            ViewData["year"] = year;
            return PartialView();
        }

        /// 删除专业(翻页)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult MajorsTable(string page, string year)
        {
            var result = ApplyService.QueryAllMajorInfo(year);
            pageInfo.CurrentPage = Convert.ToInt32(page);
            pageInfo.ItemsPerPage = 10;
            pageInfo.TotalItems = result.Count;
            ViewData["MajorsList"] = result.Skip((pageInfo.CurrentPage - 1) * pageInfo.ItemsPerPage).Take(pageInfo.ItemsPerPage).ToList();
            ViewData["pageInfo"] = pageInfo;
            ViewData["year"] = year;
            return PartialView();
        }

        /// <summary>
        /// 删除专业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteMajor(string majorId)
        {
            var args = ApplyService.DelectMajor(majorId);
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 修改专业
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AlterMajors(string year)
        {
            var qwe = ApplyService.QueryAllMajors(year);
            ViewData["MajorList"] = ApplyService.QueryAllMajors(year);

            return PartialView();
        }

        /// <summary>
        /// 修改专业(后面的参数无用,不加的话回合上面的方法冲突)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AlterMajors(string AlterMajorName, string asd)
        {
            var flag = false;
            var result = ApplyService.QueryMajorInfo(AlterMajorName);
            if (result != null)
                flag = true;
            var jsonData = new
            {
                Flag = flag,
                Num = result.major_num,
                MajorID = result.major_id
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 修改专业人数
        /// </summary>
        /// <param name="majorID"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AlterMajorsNum(string majorID, string Num)
        {
            var args = ApplyService.UpdateMajorNum(majorID, Num);
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }


    }
}
