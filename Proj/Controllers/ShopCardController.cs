using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Mvc;

namespace Proj.Controllers
{
    public class ShopCardController : Controller
    {
        ProjEntities db = new ProjEntities();
        // GET: ShopCart
        public ActionResult ShowCard()
        {
            List<ShopCardItemViewModel> list = new List<ShopCardItemViewModel>();

            if (Session["ShopCard"] != null)
            {
                List<ShopCardItem> listShop = (List<ShopCardItem>)Session["ShopCard"];

                foreach (var item in listShop)
                {
                    var product = db.Products.Where(p => p.Product_id == item.ProductID).Select(p => new
                    {
                        p.Product_image,
                        p.Product_title
                    }).Single();
                    list.Add(new ShopCardItemViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Title = product.Product_title,
                        ImageName = product.Product_image

                    });
                }
            }

            return PartialView(list);
        }

        public ActionResult Index()
        {
            return View();
        }

        List<ShowOrderViewModel> getListOrder()
        {
            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();

            if (Session["ShopCard"] != null)
            {
                List<ShopCardItem> listShop = Session["ShopCard"] as List<ShopCardItem>;

                foreach (var item in listShop)
                {
                    var product = db.Products.Where(p => p.Product_id == item.ProductID).Select(p => new
                    {
                        p.Product_image,
                        p.Product_title,
                        p.Product_price
                    }).Single();
                    list.Add(new DataLayer.ViewModels.ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = product.Product_price,
                        ImageName = product.Product_image,
                        Title = product.Product_title,
                        Sum = item.Count * product.Product_price
                    });
                }
            }
            return list;
        }

        public ActionResult Order()
        {
            return PartialView(getListOrder());
        }

        public ActionResult CommandOrder(int id, int count)
        {
            List<ShopCardItem> listShop = Session["ShopCard"] as List<ShopCardItem>;
            int index = listShop.FindIndex(p => p.ProductID == id);
            if (count == 0)
            {
                listShop.RemoveAt(index);
            }
            else
            {
                listShop[index].Count = count;
            }
            Session["ShopCard"] = listShop;

            return PartialView("Order", getListOrder());
        }

        [Authorize]
        public ActionResult Payment()
        {
            int userId = db.Users.Single(u => u.user_name == User.Identity.Name).User_id;
            Order order = new Order()
            {
                User_id = userId,
                Datetime = DateTime.Now,
                isFinally = false,
            };
            db.Orders.Add(order);

            var listdetails = getListOrder();

            foreach (var item in listdetails)
            {
                db.OrderDetails.Add(new OrderDetail()
                {
                    Count = item.Count,
                    OrderID = order.Order_id,   
                    Price = item.Price,
                    Product_id = item.ProductID,
                });
            }
            db.SaveChanges();

            //todo : online payment

            return null;
        }
    }
}