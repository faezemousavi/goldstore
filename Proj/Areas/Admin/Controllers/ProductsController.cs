using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;

namespace Proj.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ProjEntities db = new ProjEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.Products_group.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_id,Product_title,Product_ShortDescription,Product_text,Product_price,Product_image")] Product products, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedGroups == null)
                {
                    ViewBag.ErrorSelectedGroup = true;
                    ViewBag.Groups = db.Products_group.ToList();
                    return View(products);
                }
                products.Product_image = "images.jpg";
                if (imageProduct != null && imageProduct.IsImage())
                {
                    products.Product_image = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.Product_image));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.Product_image),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.Product_image));


                }
                products.Product_CreateDate = DateTime.Now;
                db.Products.Add(products);

                foreach (int selectedGroup in selectedGroups)
                {
                    db.PGRells.Add(new PGRell()
                    {
                        Product_id = products.Product_id,
                        Group_id = selectedGroup
                    });
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Tags.Add(new Tag()
                        {
                            Product_id = products.Product_id,
                            Tag1 = t.Trim()
                        });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.Products_group.ToList();
            return View(products);
        }

        public ActionResult Gallery(int id)
        {
            ViewBag.Galleries = db.Galleries.Where(p => p.product_id == id).ToList();
            return View(new Gallery()
            {
                product_id = id
            });
        }

        [HttpPost]
        public ActionResult Gallery(Gallery galleries, HttpPostedFileBase imgUp)
        {

            if (imgUp != null && imgUp.IsImage())
            {
                galleries.Gallery_image = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                imgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + galleries.Gallery_image));
                ImageResizer img = new ImageResizer();
                img.Resize(Server.MapPath("/Images/ProductImages/" + galleries.Gallery_image),
                    Server.MapPath("/Images/ProductImages/Thumb/" + galleries.Gallery_image));
                db.Galleries.Add(galleries);
                db.SaveChanges();
            }


            return RedirectToAction("Gallery", new { id = galleries.product_id });
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = db.Galleries.Find(id);

            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + gallery.Gallery_image));
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/" + gallery.Gallery_image));

            db.Galleries.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Gallery", new { id = gallery.product_id });
        }

        public ActionResult ProductFeaturs(int id)
        {

            ViewBag.Features = db.PFRells.Where(f => f.Product_id == id).ToList();
            ViewBag.Feature_id = new SelectList(db.Features, "Feature_id", "Feature_title");
            return View(new PFRell()
            {
                Product_id = id
            });
        }

        [HttpPost]
        public ActionResult ProductFeaturs(PFRell feature)
        {
            if (ModelState.IsValid)
            {
                db.PFRells.Add(feature);
                db.SaveChanges();
            }

            return RedirectToAction("ProductFeaturs", new { id = feature.Product_id });
        }
        
        public void DeleteFeature(int id)
        {
            var feature = db.PFRells.Find(id);
            db.PFRells.Remove(feature);
            db.SaveChanges();
        }


        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.SelectedGroups = products.PGRells.ToList();
            ViewBag.Groups = db.Products_group.ToList();
            ViewBag.Tags = string.Join(",", products.Tags.Select(t => t.Tag1).ToList());
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_id,Product_title,Product_ShortDescription,Product_text,Product_price,Product_image,Product_CreateDate")] Product products, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (imageProduct != null && imageProduct.IsImage())
                {
                    if (products.Product_image != "images.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + products.Product_image));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/" + products.Product_image));
                    }

                    products.Product_image = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.Product_image));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.Product_image),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.Product_image));
                }
                db.Entry(products).State = EntityState.Modified;


                db.Tags.Where(t => t.Product_id == products.Product_id).ToList().ForEach(t => db.Tags.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Tags.Add(new Tag()
                        {
                            Product_id = products.Product_id,
                            Tag1 = t.Trim()
                        });
                    }
                }


                db.PGRells.Where(g => g.Product_id == products.Product_id).ToList().ForEach(g => db.PGRells.Remove(g));
                foreach (int selectedGroup in selectedGroups)
                {
                    db.PGRells.Add(new PGRell()
                    {
                        Product_id = products.Product_id,
                        Group_id = selectedGroup
                    });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.Groups = db.Products_group.ToList();
            ViewBag.Tags = tags;
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var features = db.PFRells.Where(r => r.Product_id == id).ToList();
            foreach (var r in features)
            {
                db.PFRells.Remove(r);
            }
            var groups = db.PGRells.Where(r => r.Product_id == id).ToList();
            foreach (var r in groups)
            {
                db.PGRells.Remove(r);
            }
            var tags = db.Tags.Where(r => r.Product_id == id).ToList();
            foreach (var r in tags)
            {
                db.Tags.Remove(r);
            }
            var gal = db.Galleries.Where(r => r.product_id == id).ToList();
            foreach (var r in gal)
            {
                db.Galleries.Remove(r);
            }
            var ord = db.OrderDetails.Where(r => r.Product_id == id).ToList();
            foreach (var r in ord)
            {
                db.OrderDetails.Remove(r);
            }
            var cmt = db.Comments.Where(r => r.Product_id == id).ToList();
            foreach (var r in cmt)
            {
                db.Comments.Remove(r);
            }



            Product products = db.Products.Find(id);
            db.Products.Remove(products);
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
