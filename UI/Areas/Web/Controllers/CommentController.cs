using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class CommentController : JsonController
    {
        //
        // GET: /Web/Comment/

        public ActionResult Comment()
        {
            return View();
        }

        public JsonBackResult GetCommentList()
        {
            return JsonBackResult(ResultStatus.Success);

        }
        public JsonBackResult DestoryComment(string id)
        {
            return JsonBackResult(ResultStatus.Success);

        }

    }
}
