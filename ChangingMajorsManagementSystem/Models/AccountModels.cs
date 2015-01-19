using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChangingMajorsManagementSystem.Models
{

    #region Models

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        //[ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "用户名不存在")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "用户名密码错误,请检查后重新输入")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请检查身份是否正确")]
        [Display(Name = "权限:")]
        public string Pour { get; set; }
    }


    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        //[ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region MyModel
    public class NoticeModel
    {
        private String _noticeTitle;
        private String _noticeDetail;
        private String _noticeDate;

        [Required(ErrorMessage = "主题不能为空")]
        [DisplayName("通知主题")]
        public String NoticeTitle
        {
            get { return _noticeTitle; }
            set { _noticeTitle = value; }
        }

        [Required(ErrorMessage = "内容不能为空")]
        [DisplayName("通知主要内容")]
        public String NoticeDetail
        {
            get { return _noticeDetail; }
            set { _noticeDetail = value; }
        }

        [DisplayName("通知建立时间")]
        public String NoticeDate
        {
            get { return _noticeDate; }
            set { _noticeDate = value; }
        }
    }
    /// <summary>
    /// 用户查询Model
    /// </summary>
    public class UserQueryModel
    {
        private String _userNum;
        private String _userName;
        private int? _majorName;

        [DataType(DataType.PhoneNumber)]
        [DisplayName("学号")]
        public String UserNum
        {
            get { return _userNum; }
            set { _userNum = value; }
        }

        [DisplayName("姓名")]
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        [DisplayName("专业名")]
        public int? MajorName
        {
            get { return _majorName; }
            set { _majorName = value; }
        }
    }

    public class CollectModel
    {
        private String _majorName;
        private String _sysMajorName;
        private String _userNum;

        [DisplayName("原专业名称")]
        public String MajorName
        {
            get { return _majorName; }
            set { _majorName = value; }
        }
        [DisplayName("拟转专业名称")]
        public String SysMajorName
        {
            get { return _sysMajorName; }
            set { _sysMajorName = value; }
        }
        [DisplayName("学号")]
        public String UserNum
        {
            get { return _userNum; }
            set { _userNum = value; }
        }
    }

    public class UserInfoModel
    {
        private String _sysMajorID;
        private String _majorID;
        private String _userEnglishLevelSix;
        private String _userEnglishLevelFour;
        private String _userCreditWeightedAverage;
        private String _award;
        private String _demotion;
        private String _major;
        private String _filedClass;
        private String _majorRanking;
        private String _userMajorRankingRatio;
        private String _lastMajorNum;
        private String _majorName;
        private String _userSex;
        private String _userClass;
        private String _userNum;
        private String _userName;
        private String _sysMajorName;
        private String _userAddress;
        private String _userDisciplinaryAward;
        private String _userDemotion;
        private String _longPhone;
        private String _shortPhone;
        private String _honour;
        private String _userArtsScience;
        private String _password;
        private String _newPassword;
        private String _confirmPassword;
        private String _userDate;

        [DisplayName("年份")]
        public String UserDate
        {
            get { return _userDate; }
            set { _userDate = value; }
        }

        public String SysMajorID
        {
            get { return _sysMajorID; }
            set { _sysMajorID = value; }
        }

        public String MajorID
        {
            get { return _majorID; }
            set { _majorID = value; }
        }

        [DisplayName("英语六级")]
        public String UserEnglishLevelSix
        {
            get { return _userEnglishLevelSix; }
            set { _userEnglishLevelSix = value; }
        }

        [DisplayName("英语四级")]
        public String UserEnglishLevelFour
        {
            get { return _userEnglishLevelFour; }
            set { _userEnglishLevelFour = value; }
        }

        [DisplayName("专业排名比例")]
        public String UserMajorRankingRatio
        {
            get { return _userMajorRankingRatio; }
            set { _userMajorRankingRatio = value; }
        }

        [DisplayName("算数平均分")]
        public String UserCreditWeightedAverage
        {
            get { return _userCreditWeightedAverage; }
            set { _userCreditWeightedAverage = value; }
        }

        [DisplayName("是否有过处分")]
        public String Award
        {
            get { return _award; }
            set { _award = value; }
        }

        [DisplayName("是否同意降级")]
        public String Demotion
        {
            get { return _demotion; }
            set { _demotion = value; }
        }


        [DisplayName("专业")]
        public String Major
        {
            get { return _major; }
            set { _major = value; }
        }


        [DisplayName("是否有不及格课程")]
        public String FiledClass
        {
            get { return _filedClass; }
            set { _filedClass = value; }
        }

        [DisplayName("专业排名")]
        public String MajorRanking
        {
            get { return _majorRanking; }
            set { _majorRanking = value; }
        }

        [DisplayName("原专业人数")]
        public String LastMajorNum
        {
            get { return _lastMajorNum; }
            set { _lastMajorNum = value; }
        }

        [DisplayName("原专业")]
        public String MajorName
        {
            get { return _majorName; }
            set { _majorName = value; }
        }

        [DisplayName("性别")]
        public String UserSex
        {
            get { return _userSex; }
            set { _userSex = value; }
        }

        [DisplayName("班级")]
        public String UserClass
        {
            get { return _userClass; }
            set { _userClass = value; }
        }

        [DisplayName("学号")]
        public String UserNum
        {
            get { return _userNum; }
            set { _userNum = value; }
        }
        [DisplayName("姓名")]
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        [DisplayName("拟转专业名称")]
        public String SysMajorName
        {
            get { return _sysMajorName; }
            set { _sysMajorName = value; }
        }
        [DisplayName("籍贯")]
        public String UserAddress
        {
            get { return _userAddress; }
            set { _userAddress = value; }
        }
        [DisplayName("是否有处分")]
        public String UserDisciplinaryAward
        {
            get { return _userDisciplinaryAward; }
            set { _userDisciplinaryAward = value; }
        }
        [DisplayName("是否同意降级")]
        public String UserDemotion
        {
            get { return _userDemotion; }
            set { _userDemotion = value; }
        }
        [DisplayName("长号")]
        public String LongPhone
        {
            get { return _longPhone; }
            set { _longPhone = value; }
        }
        [DisplayName("短号")]
        public String ShortPhone
        {
            get { return _shortPhone; }
            set { _shortPhone = value; }
        }
        [DisplayName("个人荣誉")]
        public String Honour
        {
            get { return _honour; }
            set { _honour = value; }
        }
        [DisplayName("文理科")]
        public String UserArtsScience
        {
            get { return _userArtsScience; }
            set { _userArtsScience = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("当前密码")]
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required]
        //[ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("新密码")]
        public String NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("确认新密码")]
        public String ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }


    }

    /// <summary>
    /// 学生改密码model
    /// </summary>
    public class StuChangePasswordModel
    {
        private String _userNum;
        private String _userName;
        private String _password;
        private String _newPassword;
        private String _confirmPassword;

        [DisplayName("学号")]
        public String UserNum
        {
            get { return _userNum; }
            set { _userNum = value; }
        }
        [DisplayName("姓名")]
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("当前密码")]
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("新密码")]
        //[ValidatePasswordLength]
        public String NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("确认新密码")]
        public String ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }

    }
    #endregion


    #region Services
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }

    //[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
    //{
    //    private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
    //    private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

    //    public ValidatePasswordLengthAttribute()
    //        : base(_defaultErrorMessage)
    //    {
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
    //            name, _minCharacters);
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        string valueAsString = value as string;
    //        return (valueAsString != null && valueAsString.Length >= _minCharacters);
    //    }


    //    public IEnumerable<System.Web.Mvc.ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
    //        return new[]{
    //            new System.Web.Mvc.ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), _minCharacters, int.MaxValue)
    //        };
    //    }
    //}
    #endregion

}
