namespace LibraryMGMT.Models
{
    public class Library
    {
        public List<Book> Books = new List<Book>();
        public List<User> Users = new List<User>();
        public List<Lending> Lendings = new List<Lending>();

        /// <summary>
        /// Recieves an user and checks if there's already one with the same id.
        /// If there's no other with same id, it's added to the users list.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public Response AddUser(User u)
        {
            if (u != null && !UserExists(u.UserId))
            {
                Users.Add(u);

                return new Response() { Status = 200, Message = Constants.UserAdded };
            }

            return new Response() { Status = 400, Message = Constants.UserExists };
        }

        /// <summary>
        /// Returns the user that matches the given userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User? GetUser(string userId)
        {
            return Users.FirstOrDefault(u => u.UserId.Equals(userId));
        }

        /// <summary>
        /// Checks if there is already an user with the given userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool UserExists(string userId)
        {
            return Users.Any(u => u.UserId == userId);
        }
        /// <summary>
        /// Adds the new lend to the history.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="b"></param>
        private void RegisterLend(User u, Book b)
        {
            Lendings.Add(new Lending() { User = u, Book = b, BorrowDate = DateTime.Now });
        }
        /// <summary>
        /// Adds the return date to the given lending.
        /// </summary>
        /// <param name="lending"></param>
        public void RegisterReturn(Lending lending)
        {
            lending.ReturnDate = DateTime.Now;
        }

        /// <summary>
        /// Adds a new book, if it doesnt exist, to the library.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Response AddBook(Book b)
        {
            var res = new Response() { Status = 400, Message = Constants.BookExists };

            if (!Exists(b.ISBN))
            {

                Books.Add(b);
                res = new Response() { Status = 200, Message = Constants.BookAdded };


            }
            return res;
        }

        /// <summary>
        /// Checks if theres already a book added with the same ISBN.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        private bool Exists(string isbn)
        {
            return Books.Any(book => book.ISBN == isbn);
        }

        /// <summary>
        /// Returns a list of all the books that match or contains the given title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Book> GetBooks(string title)
        {
            Console.WriteLine("Searching: " + title);

            var res = Books.Where(b => b.Title.Contains(title)).ToList();

            return res;
        }

        public Book? GetBook(string title)
        {
            return Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Checks if the user and the book exists. If they do, it checks whether if available
        /// to lend or if its already borrowed and wasnt returned.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public Response BorrowBook(string userId, string title)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                var book = GetBook(title);
                if (book != null)
                {
                    var canBorrow = !Lendings.Any(l => l.Book == book) || !Lendings.Any(l => l.Book == book && l.ReturnDate == null);
                    if (canBorrow)
                    {
                        RegisterLend(user, book);

                        user.Borrow(book);
                        return new Response() { Status = 200, Message = Constants.BookBorrowSuccess };

                    }
                    else
                    {
                        return new Response() { Status = 400, Message = Constants.BookTaken };
                    }
                }
                else
                {
                    return new Response() { Status = 400, Message = Constants.BookNotFound };
                }
            }
            else
            {
                return new Response() { Status = 400, Message = Constants.UserInexistent };
            }
        }
        /// <summary>
        /// Searchs the user, the book and checks if theres an open lending that matches that criteria.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public Response ReturnBook(string userId, string title)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                var book = GetBook(title);
                if (book != null)
                {
                    var lending = Lendings.FirstOrDefault(l => l.User == user && l.Book == book && l.ReturnDate == null);
                    if (lending != null)
                    {
                        RegisterReturn(lending);
                        user.ReturnBook(book);

                        return new Response() { Status = 200, Message = Constants.LendingReturn };
                    }
                    else
                    {
                        return new Response() { Status = 400, Message = Constants.LendingNotFound };
                    }

                }
                else
                {
                    return new Response() { Status = 400, Message = Constants.BookNotFound };
                }

            }
            else
            {
                return new Response() { Status = 400, Message = Constants.UserInexistent };
            }
        }
    }
}
