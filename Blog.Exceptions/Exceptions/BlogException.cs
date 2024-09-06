using System.Net;

namespace Blog.Exceptions.Exceptions;

public abstract class BlogException(string message) : Exception(message) {
    public abstract string GetErrorMessage();
    public abstract HttpStatusCode GetStatusCode();
}