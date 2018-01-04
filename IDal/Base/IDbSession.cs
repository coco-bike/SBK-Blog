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
        int ExcuteSql(string sql, object[] parameters);
        bool SaveChanges();
    }
}
