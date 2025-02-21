using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Token;

public class TokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateToken(User? user)
    {
        var token = new Token();

        var a = _configuration["Token:SecurityKey"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(a));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(_configuration["Token:Expiration"]));

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, user.Role.ToString()),
        };
        
        JwtSecurityToken jwtSecurityToken = new(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: token.Expiration,
            notBefore: DateTime.Now,
            claims: claims,  
            signingCredentials: credentials
        );
        
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

        var numbers = new byte[32];

        using var random = RandomNumberGenerator.Create();

        random.GetBytes(numbers);
        token.RefreshToken = Convert.ToBase64String(numbers);
        
        return token;
    }
}