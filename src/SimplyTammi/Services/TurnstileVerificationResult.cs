namespace SimplyTammi.Services;

/// <summary>
/// Represents the result of a Turnstile verification attempt.
/// </summary>
/// <param name="Success">Indicates whether verification succeeded.</param>
/// <param name="ErrorCodes">Error codes returned by Turnstile when verification fails.</param>
public sealed record TurnstileVerificationResult(bool Success, IReadOnlyList<string> ErrorCodes);
