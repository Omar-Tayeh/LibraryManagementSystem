namespace LibraryManagmentSystem.Data.Model
{
    public class Member
    {
        public int MemberID { get; set; }
        public int StudentID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool AccountStatus { get; set; }
    }
}
