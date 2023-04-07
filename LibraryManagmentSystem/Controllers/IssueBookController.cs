using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
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

        
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
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
            else
            {
                var BooksInStock = _context.Books.Where(s => s.Inventory > 0 && s.Title.Contains(search));
                if (BooksInStock.Count() == 0)
                {
                    return View("Empty");
                }
                else
                {
                    return View(BooksInStock);
                }
            }
        }

        public async Task<IActionResult> Issue(int bookid)
        {
            var issueBookVM = new IssueBookViewModel()
            {
                Book = _context.Books.Where(b => b.BookID == bookid).First(),
                Members = _context.Members.ToList()
            };
            return View(issueBookVM);
        }

        [HttpPost]
        public async Task<IActionResult> Issue(IssueBookViewModel issueBookViewModel, IssueTransaction issueTransaction)
        {
            var book = _context.Books.Where(b => b.BookID == issueBookViewModel.Book.BookID).First();

            var member = _context.Members.Where( m => m.MemberID == issueBookViewModel.Book.BorrowerID).First();

            book.Inventory = --book.Inventory;

            issueTransaction.BookID = book.BookID;
            issueTransaction.MemberID = member.MemberID;
            issueTransaction.IssueDate = DateTime.Today.Date;
            issueTransaction.DueDate = DateTime.Today.Date.AddDays(7);
            issueTransaction.Status = false;
            _context.IssueTransactions.Add(issueTransaction);
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
