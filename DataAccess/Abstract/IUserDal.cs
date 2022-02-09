using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.DataAccess;
using Entities.ComplexType;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<Users>
    {
        List<UserRoleItem> GetUserRoles(Users user);
    }
}
