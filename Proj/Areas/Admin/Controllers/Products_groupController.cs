using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Proj.Areas.Admin.Controllers
{
    public class Products_groupController : Controller
    {
        private ProjEntities db = new ProjEntities();

        // GET: Admin/Products_group
        public ActionResult Index()
        {
            var products_group = db.Products_group.Where(g => g.Parent_id == null);
            return View(products_group.ToList());
        }

        // GET: Admin/Products_group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_group products_group = db.Products_group.Find(id);
            if (products_group == null)
            {
                return HttpNotFound();
            }
            return View(products_group);
        }

        // GET: Admin/Products_group/Create
        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            ViewBag.parent = null;
            if (id != null)
            {
                var group = db.Products_group.SingleOrDefault(g => g.Group_id == id);
                ViewBag.parent = group.Group_title;
            }

            //ViewBag.Parent_id = new SelectList(db.Products_group, "Group_id", "Group_title");
            return View();
        }

        // POST: Admin/Products_group/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Group_id,Group_title,Parent_id")] Products_group products_group)
        {
            if (ModelState.IsValid)
            {
                db.Products_group.Add(products_group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Parent_id = new SelectList(db.Products_group, "Group_id", "Group_title", products_group.Parent_id);
            return View(products_group);
        }

        // GET: Admin/Products_group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_group products_group = db.Products_group.Find(id);
            if (products_group == null)
            {
                return HttpNotFound();
            }
            return View(products_group);
        }

        // POST: Admin/Products_group/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Group_id,Group_title,Parent_id")] Products_group products_group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products_group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Parent_id = new SelectList(db.Products_group, "Group_id", "Group_title", products_group.Parent_id);
            return View(products_group);
        }

        // GET: Admin/Products_group/Delete/5
        public void Delete(int? id)
        {
            var group = db.Products_group.Find(id);
            if (group.Products_group1.Any())
            {
                foreach (var subGroup in db.Products_group.Where(g => g.Parent_id == id))
                {
                    //db.Entry(subGroup).State = EntityState.Deleted;
                    db.Products_group.Remove(subGroup);

                }
            }
            db.Products_group.Remove(group);
            db.SaveChanges();

        }

        // POST: Admin/Products_group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products_group product_Groups = db.Products_group.Find(id);
            db.Products_group.Remove(product_Groups);
            db.SaveChanges();
            return View("Index");
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

