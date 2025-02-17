using MediatR;

namespace Application.Books.Queries.GetAll;

public class GetAllBooksQuery : IRequest<List<BookListDto>> 
{
    
}