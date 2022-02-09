using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.ExceptionAspects;
using Core.Aspects.Postsharp.LogAspects;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.Utilities.Results;
using PostSharp.Extensibility;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Security.Hashing;
using Core.Utilities.Business;
using Entities.ComplexType;
using FluentValidation.Results;
using FluentValidation.TestHelper;

namespace Business.Concrete
{
    [LogAspect(typeof(DataBaseLogger))]
    [ExceptionLogAspect(typeof(FileLogger))]
    [LogAspect(typeof(FileLogger))]
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<Users>> GetAll()
        {
            return new SuccessDataResult<List<Users>>(_userDal.GetList());
        }

       

        public IDataResult<List<UserRoleItem>> GetUserRoles(Users users)
        {
            return new SuccessDataResult<List<UserRoleItem>>(_userDal.GetUserRoles(users));
        }

        [FluentValidationAspect(typeof(UserValidatior))]
        public IResult Add(Users users)
        {
            var result = BusinessRules.Run(CheckIfUserMailExists(users.Email));
            if (result != null)
            {
                return result;
            }

            var has = HashingHelper.MD5Olustur(users.Password);
            users.Password = has;
            _userDal.Add(users);
            return new SuccessResult(Messages.UsersApproved);
        }

        public IResult Update(Users users)
        {
            _userDal.Update(users);
            return new SuccessResult();
        }

        private IResult CheckIfUserMailExists(string mail)
        {
            var result = _userDal.GetList(k => k.Email == mail).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserMailAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<Users> GetByUserNameAndPassword(string userName, string password)
        {
            var has = HashingHelper.MD5Olustur(password);
            var result = _userDal.Get(u => u.UserName == userName && u.Password == has && u.IsApproved == true);
            if (result != null)
            {
                return new SuccessDataResult<Users>(result);
            }
            else
            {
                return new ErrorDataResult<Users>("Login Failed!");
            }


        }

        public IDataResult<Users> GetById(string confirmId)
        {
            return new SuccessDataResult<Users>(_userDal.Get(k => k.ConfirmId == confirmId));
        }

        public IDataResult<Users> GetByMail(string mail)
        {
            var result = _userDal.Get(k => k.Email == mail);
            if (result != null)
            {
                return new SuccessDataResult<Users>(result);
            }
            return new ErrorDataResult<Users>("Bu mail adresi sistemde kayıtlı değildir.");
        }
    }
}
