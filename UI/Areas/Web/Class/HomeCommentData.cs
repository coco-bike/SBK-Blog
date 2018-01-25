using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Areas.Web.Class
{
    public class HomeCommentData
    {
        public long Id { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UserData { get; set; }
        public string Content { get; set; }
    }
}