using Api.Books;
using Api.People;
using Application.People.Queries;
using MediatR;

namespace Application.Books.Queries.GetById;

public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQuery,BookDetailsDto>
{
    private readonly IBookRepository _repo;

    public GetByIdBookQueryHandler(IBookRepository repo)
    {
        _repo = repo;
    }
    public async Task<BookDetailsDto> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
    {
        var book =  await _repo.GetByIdAsync(request.Id);
        
        var bookDetails = new BookDetailsDto()
        {
            Id = book.Id,
            Name = book.Name,
            Type = book.Type,
            PageCount = book.PageCount,
            Price = book.Price,
            People = book.People.Select(p=>new PersonDto()
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
    }
}