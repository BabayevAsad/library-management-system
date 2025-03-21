using Api.Books;
using Api.Libraries;
using Api.LibraryBook;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Library.Commands.RemoveBook;

public class RemoveBookFromLibraryCommandHandler : IRequestHandler<RemoveBookFromLibraryCommand>
{
    private readonly ILibraryRepository _repoLibrary;
    private readonly IBookRepository _repoBooks;
    private readonly ILibraryBookRepository _repoLibraryBook;
    private readonly IDistributedCache _cache;

    public RemoveBookFromLibraryCommandHandler(ILibraryBookRepository repoLibraryBook, IBookRepository repoBooks, ILibraryRepository repoLibrary, IDistributedCache cache)
    {
        _repoLibrary = repoLibrary;
        _cache = cache;
        _repoBooks = repoBooks;
        _repoLibraryBook = repoLibraryBook;
    }

    public async Task Handle(RemoveBookFromLibraryCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Library)}-{request.Id}");

        var book = await _repoBooks.GetByIdAsync(request.BookId);
        var library = await _repoLibrary.GetByIdAsync(request.Id);

        var libraryBook = await _repoLibraryBook.GetByLibraryIdBookIdAsync(library.Id,book.Id);

        await _repoLibraryBook.DeleteAsync(libraryBook);
    }
}