﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Web.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Web/Comment/

        public ActionResult Comment()
        {
            return View();
        }

    }
}
