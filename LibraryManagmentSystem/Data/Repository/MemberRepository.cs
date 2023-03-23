using LibraryManagmentSystem.Data.Interfaces;
using LibraryManagmentSystem.Data.Model;

namespace LibraryManagmentSystem.Data.Repository
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
