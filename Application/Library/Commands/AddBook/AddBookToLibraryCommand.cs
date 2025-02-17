using MediatR;

namespace Application.Library.Commands.AddBook;

public class AddBookToLibraryCommand : BaseCommand, IRequest
{
    public int BookId { get; set; }
}