namespace LibraryMGMT.Models
{
    public class Lending
    {
        public User? User { get; set; }
        public Book? Book { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
