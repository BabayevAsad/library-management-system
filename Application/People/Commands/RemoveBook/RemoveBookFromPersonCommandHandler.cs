using Api.Books;
using Api.People;
using Api.PersonBook;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.People.Commands.RemoveBook;

public class RemoveBookFromPersonCommandHandler : IRequestHandler<RemoveBookFromPersonCommand>
{
    private readonly IPersonRepository _repoPerson;
    private readonly IBookRepository _repoBooks;
    private readonly IPersonBookRepository _repoPersonBook;
    private readonly IDistributedCache _cache;


    public RemoveBookFromPersonCommandHandler(IPersonBookRepository repoPersonBook, IBookRepository repoBooks, IPersonRepository repoPerson, IDistributedCache cache)
    {
        _repoPerson = repoPerson;
        _cache = cache;
        _repoBooks = repoBooks;
        _repoPersonBook = repoPersonBook;
    }

    public async Task Handle(RemoveBookFromPersonCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Person)}-{request.Id}");
        
        var book = await _repoBooks.GetByIdAsync(request.BookId);
        var person = await _repoPerson.GetByIdAsync(request.Id);

        var personBook = await _repoPersonBook.GetByPersonIdAsync(person.Id,book.Id);

        await _repoPersonBook.DeleteAsync(personBook);
    }
}