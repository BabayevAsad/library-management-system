using Api.People;
using Application.Application.Caching;
using Application.Books.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.People.Queries.GetById;

public class GetByIdPersonQueryHandler : IRequestHandler<GetByIdPersonQuery, PersonDetailsDto>
{
    private readonly IPersonRepository _repo;
    private readonly IDistributedCache _cache;

    public GetByIdPersonQueryHandler(IPersonRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task<PersonDetailsDto> Handle(GetByIdPersonQuery request, CancellationToken cancellationToken)
    {
        string key = $"{nameof(Person)}-{request.Id}";

        var person = await _cache.GetOrCreateAsync(key, async token =>
        {
            var person = await _repo.GetByIdAsync(request.Id);

            var personDetail = new PersonDetailsDto()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                FatherName = person.FatherName,
                BirthDate = person.BirthDate,
                FinNumber = person.FinNumber,
                GenderId = (Gender)GenderHelper.GetById(person.GenderId),
                Books = person.Books.Select(b => new BookDto()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Type = b.Type,
                    PageCount = b.PageCount,
                    Price = b.Price,
                }).ToList()
            };

            return personDetail;
        });

        return person; 
    } 
}