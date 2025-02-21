
namespace Application.Books.Commands;

public class BookCommand : BaseCommand
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int PageCount { get; set; }
    public int Price { get; set; }
}