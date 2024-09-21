namespace Blog.Domain.Communication.Requests.User;

public class ChangeUsernameUserRequest {
    public string NewName { get; set; } = string.Empty;

    public ChangeUsernameUserRequest() {
    }

    public ChangeUsernameUserRequest(string newName) {
        NewName = newName;
    }
}