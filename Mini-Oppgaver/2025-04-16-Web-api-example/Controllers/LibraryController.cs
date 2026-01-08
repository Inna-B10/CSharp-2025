using _2025_04_16_Web_api_example.Models;
using Core.Classes.Services;
using Core.Classes.Views;
using Core.Interfaces;

namespace _2025_04_16_Web_api_example.Controllers;

public class BookController(BookService service, ViewGenerator view) : IBookController
{
  private readonly BookService _service = service;
  private readonly ViewGenerator _view = view;
  public void Run()
  {
    while (true)
    {
      Console.Clear();
      _view.ViewMenu();
      int userChoice;
      while (true)
      {
        var input = _view.GetValidInput("Enter your choice: ");
        if (int.TryParse(input, out userChoice))
        {
          break;
        }
        Console.WriteLine("Invalid input. Please enter a whole number.");
      }
      switch (userChoice)
      {
        case 1:
          AddBook();
          break;
        case 2:
          BorrowBook();
          break;
        case 3:
          ReturnBook();
          break;
        case 4:
          DeleteBook();
          break;
        case 5:
          GetAllBooks();
          break;
        case 6:
          GetBookById();
          break;
        case 7:
          GetAllBorrowedBooks();
          break;
        case 8:
          GetBooksWithExpiredDueDate();
          break;
        case 0:
          return;
        default:
          Console.WriteLine("Invalid Choice. Press any key to continue...");
          Console.ReadKey();
          break;
      }
    }
  }

  //* --------------------------------- AddBook -------------------------------- */
  private void AddBook()
  {
    var title = _view.GetValidInput("Enter book's title: ");
    var author = _view.GetValidInput("Enter author's name: ");
    var section = _view.GetValidInput("Enter section (e.g. Fiction, Science): ");
    var shelf = _view.GetValidInput("Enter shelf (e.g. A1, A2, B12): ").ToUpper();

    var result = _service.AddBook(title, author, section, shelf);
    Console.WriteLine(result ? "Book successfully added!" : "Failed to add book.");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ------------------------------- BorrowBook ------------------------------- */
  private void BorrowBook()
  {
    string input = _view.GetValidInput("Enter valid numeric id of the book you want to borrow: ");
    int id;
    while (!int.TryParse(input, out id))
    {
      input = _view.GetValidInput("Invalid input. Please enter valid numeric whole number: ");
    }
    var result = _service.BorrowBook(id);
    if (result == null)
    {
      Console.WriteLine("Cannot register current book as borrowed. Take contact with Admin.");
    }
    else
    {
      Console.WriteLine($"The book with id={id} is borrowed due {result.Value.ToShortDateString()}");
      //NB result.Value because result nullable. When result==null filtered before and here can uses without additional checking.
      //NB can checks with result?.ToShortDateString() ?? "No date"
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ------------------------------- ReturnBook ------------------------------- */
  private void ReturnBook()
  {
    string input = _view.GetValidInput("Enter valid numeric id of the book you want to return: ");
    int id;
    while (!int.TryParse(input, out id))
    {
      input = _view.GetValidInput("Invalid input. Please enter valid numeric whole number: ");
    }
    var result = _service.ReturnBook(id);
    if (result)
    {
      Console.WriteLine($"The book with id={id} successfully returned. Thank you!");

    }
    else
    {
      Console.WriteLine("Cannot register current book as returned. Take contact with Admin.");
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ------------------------------- DeleteBook ------------------------------- */
  private void DeleteBook()
  {
    var input = _view.GetValidInput("Enter Id for the book you want to delete: ");

    int id;
    while (!int.TryParse(input, out id))
    {
      input = _view.GetValidInput("Invalid input. Please enter valid Id as whole number: ");
    }

    var result = _service.DeleteBook(id);

    if (result)
    {
      Console.WriteLine($"Book with id={id} successfully deleted.");
    }
    else
    {
      Console.WriteLine($"Failed to delete the book with id={id}.");
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ------------------------------- GetAllBooks ------------------------------ */
  private void GetAllBooks()
  {
    var books = _service.GetAllBooks();
    _view.ViewBooksList(books);
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ------------------------------- GetBookById ------------------------------ */
  private void GetBookById()
  {
    var bookId = _view.GetValidInput("Enter book Id to get details: ");
    int id;
    while (!int.TryParse(bookId, out id))
    {
      bookId = _view.GetValidInput("Invalid input. Enter numeric whole number: ");
    }
    var book = _service.GetBookById(id);

    _view.ViewBookDetails(book, id);
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ---------------------------- GetAllBorrowedBooks --------------------------- */
  private void GetAllBorrowedBooks()
  {
    var books = _service.GetAllBorrowedBooks();
    _view.ViewBooksList(books);
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }

  //* ----------------------- GetBooksWithExpiredDueDate ----------------------- */
  private void GetBooksWithExpiredDueDate()
  {
    var books = _service.GetBooksWithExpiredDueDate();
    _view.ViewBooksList(books);
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }
}