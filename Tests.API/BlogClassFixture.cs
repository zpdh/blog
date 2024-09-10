using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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
    /// <param name="token">JWT Token for authentication</param>
    /// <returns><see cref="HttpResponseMessage"/></returns>
    protected async Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method,
        string endpoint,
        object? content = null,
        string? token = null
    ) {
        var request = new HttpRequestMessage(method, endpoint);

        if (token != null) {
            AddTokenToHeaders(token, request);
        }

        if (content != null) {
            request.Content = JsonContent.Create(content);
        }

        return await _httpClient.SendAsync(request);
    }

    protected static async Task<JsonElement> ParseResponseAsync(HttpResponseMessage response) {
        var jsonString = await response.Content.ReadAsStringAsync();
        var content = JsonDocument.Parse(jsonString);

        return content.RootElement;
    }

    private static void AddTokenToHeaders(string token, HttpRequestMessage httpMessage) {
        if (httpMessage.Headers.Contains("Authorization")) {
            httpMessage.Headers.Remove("Authorization");
        }

        httpMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}