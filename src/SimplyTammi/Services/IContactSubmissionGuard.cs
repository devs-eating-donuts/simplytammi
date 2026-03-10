namespace SimplyTammi.Services;

/// <summary>
/// Guards contact submissions with simple anti-spam throttling rules.
/// </summary>
public interface IContactSubmissionGuard
{
    /// <summary>
    /// Checks whether a contact submission is currently allowed.
    /// </summary>
    /// <param name="email">The email associated with the submission.</param>
    /// <returns>True when the submission is allowed; otherwise false.</returns>
    bool IsAllowed(string email);
}
