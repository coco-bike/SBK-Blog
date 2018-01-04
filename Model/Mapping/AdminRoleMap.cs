using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mapping
{
    public class AdminRoleMap : EntityTypeConfiguration<AdminRole>
    {
        public AdminRoleMap()
        {
            //主键
            this.HasKey(t => t.Id);

            //设置自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.BuildTime).IsRequired();
            this.Property(t => t.Description).IsRequired().HasMaxLength(500);
            this.Property(t => t.RoleName).IsRequired().HasMaxLength(50);
            this.Property(t => t.State).IsRequired();
        }
    }
}
