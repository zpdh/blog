namespace Blog.Domain.Communication.Requests.User;

public class RegisterUserRequest {
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public RegisterUserRequest() {
    }

    public RegisterUserRequest(string username, string email, string password) {
        Username = username;
        Email = email;
        Password = password;
    }
};