using Microsoft.AspNetCore.Mvc;
using URLShorter.Services;

namespace URLShorter.Controllers;

[ApiController]
[Route("/")]
public class ShortenerController(IUrlShortenerService shortener) : ControllerBase
{
    [HttpPost("shorten")]
    public IActionResult Shorten([FromBody] UrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Url))
            return BadRequest("URL is required.");

        var shortUrl = shortener.Shorten(request.Url);
        return Ok(new { shortUrl });
    }

    [HttpGet("{code}")]
    public IActionResult RedirectToOriginal(string code)
    {
        var url = shortener.GetOriginal(code);
        if (url is null)
            return NotFound("Short link not found.");

        return Redirect(url);
    }

    public record UrlRequest(string Url);
}