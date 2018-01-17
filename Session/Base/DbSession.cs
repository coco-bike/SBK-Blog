using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using IDal;

namespace Session
{
    public class DbSession : IDbSession
    {
        /// <summary>
        /// 创建线程内唯一上下文对象
        /// </summary>
        public DbContext entity
        {
            get
            {
                return DbContextOnly.CreateEF();
            }
        }
        ///范例
        //private IProductAdminDal _ProductDal;
        //public IProductAdminDal ProductDal
        //{
        //    get
        //    {
        //        if (_ProductDal == null)
        //        {
        //            _ProductDal = DalFactory.CreateProductDal();
        //        }
        //        return _ProductDal;
        //    }
        //    set { _ProductDal = value; }
        //}
        private IUserWebDal _UserDal;
        public IUserWebDal UserDal
        {
            get
            {
                if (_UserDal == null)
                {
                    _UserDal = DalFactory.CreateUserDal();
                }
                return _UserDal;
            }
            set { _UserDal = value; }
        }
        private IAuthorityWebDal _AuthorityDal;
        public IAuthorityWebDal AuthorityDal
        {
            get
            {
                if (_AuthorityDal == null)
                {
                    _AuthorityDal = DalFactory.CreateAuthorityDal();
                }
                return _AuthorityDal;
            }
            set { _AuthorityDal = value; }
        }

        public IRoleWebDal _RoleDal;
        public IRoleWebDal RoleDal
        {
            get
            {
                if(_RoleDal==null)
                {
                    _RoleDal = DalFactory.CreateRoleDal();
                }
                return _RoleDal;
            }
            set { _RoleDal = value; }
        }

        public IRoleAdminDal _AdminRoleDal;
       public IRoleAdminDal AdminRoleDal
        {
            get
            {
                if (_AdminRoleDal == null)
                {
                    _AdminRoleDal = DalFactory.CreateAdminRoleDal();
                }
                return _AdminRoleDal;
            }
            set { _AdminRoleDal = value; }
        }

       public IUserAdminDal _AdminUserDal;
       public IUserAdminDal AdminUserDal
       {
           get
           {
               if (_AdminUserDal == null)
               {
                   _AdminUserDal = DalFactory.CreateAdminUserDal();
               }
               return _AdminUserDal;
           }
           set
           {
               _AdminUserDal = value;
           }
       }

        public IAuthorityAdminDal _AdminAuthorityDal;
        public IAuthorityAdminDal AdminAuthorityDal
       {
           get
           {
               if (_AdminAuthorityDal == null)
               {
                   _AdminAuthorityDal = DalFactory.CreateAdminAuthorityDal();
               }
               return _AdminAuthorityDal;
           }
           set
           {
               _AdminAuthorityDal = value;
           }
       }

        public IBlogArticleWebDal _BlogArticleDal;
        public IBlogArticleWebDal BlogArticleDal
        {
            get
            {
                if (_BlogArticleDal == null)
                {
                    _BlogArticleDal = DalFactory.CreateBlogArticleDal();
                }
                return _BlogArticleDal;
            }
            set
            {
                _BlogArticleDal = value;
            }
        }

        public IBlogCommentWebDal _BlogCommentDal;
        public IBlogCommentWebDal BlogCommentDal
        {
            get
            {
                if(_BlogCommentDal==null)
                {
                    _BlogCommentDal = DalFactory.CreateBlogCommentDal();
                }
                return _BlogCommentDal;
            }
            set
            {
                _BlogCommentDal = value;
            }
        }
        public IBlogTypeWebDal _BlogTypeDal;
        public IBlogTypeWebDal BlogTypeDal
        {
            get
            {
                if ( _BlogTypeDal == null)
                {
                     _BlogTypeDal = DalFactory.CreateBlogTypeDal();
                }
                return _BlogTypeDal;
            }
            set
            {
                 _BlogTypeDal = value;
            }
        }

        public IBlogFileWebDal _BlogFileDal;
        public IBlogFileWebDal BlogFileDal
        {
            get
            {
                if (_BlogFileDal == null)
                {
                    _BlogFileDal = DalFactory.CreateBlogFileDal();
                }
                return _BlogFileDal;
            }
            set
            {
                _BlogFileDal = value;
            }
        }
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExcuteSql(string sql, object[] parameters)
        {
            return this.entity.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            if (this.entity.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
