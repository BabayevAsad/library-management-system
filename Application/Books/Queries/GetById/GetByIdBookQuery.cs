using MediatR;

namespace Application.Books.Queries.GetById;

public class GetByIdBookQuery : BaseDto, IRequest<BookDetailsDto>
{
    
}