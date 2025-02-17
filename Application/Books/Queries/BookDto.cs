
namespace Application.Books.Queries;

public class BookDto : BaseDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int PageCount { get; set; }
    public int Price { get; set; }
}