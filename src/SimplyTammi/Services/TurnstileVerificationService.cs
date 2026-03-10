using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SimplyTammi.Services;

/// <summary>
/// Server-side verification service for Cloudflare Turnstile.
/// </summary>
public sealed class TurnstileVerificationService : ITurnstileVerificationService
{
    private static readonly Uri VerifyUri = new("https://challenges.cloudflare.com/turnstile/v0/siteverify");

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TurnstileVerificationService> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="TurnstileVerificationService"/>.
    /// </summary>
    public TurnstileVerificationService(
        HttpClient httpClient,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        ILogger<TurnstileVerificationService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<TurnstileVerificationResult> VerifyAsync(string token)
    {
        var secret = _configuration["Turnstile:SecretKey"];

        if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(token))
        {
            _logger.LogWarning("Turnstile verification skipped because secret or token is missing.");
            return new TurnstileVerificationResult(false, ["missing-input-secret-or-response"]);
        }

        var remoteIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        var fields = new List<KeyValuePair<string, string>>
        {
            new("secret", secret),
            new("response", token)
        };

        if (!string.IsNullOrWhiteSpace(remoteIp))
        {
            fields.Add(new("remoteip", remoteIp));
        }

        using var request = new FormUrlEncodedContent(fields);
        var response = await _httpClient.PostAsync(VerifyUri, request);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Turnstile verification HTTP request failed with status code {StatusCode}.", response.StatusCode);
            return new TurnstileVerificationResult(false, [$"http-{(int)response.StatusCode}"]);
        }

        var payload = await response.Content.ReadFromJsonAsync<TurnstileResponse>();

        if (payload is null)
        {
            _logger.LogWarning("Turnstile verification returned an empty response payload.");
            return new TurnstileVerificationResult(false, ["empty-response-payload"]);
        }

        if (!payload.Success)
        {
            var errorCodes = payload.ErrorCodes ?? [];
            _logger.LogWarning(
                "Turnstile verification failed with error codes: {ErrorCodes}",
                errorCodes.Length > 0 ? string.Join(",", errorCodes) : "none");

            return new TurnstileVerificationResult(false, errorCodes);
        }

        return new TurnstileVerificationResult(true, []);
    }

    private sealed record TurnstileResponse(
        [property: JsonPropertyName("success")] bool Success,
        [property: JsonPropertyName("error-codes")] string[]? ErrorCodes);
}
