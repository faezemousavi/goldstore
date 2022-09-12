using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DataLayer;
using DataLayer.ViewModels;

namespace Proj.Controllers
{
    public class ShopController : ApiController
    {

        // GET: api/Shop
        public int Get()
        {
            List<ShopCardItem> list = new List<ShopCardItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCard"] != null)
            {
                list = sessions["ShopCard"] as List<ShopCardItem>;
            }

            return list.Sum(l => l.Count);
        }

        // GET: api/Shop/5
        public int Get(int id)
        {
            List<ShopCardItem> list = new List<ShopCardItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCard"] != null)
            {
                list = sessions["ShopCard"] as List<ShopCardItem>;
            }
            if (list.Any(p => p.ProductID == id))
            {
                int index = list.FindIndex(p => p.ProductID == id);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new ShopCardItem()
                {
                    ProductID = id,
                    Count = 1
                });
            }

            sessions["ShopCard"] = list;
            return Get();
        }

    }
}
