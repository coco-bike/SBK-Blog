using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
using Model;
using UI.Controllers.Base;

namespace UI.Areas.Admin.Controllers
{
    public class AdminLoginController : JsonController
    {
        //
        // GET: /AdminLogin/index/
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult AdminForgotPassword()
        {
            return View();
        }

        #region 初始化
        readonly IUserAdminService _adminUserService;
        public AdminLoginController(IUserAdminService userService)
        {
            this._adminUserService = userService;
        }
        #endregion
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVerifiedCode()
        {
            string codeStr;
            byte[] arr = new VerifyCode().GetVerifyCode(out codeStr);
            Session.Add("Verifycode", codeStr);
            return File(arr, @"image/Gif");
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="verifyCode"></param>
        /// <param name="autoLogin"></param>
        /// <returns></returns>
        public JsonBackResult CheckLoginInfo(string username, string pwd, string verifyCode, string autoLogin)
        {
            if (Session["VerifyCode"] == null)
            {
                return JsonBackResult(ResultStatus.Fail);
            }
            string code = Session["VerifyCode"].ToString();
            if (string.Compare(code, verifyCode, true) != 0)
            {
                return JsonBackResult(ResultStatus.ValidateCodeErr);
            }
            string pwdMd5 = Common.EncryptionHelper.GetMd5Str(pwd);
            var userInfo = this._adminUserService.GetList(s => s.Name == username && s.Password == pwdMd5).FirstOrDefault();
            if (userInfo != null)
            {
                string sessionId = Guid.NewGuid().ToString();
                Response.Cookies["sessionId"].Value = sessionId;
                if (autoLogin == "true")
                {
                    CacheHelper.Set(sessionId, userInfo, DateTime.Now.AddDays(30));
                    Response.Cookies["sessiionId"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    CacheHelper.Set(sessionId, userInfo, DateTime.Now.AddDays(1));
                    Response.Cookies["sessiionId"].Expires = DateTime.Now.AddDays(1);
                }
                userInfo.LoginTime = DateTime.Now.Date;
                var res = this._adminUserService.Update(userInfo);
                if (res > 0)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #region 增删改查
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="rows">页面信息数量</param>
        /// <param name="nickname">用户名</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回一个Json对象包括分页总个数以及用户列表</returns>
        public JsonResult GetAdminUserDataList(string page, string rows, string nickname, string userid)
        {
            int totalCount;
            var pageIndex = Convert.ToInt32(page);
            var pageSize = Convert.ToInt32(rows);
            if (nickname == null && userid == null || (nickname == "" && userid == ""))
            {
                var userList = this._adminUserService.GetPagingList(pageIndex, pageSize, out totalCount, true, s => s.State == 1, s => s.Id).Select(t => new { t.Id, t.BuildTime, t.LoginTime, t.Name, t.TelNumber, t.Type }).ToList();
                return Json(new { total = totalCount, rows = userList });
            }
            else
            {
                totalCount = 1;
                var userList = this._adminUserService.GetList(s => s.Name == nickname && s.Id == Convert.ToInt32(userid) && s.State == 1).ToList();
                return Json(new { tota = totalCount, rows = userList });
            }
        }
        public JsonResult AddUserData(AdminUser userdata)
        {
            var user = this._adminUserService.GetList(s => s.Name == userdata.Name && s.State == 1).FirstOrDefault();
            if (user != null)
            {
                return Json(ResultStatus.Fail);
            }
            AdminUser user1 = new AdminUser();
            user1.BuildTime = DateTime.Now.Date;
            user1.TelNumber = userdata.TelNumber;
            user1.Name = userdata.Name;
            user1.Password = Common.EncryptionHelper.GetMd5Str(userdata.Password);
            user1.State = 1;
            user1.Type = userdata.Type;
            user1.LoginTime = DateTime.Now.Date;
            this._adminUserService.Add(user1);
            return Json(ResultStatus.Fail);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userdata">用户对象</param>
        /// <returns>返回json对象</returns>
        public JsonResult UpdateUserData(AdminUser userdata)
        {
            var user = this._adminUserService.GetList(s => s.Name == userdata.Name && s.State == 1).FirstOrDefault();
            if (user == null)
            {
                return Json(ResultStatus.Fail);
            }
            user.LoginTime = DateTime.Now.Date;
            user.TelNumber = userdata.TelNumber;
            user.Name = userdata.Name;
            user.Type = userdata.Type;
            var res = this._adminUserService.Update(user);
            if (res > 0)
            {
                return Json(ResultStatus.Success);
            }
            return Json(ResultStatus.Fail);
        }
        /// <summary>
        /// 删除用户（假删）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DestroyUser(int id)
        {
            var user = this._adminUserService.GetList(s => s.Id == id).FirstOrDefault();
            if (user == null)
            {
                return Json(ResultStatus.Fail);
            }
            int result = this._adminUserService.DeleteFake(t => t.Id == id, t => new AdminUser() { State = 0 });
            if (result > 0)
            {
                return Json(ResultStatus.Success);
            }
            return Json(ResultStatus.Fail);
        }
        #endregion

        /// <summary>
        /// 获取找回密码验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetForgetPasswordVerifiedCode()
        {
            string codeStr;
            byte[] arr = new VerifyCode().GetVerifyCode(out codeStr);
            Session.Add("FindVerifycode", codeStr);
            return File(arr, @"image/Gif");
        }
        /// <summary>
        /// 验证身份
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonBackResult ValidatePwdBackCode(string name, string icode)
        {
            if (name != null && icode != null)
            {
                string code = icode.ToLower();
                var usercode = Session["FindVerifycode"].ToString();
                string ucode = usercode.ToLower();
                if (ucode != code)
                {
                    return JsonBackResult(ResultStatus.ValidateCodeErr);
                }
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        /// <summary>
        /// 用户找回密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonBackResult UpdateAdminUserData(string name, string password)
        {
            if (name != null && password != null)
            {
                var userdata = this._adminUserService.GetList(s => s.Name == name && s.State == 1).FirstOrDefault();
                var md5password = EncryptionHelper.GetMd5Str(password);
                userdata.Password = md5password;
                this._adminUserService.Update(userdata);
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
    }
}
