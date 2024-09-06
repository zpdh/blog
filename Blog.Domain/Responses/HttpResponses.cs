namespace Blog.Domain.Responses;

public record ErrorResponse {
    public IList<string> ErrorMessages { get; set; }

    public ErrorResponse(IList<string> errorMessages) {
        ErrorMessages = errorMessages;
    }

    public ErrorResponse(string errorMessage) {
        ErrorMessages = [errorMessage];
    }
};

public record TokenErrorResponse : ErrorResponse {
    public bool IsExpired { get; set; }

    public TokenErrorResponse(
        IList<string> errorMessages,
        bool isExpired = false
    ) : base(errorMessages) {
        IsExpired = isExpired;
    }

    public TokenErrorResponse(
        string errorMessage,
        bool isExpired = false
    ) : base(errorMessage) {
        IsExpired = isExpired;
    }
}