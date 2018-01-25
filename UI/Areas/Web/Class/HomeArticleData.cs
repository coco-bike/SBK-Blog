using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Areas.Web.Class
{
    public class HomeArticleData
    {
        public long Id { get;set;}
        public long ZanCount { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public long WatchCount { get; set; }
        public int CommentCount { get; set; }
    }
}