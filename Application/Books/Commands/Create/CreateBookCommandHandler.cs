using Api.Books;
using MediatR;

namespace Application.Books.Commands.Create;

internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IBookRepository _repo;
    
    public CreateBookCommandHandler(IBookRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            Id = request.Id,
            Name = request.Name,
            Type = request.Type,
            PageCount = request.PageCount,
            Price = request.Price,
        };

        await _repo.CreateAsync(book);

        return book.Id;
    }
}