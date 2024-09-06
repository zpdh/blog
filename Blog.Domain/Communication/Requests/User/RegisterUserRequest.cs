namespace Blog.Domain.Communication.Requests.User;

public class RegisterUserRequest {
    public required string Username { get; set; }
    public required string Email { get; set; }

    public required string Password { get; set; }

    public RegisterUserRequest() {
    }

    public RegisterUserRequest(string username, string email, string password) {
        Username = username;
        Email = email;
        Password = password;
    }
};