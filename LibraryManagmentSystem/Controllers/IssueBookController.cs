using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class IssueBookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public IssueBookController(IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        
        public IActionResult Index()
        {
            var BooksInStock = _bookRepository.FindAll(s => s.Inventory > 0);

            if(BooksInStock.Count() == 0)
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
        public IActionResult Issue(IssueBookViewModel issueBookViewModel)
        {
            var book = _bookRepository.GetById(issueBookViewModel.Book.BookID);

            var member = _memberRepository.GetById(issueBookViewModel.Book.BorrowerID);

            book.Borrower = member;
            book.Inventory = --book.Inventory;

            _bookRepository.Update(book);

            return RedirectToAction("Index");
        }
    }
}
