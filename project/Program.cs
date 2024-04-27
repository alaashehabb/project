using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project
{
    public abstract class LibraryItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public abstract void DisplayDetails();
    }


    public class Book : LibraryItem
    {
        public string Genre { get; set; }
        public int CopiesAvailable { get; set; }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Book ID:{Id}");
            Console.WriteLine($"Title:{Title}");
            Console.WriteLine($"Author:{Author}");
            Console.WriteLine($"ISBN:{ISBN}");
            Console.WriteLine($"Genre:{Genre}");
            Console.WriteLine($"Avaliable:{IsAvailable}");
            Console.WriteLine($"Copies Avaliable:{CopiesAvailable}");
        }

        public virtual string GetBookType()
        {
            return "Book";
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool IsAdmin { get; set; }
        public List<string> Privileges { get; set; }
        public string LibraryCardNum { get; set; }
        public DateTime CardExpiryDate { get; set; }
        public int phoneNum { get; set; }

        private List<string> borrowedBooks = new List<string>();

        private List<string> returnedBooks = new List<string>();

        public User(int userId, string firstName, string lastName, string position, bool isAdmin, List<string> privileges, string libraryCardNum, DateTime cardExpiryDate, int PhoneNum)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            IsAdmin = isAdmin;
            Privileges = privileges;
            LibraryCardNum = libraryCardNum;
            CardExpiryDate = cardExpiryDate;
            phoneNum = PhoneNum;
        }
        public void DisplayUserInfo()
        {
            Console.WriteLine($"User ID:{UserId}");
            Console.WriteLine($"Name:{FirstName} {LastName}");
            Console.WriteLine($"Position:{Position}");
            Console.WriteLine($"Admin:{IsAdmin}");
            Console.WriteLine($"Privileges:{string.Join(",", Privileges)}");
            Console.WriteLine($"Library Card Number:{LibraryCardNum}");
            Console.WriteLine($"Library Expiry Date:{CardExpiryDate.ToShortDateString()}");
        }

        public void Borrowbook(string Booktitle)
        {
            borrowedBooks.Add(Booktitle);
            Console.WriteLine($"{FirstName} {LastName} have borrowed \"{Booktitle}\".");
        }
        public void ReturnBook(string Booktitle)
        {
            returnedBooks.Add(Booktitle);
            borrowedBooks.Remove(Booktitle);
            Console.WriteLine($"{FirstName} {LastName} have returned \"{Booktitle}\".");
        }
        public void printborrowedbooks()
        {
            Console.WriteLine($"User: {FirstName} {LastName}");
            Console.WriteLine("------------------borrrowed books---------------------");
            foreach (string Booktitle in borrowedBooks)
            { Console.WriteLine(Booktitle); }
        }
        public void printreturnedbooks()
        {
            Console.WriteLine($"User: {FirstName} {LastName}");
            Console.WriteLine("------------------returned books---------------------");
            foreach (string Booktitle in returnedBooks)
            { Console.WriteLine(Booktitle); }
        }
    }
    public class LibrarySystem
    {
        private List<Book> books;
        private List<User> users;
        public LibrarySystem()
        {
            users = new List<User>();
            books = new List<Book>();
        }
        public void AddBook(Book book)
        {
            books.Add(book);
        }
        public void RemoveBook(Book book)
        {
            books.Remove(book);
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
        }
        public void SearchBooks(string keyword)
        {
            List<Book> searchResults = books.FindAll(book =>
            book.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1 ||
            book.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1 ||
            book.ISBN.Equals(keyword, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Search results for'{keyword}':");
            foreach (var book in searchResults)
            {
                book.DisplayDetails();

            }
        }
    

    public void CheckOut(User user, Book book)
        {
            if (book.IsAvailable)
            {
                book.IsAvailable = true;
                Console.WriteLine($"Book'{book.Title}'checked out by {user.FirstName}{user.LastName}");
            }
            else
            {
                Console.WriteLine($"Book'{book.Title}'is not available for checkout.");
            }
        }
        public void CheckIn(Book book)
        {
            if (!book.IsAvailable)
            {
                book.IsAvailable = false;
                Console.WriteLine($"Book'{book.Title}'checked in");
            }
            else
            {
                Console.WriteLine($"Book'{book.Title}'is already checked in");
            }
        }

        abstract public class Libararycard
        {
            public abstract void showcardtitle();

        }
        public class Viplibrarycard : Libararycard
        {
            public override void showcardtitle()
            {
                Console.WriteLine("vip card");
            }
        }
        public class userlibrarycard : Libararycard
        {
            public override void showcardtitle()
            {
                Console.WriteLine("user card");
            }
        }

        public class FicBook : Book
        {
            public FicBook(string title, string author, string genre, int copiesAvailable)
            {
                Genre = genre;
                this.Title = title;
                this.Author = author;
                this.Genre = genre;
                this.CopiesAvailable = copiesAvailable;
            }
            public override string GetBookType()
            {
                return "Fiction";
            }
        }
        class Program
        {
            static void Main(string[] args)
            { 
                LibrarySystem librarysystem = new LibrarySystem();
                Book book1 = new Book { Id = "2101", Title = "Avengers", Author = "Stan Lee", ISBN = "1234567890", Genre = "Sci-Fi", IsAvailable = true, CopiesAvailable = 5 };
                Book book2 = new Book { Id = "5634", Title = "Confess", Author = "Lily Collen", ISBN = "0987654321", Genre = "Romance", IsAvailable = true, CopiesAvailable = 3 };
                librarysystem.AddBook(book1);
                librarysystem.AddBook(book2);
                User user1 = new User(1, "Haneen", "Lotfy", "Student", false, new List<string> { "Borrow Books" }, "1234-5674", DateTime.Now.AddYears(4), 123456789);
                User user2 = new User(2, "Roshdy", "Abaza", "Librarian", true, new List<string> { "Borrow Books", "Manage Users" }, "5648-1234", DateTime.Now.AddYears(6), 123456789);
                librarysystem.AddUser(user1);
                librarysystem.AddUser(user2);


            librarysystem.CheckOut(user1, book2);
                book1.DisplayDetails();
                user1.DisplayUserInfo();
                librarysystem.CheckIn(book2);
                Console.ReadKey();
            }
        }



    }
}
