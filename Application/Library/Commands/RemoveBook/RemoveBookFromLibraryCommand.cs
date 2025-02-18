using MediatR;

namespace Application.Library.Commands.RemoveBook;

public class RemoveBookFromLibraryCommand : BaseCommand, IRequest
{
    public int BookId { get; set; }
}