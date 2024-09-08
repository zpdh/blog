using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blog.Domain.Entities;
using Blog.Domain.Security.Tokens;
using Blog.Domain.Services;
using Blog.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Services;

public class UserTokenService(
    DataContext context,
    ITokenProvider tokenProvider
) : IUserTokenService {

    public Task<User> GetUserFromTokenAsync() {
        var token = tokenProvider.GetValue();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var id = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);

        return context.Users
            .FirstAsync(u => u.Id == id);
    }
}