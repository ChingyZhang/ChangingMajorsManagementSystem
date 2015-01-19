using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Model
{
    /// <summary>
    /// 转专业发起结束表
    /// </summary>
    public class StartModel
    {
        /// <summary>
        /// 发起批次ID
        /// </summary>
        public int StartID { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public String UserNum { get; set; }

       
        /// <summary>
        /// 发起时间
        /// </summary>
        public String StartDate { get; set; }

        /// <summary>
        /// 发起状态标签
        /// </summary>
        public int StartFlag { get; set; }
    }
}
