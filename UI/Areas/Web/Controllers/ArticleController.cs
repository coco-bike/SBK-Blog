using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Controllers.Base;
using Common;

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
        public JsonBackResult GetEditArticleContent(string id)
        {
            return JsonBackResult(ResultStatus.Success);

        }
        public JsonBackResult DestoryArticle(string id)
        {
            return JsonBackResult(ResultStatus.Success);
        }

        public JsonBackResult GetArticleType()
        {
            return JsonBackResult(ResultStatus.Success);

        }
        public JsonBackResult AddArticleContent()
        {
            return JsonBackResult(ResultStatus.Success);

        }
        public JsonBackResult PostEditArticle()
        {
            return JsonBackResult(ResultStatus.Success);
        }
    }
}
