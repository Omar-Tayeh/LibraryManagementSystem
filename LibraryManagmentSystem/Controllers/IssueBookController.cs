using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class IssueBookController : Controller

    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        protected readonly LibraryDbContext _context;
        

        public IssueBookController(IBookRepository bookRepository, IMemberRepository memberRepository, LibraryDbContext context)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
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
                Book = _bookRepository.GetById(bookid),
                Members = _memberRepository.GetAll()
            };
            return View(issueBookVM);
        }

        [HttpPost]
        public IActionResult Issue(IssueBookViewModel issueBookViewModel, IssueTransaction issueTransaction)
        {
            var book = _bookRepository.GetById(issueBookViewModel.Book.BookID);

            var member = _memberRepository.GetById(issueBookViewModel.Book.BorrowerID);

            //book.Borrower = member;
            book.Inventory = --book.Inventory;


            issueTransaction.BookID = book.BookID;
            issueTransaction.MemberID = member.MemberID;
            issueTransaction.IssueDate = DateTime.Today.Date;
            issueTransaction.DueDate = DateTime.Today.Date.AddDays(7);
            issueTransaction.Status = false;
            _context.IssueTransactions.Add(issueTransaction);
            
            _bookRepository.Update(book);

            return RedirectToAction("Index");
        }
    }
}
