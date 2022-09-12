using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;


namespace Proj.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        ProjEntities db = new ProjEntities();
        // GET: UserPanel/Account
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePass)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.user_name == User.Identity.Name);
                if (user.password == FormsAuthentication.HashPasswordForStoringInConfigFile(changePass.OldPass, "MD5")) {
                    user.password = FormsAuthentication.HashPasswordForStoringInConfigFile(changePass.Password, "MD5");
                    db.SaveChanges();
                    ViewBag.success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPass", "رمزعبور فعلی نادرست است!");
                }

            }
            return View();
        }


    }
}