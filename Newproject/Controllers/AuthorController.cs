
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newproject.Data;
using Newproject.Models;
using System.Linq;

namespace Demo.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext context;
        public AuthorController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View(context.Author.ToList());
        }
        public IActionResult Detail(int id)
        {
            var author = context.Author.Include(c => c.Book)      //Author - Book: 1 - M  
                                         .FirstOrDefault(c => c.Id == id);
            /* Note:
             * Nếu 2 bảng có kết nối trực tiếp (đi thẳng) thì dùng hàm Include
             * Nếu 2 bảng có kết nối gián tiếp (đi vòng) thông qua bảng trung gian thì dùng hàm ThenInclude
             */
            return View(author);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var author = context.Author.Find(id);
            context.Author.Remove(author);
            context.SaveChanges();
            TempData["Message"] = "Delete author successfully !";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "StoreOwner")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "StoreOwner")]
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Author.Add(author);
                context.SaveChanges();
                TempData["Message"] = "Create author successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = context.Author.Find(id);
            return View(author);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Author.Update(author);
                context.SaveChanges();
                TempData["Message"] = "Edit author successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
    }
}
