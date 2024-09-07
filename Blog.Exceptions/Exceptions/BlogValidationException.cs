using System.Net;

namespace Blog.Exceptions.Exceptions;

public class BlogValidationException(IList<string> messages) : BlogException(string.Empty) {
    public override IList<string> GetErrorMessages() {
        return messages;
    }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.BadRequest;
    }
}