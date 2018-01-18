using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Web.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Web/Article/
        public ActionResult AddArticle()
        {
            return View();
        }

    }
}
