using Api.Books;
using Api.People;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Books.Commands.Update;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private IBookRepository _repo;
    private readonly IDistributedCache _cache;


    public UpdateBookCommandHandler(IBookRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }
    
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetByIdAsync(request.Id);
        
        await _cache.RemoveAsync($"{nameof(Book)}-{request.Id}");

        foreach (var library in book.Libraries)
        {
            await _cache.RemoveAsync($"{nameof(Library)}-{library.Id}");
        }

        foreach (var person in book.People)
        {
            await _cache.RemoveAsync($"{nameof(Person)}-{person.Id}");
        }

        book.Name = request.Name;
        book.Type = request.Type;
        book.PageCount = request.PageCount;
        book.Price = request.Price;
        
        await _repo.UpdateAsync(book);
    }
}