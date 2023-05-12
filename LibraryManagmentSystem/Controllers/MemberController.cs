using LibraryManagmentSystem.Authorization;
using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagmentSystem.Controllers

{    //Members controller to manage members creation, updating and deletion.
    public class MemberController : Controller

    {
        protected readonly LibraryDbContext _context;
        protected readonly IAuthorizationService _authorizationService;
        protected readonly UserManager<IdentityUser> _userManager;
        public MemberController(LibraryDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;

        }

        //show the list of members in the database.
        [Route("Member")]
        public IActionResult Index()
        {
            //create a list of members using a viewmodel.
            var memberVM = new List<ViewModel.MemberViewModel>();
            var members = _context.Members.ToList();

            //if there is no members in the database, show a different view.
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

        //Delete method after insuring no members are in any previouse transaction, and only allowing Admins and Managers to delete.
        public IActionResult Delete(int id)
        {
            //check if the member is in a transaction.
            var memberInTransaction = _context.IssueTransactions.Where(t => t.MemberID == id).ToList();
            if (memberInTransaction.IsNullOrEmpty())
            {
                //check user role and only accept admins and managers.
                var isManager = User.IsInRole(Constants.ManagerRole);
                var isAdmin = User.IsInRole(Constants.AdminRole);

                //if not admin or manager deny access and abort operation.
                if (isManager != true && isAdmin != true)
                    return Forbid();

                //otherwise delete the book from the database and save the changes.
                var member = _context.Members.Where(m => m.MemberID == id).First();
                _context.Members.Remove(member);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("DeleteFailed");
        }

        public IActionResult Create()
        {
            return View();
        }

        //Create method of type post, which takes argumetn of type member from the view and creates an instance of type Book.
        [HttpPost]
        public IActionResult Create(Member member)
        {
            //adding the book to the database and saving changes.
            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            /*//check if the user is authorised as manager or admin.
            var isManager = User.IsInRole(Constants.ManagerRole);
            var isAdmin = User.IsInRole(Constants.AdminRole);

            if (isManager != true && isAdmin != true)
                return Forbid();*/

            //get the member from the database
            var member = _context.Members.Where(m => m.MemberID == id).First();
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        //Update post method.
        [HttpPost]
        public IActionResult Update(Member member, AccountStatus status)
        {
            member.Status = status;
            _context.Members.Update(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
