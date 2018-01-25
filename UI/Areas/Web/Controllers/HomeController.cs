using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
using Model;
using UI.Areas.Web.Class;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class HomeController : JsonController
    {
        #region 页面
        //
        // GET: /Web/Home/

        public ActionResult Index()
        {
            return View();
        }
        //文章页面
        public ActionResult Article(string id)
        {
            int articleId = Convert.ToInt32(id);
            ViewBag.Id = articleId;
            return View();
        }
        #endregion


        #region 初始化
        readonly IBlogArticleWebService _blogArticleWebService;
        readonly IBlogCommentWebService _blogCommentWebService;
        readonly IUserWebService _userWebService;

        public HomeController(IBlogArticleWebService blogArticleWebService, IBlogCommentWebService blogCommentWebService, IUserWebService userWebService)
        {
            this._blogArticleWebService = blogArticleWebService;
            this._blogCommentWebService = blogCommentWebService;
            this._userWebService = userWebService;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 点赞功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonBackResult AddZan(string id)
        {
            int articleId=Convert.ToInt32(id);
            BlogArticle article = this._blogArticleWebService.GetList(s => s.Id == articleId && s.State == 1).ToList().FirstOrDefault();
            article.ZanCount += 1;
            var count=article.ZanCount;
            var res = this._blogArticleWebService.Update(article);
            if (res > 0)
            {
                return JsonBackResult(ResultStatus.Success,count);
            }
            return JsonBackResult(ResultStatus.Fail);
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pagenow"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public JsonBackResult GetArticleList(string pagenow,string pagesize)
        {
             int totalCount;
            var pageIndex=Convert.ToInt32(pagenow);
            var pageSize = Convert.ToInt32(pagesize);

            var articleList = this._blogArticleWebService.GetPagingList(pageIndex, pageSize, out totalCount, true, s => s.State == 1, s => s.Id).Select(t => new
            {
                t.Id,
                t.ZanCount,
                t.UpdateTime,
                t.Title,
                t.Summary,
                t.WatchCount,
                t.BlogComments
            }).ToList();
            List<HomeArticleData> dataList = new List<HomeArticleData>();
            for (int i = 0; i < articleList.Count; i++)
            {
                var aId = articleList[i].Id;
                var commitCount=this._blogCommentWebService.GetList(s=>s.BlogArticleId== aId &&s.State==1).ToList().Count;
                HomeArticleData data = new HomeArticleData();
                data.Id = articleList[i].Id;
                data.CommentCount = commitCount;
                data.Summary = articleList[i].Summary;
                data.Title = articleList[i].Title;
                data.UpdateTime = articleList[i].UpdateTime;
                data.WatchCount = articleList[i].WatchCount;
                data.ZanCount = articleList[i].ZanCount;
                dataList.Add(data);
            }
            return JsonBackResult(ResultStatus.Success, new { TotalCount = totalCount, List = dataList });
        }

        /// <summary>
        /// 获取某个文章内容
        /// </summary>
        /// <returns></returns>
        public JsonBackResult GetHtml(string id)
        {
            var articleid = Convert.ToInt32(id);
            var article = this._blogArticleWebService.GetList(s => s.Id == articleid && s.State == 1).ToList().FirstOrDefault();
            if (article == null)
            {
                return JsonBackResult(ResultStatus.Fail);
            }
            var commitcount = this._blogCommentWebService.GetList(s=>s.BlogArticleId==articleid&&s.State==1).ToList().Count;
            HomeArticleHtml articleData = new HomeArticleHtml();
            articleData.CommitCount = commitcount;
            articleData.Content = article.Content;
            articleData.Title = article.Title;
            articleData.UpdateTime = article.UpdateTime;
            articleData.WatchCount = article.WatchCount;
            articleData.ZanCount = article.ZanCount;
            return JsonBackResult(ResultStatus.Success,articleData);

        }

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <returns></returns>
        public JsonBackResult GetCommentList(string id)
        {
            var articleid = Convert.ToInt32(id);
            var commentList = this._blogCommentWebService.GetList(s => s.BlogArticleId == articleid & s.State == 1).ToList();
            List<HomeCommentData> list = new List<HomeCommentData>();
            for (int i = 0; i < commentList.Count;i++ )
            {
                HomeCommentData data = new HomeCommentData();
                data.Id = commentList[i].Id;
                data.Content = commentList[i].Content;
                data.UpdateTime = commentList[i].UpdateTime;                
                UserModel user = new UserModel();
                var userid = commentList[i].UserId;
                user= this._userWebService.GetList(s => s.Id == userid).ToList().FirstOrDefault();
                data.UserData = user.Name;
                list.Add(data);
            }
            return JsonBackResult(ResultStatus.Success,list);
        }

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="committext"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public JsonBackResult PostComment(string committext, string articleId)
        {
            return JsonBackResult(ResultStatus.Success);
        }

     

        /// <summary>
        /// 保存修改的评论
        /// </summary>
        /// <returns></returns>
        public JsonBackResult SaveEditComment()
        {
            return JsonBackResult(ResultStatus.Success);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <returns></returns>
        public JsonBackResult DestoryComment()
        {
            return JsonBackResult(ResultStatus.Success);
        }
        #endregion
    }
}
