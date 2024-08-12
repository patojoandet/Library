namespace LibraryMGMT.Models
{
    public class User
    {
        public User(string userId, string name)
        {
            UserId = userId;
            Name = name;
            BorrowedBooks = new List<Book>();
        }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Book> BorrowedBooks = new List<Book>(); // es correcto?

        public void Borrow(Book book)
        {
            BorrowedBooks.Add(book);
        }

        public void ReturnBook(Book book)
        {
            BorrowedBooks.Remove(book);
        }

    }
}
