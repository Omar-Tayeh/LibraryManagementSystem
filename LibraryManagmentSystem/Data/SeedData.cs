using LibraryManagmentSystem.Authorization;
using Microsoft.AspNetCore.Identity;
using LibraryManagmentSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Data
{
    public class SeedData
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            string password = "Test@1234")
        {
           using(var _context = new LibraryDbContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryDbContext>>()))
            {
                var managerUid = await EnsureUser(serviceProvider, "manager@livlib.com", password);
                await EnsureRole(serviceProvider, managerUid, Constants.ManagerRole);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
            string userName, string initPw)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, initPw);
            }
            if (user == null)
                throw new Exception("User did not get created. password Error?");
            return user.Id;
        }

        public static async Task<IdentityResult> EnsureRole(
            IServiceProvider serviceProvider,
            string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            IdentityResult ir;

            if(await roleManager.RoleExistsAsync(role) == false)
            {
                ir = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
                throw new Exception("USer does not exist");

            ir = await userManager.AddToRoleAsync(user, role);

            return ir;
        }
    }
}
