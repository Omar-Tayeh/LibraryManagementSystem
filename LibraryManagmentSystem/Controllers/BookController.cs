using LibraryManagmentSystem.Authorization;
using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.Data.Repository;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        //Index method to fetch all books in the database and 
        [Route("Book")]
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return CheckBooks(books);
        }

        //check if the view will return books, if not it will show the Empty view.
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

        //Delete method after insuring no books are in any previouse transaction, and only allowing Admins and Managers to delete.
        public IActionResult Delete(int id)
        {
            //check if the book is in a transaction.
            var bookInTransaction = _context.IssueTransactions.Where(t => t.BookID == id).ToList();

            if (bookInTransaction.IsNullOrEmpty())
            {
                //check user role and only accept admins and managers.
                var isManager = User.IsInRole(Constants.ManagerRole);
                var isAdmin = User.IsInRole(Constants.AdminRole);

                //if not admin or manager deny access and abort operation.
                if (isManager != true && isAdmin != true)
                    return Forbid();
                //otherwise delete the book from the database and save the changes.
                var book = _context.Books.Where(b => b.BookID == id).First();
                _context.Remove(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            //if book is in a transaction fail the deletion.
            return View("DeleteFailed");
        }

        public IActionResult Create()
        {
            return View();
        }

        //Create method of type post, which takes argumetn of type BookViewModel from the view and creates an instance of type Book.
        [HttpPost]
        public IActionResult Create(BookViewModel vm)
        {
            //using the UploadFile method created below, to upload the image to the system and get a file path to bind it to the Book created.
            string stringFileName = UploadFile(vm);

            //takes the user input and store it as async new Book.
            var book = new Book
            {
                Title = vm.Title,
                Description = vm.Description,
                Author = vm.Author,
                Inventory = vm.Inventory,
                PhotoName = stringFileName
            };

            //adding the book to the database and saving changes.
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //method to bind the string property with the image file using the path.
        private string UploadFile(BookViewModel vm)
        {
            string fileName = null;
            if (vm.PhotoVM != null)
            {
                //uploadDir: directory in which the image files will be uploaded to.
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                //the name of the image file using Guid and the photo name.
                fileName = Guid.NewGuid().ToString() + "-" + vm.PhotoVM.FileName;
                //the full directory of the file.
                string filePath = Path.Combine(uploadDir, fileName);

                //using FileStream to inisilaize the file with the path and the File.Mode.
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.PhotoVM.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult Update(int? id)
        {
            //check if there is an id, if not return notFound view 404.
            if (id == null)
            {
                return NotFound();
            }
            //create variable book of the id provided from the database.
            var book = _context.Books.Where(b => b.BookID == id).First();
            if(book == null)
            {
                return NotFound();
            }
            //pass the book to the BookViewModel, if there is a photo pass it to the view model to get replaced if needed.
            var bookViewModel = new BookViewModel
            {
                BookID = book.BookID,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Inventory = book.Inventory,
                ExistingImage = book.PhotoName
            };

            return View(bookViewModel);
        }

        //Update post method.
        [HttpPost]
        public IActionResult Update(BookViewModel vm)
        {
            //find the book in the database.
            var book = _context.Books.Where(b => b.BookID == vm.BookID).First();
            //pass the viewmodel of the book and apply the changes.
            book.Title = vm.Title;
            book.Description = vm.Description;
            book.Author = vm.Author;
            book.Inventory = vm.Inventory;

            //if there is a photo, check if there was an existing photo and replace it.
            if (vm.PhotoVM != null)
            {
                if (vm.ExistingImage != null)
                {
                    //build the existing photo path.
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", vm.ExistingImage);
                    //delete the file.
                    System.IO.File.Delete(filePath);
                }

                book.PhotoName = UploadFile(vm);
            }

            //update the database and save the changes.
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
