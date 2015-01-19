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
    public class UserService : IUserService
    {
        UserDAO userDao = new UserDAO();

        #region IUserService内容

        public UserModel Logon(string userID, string password, string pour)
        {
            return UserDAO.Login(userID, password, pour);
        }

        public List<System_User> QueryStuByIDNameMajor(String userID, String userName, int? userMajor)
        {
            return UserDAO.QueryStuByIDNameMajor(userID, userName, userMajor);
        }

        public System_User FindUserInfoByID(String UserID)
        {
            return UserDAO.FindUserInfoByID(UserID);
        }

        public ArgsHelp UpdateUser1(System_User sysUser)
        {
            return UserDAO.UpdateUser1(sysUser);
        }

        public ArgsHelp UpdateUser(System_User sysUser)
        {
            return UserDAO.UpdateUser(sysUser);
        }

        public ArgsHelp UpdateUserToAdm(System_User sysUser)
        {
            return UserDAO.UpdateUserToAdm(sysUser);
        }

        public ArgsHelp AddUser(System_User sysUser)
        {
            return UserDAO.AddUser(sysUser);
        }

        public ArgsHelp DelectUser(System_User sysUser)
        {
            return UserDAO.DelectUser(sysUser);
        }

        public ArgsHelp DelectUser1(String uploadID)
        {
            return UserDAO.DelectUser1(uploadID);
        }

        public List<system_user_manager> QueryAllManager()
        {
            return UserDAO.QueryAllManager();
        }

        public  System_User QueryStuByNumAndDate(String StuNum, String Date)
        {
            return UserDAO.QueryStuByNumAndDate(StuNum, Date);
        }

        public system_user_manager QueryManagerByDate(String Date)
        {
            return UserDAO.QueryManagerByDate(Date);
        }
        #endregion

    }
}
