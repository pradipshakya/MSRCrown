using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSRCrown.Models;
using System.Web.Security;

namespace MSRCrown.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership memberModel)
        {
            using (var context = new MSRCrownEntities())
            {
                bool isValid = context.Members.Any(x => x.UserName == memberModel.UserName && x.Password == memberModel.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(memberModel.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid UserName and Password");
                return View();
            }            
        }

        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Members model)
        {
            using (var context = new MSRCrownEntities()) 
            {
                context.Members.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}