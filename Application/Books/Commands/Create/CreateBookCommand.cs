using MediatR;

namespace Application.Books.Commands.Create;

public class CreateBookCommand : BookCommand,IRequest<int>
{
}