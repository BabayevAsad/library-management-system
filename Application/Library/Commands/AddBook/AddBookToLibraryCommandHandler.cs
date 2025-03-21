using Api.Books;
using Api.Libraries;
using Api.LibraryBook;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Library.Commands.AddBook;

public class AddBookToLibraryCommandHandler : IRequestHandler<AddBookToLibraryCommand>
{
    private readonly ILibraryRepository _repoLibrary;
    private readonly IBookRepository _repoBook;
    private readonly ILibraryBookRepository _repoBookLibrary;
    private readonly IDistributedCache _cache;


    public AddBookToLibraryCommandHandler(ILibraryRepository repoLibrary, IBookRepository repoBook, ILibraryBookRepository repoBookLibrary, IDistributedCache cache)
    {
        _repoLibrary = repoLibrary;
        _repoBook = repoBook;
        _repoBookLibrary = repoBookLibrary;
        _cache = cache;
    }

    public async Task Handle(AddBookToLibraryCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Library)}-{request.Id}");
        
        var book = await _repoBook.GetByIdAsync(request.BookId);
        var library = await _repoLibrary.GetByIdAsync(request.Id);

        var bookLibrary = new LibraryBook()
        {
            BookId = book.Id,
            LibraryId = library.Id,
        };

        await _repoBookLibrary.CreateAsync(bookLibrary);
    }
}