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
    public class ManagementController : JsonController
    {

        #region 页面
        //
        // GET: /Web/Management/
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 初始化
        readonly IBlogArticleWebService _blogArticleWebService;
        public ManagementController(IBlogArticleWebService blogArticleWebService){
            this._blogArticleWebService = blogArticleWebService;
        }
        #endregion
        public JsonBackResult GetArticleList(string pagenow, string pagesize)
        {

            return JsonBackResult(ResultStatus.Success);

        }
        public JsonBackResult EditArticle()
        {
            return JsonBackResult(ResultStatus.Success);
        }

        public JsonBackResult DestoryArticle()
        {
            return JsonBackResult(ResultStatus.Success);
        }
    }
}
