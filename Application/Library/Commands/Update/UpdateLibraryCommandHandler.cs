using Api.Libraries;
using MediatR;

namespace Application.Library.Commands.Update;

public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand>
{
    private ILibraryRepository _repo;

    public UpdateLibraryCommandHandler(ILibraryRepository repo)
    {
        _repo = repo;
    }
    
    public async Task Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
    {
        var library = await _repo.GetByIdAsync(request.Id);

        if (library == null)
        {
            throw new NullReferenceException("Library with that Id is not exist");
        }

        library.Name = request.Name;
        library.Location = request.Location;
        library.Count = request.Count;

        await _repo.UpdateAsync(library);
    }
}