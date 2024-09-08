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

        var securityKey = GetSecurityKey();

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SymmetricSecurityKey GetSecurityKey() {

        var bytes = Encoding.UTF8.GetBytes(signingKey);

        return new SymmetricSecurityKey(bytes);
    }

    public Guid ValidateAndGetUserId(string token) {
        var validationParameters = new TokenValidationParameters {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = GetSecurityKey(),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        var userId = principal.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(userId);
    }
}