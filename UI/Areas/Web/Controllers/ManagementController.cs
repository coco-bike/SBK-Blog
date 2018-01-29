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
            int totalCount;
            var pageIndex = Convert.ToInt32(pagenow);
            var pageSize = Convert.ToInt32(pagesize);
            var articleList = this._blogArticleWebService.GetPagingList(pageIndex, pageSize, out totalCount, true, s => s.State == 1, s => s.Id).Select(t => new {
            t.Title,
            t.UpdateTime,
            t.State,
            t.WatchCount,
            t.Id
            }).ToList();
            return JsonBackResult(ResultStatus.Success, new { TotalCount = totalCount, List = articleList });

        }
    }
}
