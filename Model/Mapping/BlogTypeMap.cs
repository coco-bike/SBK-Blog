using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mapping
{
    public class BlogTypeMap : EntityTypeConfiguration<BlogTypes>
    {
        public BlogTypeMap()
        {
            //配置主键
            this.HasKey(t => t.Id);

            //给ID配置自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.CreateTime).IsRequired();
            this.Property(t => t.State).IsRequired();
            this.Property(t => t.TypeName).IsRequired().HasMaxLength(50);
            this.Property(t => t.UpdateTmie).IsRequired();

            
        }
    }
}
