using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mapping
{
    public class  BlogCommentMap:EntityTypeConfiguration<BlogComment>
    {
        public BlogCommentMap()
        {
            //配置主键
            this.HasKey(t => t.Id);

            //给ID配置自动增长
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //配置字段属性
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.CommentId).IsRequired();
            this.Property(t => t.Content).IsRequired().HasMaxLength(50);
            this.Property(t => t.CreateTime).IsRequired();
            this.Property(t => t.State).IsRequired();
            
            //配置关系
            this.HasRequired(s => s.BlogArticle).WithMany(t => t.BlogComments).HasForeignKey(s => s.BlogArticleId).WillCascadeOnDelete(false);

            this.HasRequired(s => s.User).WithMany(t => t.BlogComments).HasForeignKey(s => s.UserId).WillCascadeOnDelete(false);
        }
    }
}
