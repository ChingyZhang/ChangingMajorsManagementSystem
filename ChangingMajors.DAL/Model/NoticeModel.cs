using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Model
{
    public class NoticeModel
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public int? NoticeID { get; set; }

        /// <summary>
        /// 公告主题
        /// </summary>
        public String NoticeTitle { get; set; }

        /// <summary>
        /// 公告主要内容
        /// </summary>
        public String NoticeDetail { get; set; }

        /// <summary>
        /// 公告日期
        /// </summary>
        public String NoticeDate { get; set; }

        /// <summary>
        /// 公告启用标签
        /// </summary>
        public int? NoticeFlag { get; set; }

        /// <summary>
        /// 通知附件原名
        /// </summary>
        public String NoticeFileLastName { get; set; }

        /// <summary>
        /// 通知附件下载地址
        /// </summary>
        public String NoticeFileUrl { get; set; }

   

    }
}
