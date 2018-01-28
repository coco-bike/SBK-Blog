using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
using Model;
using UI.Controllers.Base;
using UI.Services;

namespace UI.Areas.Web.Controllers
{
    public class WebLoginController : JsonController
    {
        #region 页面
        //
        // GET: /Web/Index/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        #endregion

        #region 初始化
        readonly IUserWebService _userWebService;
        readonly IRoleWebService _roleWebService;

        public WebLoginController(IUserWebService userWebService, IRoleWebService roleWebService)
        {
            this._userWebService = userWebService;
            this._roleWebService = roleWebService;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="autologin"></param>
        /// <returns></returns>
        public JsonBackResult CheckLoginInfo(string email, string password, bool autologin)
        {
            var md5password = EncryptionHelper.GetMd5Str(password);
            var user = this._userWebService.GetList(s => s.EMail == email && s.Pwd == md5password && s.State == 1).FirstOrDefault();
            if (user != null)
            {
                string sessionId = Guid.NewGuid().ToString();
                Response.Cookies["sessionId"].Value = sessionId;
                if (autologin == true)
                {
                    CacheHelper.Set(sessionId, user, DateTime.Now.AddDays(30));
                    Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    CacheHelper.Set(sessionId, user, DateTime.Now.AddDays(1));
                    Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(1);
                }
                user.Count = user.Count + 1;
                user.LoginTime = DateTime.Now;
                var res = this._userWebService.Update(user);
                if (res > 0)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
            }
            return JsonBackResult(ResultStatus.Fail);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonBackResult GetCode(string email)
        {
            if (!RegularHelper.IsEmail(email))
            {
                return JsonBackResult(ResultStatus.EmailErr);
            }
            var user = this._userWebService.GetList(t => t.EMail == email && t.State == 1).FirstOrDefault();
            if (user != null)
            {
                return JsonBackResult(ResultStatus.EmailExist);
            }
            Tuple<string, bool> items = EmailSend.SendEmail(email, "Songbike网络博客", "用户注册码");
            string code = items.Item1;
            Session.Add("VerifyCode", code);
            bool sendRes = items.Item2;
            if (sendRes)
            {
                bool res1 = CacheHelper.Set("RegCode", code, DateTime.Now.AddSeconds(90));
                bool res2 = CacheHelper.Set("Email", email, DateTime.Now.AddSeconds(90));
                if (res1 && res2)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
            }
            return JsonBackResult(ResultStatus.Fail);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonBackResult RegisterUserInfo(string email, string name, string password, string code)
        {
            var usercode = Session["VerifyCode"].ToString();
            var user = this._userWebService.GetList(s => s.EMail == email).FirstOrDefault();
            if (user != null)
            {
                return JsonBackResult(ResultStatus.EmailExist, "你输入的电子邮箱已经注册过");
            }
            if (usercode != code)
            {
                return JsonBackResult(ResultStatus.ValidateCodeErr, "验证码错误,请重新发送并输入新的验证码");
            }
            UserModel realuser = new UserModel();
            RoleModel role = this._roleWebService.GetList(s=>s.RoleName=="普通用户"&&s.State==1).ToList().FirstOrDefault();
            realuser.EMail = email;
            realuser.Name = name;
            realuser.Pwd = EncryptionHelper.GetMd5Str(password);
            realuser.Count = 0;
            realuser.LoginTime = DateTime.Now;
            realuser.BuildTime = DateTime.Now;
            realuser.HeadPicUrl="~/Imgs/HeadPic/headpic-1.jpg";
            realuser.Role = role;
            realuser.State = 1;
            realuser.TelNumber = "";
            realuser.Type = 1;
            realuser.UpdateTime = DateTime.Now;
            this._userWebService.Add(realuser);

            return JsonBackResult(ResultStatus.Success);
        }

        /// <summary>
        /// 找回密码发送验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonBackResult FindUserInfo(string email)
        {
            if (!RegularHelper.IsEmail(email))
            {
                return JsonBackResult(ResultStatus.EmailErr);
            }
            var user = this._userWebService.GetList(s => s.EMail == email).ToList().FirstOrDefault();
            if (user == null)
            {
                return JsonBackResult(ResultStatus.EmailNoExist, user);
            }
            Tuple<string, bool> items = EmailSend.SendEmail(email, "SongBiKe网络博客", "用户找回密码验证码");
            string code = items.Item1;
            Session.Add("ReVerifyCode", code);
            bool sendRes = items.Item2;
            if (sendRes)
            {
                bool res1 = CacheHelper.Set("PwdBackCode", code, DateTime.Now.AddSeconds(90));
                bool res2 = CacheHelper.Set("PwdBackEMail", email, DateTime.Now.AddSeconds(90));//判断用户发送验证码和注册用的是同一个邮箱
                if (res1 && res2)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
            }
            return JsonBackResult(ResultStatus.Fail);
        }

        /// <summary>
        /// 用户找回密码核实验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonBackResult ValidatePwdBackCode(string code)
        {
            var usercode = Session["ReVerifyCode"].ToString();
            if (usercode == code)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.ValidateCodeErr);
        }

        /// <summary>
        /// 用户找回密码重置密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonBackResult GetPassword(string email, string password)
        {
            var md5password = EncryptionHelper.GetMd5Str(password);
            var user = this._userWebService.GetList(s => s.EMail == email&&s.State==1).FirstOrDefault();
            if (user == null)
            {
                return JsonBackResult(ResultStatus.EmailNoExist);
            }
            user.Pwd = md5password;
            user.Count = +1;
            user.LoginTime = DateTime.Now;
            int res = this._userWebService.Update(user);
            if (res > 0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="mail"></param>
        ///// <param name="name"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public JsonBackResult UpdateUserInfo(string mail, string name, string password)
        //{
        //    var user = this._userWebService.GetList(s => s.EMail == mail).FirstOrDefault();
        //    if (user != null)
        //    {
        //        var md5password = EncryptionHelper.GetMd5Str(password);
        //        user.Name = name;
        //        user.Pwd = md5password;
        //        user.Count = +1;
        //        user.UpdateTime = DateTime.Now;
        //        this._userWebService.Update(user);
        //        return JsonBackResult(ResultStatus.Success);
        //    }
        //    return JsonBackResult(ResultStatus.EmailNoExist);
        //}

        ///// <summary>
        ///// 删除用户
        ///// </summary>
        ///// <param name="mail"></param>
        ///// <returns></returns>
        //public JsonBackResult DeleteUserInfo(string mail)
        //{
        //    int result = this._userWebService.DeleteReal(t => t.EMail == mail);
        //    if (result > 0)
        //    {
        //        return JsonBackResult(ResultStatus.Success);
        //    }
        //    return JsonBackResult(ResultStatus.Fail);
        //}

        #endregion
    }
}
