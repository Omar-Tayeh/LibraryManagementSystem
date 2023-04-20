namespace LibraryManagmentSystem.ViewModel
{
    public class BookViewModel : EditImageViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Inventory { get; set; }
    }
}
