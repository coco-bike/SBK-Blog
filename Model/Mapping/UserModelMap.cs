using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserModelMap : EntityTypeConfiguration<UserModel>
    {
        public UserModelMap()
        {
            //主键
            this.HasKey(t=>t.Id);

            //设置自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.BuildTime).IsRequired();
            this.Property(t => t.Count).IsRequired();
            this.Property(t => t.EMail).IsRequired().HasMaxLength(500);
            this.Property(t => t.HeadPicUrl).IsRequired().HasMaxLength(500);
            this.Property(t => t.LoginTime).IsRequired();
            this.Property(t => t.Type).IsRequired();
            this.Property(t => t.Pwd).IsRequired().HasMaxLength(500);
            this.Property(t => t.State).IsRequired();
            this.Property(t => t.TelNumber).IsRequired().HasMaxLength(500);
            this.Property(t => t.Name).IsRequired().HasMaxLength(500);
            this.Property(t => t.RoleID).IsRequired();

            //配置关系
            this.HasRequired(s => s.Role).WithMany(t => t.UserModels).HasForeignKey(s => s.RoleID).WillCascadeOnDelete(false);
        }
    }
}
