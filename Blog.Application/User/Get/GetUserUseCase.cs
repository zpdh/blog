using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses.User;
using Blog.Domain.Repositories.User;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;

namespace Blog.Application.User.Get;

public interface IGetUserUseCase {
    public Task<GetUserResponse> Execute(GetUserRequest request);
}

public class GetUserUseCase(
    IUserReadRepository readRepository
) : IGetUserUseCase {
    public async Task<GetUserResponse> Execute(GetUserRequest request) {
        var user = await readRepository.GetUserByIdAsync(request.Id);

        if (user is null) {
            throw new BlogNotFoundException(ErrorMessages.UserNotFound);
        }

        return user.MapToGetResponse();
    }
}