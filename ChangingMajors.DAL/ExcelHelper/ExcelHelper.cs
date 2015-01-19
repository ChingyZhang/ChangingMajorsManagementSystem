using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ValueOffice.Excel;
using ChangingMajors.DAL.Entity;
using System.Threading;

namespace ChangingMajors.DAL.ExcelHelper
{
    public class ExcelHelper
    {
        /// <summary>
        /// 获取专业(批注,此方法必须以[文理],[拟转专业]  sheet1 -[拟转入专业]为标签
        /// </summary>
        /// <param name="FileName">文件地址</param>
        /// <returns>dataTable[文理科][拟转入专业]此为专业表</returns>
        public static DataTable GetMajor(String FileName)
        {
            try
            {
                IExcel excelHelper = ValueExcel.Instance;
                excelHelper.Create(FileName);
                String selectSQL = "SELECT [文理科],[拟转入专业],[人数] FROM [拟转入专业$]";
                System.Data.DataTable dtw = excelHelper.QuerySQL(FileName, selectSQL, true);
                return dtw;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// 获取修改(批注,此方法必须以[学号],[学分加权平均分],[专业排名],[原专业人数]  sheet1 -[sheet1]为标签
        /// </summary>
        /// <param name="FileName">文件地址</param>
        /// <returns>dataTable[学号],[学分加权平均分],[专业排名],[原专业人数]此为专业表</returns>
        public static DataTable GetUpdateStu(String FileName)
        {
            try
            {
                IExcel excelHelper = ValueExcel.Instance;
                excelHelper.Create(FileName);
                String selectSQL = "SELECT [学号],[学分加权平均分],[专业排名],[原专业人数] FROM [sheet1$]";
                System.Data.DataTable dtw = excelHelper.QuerySQL(FileName, selectSQL, true);
                return dtw;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }

        }


        /// <summary>
        /// 获取人物(批注,必须以[学号],[姓名],[性别],[原专业],[班级],[专业排名],[原专业人数],[学分加权平均分],
        /// [专业排名比例],[文理科],[英语四级],[英语六级],[生源地],[拟转入专业],[是否同意降级],[不及格课程],
        /// [纪律处分],[长号],[短号]为标签以[Sheet1]为序号
        /// </summary>
        /// <param name="FileName">文件地址</param>
        /// <returns>datatable[学号],[姓名],[性别],[原专业],[班级],[专业排名],[原专业人数],[学分加权平均分],
        /// [专业排名比例],[文理科],[英语四级],[英语六级],[生源地],[拟转入专业],[是否同意降级],[不及格课程],
        /// [纪律处分],[长号],[短号]</returns>
        public static DataTable GetUser(String FileName)
        {
            try
            {
                IExcel excelHelper = ValueExcel.Instance;
                excelHelper.Create(FileName);
                String selectSQL = "SELECT [学号],[姓名],[性别(男/女)],[原专业],[班级],[学分加权平均分],[专业排名],[原专业人数]," +
                    "[文理科],[英语四级(没过不填)],[英语六级(没过不填)],[不及格课程(有/无)] " +
                    "FROM [总名单接近完整$]";
                System.Data.DataTable dtw = excelHelper.QuerySQL(FileName, selectSQL, true);
                return dtw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static String CreateExcel(List<System_User> user, String path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("学号");
                dt.Columns.Add("姓名");
                dt.Columns.Add("性别(男/女)");
                dt.Columns.Add("原专业");
                dt.Columns.Add("班级");
                dt.Columns.Add("专业排名");
                dt.Columns.Add("原专业人数");
                dt.Columns.Add("学分加权平均分");
                //dt.Columns.Add("专业排名比例");
                dt.Columns.Add("文理科");
                dt.Columns.Add("英语四级");
                dt.Columns.Add("英语六级");
                dt.Columns.Add("生源地");
                dt.Columns.Add("拟转入专业");
                dt.Columns.Add("是否同意降级(是/否)");
                dt.Columns.Add("不及格课程(有/无)");
                dt.Columns.Add("纪律处分(有/无)");
                dt.Columns.Add("长号");
                dt.Columns.Add("短号");
                for (int i = 0; i < user.Count; i++)
                {
                    var row = dt.NewRow();
                    row["学号"] = "'" + user[i].user_num;
                    row["姓名"] = user[i].user_name;
                    if (user[i].user_sex == 0)
                    {
                        row["性别(男/女)"] = "女";
                    }
                    else
                    {
                        row["性别(男/女)"] = "男";
                    }
                    row["原专业"] = user[i].major_name;
                    row["班级"] = user[i].user_class;
                    row["专业排名"] = user[i].user_major_ranking;
                    row["原专业人数"] = user[i].user_last_major_num;
                    row["学分加权平均分"] = user[i].user_credit_weighted_average;
                    //row["专业排名比例"] = user[i];
                    if (user[i].user_arts_science == 0)
                    {
                        row["文理科"] = "文科";
                    }
                    else
                    {
                        row["文理科"] = "理科";
                    }
                    row["英语四级"] = user[i].user_english_level_four;
                    row["英语六级"] = user[i].user_english_level_six;
                    row["生源地"] = user[i].user_address;
                    row["拟转入专业"] = user[i].sys_major_name;
                    if (user[i].user_demotion == 0)
                    {
                        row["是否同意降级(是/否)"] = "不同意";
                    }
                    else
                    {
                        row["是否同意降级(是/否)"] = "同意";
                    }
                    if (user[i].user_flied_class == 0)
                    {
                        row["不及格课程(有/无)"] = "无";
                    }
                    else
                    {
                        row["不及格课程(有/无)"] = "有";
                    }
                    if (user[i].user_disciplinary_award == 0)
                    {
                        row["纪律处分(有/无)"] = "无";
                    }
                    else
                    {
                        row["纪律处分(有/无)"] = "有";
                    }
                    row["长号"] = "'" + user[i].user_long_phone;
                    row["短号"] = "'" + user[i].user_short_phone;

                    dt.Rows.Add(row);
                }


                ValueHelper.FileHelper.OfficeHelper.ExcelHelper excel = new ValueHelper.FileHelper.OfficeHelper.ExcelHelper(path);

                var asd = excel.CreateFile();

                excel.AddData(path, 0, 0, 1, dt);

                return path;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static int NullRowCount(IExcel excelHelper, String path)
        {
            string sql = "select * from [Sheet1$] where [学号] IS NULL";
            return excelHelper.QuerySQL(path, sql, true).Rows.Count;
        }

        private static void updateData(IExcel excelHelper, System_User user, String path, String num)
        {
            string sql = "UPDATE [Sheet1$] SET [学号]=" + "'" + user.user_num + "',[姓名]='" + user.user_name + "',[性别]='" + (user.user_sex == 0 ? "女" : "男") + "'," +
                                                "[原专业]='" + user.major_name + "',[班级]='" + user.user_class + "',[专业排名]='" + user.user_major_ranking + "'," +
                                                "[原专业人数]='" + user.user_last_major_num + "',[学分加权平均分]='" + user.user_credit_weighted_average + "',[文理科]='" + (user.user_arts_science == 0 ? "文科" : "理科") + "'," +
                                                "[英语四级]='" + user.user_english_level_four + "'," + "[英语六级]='" + user.user_english_level_six + "',[生源地]='" + user.user_address + "'," +
                                                "[拟转入专业]='" + user.sys_major_name + "',[是否同意降级]='" + (user.user_demotion == 0 ? "不同意" : "同意") + "',[不及格课程]='" + (user.user_flied_class == 0 ? "无" : "有") + "'," +
                                                "[纪律处分]='" + (user.user_disciplinary_award == 0 ? "无123" : "有") + "',[长号]='" + user.user_long_phone + "',[短号]='" + user.user_short_phone + "' where [编号]='" + num + "';";
            excelHelper.ExecuteSQL(path, sql, true);
        }

        private static void insertData(IExcel excelHelper, System_User user, String path, String num)
        {
            string sql = "INSERT INTO [Sheet1$] ([编号],[学号],[姓名],[性别],[原专业],[班级],[专业排名],[原专业人数],[学分加权平均分]," +
                                            "[文理科],[英语四级],[英语六级],[生源地],[拟转入专业],[是否同意降级],[不及格课程],[纪律处分],[长号],[短号]) " +
                                            "values ('" + num + "','" + user.user_num + "','" + user.user_name + "','" + (user.user_sex == 0 ? "女" : "男") + "','" + user.major_name + "','" +
                                            user.user_class + "','" + user.user_major_ranking + "','" + user.user_last_major_num + "','" + user.user_credit_weighted_average + "','" +
                                            (user.user_arts_science == 0 ? "文科" : "理科") + "','" + user.user_english_level_four + "','" + user.user_english_level_six + "','" +
                                            user.user_address + "','" + user.sys_major_name + "','" + (user.user_demotion == 0 ? "不同意" : "同意") + "','" +
                                            (user.user_flied_class == 0 ? "无" : "有") + "','" + (user.user_disciplinary_award == 0 ? "无" : "有") + "','" + user.user_long_phone + "','" + user.user_short_phone + "'" + ");";
            excelHelper.ExecuteSQL(path, sql, true);
        }

        private static void initData(IExcel excelHelper, string path)
        {
            //清空信息
            excelHelper.ExecuteSQL(path, "UPDATE [Sheet1＄] SET [学号]= NULL,[姓名]= NULL,[性别]= NULL,[原专业]= NULL,[班级]= NULL," +
                "[专业排名]= NULL,[原专业人数]= NULL,[学分加权平均分]= NULL,[文理科]= NULL,[英语四级]= NULL,[英语六级]= NULL,[生源地]= NULL,[拟转入专业]= NULL," +
                "[是否同意降级]= NULL,[不及格课程]= NULL,[纪律处分]= NULL,[长号]= NULL,[短号]= NULL;", true);
        }



    }
}