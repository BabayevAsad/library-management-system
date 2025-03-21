using Api.Books;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Books.Commands.Delete;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _repo;
    private readonly IDistributedCache _cache;


    public DeleteBookCommandHandler(IBookRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Book)}-{request.Id}");

        var book = await _repo.GetByIdAsync(request.Id);
        await _repo.DeleteAsync(book);
    }
}