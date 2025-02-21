using Api.User;
using Application.Token;
using MediatR;

namespace Application.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private IUserRepository _userRepository;
    private readonly TokenHandler _tokenHandler;

    public LoginCommandHandler(IUserRepository userRepository, TokenHandler tokenHandler)
    {
        _userRepository = userRepository;
        _tokenHandler = tokenHandler;
    }
    
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByNameAsync(request.Username);
        
        var result = user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (result != true)
            return "Token created failed";

        var token = _tokenHandler.CreateToken(user);

        return token.AccessToken;
    }
}