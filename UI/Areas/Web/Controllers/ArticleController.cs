using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Controllers.Base;
using Common;
using IService;
using Model;

namespace UI.Areas.Web.Controllers
{
    public class ArticleController : JsonController
    {
        #region 页面
        // GET: /Web/Article/
        public ActionResult AddArticle()
        {
            return View();
        }

        public ActionResult EditArticle(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        #endregion
        #region 初始化
        readonly IBlogArticleWebService _blogArticleWebService;
        readonly IBlogTypeWebService _blogTypeWebService;
        public ArticleController(IBlogArticleWebService blogArticleWebService,IBlogTypeWebService blogTypeWebService)
        {
            this._blogArticleWebService = blogArticleWebService;
            this._blogTypeWebService = blogTypeWebService;
        }
        #endregion
        #region 操作
        /// <summary>
        /// 取得修改文章的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonBackResult GetEditArticleContent(string id)
        {
            var articleId = Convert.ToInt32(id);
            var article = this._blogArticleWebService.GetList(s => s.Id == articleId && s.State == 1).Select(t => new {t.Content,t.State,t.Title,t.BlogTypeId}).ToList().FirstOrDefault();
            if (article == null)
            {
                return JsonBackResult(ResultStatus.Fail);
            }
            return JsonBackResult(ResultStatus.Success,article);

        }
        /// <summary>
        /// 获取文章总的分类
        /// </summary>
        /// <returns></returns>
        public JsonBackResult GetArticleType()
        {
            var typeList = this._blogTypeWebService.GetList(s => s.State == 1).Select(t => new { t.Id, t.TypeName }).ToList();
            return JsonBackResult(ResultStatus.Success, typeList);

        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonBackResult DestoryArticle(string id)
        {

            return JsonBackResult(ResultStatus.Success);
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonBackResult AddArticleContent(string htmltext,string title,string typeid,string summary,string checkbox1)
        {
            var id = Convert.ToInt32(typeid);
            BlogTypes type = this._blogTypeWebService.GetList(s => s.Id == id && s.State == 1).ToList().FirstOrDefault();
            BlogArticle article = new BlogArticle();
            article.Content = htmltext;
            article.CreateTime = DateTime.Now;
            article.State = 1;
            article.Summary = summary;
            article.Title = title;
            article.Type = type;
            article.UpdateTime = DateTime.Now;
            article.WatchCount = 0;
            article.ZanCount = 0;
            article.Address = " ";
            this._blogArticleWebService.Add(article);
            
            return JsonBackResult(ResultStatus.Success);

        }
        /// <summary>
        /// 保存修改的文章内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonBackResult PostEditArticle(string articleid,string htmltext, string title, string typeid,string checkbox1,string summary)
        {
            var id = Convert.ToInt32(typeid);
            var aid = Convert.ToInt32(articleid);
            var state = Convert.ToBoolean(checkbox1);
            int articleState;
            var article = this._blogArticleWebService.GetList(s => s.Id == aid && s.State == 1).ToList().FirstOrDefault();
            var type = this._blogTypeWebService.GetList(s => s.Id == id && s.State == 1).ToList().FirstOrDefault();
            if(state!=true){
             articleState =0;
            }else{
                articleState=1;
            }
            article.State = articleState;
            article.Content = htmltext;
            article.Title = title;
            article.Type = type;
            article.Summary = summary;

            var res= this._blogArticleWebService.Update(article);
            if (res > 0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
            
        }
        #endregion 
    }
}
