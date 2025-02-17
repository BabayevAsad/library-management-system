using Api.Libraries;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Library.Queries.GetAll;

public class GetAllLibrariesQueryHandler : IRequestHandler<GetAllLibrariesQuery, List<LibraryListDto>>
{
    private readonly ILibraryRepository _repo;

    public GetAllLibrariesQueryHandler(ILibraryRepository repo, IConfiguration configuration)
    {
        _repo = repo;
    }
        
    public async Task<List<LibraryListDto>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
    { 
        var libraries = await _repo.GetAllAsync();

        var dto = libraries.Where(b => !b.IsDeleted)
            .Select(b => new LibraryListDto
            {
                Id = b.Id,
                Name = b.Name,
                Location = b.Location,
                Count = b.Count,
            }).ToList();

        return dto;
    }
}