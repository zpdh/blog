namespace Blog.Domain.Communication.Requests.Post;

public class CreatePostRequest {
    public string Title { get; set; } = string.Empty;
    public string TextContent { get; set; } = string.Empty;

    public CreatePostRequest() {
    }

    public CreatePostRequest(string title, string textContent) {
        Title = title;
        TextContent = textContent;
    }
}