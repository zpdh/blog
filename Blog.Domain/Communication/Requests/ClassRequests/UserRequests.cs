namespace Blog.Domain.Communication.Requests.ClassRequests;

public record UserCreationRequest(string Username, string Email, string Password);