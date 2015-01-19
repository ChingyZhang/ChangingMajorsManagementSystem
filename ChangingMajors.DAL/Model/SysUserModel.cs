using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Model
{
    public class SysUserModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户学号
        /// </summary>
        public String UserNum { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public String UserPassword { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 用户性别,0女,1男
        /// </summary>
        public String UserSex { get; set; }

        /// <summary>
        /// 用户班级
        /// </summary>
        public String UserClass { get; set; }

        /// <summary>
        /// 用户专业排名
        /// </summary>
        public String UserMajorRanking { get; set; }

        /// <summary>
        /// 用户原专业人数
        /// </summary>
        public String UserLastMajorNum { get; set; }

        /// <summary>
        /// 学分加权平均分
        /// </summary>
        public String UserCreditWeightedAverage { get; set; }

        /// <summary>
        /// 专业排名比例
        /// </summary>
        public String UserMajorRankingRatio { get; set; }

        /// <summary>
        /// 文理科,0文,1理
        /// </summary>
        public String UserArtsScience { get; set; }

        /// <summary>
        /// 用户英语等级,0无,4四级,6六级,8专八
        /// </summary>
        public String UserEnglishPower { get; set; }

        /// <summary>
        /// 生源地
        /// </summary>
        public String UserAddress { get; set; }

        /// <summary>
        /// 是否同意降级,0不同意,1同意
        /// </summary>
        public String UserDemotion { get; set; }

        /// <summary>
        /// 不及格课程,0没有,1有
        /// </summary>
        public String UserFiledClass { get; set; }

        /// <summary>
        /// 是否有纪律处分,0没有,1有
        /// </summary>
        public String UserDisciplinaryAward { get; set; }

        /// <summary>
        /// 用户长号
        /// </summary>
        public String UserLongPhone { get; set; }

        /// <summary>
        /// 用户短号
        /// </summary>
        public String UserShortPhone { get; set; }

        /// <summary>
        /// 个人荣誉
        /// </summary>
        public String UserHonour { get; set; }

        /// <summary>
        /// 用户权限0超级管理员,1管理员,2学生
        /// </summary>
        public int? UserPower { get; set; }

        /// <summary>
        /// 用户启用标签
        /// </summary>
        public int? UserFlag { get; set; }


        /// <summary>
        /// 原专业名称
        /// </summary>
        public String MajorName { get; set; }

        /// <summary>
        /// 拟转专业名称
        /// </summary>
        public String SysMajorName { get; set; }

        /// <summary>
        /// 用户注册时间
        /// </summary>
        public String UsserDate { get; set; }
    }
}
