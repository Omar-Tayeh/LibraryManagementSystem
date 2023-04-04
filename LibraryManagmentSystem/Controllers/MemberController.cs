using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class MemberController : Controller
      
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        protected readonly LibraryDbContext _context;
        public MemberController(IBookRepository bookRepository, IMemberRepository memberRepository, LibraryDbContext context)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _context = context;
        }

        [Route("Member")]
        public IActionResult MemberTable()
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
                    BookCount = _context.IssueTransactions.Count(m => m.MemberID == member.MemberID && m.Status == false)
                });
            
            }

            return View(memberVM);
        }

        public IActionResult Delete(int id)
        {
            var member = _memberRepository.GetById(id);

            _memberRepository.Delete(member);
            return RedirectToAction("MemberTable");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            _memberRepository.Create(member);
            return RedirectToAction("MemberTable");
        }

        public IActionResult Update(int id)
        {
            var member = _memberRepository.GetById(id);
            return View(member);
        }

        [HttpPost]
        public IActionResult Update(Member member)
        {
            _memberRepository.Update(member);
            return RedirectToAction("MemberTable");
        }
    }
}
