using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Business.Abstract;
using Core.CrossCuttingConcerns.Security.Hashing;
using Core.CrossCuttingConcerns.Security.Web;
using MvcWebUI.Filters;
using MvcWebUI.Models;
using MvcWebUI.Utilities;

namespace MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ExceptionHandler]
        public ActionResult Login(LoginModel model)
        {
            var user = _userService.GetByUserNameAndPassword(model.Users.UserName,model.Users.Password).Data;
           
            if (user != null)
            {
                var userRoles = _userService.GetUserRoles(user).Data.Select(k => k.RoleName).ToArray();
                AuthenticationHelper.CreateAuthCookie(
                    new Guid(),
                    user.UserName,
                    DateTime.Now.AddMinutes(1),
                    userRoles,
                    false,
                    user.FirsName,
                    user.LastName
                );
                return RedirectToAction("WeatherIndex","Home");
            }

            ModelState.AddModelError(String.Empty, "User is Not authenticated");
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
        
        public ActionResult Register()
        {
            return View();
        }
        [ExceptionHandler]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var confirmToken = Guid.NewGuid().ToString();
            model.Users.ConfirmId = confirmToken;
            var result = _userService.Add(model.Users);
            if (result.Success)
            {
                var successMail = new EmailConfiguration().SendConfirmationEmail(confirmToken, model.Users.Email);
                if (successMail.Success)
                {
                    ModelState.AddModelError(String.Empty, result.Message);
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Mail Gönderilirken Bir Hata Oluştu.");
                }

            }

            return View();
        }
        [ExceptionHandler]
        public ActionResult Verify(string id)
        {
            var users = _userService.GetById(id);
            if (users.Data != null)
            {
                users.Data.IsApproved = true;
                users.Data.ConfirmId = Guid.NewGuid().ToString();
                _userService.Update(users.Data);
                ModelState.AddModelError(String.Empty, "Hesabınız aktif edilmiştir.");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Hesabınız aktif edilememiştir. Lütfen daha sonra tekrar deneyiniz.");
            }

            return View("Login");
        }



        [HttpPost]
        public JsonResult ForgetPassword(string email)
        {
            var result = _userService.GetByMail(email);

            if (result.Success)
            {
                var mailResult = new EmailConfiguration().SendForgotPasswordEmail(result.Data.ConfirmId, email);

                return Json(mailResult, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult NewPassword(string pas)
        {
            var users = _userService.GetById(pas);
            if (users.Data != null)
            {
                ViewBag.id = pas;
                return View();
            }
            return View("Error");
        }
        [HttpPost]
        public JsonResult NewCreatePass(string id, string pasword)
        {
            var users = _userService.GetById(id);

            if (users.Data != null)
            {
                users.Data.ConfirmId = Guid.NewGuid().ToString();
                users.Data.Password = HashingHelper.MD5Olustur(pasword);
               _userService.Update(users.Data);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}