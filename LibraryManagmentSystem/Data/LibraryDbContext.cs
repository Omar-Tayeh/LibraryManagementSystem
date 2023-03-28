using LibraryManagmentSystem.Controllers;
using LibraryManagmentSystem.Data.Model;
using LibraryManagmentSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<IssueTransaction> IssueTransactions { get; set; }
    }
}
