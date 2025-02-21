using MediatR;

namespace Application.People.Commands.AddBook;

public class AddBookToPersonCommand : BaseCommand, IRequest
{
    public int BookId { get; set; }
}