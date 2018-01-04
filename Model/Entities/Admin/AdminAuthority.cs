using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdminAuthority
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 权限建立时间
        /// </summary>
        public DateTime BuildTime { get; set; }
        //状态：1启用 0禁用
        public int State { get; set; }
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
