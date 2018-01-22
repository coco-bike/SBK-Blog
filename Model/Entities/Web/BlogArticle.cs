using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BlogArticle
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 阅读人数
        /// </summary>
        public long WatchCount { get; set; }
        /// <summary>
        /// 点赞人数
        /// </summary>
        public long ZanCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 博客地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 状态：1启用 ，0禁止
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 博客类型外键
        /// </summary>
        public long BlogTypeId{get;set;}

        public virtual BlogTypes Type { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; }
             
    }
}
