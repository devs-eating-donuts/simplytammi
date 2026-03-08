namespace SimplyTammi.Services;

/// <summary>
/// Verifies Cloudflare Turnstile tokens.
/// </summary>
public interface ITurnstileVerificationService
{
    /// <summary>
    /// Verifies a Turnstile response token.
    /// </summary>
    /// <param name="token">The token from the client widget.</param>
    /// <returns>A detailed verification result including Turnstile error codes.</returns>
    Task<TurnstileVerificationResult> VerifyAsync(string token);
}
