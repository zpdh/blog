namespace Blog.Domain.Communication.Requests.User;

public class GetUserRequest {
    public Guid Id { get; set; }

    public GetUserRequest() {
    }

    public GetUserRequest(Guid id) {
        Id = id;
    }
}