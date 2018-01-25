using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Areas.Web.Class
{
    public class HomeArticleHtml
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdateTime { get; set; }
        public long WatchCount { get; set; }
        public long CommitCount { get; set; }
        public long ZanCount { get; set; }
    }
}