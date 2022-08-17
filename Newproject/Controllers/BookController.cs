
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newproject.Data;
using Newproject.Models;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Demo.Controllers
{
    public class BookController : Controller
    {

        //khai báo ApplicationDbContext để truy xuất và thay đổi dữ liệu của bảng
        private ApplicationDbContext context;
        public BookController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        //load toàn bộ dữ liệu của bảng  
        //[Authorize(Roles = "Admin")]
        /*
        public IActionResult Index()
        {
            //xếp book mới được hiển thị ở đầu danh sách (sort id giảm dần)
            var books = context.Book.OrderByDescending(m => m.Id).ToList();
            return View(books);
        }
        */
        public IActionResult Index()
        {
            return View(context.Book.ToList());
        }

        [Authorize(Roles = "Customer")]
        //hiển thị giao diện dạng card cho khách hàng order sản phẩm


        //xoá dữ liệu từ bảng
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var book = context.Book.Find(id);
            context.Book.Remove(book);
            context.SaveChanges();
            TempData["Message"] = "Delete book successfully !";
            return RedirectToAction("Index");
        }

        //xem thông tin theo id
        //[Authorize(Roles = "Admin, Customer")]
        public IActionResult Detail(int id)
        {
            var book = context.Book.Include(m => m.Category)  //Book  - Category : 1 - M
                                       .Include(b => b.Author)  //Book - Author : M - 1
                                       .FirstOrDefault(b => b.Id == id);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            //đẩy danh sách của country sang bảng Add book
            ViewBag.Authors = context.Author.ToList();
            ViewBag.Brands = context.Category.ToList();
            return View();
        }

        //hàm 2: nhận và xử lý dữ liệu được gửi từ form
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Book book)
        {
            //check tính hợp lệ của dữ liệu 
            if (ModelState.IsValid)
            {
                //add dữ liệu vào DB
                context.Book.Add(book);
                context.SaveChanges();
                //hiển thị thông báo thành công về view
                TempData["Message"] = "Add book successfully !";
                //quay ngược về trang index
                return RedirectToAction(nameof(Index));
            }
            //nếu dữ liệu không hợp lệ thì trả về form để nhập lại
            return View(book);
        }


        //sửa dữ liệu của bảng
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Authors = context.Author.ToList();
            ViewBag.Brands = context.Category.ToList();
            return View(context.Book.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            //check tính hợp lệ của dữ liệu 
            if (ModelState.IsValid)
            {
                //update dữ liệu vào DB
                context.Update(book);
                context.SaveChanges();
                //hiển thị thông báo thành công về view
                TempData["Message"] = "Edit book successfully !";
                //quay ngược về trang index
                return RedirectToAction(nameof(Index));
            }
            //nếu dữ liệu không hợp lệ thì trả về form để nhập lại
            return View(book);
        }
        /*
        public IActionResult PriceAsc()
        {
            var books = context.Book.OrderBy(m => m.Price).ToList();
            return View("Store", books);
        }

        public IActionResult PriceDesc()
        {
            var books = context.Book.OrderByDescending(m => m.Price).ToList();
            return View("Store", books);
        }

        [HttpPost]
        public IActionResult Search(string keyword)
        {
            var books = context.Book.Where(m => m.Name.Contains(keyword)).ToList();
            return View("Store", books);
        }
        */
        public IActionResult Shop()
        {
            return View(context.Book.ToList());
        }
    }
}
