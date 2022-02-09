using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Entities.ComplexType;

namespace DataAccess.Concrete.EntityFramework
{
   public class EfUserDal:EfEntityRepositoryBase<Users,WeatherForecastContext>,IUserDal
    {
        public List<UserRoleItem> GetUserRoles(Users user)
        {

            using (WeatherForecastContext context = new WeatherForecastContext())
            {
                var result = from kr in context.UserRole
                             join r in context.Roles
                        on kr.RoleId equals r.Id
                    where kr.KullaniciId == user.Id
                    select new UserRoleItem { RoleName = r.RoleName };

                return result.ToList();
            }

        }
    }
}
