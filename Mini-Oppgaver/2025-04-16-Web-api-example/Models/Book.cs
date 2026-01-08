
using _2025_04_16_Web_api_example.Interfaces;

namespace _2025_04_16_Web_api_example.Models;

public class Book(int id, string title, string author, string section, string shelf) : IBook
{
  public int Id { get; init; } = id;
  public string Title { get; set; } = title;
  public string Author { get; set; } = author;
  public bool IsBorrowed { get; set; }
  public DateTime? DueDate { get; set; }
  public string Section { get; set; } = section;
  public string Shelf { get; set; } = shelf;
}