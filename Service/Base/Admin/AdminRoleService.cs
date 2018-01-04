using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Model;

namespace Service
{
    public class AdminRoleService:BaseService<AdminRole>,IAdminRoleService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.AdminRoleDal;
        }
    }
}
