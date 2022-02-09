using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
   public class Users:IEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ConfirmId { get; set; }

        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }


    }
}
