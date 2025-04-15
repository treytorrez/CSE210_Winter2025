#nullable enable

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace Homeworktriage.Parsers;

public abstract class Parser
{
    // Shared HTTP client and base URL for all parsers
    protected static readonly string? Token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
    protected static readonly string BaseUrl = "https://byui.instructure.com/api/v1";
    protected static readonly HttpClient Client = new HttpClient();

    static Parser()
    {
        if (string.IsNullOrEmpty(Token))
        {
            Console.WriteLine("Error: API token not found in environment variables.");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("On macOS? Make sure you've added:");
                Console.WriteLine("    export CANVAS_API_TOKEN=\"your_token_here\"");
                Console.WriteLine("to your ~/.zshrc or ~/.bash_profile file and restarted the terminal.");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("On Windows? Set the environment variable via System Properties > Environment Variables.");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("On Linux? Add:");
                Console.WriteLine("    export CANVAS_API_TOKEN=\"your_token_here\"");
                Console.WriteLine("to your ~/.bashrc or ~/.zshrc.");
            }

            Environment.Exit(1);
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