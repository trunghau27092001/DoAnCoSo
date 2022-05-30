using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NHOMH2KT_WebCoffee
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Coffee", action = "Index" }
            );

            routes.MapRoute(
                name: "About",
                url: "About",
                defaults: new { controller = "Coffee", action = "About" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "Contact",
                defaults: new { controller = "Coffee", action = "Contact" }
            );

            routes.MapRoute(
                name: "Menu",
                url: "Menu",
                defaults: new { controller = "Coffee", action = "Menu" }
            );

            routes.MapRoute(
                name: "Reservation",
                url: "Reservation",
                defaults: new { controller = "Coffee", action = "Reservation" }
            );

            routes.MapRoute(
                name: "Service",
                url: "Service",
                defaults: new { controller = "Coffee", action = "Service" }
            );

            routes.MapRoute(
                name: "Testimonial",
                url: "Testimonial",
                defaults: new { controller = "Coffee", action = "Testimonial" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                name: "HomeUs",
                url: "HomeUs",
                defaults: new { controller = "User", action = "IndexUs" }
            );
            routes.MapRoute(
                name: "AboutUs",
                url: "AboutUs",
                defaults: new { controller = "User", action = "AboutUs" }
            );

            routes.MapRoute(
                name: "ContactUs",
                url: "ContactUs",
                defaults: new { controller = "User", action = "ContactUs" }
            );

            routes.MapRoute(
                name: "MenuUs",
                url: "MenuUs",
                defaults: new { controller = "User", action = "MenuUs" }
            );

            routes.MapRoute(
                name: "ReservationUs",
                url: "ReservationUs",
                defaults: new { controller = "User", action = "ReservationUs" }
            );

            routes.MapRoute(
                name: "ServiceUs",
                url: "ServiceUs",
                defaults: new { controller = "User", action = "ServiceUs" }
            );

            routes.MapRoute(
                name: "TestimonialUs",
                url: "TestimonialUs",
                defaults: new { controller = "User", action = "TestimonialUs" }
            );
            routes.MapRoute(
                name: "Product",
                url: "Product",
                defaults: new { controller = "Admin", action = "Product" }
             );
            routes.MapRoute(
               name: "Detail",
               url: "Detail",
               defaults: new { controller = "Admin", action = "Detail" }
             );

            routes.MapRoute(
             name: "Delete",
             url: "Delete",
             defaults: new { controller = "Admin", action = "Delete" }
             );

            routes.MapRoute(
             name: "Create",
             url: "Create",
             defaults: new { controller = "Admin", action = "Create" }
             );

            routes.MapRoute(
             name: "Edit",
             url: "Edit",
             defaults: new { controller = "Admin", action = "Edit" }
             );

            routes.MapRoute(
             name: "Category",
             url: "Category",
             defaults: new { controller = "Admin", action = "Category" }
             );

            routes.MapRoute(
             name: "DeleteCategory",
             url: "DeleteCategory",
             defaults: new { controller = "Admin", action = "DeleteCategory" }
             );

            routes.MapRoute(
             name: "Manage",
             url: "Manage",
             defaults: new { controller = "Admin", action = "Manage" }
             );

            routes.MapRoute(
             name: "LogOut",
             url: "LogOut",
             defaults: new { controller = "Admin", action = "LogOut" }
             );

            routes.MapRoute(
            name: "Thongtin",
            url: "Thongtin",
            defaults: new { controller = "User", action = "Thongtin" }
            );
            routes.MapRoute(
            name: "ResetPass",
            url: "ResetPass",
            defaults: new { controller = "Admin", action = "ResetPass" }
            );
            routes.MapRoute(
            name: "GioHang",
            url: "GioHang",
            defaults: new { controller = "Cart", action = "GioHang" }
            );
            routes.MapRoute(
            name: "DangKy",
            url: "DangKy",
            defaults: new { controller = "Admin", action = "DangKy" }
            );
            routes.MapRoute(
            name: "ResetPassUs",
            url: "ResetPassUs",
            defaults: new { controller = "User", action = "ResetPassUs" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Coffee", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
