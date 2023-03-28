using LibraryManagmentSystem.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public ReturnController(IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }
        /*public IActionResult Index()
        {
            
            var borrowedBooks = _bookRepository.FindWithBorrower(x => x.borrowerID.Contains(Member));
            
            if (borrowedBooks == null || borrowedBooks.ToList().Count() == 0)
            {
                return View("Empty");
            }
            return View(borrowedBooks);
        }*/
    }
}
