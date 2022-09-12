using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proj.Controllers
{
    public class HomeController : Controller
    {
        DataLayer.ProjEntities db = new DataLayer.ProjEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            return PartialView(db.Sliders.Where(s => s.isActive ));
        }
    }
}