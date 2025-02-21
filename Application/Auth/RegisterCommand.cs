using MediatR;

namespace Application.Auth;

public class RegisterCommand : BaseCommand, IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
}