using System.Net.Http.Json;
using System.Text.Json;

namespace Tests.API;

public class BlogClassFixture : IClassFixture<BlogWebApplicationFactory> {
    private readonly HttpClient HttpClient;

    protected BlogClassFixture(BlogWebApplicationFactory factory) {
        HttpClient = factory.CreateClient();
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

        return await HttpClient.SendAsync(request);
    }


    protected static async Task<JsonElement> ParseResponseAsync(HttpResponseMessage response) {
        var jsonString = await response.Content.ReadAsStringAsync();
        var content = JsonDocument.Parse(jsonString);

        return content.RootElement;
    }
}