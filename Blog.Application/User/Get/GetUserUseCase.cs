using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses.User;
using Blog.Domain.Repositories.User;
using Blog.Domain.Services;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;

namespace Blog.Application.User.Get;

public interface IGetUserUseCase {
    public Task<GetUserResponse> Execute();
}

public class GetUserUseCase(
    IUserTokenService userTokenService
) : IGetUserUseCase {
    public async Task<GetUserResponse> Execute() {
        var user = await userTokenService.GetUserFromTokenAsync();

        if (user is null) {
            throw new BlogNotFoundException(ErrorMessages.UserNotFound);
        }

        return user.MapToGetResponse();
    }
}