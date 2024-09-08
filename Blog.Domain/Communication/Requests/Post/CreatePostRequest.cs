namespace Blog.Domain.Communication.Requests.Post;

public record CreatePostRequest(string Title, string TextContent);