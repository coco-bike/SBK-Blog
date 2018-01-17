using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BlogTypes
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 分类类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTmie { get; set; }
        /// <summary>
        /// 状态：1启用，0禁用
        /// </summary>
        public int State { get; set; }

        public virtual ICollection<BlogArticle> BlogArticles { get;set;}
    }
}
