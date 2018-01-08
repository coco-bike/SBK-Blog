using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Areas.Admin.Models
{
    public class AdminUserSaveData
    {
        public string id { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 管理员类型1： 1级管理员,2：2级管理员,3：3级管理员
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string TelNumber { get; set; }
        
        public string Roles { get; set; }
    }
}