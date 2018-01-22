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
    public class BlogArticleMap : EntityTypeConfiguration<BlogArticle>
    {
        public BlogArticleMap()
        {
            //配置主键
            this.HasKey(t => t.Id);

            //给ID配置自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Address).IsRequired().HasMaxLength(500);
            this.Property(t => t.Content).IsRequired().HasMaxLength(20000);
            this.Property(t => t.CreateTime).IsRequired();
            this.Property(t => t.State).IsRequired();
            this.Property(t => t.Title).IsRequired().HasMaxLength(50);
            this.Property(t => t.Summary).IsRequired().HasMaxLength(50);
            this.Property(t => t.UpdateTime).IsRequired();
            this.Property(t => t.WatchCount).IsRequired();
            this.Property(t => t.ZanCount).IsRequired();

            //配置关系
            this.HasRequired(s => s.Type).WithMany(t => t.BlogArticles).HasForeignKey(s => s.BlogTypeId).WillCascadeOnDelete(false);

        }
    }
}
