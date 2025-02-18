using Api.Books;
using Api.People;
using Api.PersonBook;
using MediatR;

namespace Application.People.Commands.RemoveBook;

public class RemoveBookFromPersonCommandHandler : IRequestHandler<RemoveBookFromPersonCommand>
{
    private readonly IPersonRepository _repoPerson;
    private readonly IBookRepository _repoBooks;
    private readonly IPersonBookRepository _repoPersonBook;

    public RemoveBookFromPersonCommandHandler(IPersonBookRepository repoPersonBook, IBookRepository repoBooks, IPersonRepository repoPerson)
    {
        _repoPerson = repoPerson;
        _repoBooks = repoBooks;
        _repoPersonBook = repoPersonBook;
    }

    public async Task Handle(RemoveBookFromPersonCommand request, CancellationToken cancellationToken)
    {
        var book = await _repoBooks.GetByIdAsync(request.BookId);
        var person = await _repoPerson.GetByIdAsync(request.Id);

        var personBook = await _repoPersonBook.GetByPersonIdAsync(person.Id,book.Id);

        await _repoPersonBook.DeleteAsync(personBook);
    }
}