namespace Application.Library.Queries;

public class LibraryDto : BaseDto
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int Count { get; set; }
}