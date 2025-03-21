using Api.Libraries;
using Application.Application.Caching;
using Application.Books.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Library.Queries.GetById;

public class GetByIdLibraryQueryHandler : IRequestHandler<GetByIdLibraryQuery,LibraryDetailsDto>
{
    private readonly ILibraryRepository _repo;
    private readonly IDistributedCache _cache;


    public GetByIdLibraryQueryHandler(ILibraryRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }
    public async Task<LibraryDetailsDto> Handle(GetByIdLibraryQuery request, CancellationToken cancellationToken)
    {
        string key = $"{nameof(Library)}-{request.Id}";

        var library = await _cache.GetOrCreateAsync(key, async token =>
        {
            var library = await _repo.GetByIdAsync(request.Id);
            var libraryDetails = new LibraryDetailsDto()
            {
                Id = library.Id,
                Name = library.Name,
                Location = library.Location,
                Count = library.Count,
                Books = library.Books.Select(b => new BookDto()
                {
                    Name = b.Name,
                    Type = b.Type,
                    PageCount = b.PageCount,
                    Price = b.Price,
                }).ToList()
            };
            
            return libraryDetails;
        });
        
        return library;
    }
}