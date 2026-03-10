using Microsoft.Extensions.Caching.Memory;

namespace SimplyTammi.Services;

/// <summary>
/// In-memory per-IP and per-email submission throttling for the contact form.
/// </summary>
public sealed class ContactSubmissionGuard : IContactSubmissionGuard
{
    private const int MaxSubmissionsPerWindow = 5;
    private static readonly TimeSpan Window = TimeSpan.FromMinutes(10);

    private readonly IMemoryCache _memoryCache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of <see cref="ContactSubmissionGuard"/>.
    /// </summary>
    public ContactSubmissionGuard(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
    {
        _memoryCache = memoryCache;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public bool IsAllowed(string email)
    {
        var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var ipAllowed = IncrementAndCheck($"contact:ip:{ip}");
        var emailAllowed = IncrementAndCheck($"contact:email:{email.Trim().ToLowerInvariant()}");

        return ipAllowed && emailAllowed;
    }

    private bool IncrementAndCheck(string key)
    {
        var count = _memoryCache.GetOrCreate<int>(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = Window;
            return 0;
        });

        count++;
        _memoryCache.Set(key, count, Window);

        return count <= MaxSubmissionsPerWindow;
    }
}
