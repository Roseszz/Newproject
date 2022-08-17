
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newproject.Data;
using Newproject.Models;
using System.Data;
using System.Linq;

namespace Demo.Controllers
{
    public class CategoryController : Controller
    {
        //khai báo ApplicationDbContext để truy xuất và thay đổi dữ liệu của bảng
        private ApplicationDbContext context;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        //load toàn bộ dữ liệu của bảng  
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(context.Category.ToList());
        }

        //xoá dữ liệu từ bảng
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = context.Category.Find(id);
            context.Category.Remove(category);
            context.SaveChanges();
            TempData["Message"] = "Delete Category successfully !";
            return RedirectToAction("Index");
        }

        //xem thông tin theo id
        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int id)
        {
            var category = context.Category.Include(b => b.Books)  //Brand - Mobile : 1 - M
                                       //Brand - Country : M - 1
                                     .FirstOrDefault(b => b.Id == id);
            return View(category);
        }

        //thêm dữ liệu vào bảng
        //hàm 1: render ra view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            //đẩy danh sách của country sang bảng Add Brand
            //ViewBag.Countries = context.Author.ToList();
            return View();
        }

        //hàm 2: nhận và xử lý dữ liệu được gửi từ form
        [HttpPost]
        public IActionResult Create(string n)
        {
            //check tính hợp lệ của dữ liệu 
            var category = new Category();
            if (ModelState.IsValid)
            {
                //add dữ liệu vào DB
                category.Name = n;
                context.Add(category);
                context.SaveChanges();
                //hiển thị thông báo thành công về view
                TempData["Message"] = "Add brand successfully !";
                //quay ngược về trang index
                return RedirectToAction(nameof(Index));
            }
            //nếu dữ liệu không hợp lệ thì trả về form để nhập lại
            return View(category);
        }

        //sửa dữ liệu của bảng
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Countries = context.Category.ToList();
            return View(context.Category.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //check tính hợp lệ của dữ liệu 
            if (ModelState.IsValid)
            {
                //update dữ liệu vào DB
                context.Update(category);
                context.SaveChanges();
                //hiển thị thông báo thành công về view
                TempData["Message"] = "Edit brand successfully !";
                //quay ngược về trang index
                return RedirectToAction(nameof(Index));
            }
            //nếu dữ liệu không hợp lệ thì trả về form để nhập lại
            return View(category);
        }
    }
}
