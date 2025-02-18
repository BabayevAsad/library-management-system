using Api.Books;
using Api.People;
using Api.PersonBook;
using MediatR;

namespace Application.People.Commands.AddBook;

public class AddBookToPersonCommandHandler : IRequestHandler<AddBookToPersonCommand>
{
    private readonly IPersonRepository _repoPerson;
    private readonly IBookRepository _repoBooks;
    private readonly IPersonBookRepository _repoPersonBook;

    public AddBookToPersonCommandHandler(IPersonBookRepository repoPersonBook, IBookRepository repoBooks, IPersonRepository repoPerson)
    {
        _repoPerson = repoPerson;
        _repoBooks = repoBooks;
        _repoPersonBook = repoPersonBook;
    }

    public async Task Handle(AddBookToPersonCommand request, CancellationToken cancellationToken)
    {
        var book = await _repoBooks.GetByIdAsync(request.BookId);
        var person = await _repoPerson.GetByIdAsync(request.Id);
        
        var personBook = new PersonBook()
        {
            BookId = request.BookId,
            PersonId = request.Id
        };

        await _repoPersonBook.CreatAsync(personBook);
    }
}