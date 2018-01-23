using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
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
        public ActionResult article(string id)
        {
            int articleId = Convert.ToInt32(id);
            ViewBag.Id = articleId;
            return View();
        }
        #endregion


        #region 初始化
        readonly IBlogArticleWebService _blogArticleWebService;
        readonly IBlogCommentWebService _blogCommentWebService;

        public HomeController(IBlogArticleWebService blogArticleWebService, IBlogCommentWebService blogCommentWebService)
        {
            this._blogArticleWebService = blogArticleWebService;
            this._blogCommentWebService = blogCommentWebService;
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
            return JsonBackResult(ResultStatus.Success);
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pagenow"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public JsonBackResult GetArticleList(string pagenow,string pagesize)
        {
            return JsonBackResult(ResultStatus.Success);
        }

        /// <summary>
        /// 获取某个文章内容
        /// </summary>
        /// <returns></returns>
        public JsonBackResult GetHtml(string id)
        {
            return JsonBackResult(ResultStatus.Success);

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
        /// 获取评论列表
        /// </summary>
        /// <returns></returns>
        public JsonBackResult GetCommentList()
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
