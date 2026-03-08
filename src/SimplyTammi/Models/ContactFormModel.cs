using System.ComponentModel.DataAnnotations;

namespace SimplyTammi.Models;

/// <summary>
/// Represents the data submitted through the contact form.
/// </summary>
public class ContactFormModel
{
    /// <summary>
    /// The sender's full name.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The sender's email address.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The message body.
    /// </summary>
    [Required(ErrorMessage = "Message is required.")]
    [StringLength(2000, ErrorMessage = "Message must be 2000 characters or fewer.")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Hidden honeypot field. Bots typically fill this, humans do not.
    /// </summary>
    public string Website { get; set; } = string.Empty;
}
