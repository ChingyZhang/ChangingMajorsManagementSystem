using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.Infrastructure;

namespace ChangingMajors.BLL.Infrastructure
{
    public interface INoticeService
    {
        List<User_Notice> QueryAllNotices();

        NoticeModel QueryNoticeByID(int? noticeID);

        ArgsHelp AddNotice(User_Notice userNotice);

        ArgsHelp DelectNotice(String NoticeID);
    }
}
