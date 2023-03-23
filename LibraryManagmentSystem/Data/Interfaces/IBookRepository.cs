using LibraryManagmentSystem.Data.Model;
using static LibraryManagmentSystem.Data.Interfaces.IRepository;

namespace LibraryManagmentSystem.Data.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> FindWithBorrower(Func<Book, bool> predicate);
    }
}
