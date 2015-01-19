using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChangingMajorsManagementSystem.Models
{
    /// <summary>
    /// 表格要显示的信息
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 总共行数
        /// </summary>
        public Int32 TotalItems { get; set; }

        /// <summary>
        /// 每次呈现的行数
        /// </summary>
        public Int32 ItemsPerPage { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public Int32 CurrentPage { get; set; }

        public Int32 TotalPages
        {
            get { return (Int32)Math.Ceiling((Decimal)TotalItems / ItemsPerPage); }
        }
    }
}