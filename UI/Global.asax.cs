using Autofac;
using log4net;
using UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UI.Services;
using Model;
using System.Data.Entity;
using Common;
using Autofac.Integration.Mvc;
using IService;
using Service;

namespace UI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Autofac在MVC中注册
            ContainerBuilder builder = new ContainerBuilder();
            var service = Assembly.Load("IService");
            var service1 = Assembly.Load("Service");
            var service2 = Assembly.Load("Model");
            Assembly[] assemblyArr = new Assembly[] { service, service1, service2 };
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(assemblyArr).AsImplementedInterfaces();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            #region 添加初始数据
            DbContext db = new MyContext();
            if (db.Database.CreateIfNotExists())
            {
                //用户数据
                IUserWebService userService = new UserWebService();
                List<UserModel> userList = new List<UserModel>()
                {
                    new UserModel(){BuildTime=DateTime.Now,Count=0,EMail="5544332211@qq.com",HeadPicUrl="~/Imgs/HeadPic/headpic-1.jpg",LoginTime=DateTime.Now,Type=1,Pwd="112233",State=1,TelNumber="155555555",UName="MrChen",UpdateTime=DateTime.Now},
                    new UserModel(){BuildTime=DateTime.Now,Count=0,EMail="1122334455@qq.com",HeadPicUrl="~/Imgs/HeadPic/headpic-2.jpg",LoginTime=DateTime.Now,Type=2,Pwd="112233",State=1,TelNumber="155555555",UName="MrSong",UpdateTime=DateTime.Now}                    
                };
                userService.AddRange(userList);

                //管理员数据
                IAdminUserService adminUserService = new AdminUserService();
                List<AdminUser> adminUserList = new List<AdminUser>()
                {
                    new AdminUser(){BuildTime=DateTime.Now,Name="sbk",LoginTime=DateTime.Now,Password= Common.EncryptionHelper.GetMd5Str("abc112233"),State=1,TelNumber="18251935175",Type=1},
                    new AdminUser(){BuildTime=DateTime.Now,Name="admin",LoginTime=DateTime.Now,Password= Common.EncryptionHelper.GetMd5Str("abc112233"),State=1,TelNumber="18251935175",Type=1}
                };
                adminUserService.AddRange(adminUserList);

                //后台权限数据
                IAdminAuthorityService adminAuthorityService = new AdminAuthorityService();
                List<AdminAuthority> adminAuthorityList = new List<AdminAuthority>(){
                    new AdminAuthority(){BuildTime=DateTime.Now,Description="用于读取基本信息的权限",Name="读权限",State=1,Type=1},
                    new AdminAuthority(){BuildTime=DateTime.Now,Description="用于写入基本信息的权限",Name="写权限",State=1,Type=1}
                };
                adminAuthorityService.AddRange(adminAuthorityList);

                //后台角色数据
                IAdminRoleService adminRoleService = new AdminRoleService();
                List<AdminRole> adminRoleList = new List<AdminRole>()
                {
                    new AdminRole(){BuildTime=DateTime.Now,Description="管理单个项目",RoleName="S级管理员",State=1},
                    new AdminRole(){BuildTime=DateTime.Now,Description="管理单个项目",RoleName="SS级管理员",State=1}
                };
                adminRoleService.AddRange(adminRoleList);
            }
            #endregion
            //log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息

            //MiniProfilerEF6.Initialize();//注册MiniProfiler，网页性能插件

            log4net.Config.XmlConfigurator.Configure();

            //WaitCallback
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();//出队
                        //string fileName = DateTime.Now.ToString("yyyy-MM-dd")+".txt";
                        //File.AppendAllText(fileLogPath + fileName, ex.ToString(), System.Text.Encoding.Default);
                        //ILog logger = LogManager.GetLogger("errorMsg");
                        ILog logger = log4net.LogManager.GetLogger("logger");
                        logger.Error(ex.ToString());

                        #region 发送邮件
                        //MailHelper mail = new MailHelper();
                        //mail.MailServer = "smtp.qq.com";
                        //mail.MailboxName = "2872845261@qq.com";
                        //mail.MailboxPassword = "obxxsfowztbideee";//开启QQ邮箱POP3/SMTP服务时给的授权码
                        ////操作打开QQ邮箱->在账号下方点击"设置"->账户->POP3/IMAP/SMTP/Exchange/CardDAV/CalDAV服务
                        ////obxxsfowztbideee为2872845261@qq的授权码
                        //mail.MailName = "Error";
                        //try
                        //{
                        //    mail.Send("1015934551@qq.com", "Error", ex.ToString());
                        //}
                        //catch
                        //{ } 
                        #endregion

                    }
                    else
                    {
                        Thread.Sleep(3000);//如果队列中没有数据，则休息为了避免占用CPU的资源.
                    }
                }
            });
        }

        //protected void Application_BeginRequest()
        //{
        //    MiniProfiler.Start();
        //}

        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}
    }
}