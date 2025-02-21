using Api.Books;
using Api.Libraries;
using Application.Books.Commands.Create;
using MediatR;

namespace Application.Library.Commands.Create;

internal class CreateLibraryCommandHandler : IRequestHandler<CreateLibraryCommand, int>
{
    private readonly ILibraryRepository _repo;
    
    public CreateLibraryCommandHandler(ILibraryRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<int> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
    {
        var library = new Api.Libraries.Library()
        {
            Id = request.Id,
            Name = request.Name,
            Location = request.Location,
            Count = request.Count,
        };

        await _repo.CreateAsync(library);

        return library.Id;
    }
}