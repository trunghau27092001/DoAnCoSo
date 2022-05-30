using NHOMH2KT_WebCoffee.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHOMH2KT_WebCoffee.Controllers
{
    public class CoffeeController : Controller
    {
        // GET: Coffee
        CoffeeDataDataContext data = new CoffeeDataDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Menu(int? page)
        {
            if (page == null) page = 1;
            var all_sanpham = (from s in data.Products select s).OrderBy(m => m.idProd);
            int pageSize = 7;
            int pageNumber = page ?? 1;
            return View(all_sanpham.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Reservation()
        {
            return View();
        }
        public ActionResult Testimonial()
        {
            return View();
        }
    }
}