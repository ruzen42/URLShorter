namespace URLShorter.Services;

public interface IUrlShortenerService
{
    string Shorten(string originalUrl);
    string? GetOriginal(string shortCode);
}