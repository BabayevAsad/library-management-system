using Api.Libraries;
using Application.Books.Queries;
using MediatR;

namespace Application.Library.Queries.GetById;

public class GetByIdLibraryQueryHandler : IRequestHandler<GetByIdLibraryQuery,LibraryDetailsDto>
{
    private readonly ILibraryRepository _repo;

    public GetByIdLibraryQueryHandler(ILibraryRepository repo)
    {
        _repo = repo;
    }
    public async Task<LibraryDetailsDto> Handle(GetByIdLibraryQuery request, CancellationToken cancellationToken)
    {
        var library =  await _repo.GetByIdAsync(request.Id);
        
        var libraryDetails = new LibraryDetailsDto()
        {
            Id = library.Id,
            Name = library.Name,
            Location = library.Location,
            Count = library.Count,
            Books = library.Books.Select(b=>new BookDto()
            {
                Name = b.Name,
                Type = b.Type,
                PageCount = b.PageCount,
                Price = b.Price,
            }).ToList()
        };
        return libraryDetails;
    }
}