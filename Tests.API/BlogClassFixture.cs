using System.Net.Http.Json;

namespace Tests.API;

public class BlogClassFixture : IClassFixture<BlogWebApplicationFactory> {
    private readonly HttpClient _httpClient;

    protected BlogClassFixture(BlogWebApplicationFactory factory) {
        _httpClient = factory.CreateClient();
    }

    /// <summary>
    /// Method to send HTTP Requests to our API
    /// </summary>
    /// <param name="method">HTTP method to be used</param>
    /// <param name="endpoint">API endpoint</param>
    /// <param name="content">Content (if any) to be sent</param>
    /// <returns><see cref="HttpResponseMessage"/></returns>
    protected async Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method,
        string endpoint,
        object? content = null
    ) {
        var request = new HttpRequestMessage(method, endpoint);

        if (content != null) {
            request.Content = JsonContent.Create(content);
        }

        return await _httpClient.SendAsync(request);
    }
}