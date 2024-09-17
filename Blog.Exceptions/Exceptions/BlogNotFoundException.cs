using System.Net;

namespace Blog.Exceptions.Exceptions;

public class BlogNotFoundException(string message) : BlogException(message) {
    public override IList<string> GetErrorMessages() {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.NotFound;
    }
}