using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;
using DataLayer;

namespace Proj.Controllers
{
    public class ProductController : Controller
    {
        ProjEntities db = new ProjEntities();
        public ActionResult ShowGroups()
        {
            return PartialView(db.Products_group.ToList());
        }

        public ActionResult LastProduct()
        {
            return PartialView(db.Products.OrderByDescending(p => p.Product_CreateDate).Take(12));
        }

        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int id)
        {
            var product = db.Products.Find(id);
            ViewBag.ProductFeatures = product.PFRells.DistinctBy(f => f.Feature_id).Select(f => new ShowProductFeatureViewModel()
            {
                FeatureTitle = f.Feature.Feature_title,
                Values = db.PFRells.Where(fe => fe.Feature_id == f.Feature_id).Select(fe => fe.value).ToList()
            }).ToList();
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(db.Comments.Where(c => c.Product_id == id));
        }

        public ActionResult CreateComment(int id)
        {
            return PartialView(new Comment()
            {
                Product_id = id
            });
        }

        [HttpPost]
        public ActionResult CreateComment(Comment productComment)
        {
            if (ModelState.IsValid)
            {
                productComment.Create_Date = DateTime.Now;
                db.Comments.Add(productComment);
                db.SaveChanges();

                return PartialView("ShowComments", db.Comments.Where(c => c.Product_id == productComment.Product_id));

            }
            return PartialView(productComment);
        }

        [Route("Archive")]
        public ActionResult ArchiveProduct(int pageId = 1, string title = "", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.Groups = db.Products_group.ToList();
            ViewBag.productTitle = title;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.pageId = pageId;
            ViewBag.selectGroup = selectedGroups;
            List<Product> list = new List<Product>();
            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int group in selectedGroups)
                {
                    list.AddRange(db.PGRells.Where(g => g.Group_id == group).Select(g => g.Product).ToList());
                }
                list = list.Distinct().ToList();
            }
            else
            {
                list.AddRange(db.Products.ToList());
            }


            if (title != "")
            {
                list = list.Where(p => p.Product_title.Contains(title)).ToList();
            }
            if (minPrice > 0)
            {
                list = list.Where(p => p.Product_price >= minPrice).ToList();
            }
            if (maxPrice > 0)
            {
                list = list.Where(p => p.Product_price <= maxPrice).ToList();
            }

            //Pagging
            int take = 12;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (list.Count() / take)+1;
            return View(list.OrderByDescending(p => p.Product_CreateDate).Skip(skip).Take(take).ToList());
        }
    }
}