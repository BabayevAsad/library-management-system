using MediatR;

namespace Application.Library.Commands.Create;

public class CreateLibraryCommand : LibraryCommand,IRequest<int>
{
}