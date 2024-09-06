using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses.User;

namespace Blog.Application.Mappers;

public static class UserMapper {

    #region Register Classes

    public static Domain.Entities.User MapToUser(this RegisterUserRequest request) {
        var user = new Domain.Entities.User {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };

        return user;
    }

    public static RegisterUserResponse MapToRegisterResponse(this Domain.Entities.User user, string token) {
        var response = new RegisterUserResponse(user.Username, user.Email, token);

        return response;
    }

    #endregion

    #region Login Classes

    public static void MapToLoginResponse(this Domain.Entities.User user) {
        // TODO
    }

    #endregion

}