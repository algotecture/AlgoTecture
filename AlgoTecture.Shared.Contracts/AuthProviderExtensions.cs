namespace Algotecture.Shared.Contracts;

public static class AuthProviderExtensions
{
    public static string ToDbValue(this AuthProvider provider) =>
        provider switch
        {
            AuthProvider.Telegram => "telegram",
            _ => throw new ArgumentOutOfRangeException(nameof(provider))
        };

    public static AuthProvider FromDbValue(string value) =>
        value.ToLowerInvariant() switch
        {
            "telegram" => AuthProvider.Telegram,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
}