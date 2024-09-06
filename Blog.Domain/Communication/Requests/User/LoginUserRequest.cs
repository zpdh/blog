namespace Blog.Domain.Communication.Requests.User;

public class LoginUserRequest {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public LoginUserRequest() {
    }

    public LoginUserRequest(string email, string password) {
        Email = email;
        Password = password;
    }
}