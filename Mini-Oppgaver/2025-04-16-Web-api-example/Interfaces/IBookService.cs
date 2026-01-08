namespace _2025_04_16_Web_api_example.Interfaces;

public interface IBookService
{
  bool AddBook(string title, string author, string section, string shelf);
  DateTime? BorrowBook(int id);
  bool ReturnBook(int id);
  bool DeleteBook(int id);
  List<IBook> GetAllBooks();
  IBook? GetBookById(int id);
  List<IBook> GetAllBorrowedBooks();
  List<IBook> GetBooksWithExpiredDueDate();
  DateTime MarkAsBorrowed(IBook book);
  void MarkAsReturned(IBook book);
}
