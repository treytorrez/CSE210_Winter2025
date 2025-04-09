#nullable enable

using System.Diagnostics;
using System.Net.Http.Headers;

namespace Homeworktriage.Parsers;

public abstract class Parser
{
    // Shared HTTP client and base URL for all parsers
    protected static readonly string? Token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
    protected static readonly string BaseUrl = "https://byui.instructure.com/api/v1";
    protected static readonly HttpClient Client = new HttpClient();

    static Parser()
    {
        if (!string.IsNullOrEmpty(Token))
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Debug.WriteLine("[DEBUG] Authorization header set for HTTP client.");
        }
        Client.BaseAddress = new Uri(BaseUrl);
        Debug.WriteLine("[DEBUG] HTTP client initialized with base URL.");
    }

    // Check if the response contains a link to the next page
    protected static bool HasNextPage(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("Link", out var linkHeaders))
        {
            foreach (var linkHeader in linkHeaders)
            {
                if (linkHeader.Contains("rel=\"next\""))
                {
                    Debug.WriteLine("[DEBUG] Next page link found in response headers.");
                    return true;
                }
            }
        }
        return false;
    }

    // Extract the URL for the next page from the response headers
    protected static string GetNextPageUrl(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("Link", out var linkHeaders))
        {
            foreach (var linkHeader in linkHeaders)
            {
                var links = linkHeader.Split(',');
                foreach (var link in links)
                {
                    if (link.Contains("rel=\"next\""))
                    {
                        int start = link.IndexOf('<');
                        int end = link.IndexOf('>');
                        if (start != -1 && end != -1)
                        {
                            string nextPageUrl = link.Substring(start + 1, end - start - 1).Trim();
                            Debug.WriteLine($"[DEBUG] Extracted next page URL: {nextPageUrl}");
                            return nextPageUrl;
                        }
                    }
                }
            }
        }
        return string.Empty;
    }
}