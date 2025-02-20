using Api.Books;
using MediatR;

namespace Application.Books.Commands.Delete;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _repo;

    public DeleteBookCommandHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetByIdAsync(request.Id);
        await _repo.DeleteAsync(book);
    }
}