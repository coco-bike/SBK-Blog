using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class ManagementController : JsonController
    {
        //
        // GET: /Web/Management/
        public ActionResult Index()
        {
            return View();
        }

        public JsonBackResult GetArticleList()
        {
            return JsonBackResult(ResultStatus.Success);

        }
    }
}
