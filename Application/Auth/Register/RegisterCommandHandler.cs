using Api.Roles;
using Api.User;
using Application.Token;
using MediatR;

namespace Application.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly TokenHandler _tokenHandler;

    public RegisterCommandHandler(IUserRepository userRepository, TokenHandler tokenHandler)
    {
        _userRepository = userRepository;
        _tokenHandler = tokenHandler;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByNameAsync(request.Username);

        if (existingUser != null)
            throw new Exception("User exists");

        var newUser = new User()
        {
            UserName = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = RoleHelper.GetById(request.RoleId),
        };

        await _userRepository.CreateAsync(newUser);
        var token = _tokenHandler.CreateToken(newUser);

        return token.AccessToken;
    }
}