using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Data.Model
{
    [Table("IssueTransaction")]
    public class IssueTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }

    }
}
