using Api.Books;
using MediatR;

namespace Application.Books.Commands.Update;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private IBookRepository _repo;

    public UpdateBookCommandHandler(IBookRepository repo)
    {
        _repo = repo;
    }
    
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetByIdAsync(request.Id);

        book.Name = request.Name;
        book.Type = request.Type;
        book.PageCount = request.PageCount;
        book.Price = request.Price;
        
        await _repo.UpdateAsync(book);
    }
}