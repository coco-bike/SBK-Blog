using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Services;

namespace UI.Areas.Admin.Controllers
{
    public class AuthorityController : Controller
    {
        //
        // GET: /Admin/Authority/
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }

    }
}
