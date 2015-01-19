using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.DAO.Base;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.Infrastructure;
using ChangingMajors.BLL.Infrastructure;


namespace ChangingMajors.BLL.Service
{
    public class NoticeService : INoticeService
    {
        NoticeDAO noticeDao = new NoticeDAO();

        #region INoticeService内容

        public List<User_Notice> QueryAllNotices()
        {
            return NoticeDAO.QueryAllNotice();
        }

        public NoticeModel QueryNoticeByID(int? noticeID)
        {
            return NoticeDAO.QueryNoticeByID(noticeID);
        }

        public ArgsHelp AddNotice(User_Notice userNotice)
        {
            return NoticeDAO.AddNotice(userNotice);
        }

        public ArgsHelp DelectNotice(String NoticeID)
        {
            return NoticeDAO.DelectNotice(NoticeID);
        }
        #endregion

    }
}
