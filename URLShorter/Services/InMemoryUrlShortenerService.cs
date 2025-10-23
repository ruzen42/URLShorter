using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace URLShorter.Services;

public class InMemoryUrlShortenerService : IUrlShortenerService
{
    private readonly ConcurrentDictionary<string, string> _urlMap = new();
    private const string BaseUrl = "https://localhost:5278/";

    public string Shorten(string originalUrl)
    {
        var hash = ComputeShortCode(originalUrl);
        _urlMap[hash] = originalUrl;
        return BaseUrl + hash;
    }

    public string? GetOriginal(string shortCode)
    {
        _urlMap.TryGetValue(shortCode, out var url);
        return url;
    }

    private static string ComputeShortCode(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes)
            .Replace("/", "_")
            .Replace("+", "-")[..6];
    }
}