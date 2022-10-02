using LibraryMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMangement.Controllers
{
    public class UserController : Controller
    {
        Library_ManagementEntities userDb = new Library_ManagementEntities();

        // Returns user login view, here admin can login.
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Checks user credentials, redirecting to admin section (index, tblBooks). 
        [HttpPost]
        public ActionResult Login(Usertbl user)
        {
            var adm = userDb.Usertbls.SingleOrDefault(a => a.UserEmail == user.UserEmail && a.UserPass == user.UserPass);
            if (adm != null)
            {
                Session["userId"] = adm.UserId;
                Session["userName"] = adm.UserName;
                return RedirectToAction("Index", "Transaction", new { userId = adm.UserId, userName = adm.UserName });
            }
            else if (user.UserEmail == null && user.UserPass == null)
            {
                return View(adm);
            }
            ViewBag.Message = "User name and password are not matching";
            return View();
        }

        /* User credentials validation two
        public ActionResult Validate(tblUser user)
        {
            var adm = db.tblUsers.SingleOrDefault(a => a.UserEmail == user.UserEmail && a.UserPass == user.UserPass);
            if (adm != null)
            {
                Session["userId"] = adm.UserId;
                Session["userName"] = adm.UserName;
                ViewBag.userId= adm.UserId;
                ViewBag.userId = adm.UserId;
                return RedirectToAction("Index", "Borrow", new { userId = adm.UserId, userName = adm.UserName });
            }
               else if (user.UserEmail == null && user.UserPass == null)
            {
                return View("Login");
            }
            ViewBag.Message = "User name and password are not matching";
            return View("Login");
        }
        */

        // User logout, redirect to main. 
        public ActionResult Logout()
        {
            Session.Remove("userId");
            Session.Remove("userName");
            return RedirectToAction("Index", "Home");
        }
    }
}