using PostmarkDotNet;
using SimplyTammi.Models;

namespace SimplyTammi.Services;

/// <summary>
/// Sends emails using the Postmark transactional email API.
/// </summary>
public class PostmarkEmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PostmarkEmailService> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="PostmarkEmailService"/>.
    /// </summary>
    public PostmarkEmailService(IConfiguration configuration, ILogger<PostmarkEmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<bool> SendContactEmailAsync(ContactFormModel model)
    {
        var serverToken = _configuration["Postmark:ServerToken"];
        var fromAddress = _configuration["Postmark:FromAddress"];
        var toAddress = _configuration["Postmark:ToAddress"];

        if (string.IsNullOrWhiteSpace(serverToken) ||
            string.IsNullOrWhiteSpace(fromAddress) ||
            string.IsNullOrWhiteSpace(toAddress))
        {
            _logger.LogError("Postmark configuration is missing. Check Postmark:ServerToken, Postmark:FromAddress, and Postmark:ToAddress in appsettings.");
            return false;
        }

        try
        {
            var client = new PostmarkClient(serverToken);

            var message = new PostmarkMessage
            {
                To = toAddress,
                From = fromAddress,
                Subject = $"Contact from {model.Name}",
                TextBody = $"Name: {model.Name}\nEmail: {model.Email}\n\n{model.Message}",
                HtmlBody = $"""
                    <h2>New Contact Form Submission</h2>
                    <p><strong>Name:</strong> {model.Name}</p>
                    <p><strong>Email:</strong> {model.Email}</p>
                    <hr />
                    <p>{model.Message}</p>
                    """,
                ReplyTo = model.Email
            };

            var response = await client.SendMessageAsync(message);

            if (response.Status == PostmarkStatus.Success)
            {
                _logger.LogInformation("Contact email sent successfully from {Email}.", model.Email);
                return true;
            }

            _logger.LogWarning("Postmark returned status {Status}: {Message}", response.Status, response.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send contact email via Postmark.");
            return false;
        }
    }
}
