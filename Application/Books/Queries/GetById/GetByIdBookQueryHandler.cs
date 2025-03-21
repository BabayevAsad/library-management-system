using Api.Books;
using Api.People;
using Application.Application.Caching;
using Application.People.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Books.Queries.GetById;

public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQuery, BookDetailsDto>
{
    private readonly IBookRepository _repo;
    private readonly IDistributedCache _cache;


    public GetByIdBookQueryHandler(IBookRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task<BookDetailsDto> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
    {
        string key = $"{nameof(Book)}-{request.Id}";

        var book = await _cache.GetOrCreateAsync(key, async token =>
        {
            var book = await _repo.GetByIdAsync(request.Id);

            var bookDetails = new BookDetailsDto()
            {
                Id = book.Id,
                Name = book.Name,
                Type = book.Type,
                PageCount = book.PageCount,
                Price = book.Price,
                People = book.People.Select(p => new PersonDto()
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    FatherName = p.FatherName,
                    BirthDate = p.BirthDate,
                    GenderId = (Gender)GenderHelper.GetById(p.GenderId),
                    FinNumber = p.FinNumber
                }).ToList()
            };

            return bookDetails;
        });

        return book;
    }
}