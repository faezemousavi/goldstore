using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Proj.Controllers
{
    public class SearchController : Controller
    {
        ProjEntities db = new ProjEntities();
        public ActionResult Index(string q)
        {
            List<Product> list = new List<Product>();

            list.AddRange(db.Tags.Where(t => t.Tag1 == q).Select(t => t.Product).ToList());
            list.AddRange(db.Products.Where(p => p.Product_title.Contains(q) || p.Product_ShortDescription.Contains(q) || p.Product_text.Contains(q)).ToList());

            ViewBag.search = q;
            return View(list.Distinct());
        }
    }
}