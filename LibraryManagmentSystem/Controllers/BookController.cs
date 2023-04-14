using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.Data.Repository;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(LibraryDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(BookViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var book = new Book
            {
                Title = vm.Title,
                Description = vm.Description,
                Author = vm.Author,
                Inventory = vm.Inventory,
                PhotoName = stringFileName
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private string UploadFile(BookViewModel vm)
        {
            string fileName = null;
            if (vm.PhotoVM != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.PhotoVM.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.PhotoVM.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Books.Where(b => b.BookID == id).First();
            var bookViewModel = new BookViewModel
            {
                BookID = book.BookID,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Inventory = book.Inventory,
                ExistingImage = book.PhotoName
            };

            if (book == null)
            {
                return NotFound();
            }
            return View(bookViewModel);
        }

        [HttpPost]
        public IActionResult Update(int id, BookViewModel vm)
        {
            var book = _context.Books.Where(b => b.BookID == id).First();
            book.Title = vm.Title;
            book.Description = vm.Description;
            book.Author = vm.Author;
            book.Inventory = vm.Inventory;

            if (vm.PhotoVM != null)
            {
                if (vm.ExistingImage != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", vm.ExistingImage);
                    System.IO.File.Delete(filePath);
                }

                book.PhotoName = UploadFile(vm);
            }


            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
