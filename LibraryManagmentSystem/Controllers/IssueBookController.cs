using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class IssueBookController : Controller

    {
        protected readonly LibraryDbContext _context;
        
        public IssueBookController(LibraryDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            //var BooksInStock = _bookRepository.FindAll(s => s.Inventory > 0);
            var BooksInStock = _context.Books.Where(s => s.Inventory > 0);

            if (BooksInStock.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(BooksInStock);
            }
        }

        public IActionResult Issue(int bookid)
        {
            var issueBookVM = new IssueBookViewModel()
            {
                Book = _context.Books.Find(bookid),
                Members = _context.Members.ToList(),
            };
            return View(issueBookVM);
        }

        [HttpPost]
        public IActionResult Issue(IssueBookViewModel issueBookViewModel, IssueTransaction issueTransaction)
        {
            var book = _context.Books.Find(issueBookViewModel.Book.BookID);

            var member = _context.Members.Find(issueBookViewModel.Book.BorrowerID);

            //book.Borrower = member;
            book.Inventory = --book.Inventory;

            issueTransaction.BookID = book.BookID;
            issueTransaction.MemberID = member.MemberID;
            issueTransaction.IssueDate = DateTime.Today.Date;
            issueTransaction.DueDate = DateTime.Today.Date.AddDays(7);
            issueTransaction.Status = false;
            _context.IssueTransactions.Add(issueTransaction);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
