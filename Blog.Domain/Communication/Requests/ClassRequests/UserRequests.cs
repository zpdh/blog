namespace Blog.Domain.Communication.Requests.ClassRequests;

public record RegisterUserRequest(string Username, string Email, string Password);