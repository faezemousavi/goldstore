using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proj.Controllers
{
    public class EmailManagerController : Controller
    {
        // GET: EmailManager
        public ActionResult ActivationEmail()
        {
            return PartialView();
        }
        public ActionResult PasswordRecoveryEmail()
        {
            return PartialView();
        }
    }
}