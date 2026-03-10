using SimplyTammi.Models;

namespace SimplyTammi.Services;

/// <summary>
/// Defines a contract for sending emails via the contact form.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends a contact form submission as an email.
    /// </summary>
    /// <param name="model">The contact form data.</param>
    /// <returns>True if the email was sent successfully; otherwise false.</returns>
    Task<bool> SendContactEmailAsync(ContactFormModel model);
}
