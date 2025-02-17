using Api.Books;
using Api.Libraries;
using Application.Books.Commands.Delete;
using MediatR;

namespace Application.Library.Commands.Delete;

public class DeleteLibraryCommandHandler : IRequestHandler<DeleteLibraryCommand>
{
    private readonly ILibraryRepository _repo;

    public DeleteLibraryCommandHandler(ILibraryRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
    {
        var library = await _repo.GetByIdAsync(request.Id);
        await _repo.DeleteAsync(library);
    }
}