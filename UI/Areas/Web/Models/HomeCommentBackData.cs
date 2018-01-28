using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Areas.Web
{
    public class HomeCommentBackData
    {
        public long Id { get; set; }

        public int CommentCount { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}