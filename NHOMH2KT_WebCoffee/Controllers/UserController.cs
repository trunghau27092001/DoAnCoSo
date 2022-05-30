using NHOMH2KT_WebCoffee.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHOMH2KT_WebCoffee.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        CoffeeDataDataContext data = new CoffeeDataDataContext(); 
        public ActionResult IndexUs()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Coffee");
            }
            else return View();
        }
        public ActionResult AboutUs()
        {
        
            return View();
        }

        public ActionResult MenuUs(int? page)
        {
            if (page == null) page = 1;
            var all_sanpham = (from s in data.Products select s).OrderBy(m => m.idProd);
            int pageSize = 7;
            int pageNumber = page ?? 1;
            return View(all_sanpham.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ServiceUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult ReservationUs()
        {
            return View();
        }
        public ActionResult TestimonialUs()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Thongtin(FormCollection collection)
        {
            Account acc = (Account)Session["Account"];
            return View(acc);
        }
        public ActionResult ResetPassUs()
        {
            Account a = (Account)Session["Account"];
            var r_reset = data.Accounts.Where(r => r.username == a.username).First();
            return View(r_reset);
        }
        [HttpPost]
        public ActionResult ResetPassUs(FormCollection collection)
        {
            Account a = (Account)Session["Account"];
            var r_re = data.Accounts.Where(r => r.username == a.username).First();
            var r_pass = collection["password"];
            var oldpass = collection["oldpass"];
            var re_pass = collection["re_pass"];
            if (string.IsNullOrEmpty(r_pass) && string.IsNullOrEmpty(oldpass) && string.IsNullOrEmpty(re_pass))
            {
                ViewData["Error"] = "Không được để trống !";
            }
            else if (r_re.password != oldpass)
            {
                ViewData["Error"] = "Mật khẩu cũ không đúng !";
            }
            else if (r_pass != re_pass)
            {
                ViewData["Error"] = "Mật khẩu phải trùng !";
            }
            else if (r_re.password == r_pass)
            {
                ViewData["Error"] = "Trùng mật khẩu cũ !";
            }
            else
            {
                r_re.password = r_pass;
                UpdateModel(r_re);
                data.SubmitChanges();
                return RedirectToAction("Login", "Admin");
            }
            return this.ResetPassUs();
        }

    }
}