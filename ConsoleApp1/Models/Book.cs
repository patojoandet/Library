using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
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
