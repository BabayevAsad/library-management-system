using MediatR;

namespace Application.Library.Queries.GetById;

public class GetByIdLibraryQuery : BaseDto, IRequest<LibraryDetailsDto>
{
    
}