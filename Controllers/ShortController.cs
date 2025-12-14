using Microsoft.AspNetCore.Mvc;
using URLShorter.Services;

namespace URLShorter.Controllers;

[ApiController]
[Route("/")]
public class ShortenerController(IUrlShortenerService shortener, ILogger<ShortenerController> logger) : ControllerBase
{
    [HttpPost("shorten")]
    public IActionResult Shorten([FromBody] UrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Url))
            return BadRequest("URL is required.");

        var shortUrl = shortener.Shorten(request.Url);
        logger.LogDebug("Shortened URL: {ShortUrl}", shortUrl);
        return Ok(new { shortUrl });
    }

    [HttpGet("{code}")]
    public IActionResult RedirectToOriginal(string code)
    {
        var url = shortener.GetOriginal(code);
        if (url is null)
        {
            logger.LogDebug("Shortened URL not found: {ShortUrl}", code);
            return NotFound("Short link not found.");
        }

        logger.LogInformation("Shortened URL get: {ShortUrl}", code);
        return Redirect(url);
    }

    public record UrlRequest(string Url);
}
