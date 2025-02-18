using Api.Books;
using Api.Libraries;
using Api.LibraryBook;
using MediatR;

namespace Application.Library.Commands.RemoveBook;

public class RemoveBookFromLibraryCommandHandler : IRequestHandler<RemoveBookFromLibraryCommand>
{
    private readonly ILibraryRepository _repoLibrary;
    private readonly IBookRepository _repoBooks;
    private readonly ILibraryBookRepository _repoLibraryBook;

    public RemoveBookFromLibraryCommandHandler(ILibraryBookRepository repoLibraryBook, IBookRepository repoBooks, ILibraryRepository repoLibrary)
    {
        _repoLibrary = repoLibrary;
        _repoBooks = repoBooks;
        _repoLibraryBook = repoLibraryBook;
    }

    public async Task Handle(RemoveBookFromLibraryCommand request, CancellationToken cancellationToken)
    {
        var book = await _repoBooks.GetByIdAsync(request.BookId);
        var library = await _repoLibrary.GetByIdAsync(request.Id);

        var libraryBook = await _repoLibraryBook.GetByLibraryIdBookIdAsync(library.Id,book.Id);

        await _repoLibraryBook.DeleteAsync(libraryBook);
    }
}