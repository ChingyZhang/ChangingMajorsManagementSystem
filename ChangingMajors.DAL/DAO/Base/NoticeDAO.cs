using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;

namespace ChangingMajors.DAL.DAO.Base
{
    public class NoticeDAO
    {

        /// <summary>
        /// 查询所有的公告
        /// </summary>
        /// <returns></returns>
        public static List<User_Notice> QueryAllNotice()
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var notice = cmms.User_Notice.Where(x => x.notice_flag == 1).OrderByDescending(x => x.notice_id).ToList();
                if (notice == null)
                {
                    return null;
                }
                else
                {
                    return notice;
                }
            }
        }

        /// <summary>
        /// 查询某条公告通过id
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public static NoticeModel QueryNoticeByID(int? noticeID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var notice = cmms.User_Notice.AsParallel().FirstOrDefault(x => x.notice_id == noticeID);
                if (notice == null)
                {
                    return null;
                }
                else
                {

                    NoticeModel noticeModel = new NoticeModel()
                    {
                        NoticeID = notice.notice_id,
                        NoticeTitle = notice.notice_title,
                        NoticeDetail = notice.notice_detail,
                        NoticeDate = notice.notice_date,
                        NoticeFlag = notice.notice_flag,
                    };
                    if (!string.IsNullOrEmpty(notice.notice_file))
                    {
                        var array = notice.notice_file.Split(';');
                        noticeModel.NoticeFileUrl = array[1];
                        noticeModel.NoticeFileLastName = array[0];
                    }
                    return noticeModel;
                }
            }

        }

        public static ArgsHelp DelectNotice(String NoticeID)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var delectData = cmms.User_Notice.AsParallel().FirstOrDefault(x => x.notice_id == Convert.ToInt32(NoticeID));
                cmms.DeleteObject(delectData);
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("删除成功", true);
            }
        }

        /// <summary>
        /// 增加公告
        /// </summary>
        /// <param name="userNotice"></param>
        /// <returns></returns>
        public static ArgsHelp AddNotice(User_Notice userNotice)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                cmms.User_Notice.AddObject(userNotice);
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("添加新公告成功", true);
            }

        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="userNotice"></param>
        /// <returns></returns>
        public static ArgsHelp DelectNotice(NoticeModel userNotice)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var notice = cmms.User_Notice.AsParallel().FirstOrDefault(x => x.notice_id == userNotice.NoticeID);
                notice.notice_flag = 0;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("删除成功", true);
            }
        }

        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="userNotice"></param>
        /// <returns></returns>
        public static ArgsHelp UpdateNotice(NoticeModel userNotice)
        {
            using (ChangingMajorsManagementSystemEntities cmms = new ChangingMajorsManagementSystemEntities())
            {
                var notice = cmms.User_Notice.AsParallel().FirstOrDefault(x => x.notice_id == userNotice.NoticeID);
                notice.notice_title = userNotice.NoticeTitle;
                notice.notice_detail = userNotice.NoticeDetail;
                notice.notice_date = userNotice.NoticeDate;
                notice.notice_flag = userNotice.NoticeFlag;
                try
                {
                    cmms.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelp(ex.ToString());
                }
                return new ArgsHelp("修改公告成功", true);

            }
        }

    }
}
