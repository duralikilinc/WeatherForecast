
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Business.Abstract;
using Business.Concrete;
using Core.CrossCuttingConcerns.Security.Web;
using DataAccess.Concrete.EntityFramework;
using MvcWebUI.Filters;
using MvcWebUI.Models;
using MvcWebUI.Utilities;

namespace MvcWebUI.Controllers
{
    [ExceptionHandler]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        
        public ActionResult WeatherIndex()
        {
            return View();
        }
    }
}