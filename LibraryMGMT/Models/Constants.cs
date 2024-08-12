namespace LibraryMGMT.Models
{
    internal class Constants
    {
        public const string Separator = "=============================================";

        public const string MainMenu = "\t\tMain menu";
        public const string NewBook = "{0}. Add new book.";
        public const string SearchBook = "{0}. Search book by title.";
        public const string ListBooks = "{0}. List all books.";
        public const string CreateUser = "{0}. Create new user.";
        public const string SearchUser = "{0}. Search user.";
        public const string BorrowBook = "{0}. Borrow book.";
        public const string ReturnBook = "{0}. Return book";
        public const string BorrowHistory = "{0}. Check borrow history.";
        public const string Exit = "{0}. Exit.";

        public const string InvalidOption = "Invalid option. Try again.";
        public const string InvalidInput = "Invalid input. Please try again";
        public const string InvalidYear = "Year must be grater than 0 and lower or equal to current year.";

        public const string RequestTitle = "Enter book title: ";
        public const string RequestAuthor = "Enter author: ";
        public const string RequestISBN = "Enter ISBN: ";
        public const string RequestYear = "Enter release year: ";
        public const string RequestName = "Enter user name: ";
        public const string RequestId = "Enter user id: ";

        public const string BookAdded = "The book was added succesfully.";
        public const string BookExists = "The book has already been added.";
        public const string BookNotFound = "The book was not found";
        public const string BookTaken = "The book is already borrowed";
        public const string BookBorrowSuccess = "The book has been borrowed succesfully";
        public const string BookInfo = "Title: {0} | Author: {1} | Release year: {2} | ISBN: {3} ";

        public const string UserAdded = "The user was successfully created.";
        public const string UserExists = "The user has already been created.";
        public const string UserInexistent = "The user doesnt exist.";
        public const string UserInfo = "Id: {0} | Name: {1}";

        public const string LendingNotFound = "The lending doesnt exist.";
        public const string LendingReturn = "The book was successfully returned";
        public const string LendingInfo = "User: {0} | Book: {1} | Borrow date: {2} | Return date: {3} |";

        public const string ListingBooks = "Books: ";

        public const string NoBooks = "There are no books.";
        public const string NoLendings = "There are no lendings";

    }
}
