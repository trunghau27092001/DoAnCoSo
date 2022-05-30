using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NHOMH2KT_WebCoffee.Models
{
    public class Cart
    {
        CoffeeDataDataContext data = new CoffeeDataDataContext();
        public string idProd { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string nameProd { get; set; }
        [Display(Name = "Ảnh sản phẩm")]
        public string images { get; set; }
        [Display(Name = "Giá bán")]
        public int? price { get; set; }
        [Display(Name = "Số lượng")]
        public int isoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public int? dThanhtien
        {
            get { return isoluong * price; }
        }
        public Cart(string id)
        {
            this.idProd = id;
            Product product = data.Products.Single(n => n.idProd == id);
            nameProd = product.nameProd;
            images = product.images;
            price = product.price;
            isoluong = 1;
        }
    }
}