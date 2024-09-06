using Blog.Domain.Communication.Requests.ClassRequests;
using Blog.Domain.Communication.Responses.ClassResponses;

namespace Blog.Application.Mappers;

public static class UserMapper {
    public static Domain.Entities.User MapToUser(this RegisterUserRequest request) {
        var user = new Domain.Entities.User {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };

        return user;
    }

    public static RegisterUserResponse MapToResponse(this Domain.Entities.User user) {
        var response = new RegisterUserResponse(user.Username, user.Email);

        return response;
    }
}