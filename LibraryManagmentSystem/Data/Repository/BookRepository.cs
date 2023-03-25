using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }
        public IEnumerable<Book> FindAll(Func<Book, bool> predicate)
        {
            return _context.Books
                .Where(predicate);
        }
        public IEnumerable<Book> FindWithBorrower(Func<Book, bool> predicate)
        {
            return _context.Books
                .Include(a => a.Borrower)
                .Where(predicate);
        }
    }
}
