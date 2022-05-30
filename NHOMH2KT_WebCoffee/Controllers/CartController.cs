
using NHOMH2KT_WebCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHOMH2KT_WebCoffee.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        CoffeeDataDataContext data = new CoffeeDataDataContext();
        //Lấy giỏ hàng
        public List<Cart> Laygiohang()
        {
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Cart>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        // Them gio hang
        public ActionResult ThemGioHang(string id, string strURL)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.Find(n => n.idProd == id);
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin"); 
            }
            else if (sanpham == null)
            {
                sanpham = new Cart(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else 
            {
                sanpham.isoluong++;
                return Redirect(strURL);
            }

        }
        // tong so luong san pham
        private int TongSoLuong()
        {
            int tsl = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.isoluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private int? TongTien()
        {
            int? tt = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        // gio hang
        public ActionResult GioHang()
        {
            List<Cart> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult XoaGioHang(string id)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.idProd == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.idProd == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(string id, FormCollection collection)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.idProd == id);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(collection["txtSolg"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<Cart> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        // Dat hang
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            if (Session["Account"] == null)
            {
                return RedirectToAction("Index", "Coffee");
            }
            List<Cart> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            tOrder dh = new tOrder();
            Account kh = (Account)Session["Account"];
            Product pr = new Product();
            List<Cart> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            dh.idCus = kh.idCus;
            dh.dateOrder = DateTime.Now;
            dh.dateDeli = DateTime.Parse(ngaygiao);
            dh.status = false;
            dh.payment = false;

            data.tOrders.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                OrderData ctdh = new OrderData();
                List<Cart> lstGiohang = Laygiohang();
                ctdh.idOrder = dh.idOrder;
                ctdh.idProd = item.idProd;
                ctdh.amount = item.isoluong;
                pr = data.Products.Single(n => n.idProd == item.idProd);
                data.SubmitChanges();
                data.OrderDatas.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacnhanDonhang", "Cart");
        }
        public ActionResult OrderDetail()
        {
            return View();
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

    }
}