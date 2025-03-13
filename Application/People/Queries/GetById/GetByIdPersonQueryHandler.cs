﻿using Api.People;
using Application.Books.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.People.Queries.GetById;

public class GetByIdPersonQueryHandler : IRequestHandler<GetByIdPersonQuery, PersonDetailsDto>
{
    private readonly IPersonRepository _repo;

    public GetByIdPersonQueryHandler(IPersonRepository repo)
    {
        _repo = repo;
    }

    public async Task<PersonDetailsDto> Handle(GetByIdPersonQuery request, CancellationToken cancellationToken)
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
            Books =person.Books.Select(b => new BookDto()
                { 
                    Name = b.Name,
                    Type = b.Type,
                    PageCount = b.PageCount,
                    Price = b.Price,
                }).ToList()
        };
        return personDetail;
    }
}