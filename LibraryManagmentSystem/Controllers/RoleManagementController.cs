using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class RoleManagementController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleManagementController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    //allow access only for admin users, show roles.
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
    //only allow access to admin users, ability to add new roles.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRole(string roleName)
    {
        if (roleName != null)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
        }
        return RedirectToAction("Index");
    }
}