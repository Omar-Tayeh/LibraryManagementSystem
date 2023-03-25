using LibraryManagmentSystem.Data.Model;

namespace LibraryManagmentSystem.ViewModel
{
    public class IssueBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
