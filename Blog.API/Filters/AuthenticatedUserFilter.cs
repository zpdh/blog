using Blog.Domain.Communication.Responses;
using Blog.Domain.Repositories.User;
using Blog.Domain.Security.Tokens;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Blog.API.Filters;

public class AuthenticatedUserFilter(
    ITokenValidator tokenValidator,
    IUserReadRepository readRepository
) : IAsyncAuthorizationFilter {

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
        try {
            var token = GetTokenFromHeaders(context);
            var userId = tokenValidator.ValidateAndGetUserId(token);

            var exists = await readRepository.UserWithIdExistsAsync(userId);

            if (!exists) {
                throw new BlogAuthenticationException(ExceptionMessages.NoPermissionsException);
            }
        } catch (BlogAuthenticationException e) {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(e.Message));
        } catch (SecurityTokenExpiredException) {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse("Expired token"));
        } catch {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(ExceptionMessages.NoPermissionsException));
        }
    }

    private static string GetTokenFromHeaders(AuthorizationFilterContext context) {
        var authorizationHeaders = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeaders)) {
            throw new BlogAuthenticationException(ExceptionMessages.NoTokenException);
        }

        // Get token from authorization headers
        var token = authorizationHeaders["Bearer ".Length..].Trim();

        return token;
    }
}