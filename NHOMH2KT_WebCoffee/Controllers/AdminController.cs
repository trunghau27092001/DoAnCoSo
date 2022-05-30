
using NHOMH2KT_WebCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace NHOMH2KT_WebCoffee.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CoffeeDataDataContext data = new CoffeeDataDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product(int ?page)
        {
            if (page == null) page = 1;
            var all_sanpham = (from s in data.Products select s).OrderBy(m => m.idProd);
            int pageSize = 7;
            int pageNumber = page ?? 1;
            return View(all_sanpham.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mật khẩu trống";
            }
            else
            {
                Account admin = data.Accounts.SingleOrDefault(n => n.username == tendn && n.password == matkhau);
                if (admin != null && admin.idType == "ad")
                {
                    ViewBag.Thongbao = "Bạn đã đăng nhập thành công";
                    Session["Account"] = admin;
                    return RedirectToAction("Manage", "Admin");
                }
                else if (admin != null && admin.idType == "cus")
                {
                    ViewBag.Thongbao = "Bạn đã đăng nhập thành công";
                    Session["Account"] = admin;
                    return RedirectToAction("IndexUs", "User");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Coffee");
        }

        public ActionResult Detail(string id)
        {
            var D_sp = data.Products.Where(m => m.idProd == id).First();
            return View(D_sp);
        }
        public ActionResult Delete(string id)
        {
            var D_sp = data.Products.First(m => m.idProd == id);
            return View(D_sp);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_sp = data.Products.Where(m => m.idProd == id).First();
            data.Products.DeleteOnSubmit(D_sp);
            data.SubmitChanges();
            return RedirectToAction("Product", "Admin");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Product sp)
        {
            var Sp_id = collection["idProd"];
            var Sp_Name = collection["nameProd"];
            var Sp_images = collection["images"];
            var Sp_price = Convert.ToInt32(collection["price"]);
            var Sp_Decription =collection["decription"];
            var Sp_idcate = collection["idCate"];
            if (string.IsNullOrEmpty(Sp_Name))
            {
                ViewData["Error"] = "Không được để trống !";
            }
            else
            {
                sp.nameProd = Sp_Name.ToString();
                sp.images = Sp_images.ToString();
                sp.price = Sp_price;
                sp.decription = Sp_Decription.ToString();
                sp.idCate = Sp_idcate.ToString();
                sp.idProd = Sp_id.ToString();
                data.Products.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("Product", "Admin");
            }
            return this.Create();
        }
        public ActionResult Edit(string id)
        {
            var Sp_product = data.Products.First(m => m.idProd == id); return View(Sp_product);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var Sp_prod = data.Products.First(m => m.idProd == id);
            var Sp_name = collection["nameProd"];
            var Sp_images = collection["images"];
            var Sp_price = Convert.ToInt32(collection["price"]);
            var Sp_Decription =collection["decription"];
            var Sp_idcate = collection["idCate"];
            Sp_prod.idProd = id;
            if (string.IsNullOrEmpty(Sp_name))
            {
                ViewData["Error"] = "Không được để trống !";
            }
            else
            {
                Sp_prod.nameProd = Sp_name;
                Sp_prod.images = Sp_images;
                Sp_prod.price = Sp_price;
                Sp_prod.decription = Sp_Decription;
                Sp_prod.idCate = Sp_idcate;
                UpdateModel(Sp_prod);
                data.SubmitChanges();
                return RedirectToAction("Product", "Admin");
            }
            return this.Edit(id);
        }
        //-------------------Danh mục----------------------
        public ActionResult Category()
        {
            var Category = from s in data.Categories select s;
            return PartialView(Category);
        }
        public ActionResult DeleteCategory(string id)
        {
            var D_cate = data.Categories.First(m => m.idCate == id);
            return View(D_cate);
        }
        [HttpPost]
        public ActionResult DeleteCategory(string id, FormCollection collection)
        {
            var D_cate = data.Categories.Where(m => m.idCate == id).First();
            data.Categories.DeleteOnSubmit(D_cate);
            data.SubmitChanges();
            return RedirectToAction("Category", "Admin");
        }
        public ActionResult Manage( int ?page)
        {
            if(Session["Account"] == null)
            {
                return RedirectToAction("Index", "Coffee");
            }   
            else
            {
                if (page == null) page = 1;
                var all_sanpham = (from s in data.Products select s).OrderBy(m => m.idProd);
                int pageSize = 7;
                int pageNumber = page ?? 1;
                return View(all_sanpham.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(FormCollection collection, Category sp)
        {
            var Sp_id = collection["idCate"];
            var Sp_Name = collection["nameCate"];
            if (string.IsNullOrEmpty(Sp_Name))
            {
                ViewData["Error"] = "Không được để trống !";
            }
            else
            {
                sp.idCate = Sp_id.ToString();
                sp.nameCate = Sp_Name.ToString();
                data.Categories.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("CreateCategory", "Admin");
            }
            return this.Create();
        }

        public ActionResult ResetPass()
        {
            Account a = (Account)Session["Account"];
            var r_reset= data.Accounts.Where(r=>r.username==a.username).First(); 
            return View(r_reset);
        }
        [HttpPost]
        public ActionResult ResetPass( FormCollection collection)
        {
            Account a = (Account)Session["Account"];
            var r_re = data.Accounts.Where(r => r.username == a.username).First();
            var r_pass = collection["password"];
            var oldpass = collection["oldpass"];
            var re_pass = collection["re_pass"];
            if (string.IsNullOrEmpty(r_pass)&&string.IsNullOrEmpty(oldpass)&&string.IsNullOrEmpty(re_pass))
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
            else if (r_re.password==r_pass)
            {
                ViewData["Error"] = "Trùng mật khẩu cũ !";
            }
            else
            {
                r_re.password=r_pass;
                UpdateModel(r_re);
                data.SubmitChanges();
                return RedirectToAction("Login", "Admin");
            }
            return this.ResetPass();
        }
        //Dang ky tai khoan
        [HttpGet]
        public int checkThongtin(FormCollection collection, Account acc)
        {
            var tendangnhap = collection["username"];
            var matkhau = collection["password"];
            var MatkhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["email"];
            var dienthoai = collection["phoneNum"];
            if (tendangnhap.Length < 6)
            {
                ViewData["LoiUserName"] = "Tên đăng nhập phải trên 5 kí tự";
            }
            else if (matkhau.Length < 6)
            {
                ViewData["LoiPass"] = "Mật khẩu phải trên 5 kí tự";
            }
            else if (email.Length < 15 || !email.Contains("@gmail.com"))
            {
                ViewData["LoiPass"] = "Mật khẩu dưới 5 kí tự hoặc sai định dạng";
            }
            else if (dienthoai.Length < 10)
            {
                ViewData["LoiPhone"] = "Số điện thoại chưa đúng";
                
            }
            return 0;
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, Account acc)
        {
            var hoten = collection["nameCus"];
            var tendangnhap = collection["username"];
            var matkhau = collection["password"];
            var MatkhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["email"];
            var diachi = collection["addressCus"];
            var dienthoai = collection["phoneNum"];
            var loaitk = collection["idType"];
            if (tendangnhap.Length < 4)
            {
                ViewData["LoiUserName"] = "Tên đăng nhập phải trên 4 kí tự";
            }
            else if (matkhau.Length < 4)
            {
                ViewData["LoiPass"] = "Mật khẩu phải trên 4 kí tự";
            }
            else if (email.Length < 15 || !email.Contains("@gmail.com"))
            {
                ViewData["LoiEmail"] = "Email sai định dạng";
            }
            else if (dienthoai.Length < 10)
            {
                ViewData["LoiPhone"] = "Số điện thoại chưa đúng";

            }
            else if (String.IsNullOrEmpty(MatkhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!matkhau.Equals(MatkhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    acc.nameCus = hoten;
                    acc.username = tendangnhap;
                    acc.password = matkhau;
                    acc.email = email;
                    acc.addressCus = diachi;
                    acc.phoneNum = dienthoai;
                    acc.idType = "cus";
                    data.Accounts.InsertOnSubmit(acc);
                    data.SubmitChanges();
                    return RedirectToAction("Login", "Admin");
                }
            }
            return this.DangKy();
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/imgProd/" + file.FileName));
            return "~/Content/imgProd/" + file.FileName;
        }
        public ActionResult LoadOrderdata(int? page)
        {
            if (page == null) page = 1;
            var all_order = (from s in data.tOrders select s).OrderBy(m => m.idOrder);
            int pageSize = 7;
            int pageNumber = page ?? 1;
            return View(all_order.ToPagedList(pageNumber, pageSize));

        }
    }
}