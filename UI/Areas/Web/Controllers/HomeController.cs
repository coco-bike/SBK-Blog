using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class HomeController : JsonController
    {
        //
        // GET: /Web/Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult article(string id)
        {
            int articleId = Convert.ToInt32(id);
            ViewBag.Id = articleId;
            return View();
        }
        public JsonBackResult AddZan(string id)
        {
            return JsonBackResult(ResultStatus.Success);
        }
    }
}
