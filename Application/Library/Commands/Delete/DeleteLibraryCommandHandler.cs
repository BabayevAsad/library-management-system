using Api.Libraries;
using Api.LibraryBook;
using MediatR;

namespace Application.Library.Commands.Delete;

public class DeleteLibraryCommandHandler : IRequestHandler<DeleteLibraryCommand>
{
    private readonly ILibraryRepository _repo;
    private readonly ILibraryBookRepository _repoLB;

    public DeleteLibraryCommandHandler(ILibraryRepository repo, ILibraryBookRepository repoLb)
    {
        _repo = repo;
        _repoLB = repoLb;
    }

    public async Task Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
    {
        var library = await _repo.GetByIdAsync(request.Id);
        await _repo.DeleteAsync(library);
    }
}