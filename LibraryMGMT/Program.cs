using LibraryMGMT.Models;

class Program
{
    private static Library? library;

    public static Library Library
    {
        get
        {
            if (library == null)
            {
                library = new Library();
            }

            return library;
        }
    }

    static void Main()
    {
        var isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            PrintMenu();

            var input = Console.ReadLine();


            if (int.TryParse(input, out int op))
            {
                switch (op)
                {
                    case 0:
                        isRunning = false;
                        break;
                    case 1:
                        NewBook();
                        break;
                    case 2:
                        SearchBook();
                        break;
                    case 3:
                        ListAllBooks();
                        break;
                    case 4:
                        CreateNewUser();
                        break;
                    case 5:
                        SearchUser();
                        break;
                    case 6:
                        BorrowBook();
                        break;
                    case 7:
                        ReturnBook();
                        break;
                    case 8:
                        BorrowHistory();
                        break;
                    default:
                        Console.WriteLine(Constants.InvalidOption);
                        break;
                }
            }
            else
            {
                Console.WriteLine(Constants.InvalidInput);
            }
        }
    }
    /// <summary>
    /// Requests valid inputs to create a new user. Once that the inputs are validated,
    /// it saves the response from the library. Then it informs to the user the outcome.
    /// </summary>
    private static void CreateNewUser()
    {
        Console.Clear();
        Console.Title = "Create user";
        var name = ValidateInput(Constants.RequestName);
        var id = ValidateInput(Constants.RequestId);

        var res = Library.AddUser(new User(id, name));

        Console.WriteLine(res.Message);
        Console.WriteLine(Constants.Separator);
        Console.ReadKey();
    }
    /// <summary>
    /// Request valid inputs to create a new book.
    /// </summary>
    private static void NewBook()
    {
        Console.Clear();
        Console.Title = "Create book";
        var title = ValidateInput(Constants.RequestTitle);
        var author = ValidateInput(Constants.RequestAuthor);
        var isbn = ValidateInput(Constants.RequestISBN);
        var year = ValidateYear(Constants.RequestYear, DateTime.Now.Year);


        var res = Library.AddBook(new Book(title, author, isbn, year));
        Console.WriteLine(res.Message);
        Console.WriteLine(Constants.Separator);
        Console.ReadKey();
    }
    /// <summary>
    /// Requests a valid input to pass to the library to check for an existing user with that id.
    /// </summary>
    private static void SearchUser()    {
        Console.Clear();
        Console.Title = "Search user";
        var id = ValidateInput(Constants.RequestId);

        var user = Library.GetUser(id);

        if (user != null)
        {
            PrintUser(user);
        }
        else
        {
            Console.WriteLine(Constants.UserInexistent);
        }
        Console.ReadKey();

    }
    /// <summary>
    /// Request valid inputs to borrow a book.
    /// </summary>
    private static void BorrowBook()
    {
        Console.Clear();
        Console.Title = "Borrow book";
        var id = ValidateInput(Constants.RequestId);

        var title = ValidateInput(Constants.RequestTitle);

        var res = Library.BorrowBook(id, title);

        Console.WriteLine(res.Message);
        Console.ReadKey();

    }

    private static void ReturnBook()
    {
        Console.Clear();
        Console.Title = "Return book";
        var id = ValidateInput(Constants.RequestId);
        var title = ValidateInput(Constants.RequestTitle);

        var res = Library.ReturnBook(id, title);

        Console.WriteLine(res.Message);
        Console.ReadKey();
    }

    private static void BorrowHistory()
    {
        Console.Clear();
        Console.Title = "Borrow history";
        if (Library.Lendings.Count > 0)
        {
            foreach (var lend in Library.Lendings)
            {
                Console.WriteLine(string.Format(Constants.LendingInfo, lend.User?.Name, lend.Book?.Title, lend.BorrowDate, (lend.ReturnDate == null ? "Pending" : lend.ReturnDate)));
            }

        }
        else
        {
            Console.WriteLine(Constants.NoLendings);
        }
        Console.ReadKey();
    }
    private static void PrintUser(User user)
    {
        Console.WriteLine(string.Format(Constants.UserInfo, user.UserId, user.Name));
    }
    private static void SearchBook()
    {
        Console.Clear();
        Console.Title = "Search book";
        Console.Write(Constants.RequestTitle);
        var title = Console.ReadLine();

        if (!string.IsNullOrEmpty(title))
        {
            var searched = Library.GetBooks(title);
            if (searched.Count > 0)
            {
                ProcessList(searched);
            }
            else
            {
                Console.WriteLine(Constants.BookNotFound);
            }
        }
        else
        {
            Console.Write(Constants.InvalidInput);
        }
        Console.ReadKey();

    }

    private static void ListAllBooks()
    {
        Console.Clear();
        Console.Title = "List books";
        if (Library.Books.Count > 0)
        {
            Console.WriteLine(Constants.ListingBooks);

            ProcessList(Library.Books);

        }
        else
        {
            Console.WriteLine(Constants.NoBooks);
        }
        Console.ReadKey();
    }

    private static void ProcessList(List<Book> list)
    {
        foreach (var book in list)
        {
            Console.WriteLine(book.ToString());
        }

    }

    /// <summary>
    /// Prints the message to indicate the user what they have to input.
    /// Checks if the input is valid, then returns it.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static string ValidateInput(string message)
    {
        Console.WriteLine(Constants.Separator);
        Console.Write(message);
        var input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine(Constants.InvalidInput);
            input = Console.ReadLine();
        }

        return input;
    }

    private static int ValidateYear(string message, int limit)
    {
        Console.WriteLine(Constants.Separator);
        Console.WriteLine(message);

        bool validYear = false;
        int year = 0;

        while (!validYear)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out year) && (year > 0 && year <= limit))
            {
                validYear = true;
            }
            else
            {
                Console.WriteLine(Constants.InvalidYear);
            }
        }

        return year;
    }

    /// <summary>
    /// Prints all the menu options.
    /// </summary>
    private static void PrintMenu()
    {
        Console.Title = "Main menu";
        Console.WriteLine(Constants.Separator);
        Console.WriteLine(Constants.MainMenu);
        Console.WriteLine(Constants.Separator);
        Console.WriteLine();
        Console.WriteLine(string.Format(Constants.NewBook, 1));
        Console.WriteLine(string.Format(Constants.SearchBook, 2));
        Console.WriteLine(string.Format(Constants.ListBooks, 3));
        Console.WriteLine(string.Format(Constants.CreateUser, 4));
        Console.WriteLine(string.Format(Constants.SearchUser, 5));
        Console.WriteLine(string.Format(Constants.BorrowBook, 6));
        Console.WriteLine(string.Format(Constants.ReturnBook, 7));
        Console.WriteLine(string.Format(Constants.BorrowHistory, 8));
        Console.WriteLine();
        Console.WriteLine(string.Format(Constants.Exit, 0));
        Console.WriteLine(Constants.Separator);
    }

    /// <summary>
    /// Tells the book, which is recieved by params, to print its data.
    /// </summary>
    /// <param name="book"></param>
    private static void PrintBook(Book book)
    {
        Console.WriteLine(Constants.Separator);
        Console.WriteLine(book.ToString());
        Console.WriteLine(Constants.Separator);
    }
}