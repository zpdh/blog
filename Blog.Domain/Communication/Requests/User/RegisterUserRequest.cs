namespace Blog.Domain.Communication.Requests.User;

public class RegisterUserRequest {
    public required string Username { get; init; }
    public required string Email { get; init; }

    public required string Password { get; init; }

    public RegisterUserRequest() {
    }

    public RegisterUserRequest(string username, string email, string password) {
        Username = username;
        Email = email;
        Password = password;
    }
};