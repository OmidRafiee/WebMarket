using Shop.Models.EntityModels;
using Shop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        readonly UserRepository _blUser = new UserRepository(new DatabaseContext());

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, bool rememberMe)
        {
            if (_blUser.Exist(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, rememberMe);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "نام کاربری یا پسورد را اشتباه وارد کردید";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public string FullName()
        {
            string username = User.Identity.Name;
            string name = _blUser.Where(p => p.UserName == username).FirstOrDefault().Name;
            string family = _blUser.Where(p => p.UserName == username).FirstOrDefault().Family;
            var fullName = name + " " + family;
            return fullName;
        }


        
    }
}