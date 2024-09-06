namespace Blog.Domain.Communication.Requests.User;

public class LoginUserRequest {
    public required string Email { get; set; }
    public required string Password { get; set; }

    public LoginUserRequest() {
    }

    public LoginUserRequest(string email, string password) {
        Email = email;
        Password = password;
    }
}