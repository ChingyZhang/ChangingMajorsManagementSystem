using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangingMajors.DAL.Model;
using ChangingMajors.DAL.Entity;
using ChangingMajors.DAL.Infrastructure;

namespace ChangingMajors.BLL.Infrastructure
{
    public interface IUserService
    {
        UserModel Logon(String userID, String password, String pour);

        List<System_User> QueryStuByIDNameMajor(String userID, String userName, int? userMajor);

        System_User FindUserInfoByID(String UserID);

        ArgsHelp UpdateUser(System_User sysUser);

        ArgsHelp UpdateUser1(System_User sysUser);

        ArgsHelp UpdateUserToAdm(System_User sysUser);

        ArgsHelp AddUser(System_User sysUser);

        ArgsHelp DelectUser(System_User sysUser);

        ArgsHelp DelectUser1(String uploadID);

        List<system_user_manager> QueryAllManager();

        System_User QueryStuByNumAndDate(String StuNum, String Date);

        system_user_manager QueryManagerByDate(String Date);
    }
}
