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
                //前台权限
                IAuthorityWebService authorityService = new AuthorityWebService();
                List<AuthorityModel> authorityList = new List<AuthorityModel>()
                {
                    new AuthorityModel(){BuildTime=DateTime.Now,Description="读博客的权限",Name="读权限",State=1,Type=1,UpdateTime=DateTime.Now,RoleModels=new List<RoleModel>()},
                    new AuthorityModel(){BuildTime=DateTime.Now,Description="写博客的权限",Name="写权限",State=1,Type=1,UpdateTime=DateTime.Now,RoleModels=new List<RoleModel>()}
                };
                authorityService.AddRange(authorityList);
                AuthorityModel authority1 = new AuthorityModel();
                authority1 = authorityService.GetList(s => s.Name == "读权限" && s.State == 1).ToList().FirstOrDefault();


                //前台角色
                IRoleWebService roleService = new RoleWebService();
                List<RoleModel> roleList = new List<RoleModel>(){
                    new RoleModel(){BuildTime=DateTime.Now,Description="一般用户",RoleName="普通用户",State=1,UpdateTime=DateTime.Now,AuthorityModels=new List<AuthorityModel>(),UserModels=new List<UserModel>()},
                    new RoleModel(){BuildTime=DateTime.Now,Description="充值的用户拥有更多权限",RoleName="VIP用户",State=1,UpdateTime=DateTime.Now,AuthorityModels=new List<AuthorityModel>(),UserModels=new List<UserModel>()}
                };
                roleService.AddRange(roleList);

                var role1 = roleService.GetList(s=>s.RoleName=="普通用户"&&s.State==1).ToList().FirstOrDefault();
                var role2 = roleService.GetList(s=>s.RoleName=="VIP用户"&&s.State==1).ToList().FirstOrDefault();
                role1.AuthorityModels.Add(authority1);
                role2.AuthorityModels.Add(authority1);
                roleService.Update(role1);
                roleService.Update(role2);

                //用户数据
                IUserWebService userService = new UserWebService();
                List<UserModel> userList = new List<UserModel>()
                {
                    new UserModel(){BuildTime=DateTime.Now,Count=0,EMail="332211@qq.com",HeadPicUrl="~/Imgs/HeadPic/headpic-1.jpg",LoginTime=DateTime.Now,Type=1,Pwd= Common.EncryptionHelper.GetMd5Str("112233"),State=1,TelNumber="155555555",Name="MrChen",UpdateTime=DateTime.Now,Role=role1},
                    new UserModel(){BuildTime=DateTime.Now,Count=0,EMail="112233@qq.com",HeadPicUrl="~/Imgs/HeadPic/headpic-2.jpg",LoginTime=DateTime.Now,Type=2,Pwd= Common.EncryptionHelper.GetMd5Str("112233"),State=1,TelNumber="155555555",Name="MrSong",UpdateTime=DateTime.Now,Role=role2}                    
                };
                userService.AddRange(userList);

                UserModel user11 = new UserModel();
                user11 = userService.GetList(s => s.Id == 1).ToList().FirstOrDefault();

                //博客类型
                IBlogTypeWebService blogTypeService = new BlogTypeWebService();
                List<BlogTypes> typeList = new List<BlogTypes>(){
                    new BlogTypes(){CreateTime=DateTime.Now,State=1,TypeName="类型1",UpdateTmie=DateTime.Now,BlogArticles =new List<BlogArticle>()},
                    new BlogTypes(){CreateTime=DateTime.Now,State=1,TypeName="类型2",UpdateTmie=DateTime.Now,BlogArticles =new List<BlogArticle>()},
                };
                blogTypeService.AddRange(typeList);

                var type1 = blogTypeService.GetList(s => s.Id == 1).ToList().FirstOrDefault();
                var type2 = blogTypeService.GetList(s => s.Id == 2).ToList().FirstOrDefault();
                BlogTypes type11 = new BlogTypes();
                type11 = type1;
                BlogTypes type22 = new BlogTypes();
                type22 = type2;

                //博客文章
                IBlogArticleWebService blogArticleService = new BlogArticleWebService();
                List<BlogArticle> articleList = new List<BlogArticle>()
                {
                    new BlogArticle(){Address="1313",Content="1211122212",CreateTime=DateTime.Now,State=1,Title="测试1",UpdateTime=DateTime.Now,WatchCount=1,ZanCount=0,BlogComments=new List<BlogComment>(),Type=type11,Summary="aaa" },
                    new BlogArticle(){Address="1312",Content="1211122212",CreateTime=DateTime.Now,State=1,Title="测试2",UpdateTime=DateTime.Now,WatchCount=1,ZanCount=0,BlogComments=new List<BlogComment>(),Type=type22,Summary="bbb" }
                };
                blogArticleService.AddRange(articleList);

                BlogArticle article1 = new BlogArticle();
                article1 = blogArticleService.GetList(s => s.Id == 1).ToList().FirstOrDefault();

                //博客评论
                IBlogCommentWebService blogCommentService = new BlogCommentWebService();
                List<BlogComment> commentList = new List<BlogComment>(){
                    new BlogComment(){Content="你好啊",UpdateTime=DateTime.Now,State=1,CommentId=0,BlogArticle=article1,User=user11,CreateTime=DateTime.Now},
                    new BlogComment(){Content="你好啊",UpdateTime=DateTime.Now,State=1,CommentId=1,BlogArticle=article1,User=user11,CreateTime=DateTime.Now},
                };
                blogCommentService.AddRange(commentList);

               
               



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