using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [Route("Book")]
        public IActionResult Index(int? borrowerID)
        {
            if(borrowerID == null)
            {
                var books = _bookRepository.GetAll();
                return CheckBooks(books);
            }
            else if(borrowerID.HasValue)
            {
                var books = _bookRepository.FindWithBorrower(b => b.BorrowerID == borrowerID);
                return CheckBooks(books);
            }
            else
            {
                throw new ArgumentException();
            }
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
            var book = _bookRepository.GetById(id);

            _bookRepository.Delete(book);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _bookRepository.Create(book);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var book = _bookRepository.GetById(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            _bookRepository.Update(book);
            return RedirectToAction("Index");
        }
    }
}
