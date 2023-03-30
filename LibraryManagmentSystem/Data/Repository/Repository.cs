using LibraryManagmentSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using static LibraryManagmentSystem.Data.Interfaces.IRepository;

namespace LibraryManagmentSystem.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryDbContext _context;
        public Repository(LibraryDbContext context) 
        {
            _context = context;
        }
        protected void Save() => _context.SaveChanges();

        public void Create(T entity)
        {
            _context.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
