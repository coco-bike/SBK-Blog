using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
using Model;
using UI.Areas.Admin.Models;

namespace UI.Areas.Admin.Controllers
{
    public class AdminUserController : Controller
    {
        #region 页面
        //
        // GET: /Admin/Index/
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 初始化

        readonly IAdminUserService _adminUserService;
        readonly IAdminRoleService _adminRoleService;
        public AdminUserController(IAdminUserService adminUserService, IAdminRoleService adminRoleService)
        {
            this._adminRoleService = adminRoleService;
            this._adminUserService = adminUserService;
        }
        #endregion
        /// <summary>
        /// 后台用户分页查询
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAdminUserListData(string page, string rows, string username)
        {
            int totalCount;
            var pageIndex = Convert.ToInt32(page);
            var pageSize = Convert.ToInt32(rows);
            if (username == null || username == "")
            {
                var adminUserList = this._adminUserService.GetPagingList(pageIndex, pageSize, out totalCount, true, s => s.State == 1, s => s.Id).Select(t => new { t.Id, t.Name, t.LoginTime, t.TelNumber, t.BuildTime, t.Type, t.AdminRoles, t.State }).ToList();
                List<AdminUserData> adminUserDataList = new List<AdminUserData>();
                for (int i = 0; i < adminUserList.Count; i++)
                {
                    int count = 0;
                    AdminUserData userdata = new AdminUserData();
                    userdata.Id = adminUserList[i].Id;
                    userdata.Name = adminUserList[i].Name;
                    userdata.TelNumber = adminUserList[i].TelNumber;
                    userdata.BuildTime = adminUserList[i].BuildTime.ToString();
                    userdata.LoginTime = adminUserList[i].LoginTime.ToString();
                    userdata.Type = adminUserList[i].Type;
                    userdata.State = adminUserList[i].State;
                    var rolelist = adminUserList[i].AdminRoles.ToList();
                    if (rolelist.Count == 0)
                    {
                        userdata.AdminRoles = "无角色";
                    }
                    else
                    {
                        foreach (AdminRole j in rolelist)
                        {
                            if (count == 0)
                            {
                                userdata.AdminRoles = j.RoleName;
                            }
                            else
                            {
                                userdata.AdminRoles = userdata.AdminRoles + "," + j.RoleName;
                            }
                            count++;
                        }
                    }

                    adminUserDataList.Add(userdata);
                }
                return Json(new { total = totalCount, rows = adminUserDataList });

            }
            else
            {
                totalCount = 1;
                var adminUserList = this._adminUserService.GetList(s => s.Name == username && s.State == 1).ToList();
                if (adminUserList.Count > 0)
                {
                    List<AdminUserData> adminUserDataList = new List<AdminUserData>();
                    AdminUserData userdata = new AdminUserData();
                    userdata.Id = adminUserList[0].Id;
                    userdata.Name = adminUserList[0].Name;
                    userdata.TelNumber = adminUserList[0].TelNumber;
                    userdata.BuildTime = adminUserList[0].BuildTime.ToString();
                    userdata.Type = adminUserList[0].Type;
                    userdata.LoginTime = adminUserList[0].LoginTime.ToString();
                    userdata.State = adminUserList[0].State;
                    var rolelist = adminUserList[0].AdminRoles.ToList();
                    if (rolelist.Count == 0)
                    {
                        userdata.AdminRoles = "无角色";
                    }
                    else
                    {
                        foreach (AdminRole j in rolelist)
                        {
                            userdata.AdminRoles = userdata.AdminRoles + "," + j.RoleName;
                        }
                    }
                    adminUserDataList.Add(userdata);
                    return Json(new { total = totalCount, rows = adminUserDataList });
                }
                return Json(ResultStatus.Fail);
            }
        }
        public JsonResult GetAdminRoleListData()
        {
            var adminRoleList = this._adminRoleService.GetList(s => s.State == 1).Select(t => new { t.RoleName, t.Id }).ToList();
            if (adminRoleList == null)
            {
                return Json(ResultStatus.Fail);
            }
            List<object> AdminUserObject = new List<object>();
            for (int i = 0; i < adminRoleList.Count; i++)
            {
                if (i == 0)
                {
                    DefaultInputList adminRoleDefault = new DefaultInputList();
                    adminRoleDefault.id = adminRoleList[i].Id;
                    adminRoleDefault.name = adminRoleList[i].RoleName;
                    adminRoleDefault.desc = " ";
                    adminRoleDefault.selected = true;
                    AdminUserObject.Add(adminRoleDefault);
                }
                else
                {
                    InputList list = new InputList();
                    list.id = adminRoleList[i].Id;
                    list.name = adminRoleList[i].RoleName;
                    list.desc = " ";
                    AdminUserObject.Add(list);
                }

            }
            return Json(AdminUserObject, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveAdminUserData(AdminUserSaveData adminUserData)
        {
            AdminUserSaveData adminUser = new AdminUserSaveData();
            adminUser = adminUserData;
            AdminUser user = new AdminUser();
            var id = Convert.ToInt32(adminUser.Roles);
            //AdminRole roleData = new AdminRole();
            var role = this._adminRoleService.GetList(s => s.Id == id && s.State == 1).ToList().FirstOrDefault();            
            if (role != null)
            {
                user.LoginTime = DateTime.Now;
                user.BuildTime = DateTime.Now;
                user.Name = adminUser.Name;
                user.Password = "abc112233";
                user.State = 1;
                user.TelNumber = adminUser.TelNumber;
                user.Type = adminUser.Type;
            }
            this._adminUserService.Add(user);
            return Json(ResultStatus.Success);
        }
        public JsonResult AddAdminRole(AdminUserSaveData adminUserData)
        {
            var roleid = Convert.ToInt32(adminUserData.Roles);
            var username = adminUserData.Name;
            var role = this._adminRoleService.GetList(s => s.Id == roleid && s.State == 1).ToList().FirstOrDefault();
            var userData = this._adminUserService.GetList(s => s.Name == username && s.State == 1).ToList().FirstOrDefault();
            if(role!=null&&userData!=null){
                userData.AdminRoles.Add(role);
                this._adminUserService.Update(userData);
                return Json(ResultStatus.Success);
            }
            return Json(ResultStatus.Fail);
        }
        public JsonResult UpdateAdminUserData(AdminUserSaveData adminUserData)
        {
            AdminUserSaveData adminUser = new AdminUserSaveData();
            adminUser = adminUserData;
            var id =Convert.ToInt32( adminUser.id);
            var roleId = Convert.ToInt32(adminUser.Roles);
            var user = this._adminUserService.GetList(s => s.Id == id && s.State == 1).ToList().FirstOrDefault();
            var role = this._adminRoleService.GetList(s => s.Id ==roleId && s.State == 1).ToList().FirstOrDefault();
            if (role != null)
            {
                user.LoginTime = DateTime.Now;
                user.Name = adminUser.Name;
                user.Type = adminUser.Type;
                user.TelNumber = adminUser.TelNumber;
                user.AdminRoles.Add(role);
            }
            this._adminUserService.Update(user);
            return Json(ResultStatus.Success);
        }
        public JsonResult DeleteAdminUserData(int id)
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
    }
}
