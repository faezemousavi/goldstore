using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;

namespace Proj.Controllers
{
    public class AccountController : Controller
    {
        ProjEntities db = new ProjEntities();

        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if(!db.Users.Any(u => u.email == register.Email.Trim().ToLower() ))
                {
                    if (!db.Users.Any(u => u.user_name == register.Username.Trim().ToLower()))
                    {
                        User user = new User()
                        {
                            user_name = register.Username.Trim().ToLower(),
                            email = register.Email.Trim().ToLower(),
                            password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                            ActiveCode = Guid.NewGuid().ToString(),
                            isActive = false,
                            RegisterDate = DateTime.Now,
                            Role_id = 1

                        };
                        db.Users.Add(user);
                        db.SaveChanges();


                        //sending activation email
                        string body = PartialToStringClass.RenderPartialView("EmailManager", "ActivationEmail", user);
                        SendEmail.Send(user.email, " برلیان - ایمیل فعالسازی", body);

                        return View("SuccessRegister", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "نام کاربری تکراری است");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل تکراری است");
                    
                }
            }
            return View(register);
        }

        
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login , string ReturnURL = "/") {

            if (ModelState.IsValid)
            {
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(login.password, "MD5");
                var user = db.Users.SingleOrDefault(u => u.email.Trim().ToLower() == login.Email.Trim().ToLower() && u.password == password);
                if(user != null)
                {
                    if (user.isActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.user_name, login.remembreMe);
                        return Redirect(ReturnURL);
                    }
                    else
                    {
                        ModelState.AddModelError("password", "حساب کاربری شما فعال نیست!");
                    }
                }
                else
                {
                    ModelState.AddModelError("password", "کاربری با این اطلاعات پیدا نشد!");
                }
            }

            //ViewBag.type = db.Users.Where(u => u.email == login.Email.ToLower()).Select(u => u.email);
            return View(login);
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }


        public ActionResult ActiveUser(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
            if(user == null){
                return HttpNotFound();
            }
            user.isActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.SaveChanges();

            ViewBag.username = user.user_name;
            return View();
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {

            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.email == forgot.Email);
                if(user != null)
                {
                    if (user.isActive)
                    {
                        string body = PartialToStringClass.RenderPartialView("EmailManager", "PasswordRecoveryEmail", user);
                        SendEmail.Send(user.email, "برلیان - بازیابی کلمه عبور", body);

                        return View("SuccessPasswordRecovery", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری یافت نشد!");
                }
            }
            return View();
        }



        public ActionResult PasswordRecovery(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult PasswordRecovery(string id , RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
                if(user != null)
                {
                    user.password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                    user.ActiveCode = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    return Redirect("/Login?recovery=true");
                }
                else
                {
                    return HttpNotFound();
                }
               
            }
            return View();
        }


    }
}