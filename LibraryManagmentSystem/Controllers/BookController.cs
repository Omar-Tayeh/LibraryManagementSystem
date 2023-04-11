using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        [Route("Book")]
        public IActionResult Index()
        {
                var books = _context.Books.ToList();
                return CheckBooks(books);
        }
        public IActionResult CheckBooks(IEnumerable<Book> books)
        {
            if (books.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(books);
            }
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Books.Where(b => b.BookID == id).First();
            _context.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var book = _context.Books.Where(b => b.BookID == id).First();
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
