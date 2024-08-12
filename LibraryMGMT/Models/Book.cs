namespace LibraryMGMT.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }

        public Book(string title, string author, string isbn, int year)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = year;

        }

        public override string? ToString()
        {
            return string.Format(Constants.BookInfo, Title, Author, PublicationYear, ISBN);
        }
    }
}
