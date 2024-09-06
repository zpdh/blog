using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Infrastructure.Security.Tokens;

public class JwtTokenHandler(
    ushort expirationInMinutes,
    string signingKey
) : ITokenGenerator, ITokenValidator {
    public string Generate(Guid userId) {
        var claims = new List<Claim> {
            new(ClaimTypes.Sid, userId.ToString())
        };

        var bytes = Encoding.UTF8.GetBytes(signingKey);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public Guid ValidateAndGetUserId(string token) {
        throw new NotImplementedException();
    }
}