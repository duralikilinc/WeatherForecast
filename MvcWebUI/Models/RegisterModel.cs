using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Concrete;

namespace MvcWebUI.Models
{
    public class RegisterModel
    {
        public Users Users { get; set; }
        public string ConfirmPassword { get; set; }
    }
}