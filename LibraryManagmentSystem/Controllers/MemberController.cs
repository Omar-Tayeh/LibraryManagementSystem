using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class MemberController : Controller
      
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBookRepository _bookRepository;
        public MemberController(IMemberRepository memberRepository,
            IBookRepository bookRepository)
        {
            _memberRepository = memberRepository;
            _bookRepository = bookRepository;
        }
        public IActionResult List()
        {
            var memberVM = new List<ViewModel.MemberViewModel>();
            var members = _memberRepository.GetAll();

            if (members.Count() == 0) 
            {
                return View("Empty");
            }

            foreach ( var member in members) 
            {
                memberVM.Add(new ViewModel.MemberViewModel
                {
                    Member = member,
                    BookCount = _bookRepository.Count(x => x.BorrowerID == member.MemberID)
                });
            
            }

            return View(memberVM);
        }
    }
}
