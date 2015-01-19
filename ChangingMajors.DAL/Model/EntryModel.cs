using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Model
{
    /// <summary>
    /// 报名表
    /// </summary>
    public class EntryModel
    {
        /// <summary>
        /// 报名表ID
        /// </summary>
        public int EntryID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 原专业ID
        /// </summary>
        public int MajorID { get; set; }

        /// <summary>
        /// 拟转入专业ID
        /// </summary>
        public int SysMajorID { get; set; }

        /// <summary>
        /// 发起批次ID
        /// </summary>
        public int StartID { get; set; }

        /// <summary>
        /// 报名状态
        /// </summary>
        public int EntryState { get; set; }

        /// <summary>
        /// 报名表启用状态
        /// </summary>
        public int EntryFlag { get; set; }

    }
}
