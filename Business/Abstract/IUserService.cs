using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.ComplexType;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<Users>> GetAll();
        IDataResult<List<UserRoleItem>> GetUserRoles(Users users);
        IResult Add(Users users);
        IResult Update(Users users);
        IDataResult<Users> GetByUserNameAndPassword(string userName, string password);
        IDataResult<Users> GetById(string confirmId);
        IDataResult<Users> GetByMail(string mail);
    }
}
