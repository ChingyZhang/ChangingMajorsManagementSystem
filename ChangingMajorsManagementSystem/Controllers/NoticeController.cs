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
using System.Text;

namespace ChangingMajorsManagementSystem.Controllers
{
    [HandleError]
    public class NoticeController : Controller
    {
        public INoticeService NoticeService { get; set; }
        //
        // GET: /Notice/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (NoticeService == null)
            {
                NoticeService = new NoticeService();
            }
            base.Initialize(requestContext);
        }

        /// <summary>
        /// 第一次页面
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        //[ValidateInput(false)]
        public PartialViewResult AddNotice()
        {
            //ViewData["UserID"] = UserID;
            return PartialView();
        }

        /// <summary>
        /// 本身是用于xeditor的数据提交的,但是后来换成FCK了,所以没什么大用了
        /// </summary>
        /// <param name="noticeModel"></param>
        /// <param name="UserID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AddNotice(NoticeModel noticeModel, String UserID, String userID)
        {
            ViewData["UserID"] = UserID;

            return PartialView();


        }


        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult UploadFile(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                var fileLastName = fileData.FileName;
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");//获取时间
                string extension = Path.GetExtension(fileData.FileName);//获取扩展名
                string newFileName = time + extension;//重组成新的文件名
                string tempPath = Path.Combine("D:\\UserNoticeFile", "adm");//组成存储路径
                //PayVoucherPath.Add(tempPath);

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                string payVoucherPath = Path.Combine(tempPath, newFileName);//完整路径
                fileData.SaveAs(payVoucherPath);
                var response = fileLastName + ";" + payVoucherPath;


                return Content(response);
            }
            return Content("");
        }


        /// <summary>
        /// 用于FCK数据的提交
        /// </summary>
        /// <param name="Fckcontext"></param>
        /// <param name="Fckcontext2"></param>
        /// <param name="noticeTitle"></param>
        /// <returns></returns>
        public JsonResult Submit(String Fckcontext, String Fckcontext2, String noticeTitle, String FilePath)
        {

            String context = Server.HtmlDecode(Fckcontext);
            String contexts = Server.HtmlEncode(context);

            String context2 = Server.HtmlDecode(Fckcontext2);

            User_Notice userNotice = new User_Notice()
            {
                notice_title = noticeTitle,
                notice_detail = context2,
                notice_flag = 1,
                notice_date = DateTime.Now.ToString("yyyy-MM-dd"),
                notice_file = FilePath
            };

            var args = NoticeService.AddNotice(userNotice);

            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg,
                contents = context
            };

            return Json(jsonData);
        }

        /// <summary>
        /// 公告列表页
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public PartialViewResult Notice()
        {
            ViewData["Notice"] = NoticeService.QueryAllNotices();

            return PartialView();
        }

        public ActionResult NoticeDownload(string FileName, string FilePath)
        {
            return File(FilePath, "text/plain", Server.UrlPathEncode(FileName));
        }

        /// <summary>
        /// 公告列表页
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public PartialViewResult NoticeStu()
        {
            ViewData["NoticeStu"] = NoticeService.QueryAllNotices();

            return PartialView();
        }


        public void DelectNotice(String NoticeId)
        {
            NoticeService.DelectNotice(NoticeId);
        }

        /// <summary>
        /// 公告详细内容
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public PartialViewResult NoticeDetail(int? noticeID)
        {
            var aa = NoticeService.QueryNoticeByID(noticeID);
            aa.NoticeDetail = Server.HtmlDecode(aa.NoticeDetail);
            //var aa=Server.HtmlDecode()
            ViewData["NoticeDetail"] = aa;

            return PartialView();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
