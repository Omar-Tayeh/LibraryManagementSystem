using LibraryManagmentSystem.Data.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.ViewModel
{
    public class IssueBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
