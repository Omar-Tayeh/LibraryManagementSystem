using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class MemberController : Controller
      
    {
        protected readonly LibraryDbContext _context;
        public MemberController(LibraryDbContext context)
        {
            _context = context;
        }

        [Route("Member")]
        public IActionResult Index()
        {
                var memberVM = new List<ViewModel.MemberViewModel>();
                var members = _context.Members.ToList();

                if (members.Count() == 0)
                {
                    return View("Empty");
                }

                foreach (var member in members)
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
            var member = _context.Members.Where( m => m.MemberID == id).First();
            _context.Members.Remove(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var member = _context.Members.Where(m => m.MemberID == id).First();
            return View(member);
        }

        [HttpPost]
        public IActionResult Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
