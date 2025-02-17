using Api.Books;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Books.Commands.Delete;

public class DeleteBookCommandHandler : DeleteBookCommand,IRequestHandler<DeleteBookCommand>
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