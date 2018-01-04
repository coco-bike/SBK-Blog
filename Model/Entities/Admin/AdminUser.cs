using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdminUser
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 管理员类型1： 1级管理员,2：2级管理员,3：3级管理员
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string TelNumber { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime BuildTime { get; set; }
        /// <summary>
        /// 最新登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 状态(0：删除，1：启用，2：禁用)
        /// </summary>
        public int State { get; set; }

        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
