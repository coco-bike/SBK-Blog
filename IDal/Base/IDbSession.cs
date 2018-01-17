using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal
{
    public interface IDbSession
    {
        DbContext entity { get; }
        IUserWebDal UserDal { get; set; }
        IAuthorityWebDal AuthorityDal { get; set; }
        IRoleWebDal RoleDal { get; set; }
        IAuthorityAdminDal AdminAuthorityDal { get; set; }
        IRoleAdminDal AdminRoleDal { get; set; }
        IUserAdminDal AdminUserDal { get; set; }
        IBlogArticleWebDal BlogArticleDal { get; set; }
        IBlogCommentWebDal BlogCommentDal { get; set; }
        IBlogFileWebDal BlogFileDal { get; set; }
        IBlogTypeWebDal BlogTypeDal { get; set; }
        int ExcuteSql(string sql, object[] parameters);
        bool SaveChanges();
    }
}
