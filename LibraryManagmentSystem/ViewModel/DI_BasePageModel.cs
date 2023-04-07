using LibraryManagmentSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagmentSystem.ViewModel
{
    public class DI_BasePageModel : Controller
    {
        protected readonly LibraryDbContext _context;
        protected readonly IAuthorizationService _authorizationService;
        protected readonly UserManager<IdentityUser> _userManager;

        public DI_BasePageModel(
            LibraryDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
    }
}
