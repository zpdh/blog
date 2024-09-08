using System.Net;

namespace Blog.Exceptions.Exceptions;

public class BlogAuthenticationException(string message) : BlogException(message) {
    public override IList<string> GetErrorMessages() {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.Unauthorized;
    }
}