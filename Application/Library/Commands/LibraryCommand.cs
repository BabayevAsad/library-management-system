namespace Application.Library.Commands;

public class LibraryCommand : BaseCommand
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int Count { get; set; }
}