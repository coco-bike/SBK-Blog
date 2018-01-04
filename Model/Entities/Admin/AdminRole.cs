using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdminRole
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色姓名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime BuildTime { get; set; }
        //状态（0：删除，1：存在）
        public int State { get; set; }

        public virtual ICollection<AdminAuthority> AdminAuthoritys { get; set; }
        public virtual ICollection<AdminUser> AdminUsers { get; set; }
    }
}
