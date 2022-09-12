using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proj.Controllers
{
    public class CompareController : Controller
    {
        DataLayer.ProjEntities db = new DataLayer.ProjEntities();
        // GET: Compare
        public ActionResult Index()
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
            }
            List<DataLayer.Feature> features = new List<DataLayer.Feature>();
            List<DataLayer.PFRell> productFeatures = new List<DataLayer.PFRell>();
            foreach (var item in list)
            {
                features.AddRange(db.PFRells.Where(p => p.Product_id == item.ProductID).Select(f => f.Feature).ToList());
                productFeatures.AddRange(db.PFRells.Where(p => p.Product_id == item.ProductID).ToList());
            }
            ViewBag.features = features.Distinct().ToList();
            ViewBag.productFeatures = productFeatures;
            return View(list);
        }

        public ActionResult AddToCompare(int id)
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
            }

            if (!list.Any(p => p.ProductID == id))
            {
                var product = db.Products.Where(p => p.Product_id == id).Select(p => new { p.Product_title, p.Product_image }).Single();
                list.Add(new DataLayer.ViewModels.CompareItem()
                {
                    ProductID = id,
                    Title = product.Product_title,
                    ImageName = product.Product_image
                });
            }
            Session["Compare"] = list;

            return PartialView("ListCompare", list);
        }

        public ActionResult ListCompare()
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
            }
            return PartialView(list);
        }

        public ActionResult DeleteFromCompare(int id)
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
                int index = list.FindIndex(p => p.ProductID == id);
                list.RemoveAt(index);
                Session["Compare"] = list;
            }
            return PartialView("ListCompare", list);

        }
    }
}