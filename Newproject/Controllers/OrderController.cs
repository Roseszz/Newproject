
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newproject.Data;
using Newproject.Models;
using System;
using System.Data;
using System.Linq;

namespace Demo.Controllers
{
    public class OrderController : Controller
    {
        //khai báo ApplicationDbContext để truy xuất và thay đổi dữ liệu của bảng
        private ApplicationDbContext context;
        public OrderController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult Make(int id, int quantity)
        {
            //tạo Order mới
            var order = new Order();
            //set giá trị trong từng thuộc tính của Order
            var mobile = context.Book.Find(id);
            order.Book = mobile;
            order.BookId = id;
            order.OrderQuantity = quantity;
            order.OrderPrice = mobile.Price * quantity;
            order.OrderDate = DateTime.Now;
            order.UserEmail = User.Identity.Name;
            //add Order vào DB
            context.Order.Add(order);
            //trừ quantity của mobile
            mobile.Quantity -= quantity;
            context.Book.Update(mobile);
            //lưu cập nhật vào DB
            context.SaveChanges();
            //gửi về thông báo order thành công
            TempData["Success"] = "Order Book successfully !";
            //redirect về trang mobile store
            return RedirectToAction("Store", "Book");
        }
        
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Delete(int id)
        {
            var order = context.Order.Find(id);
            context.Order.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Index", "Order");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var orders = context.Order
                .Include(c => c.Book)
                .ToList();
            return View(orders);
        }
        // renders orders of the current user
        [Authorize(Roles = "Customer")]
        public IActionResult IndexForCurrent()
        {
            var orders = context.Order
                .Include(c => c.Book)
                .Where(c => c.UserEmail == User.Identity.Name)
                .ToList();
            return View(orders);
        }
        
    }
}
