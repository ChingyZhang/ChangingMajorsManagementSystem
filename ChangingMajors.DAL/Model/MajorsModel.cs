using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Model
{
    /// <summary>
    /// 专业表
    /// </summary>
    public class MajorsModel
    {
        /// <summary>
        /// 专业id
        /// </summary>
        public int? MajorID { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public String MajorName { get; set; }

        /// <summary>
        /// 文理科
        /// </summary>
        public int? MajorArtScience { get; set; }

        /// <summary>
        /// 专业启用标签,0不启用,1启用
        /// </summary>
        public int? MajorFlag { get; set; }

        /// <summary>
        /// 专业批次时间
        /// </summary>
        public String MajorDate { get; set; }
    }
}
