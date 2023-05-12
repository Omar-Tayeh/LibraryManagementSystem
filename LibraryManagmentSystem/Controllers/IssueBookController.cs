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

        //show a book to be selected for issuing.
        public  IActionResult Index()
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
        //after selecting the book, show the members to lend books to.
        public IActionResult Issue(int bookid)
        {
            
                var issueBookVM = new IssueBookViewModel()
                {
                    Book = _context.Books.Where(b => b.BookID == bookid).First(),
                    Members = _context.Members.Where(m => m.Status.Equals(AccountStatus.Active)).ToList()
                };
                return View(issueBookVM);
            
        }
        //lend books to members and adjust inventory, create a transaction record, add and save changes to database.
        public IActionResult Lend(int bookId, int memberId)
        {
            var book = _context.Books.Where(b => b.BookID == bookId).First();

            var member = _context.Members.Where(m => m.MemberID == memberId).First();

            book.Inventory = --book.Inventory;

            var issueTransaction = new IssueTransaction
            {
                MemberID = member.MemberID,
                MemberName = member.MemberName,
                BookID = book.BookID,
                BookTitle = book.Title,
                IssueDate = DateTime.Today.Date,
                DueDate = DateTime.Today.Date.AddDays(7),
                Status = false
            };
            _context.IssueTransactions.Add(issueTransaction);
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
