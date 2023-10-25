using System;
using System.Collections.Generic;
public class Borrow
{
    public string BorrowTitle { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateBorrowed { get; set; }
    public DateTime ReturnDate { get; set; }
    public Borrow(string borrowtitle, string name, string surname, DateTime dateborrowed, DateTime returndate)
    {
        BorrowTitle = borrowtitle;
        Name = name;
        Surname = surname;
        DateBorrowed = dateborrowed;
        ReturnDate = returndate;
    }
}

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearReleased { get; set; }
    public string Publisher { get; set; }
    public string Genre { get; set; }
    public string ISBN { get; set; }
    public int Stock { get; set; }
    public Book(string title, string author, int yearreleased, string publisher, string genre, string isbn, int stock)
    {
        Title = title;
        Author = author;
        YearReleased = yearreleased;
        Publisher = publisher;
        Genre = genre;
        ISBN = isbn;
        Stock = stock;
    }
}
public static class LibrarySingleton
{
    public static Library library { get; } = new Library();
}
public class Library
{
    public List<Borrow> borrows { get; set; } = new List<Borrow>();
    public List<Book> books { get; set; } = new List<Book>();
    public void CreateBooks()
    {
        books.Add(new Book("Game of Thrones", "J.K. Rowling", 1678, "Voyager Books", "Romance", "9618763470245", 20));
        books.Add(new Book("Diary of a Wimpy Adult", "George R.R. Martin", 2010, "Bloomsbury", "Drama", "2146198400712", 5));
        books.Add(new Book("Harry Potter and the Lost Frog", "Robert Lewandowski", 1993, "Amulet Books", "Comedy", "9170949062345", 0));
        books.Add(new Book("Game of Percy Jackson", "Terry Prachett", 2005, "Fabryka Słów", "Romance", "0103663454162", 27)); 
        books.Add(new Book("20,000 Leagues Under the Sea", "J.R.R. Tolkien", 1872, "Bloomsbury", "Fantasy", "4824083002916", 54));
    }

    public void AddBook(string title, string author, int yearreleased, string publisher, string genre, string isbn, int stock)
    {
        Book book = new Book(title, author, yearreleased, publisher, genre, isbn, stock);
        books.Add(book);
    }
    public void DeleteBook(string title)
    {
        Book bookToRemove = books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
        }
    }

    public void BorrowBook(string title, string name, string surname)
    {
        Book bookToBorrow = books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToBorrow.Stock>0)
        {
            bookToBorrow.Stock --;
            Borrow borrower = new Borrow(bookToBorrow.Title, name, surname, DateTime.Now, DateTime.Now.AddDays(5));
            borrows.Add(borrower);
            Console.WriteLine($"{name} has borrowed '{title}'.");
        }
        else
        {
            Console.WriteLine($"'{title}' nie jest dostępny do wypożyczenia.");
        }
    }
    public List<Book> SearchBooks(string searchType, string query)
    {
        List<Book> results = new List<Book>();

        switch (searchType.ToLower())
        {
            case "tytul":
                results = books.FindAll(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase));
                break;
            case "autor":
                results = books.FindAll(book => book.Author.Contains(query, StringComparison.OrdinalIgnoreCase));
                break;
            case "gatunek":
                results = books.FindAll(book => book.Genre.Contains(query, StringComparison.OrdinalIgnoreCase));
                break;
            default:
                Console.WriteLine("Nieprawidłowy tytuł. \n");
                break;
        }
        return results;
    }
    public void EditBook(string title, string editType, string modify)
    {
        Book bookToEdit = books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToEdit != null)
        {
            switch (editType.ToLower())
            {
                case "tytul":
                    bookToEdit.Title = modify;
                    break;
                case "autor":
                    bookToEdit.Author = modify;
                    break;
                case "rok":
                    bookToEdit.YearReleased = Convert.ToInt32(modify);
                    break;
                case "wydawca":
                    bookToEdit.Publisher = modify;
                    break;
                case "gatunek":
                    bookToEdit.Genre = modify;
                    break;
                case "isbn":
                    bookToEdit.ISBN = modify;
                    break;
                case "magazyn":
                    bookToEdit.Stock = Convert.ToInt32(modify);
                    break;
                default:
                    Console.WriteLine("Niepoprawny parametr. \n");
                    break;
            }
        }
        else
            Console.WriteLine("Tytuł nie został znaleziony. \n");
    }
    public void ShowBook()
    {
        Console.WriteLine("Dostępne książki: \n" +
            "\n" +
            "Tytuł | Autor | Rok Wydania | Wydawca | Gatunek | ISBN | Ilość w magazynie \n");
        foreach (var book in books)
        {
            if (book.Stock > 0)
            {
                Console.WriteLine($"{book.Title} | {book.Author} | {book.YearReleased} | {book.Publisher} | {book.Genre} | {book.ISBN} | {book.Stock} \n");
            }
        }
    }
    public void ShowBorrows()
    {
        Console.WriteLine("Lista wypożyczających: \n" +
            "\n" +
            "Tytuł | Imię | Nazwisko | Data wypożyczenia | Data zwrotu \n");
        foreach (var borrow in borrows)
        {
                Console.WriteLine($"{borrow.BorrowTitle} | {borrow.Name} | {borrow.Surname} | {borrow.DateBorrowed} | {borrow.ReturnDate} \n");
        }
    }
}
class Program
{
    static void Main()
    {
        LibrarySingleton.library.CreateBooks();
        Login.PokazLogin();
    }
}
public class Login
{
    public static void PokazLogin()
    {
        Console.WriteLine("Witaj w Bibliotece SAN! \n" +
            "\n" +
            "Zaloguj się jako: \n" +
            "\n" +
            "1: Pracownik \n" +
            "\n" + 
            "2: Użytkownik \n");
        LoginScreen();
    }
    static void LoginScreen()
    {
        string klawisz = Console.ReadLine();
        while (true)
        {
            int value;
            if (int.TryParse(klawisz, out value))
            {
                switch (value)
                {
                    case 1:
                        Console.Clear();
                        Pracownik.PracownikLogin();
                        break;
                    case 2:
                        Console.Clear();
                        Uzytkownik.UzytkownikLogin();
                        break;
                    default:
                        Console.WriteLine("Zły klawisz");
                        System.Threading.Thread.Sleep(500);
                        Console.Clear();
                        PokazLogin();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy input.");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                PokazLogin();
            }
        }
    } 
}
public class Pracownik
{
    internal static void PracownikLogin()
    {
        Console.WriteLine("Podaj hasło: \n");
        string password = Console.ReadLine();
        if (password == "biblioteka")
        {
            Console.Clear();
            OptionsPracownik();
        }
        else
        {
            Console.WriteLine("Nieprawidłowe hasło. \n");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
            PracownikLogin();
        }
    }
    public static void OpcjePracownik()
    {
        Console.WriteLine("Wybierz opcje: \n" +
            "\n" +
            "1: Dodaj książkę \n" +
            "\n" +
            "2: Usuń książkę \n" +
            "\n" +
            "3: Sprawdź spis książek \n" +
            "\n" +
            "4: Wyszukaj książkę \n" +
            "\n" +
            "5: Modyfikuj książkę \n" +
            "\n" +
            "6: Pokaż listę wypożyczających \n" +
            "\n" +
            "7: Wyloguj \n" +
            "\n");
    }
    public static void OptionsPracownik()
    {
        while (true)
        {    
            OpcjePracownik();
            string klawisz = Console.ReadLine();
            int value;
            if (int.TryParse(klawisz, out value))
            {
                switch (value)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Tytuł: \n");
                        string tytul = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("Autor: \n");
                        string autor = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("Rok Wydania: \n");
                        int rok = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n");
                        Console.WriteLine("Wydawca: \n");
                        string wydawca = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("Gatunek: \n");
                        string gatunek = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("ISBN: \n");
                        string isbn = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("instock \n");
                        int instock = Convert.ToInt32(Console.ReadLine());
                        LibrarySingleton.library.AddBook(tytul, autor, rok, wydawca, gatunek, isbn, instock);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Podaj tytuł książki do usunięcia: \n");
                        string title = Console.ReadLine();
                        LibrarySingleton.library.DeleteBook(title);
                        break;
                    case 3:
                        Console.Clear();
                        LibrarySingleton.library.ShowBook();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Wybierz: tytul, autor, gatunek \n");
                        string searchType = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę poszukiwanego wyboru: \n");
                        string query = Console.ReadLine();
                        List<Book> searchResults = LibrarySingleton.library.SearchBooks(searchType, query);
                        foreach (var book in searchResults)
                        {
                            Console.WriteLine($"{book.Title} by {book.Author} ({book.Genre}) \n");
                        }
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Podaj tytuł książki którą chcesz modyfikować: \n");
                        title = Console.ReadLine();
                        Console.WriteLine("\n" +
                            "Podaj parametr do modyfikacji (tytul, autor, rok, wydawca, gatunek, isbn, magazyn): \n");
                        string editType = Console.ReadLine();
                        Console.WriteLine("\n" +
                            "Wprowadź nowe dane: \n");
                        string modify = Console.ReadLine();
                        LibrarySingleton.library.EditBook(title, editType, modify);
                        break;
                    case 6:
                        Console.Clear();
                        LibrarySingleton.library.ShowBorrows();
                        break;
                    case 7:
                        Console.Clear();
                        Login.PokazLogin();
                        break;
                    default:
                        Console.WriteLine("Zły klawisz");
                        System.Threading.Thread.Sleep(500);
                        Console.Clear();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy input.");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }
        }
    }
}
public class Uzytkownik
{
    public static void UzytkownikLogin()
    {
        Console.WriteLine("Podaj imię: ");
        string WypozyczImie = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Podaj nazwisko: ");
        string WypozyczNazwisko = Console.ReadLine();
        OptionsUzytkownik(WypozyczImie, WypozyczNazwisko);
    }
    static void OpcjeUzytkownik()
    {
        Console.WriteLine("Wybierz opcje: \n" +
            "\n" +
            "1. Sprawdź spis książek \n" +
            "\n" +
            "2. Wyszukaj książkę \n" +
            "\n" +
            "3. Wypożycz książkę \n" +
            "\n" +
            "4. Wyloguj \n" +
            "\n");
    }
    static void OptionsUzytkownik(string WypozyczImie, string WypozyczNazwisko)
    {
        while (true)
        {
            OpcjeUzytkownik();
            string klawisz = Console.ReadLine();
            int value;
            if (int.TryParse(klawisz, out value))
            {
                switch (value)
                {
                    case 1:
                        Console.Clear();
                        LibrarySingleton.library.ShowBook();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Wybierz: tytul, autor, gatunek \n");
                        string searchType = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę poszukiwanego wyboru: \n");
                        string query = Console.ReadLine();
                        List<Book> searchResults = LibrarySingleton.library.SearchBooks(searchType, query);
                        foreach (var book in searchResults)
                        {
                            Console.WriteLine($"{book.Title} by {book.Author} ({book.Genre}) \n");
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Podaj tytuł książki do wypożyczenia: \n");
                        string title = Console.ReadLine();
                        LibrarySingleton.library.BorrowBook(title, WypozyczImie, WypozyczNazwisko) ;
                        break;
                    case 4:
                        Console.Clear();
                        Login.PokazLogin();
                        break;
                    default:
                        Console.WriteLine("Zły klawisz");
                        System.Threading.Thread.Sleep(500);
                        Console.Clear();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy input.");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }
        }
    }
}