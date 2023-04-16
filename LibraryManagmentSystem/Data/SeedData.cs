using LibraryManagmentSystem.Authorization;
using Microsoft.AspNetCore.Identity;
using LibraryManagmentSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Data
{
    public class SeedData
    {
        private readonly LibraryDbContext _context;
        public SeedData(LibraryDbContext context)
        {
            _context = context;
        }

        
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            string password = "Test@1234")
        {
            using (var _context = new LibraryDbContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryDbContext>>()))
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

            if (await roleManager.RoleExistsAsync(role) == false)
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

        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();

                if (!_context.Books.Any())
                {
                    var books = new List<Book>()
                {
                new Book()
                {
                    Title = "Tree Book",
                    Description = "Trees",
                    Author = "Forest Author",
                    Inventory = 0
                },
                new Book()
                {
                    Title = "Veggies Book",
                    Description = "Vegtables",
                    Author = "Forest Author",
                    Inventory = 5
                },
                new Book()
                {
                    Title = "Plant Book",
                    Description = "Grow plants",
                    Author = "Forest Author",
                    Inventory = 5
                },
                new Book()
                {
                    Title = "Rock Book",
                    Description = "Rock types",
                    Author = "Forest Author",
                    Inventory = 5
                }
                };
                    _context.Books.AddRange(books);
                    _context.SaveChanges();
                }
                if (!_context.Members.Any())
                {
                    var members = new List<Member>()
            {
                new Member()
                {
                    MemberName = "Mem 1",
                    Email = "mem1@mem1.com",
                    StudentID = 1111,
                    Address = "Manchester",
                    Status = AccountStatus.Active
                },
                new Member()
                {
                    MemberName = "Mem 2",
                    Email = "mem2@mem2.com",
                    StudentID = 1112,
                    Address = "Birmingham"
                },
                new Member()
                {
                    MemberName = "Mem 3",
                    Email = "mem3@mem3.com",
                    StudentID = 1113,
                    Address = "London"
                },
                new Member()
                {
                    MemberName = "Mem 4",
                    Email = "mem4@mem4.com",
                    StudentID = 1114,
                    Address = "Liverpool"
                }
            };
                    _context.Members.AddRange(members);
                    _context.SaveChanges();
                }
            }
        }
    }
}
