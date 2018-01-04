using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model.Mapping
{
    public class AdminUserMap : EntityTypeConfiguration<AdminUser>
    {
        public AdminUserMap()
        {
            //主键
            this.HasKey(t => t.Id);

            //设置自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.BuildTime).IsRequired();
            this.Property(t => t.LoginTime).IsRequired();
            this.Property(t => t.Name).IsRequired().HasMaxLength(500);
            this.Property(t => t.Password).IsRequired().HasMaxLength(50);
            this.Property(t => t.State).IsRequired();
            this.Property(t => t.TelNumber).IsRequired().HasMaxLength(50);
            this.Property(t => t.Type).IsRequired();

            //配置关系
            this.HasMany(t => t.AdminRoles).WithMany(t => t.AdminUsers).Map(t => t.ToTable("AdminUserRole").MapLeftKey("UserID").MapRightKey("RoleID"));
        }
    }
}
