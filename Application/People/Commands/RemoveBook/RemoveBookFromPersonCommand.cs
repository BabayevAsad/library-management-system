using MediatR;

namespace Application.People.Commands.RemoveBook;

public class RemoveBookFromPersonCommand : BaseCommand, IRequest
{
    public int BookId { get; set; }
}