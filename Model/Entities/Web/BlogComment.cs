using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BlogComment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 父评论（无父评论为0）
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 状态：1启用，0禁用
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 外键用户的ID
        /// </summary>
        public long UserId { get; set; }
        public long BlogArticleId { get; set; }
        public virtual UserModel User { get; set; }
        public virtual BlogArticle BlogArticle { get; set; }
    }
}
