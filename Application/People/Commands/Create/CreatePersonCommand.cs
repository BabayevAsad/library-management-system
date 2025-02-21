using MediatR;

namespace Application.People.Commands.Create;

public class CreatePersonCommand : PersonCommand, IRequest<int>
{
}