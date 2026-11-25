namespace _4EPlatform.Application.Helpers
{
    /// <summary>
    /// Holds the caller's Authorization token and language for work running outside the HTTP request
    /// (e.g. background notifications), so <see cref="TokenService"/> can still attach them to gRPC calls.
    /// </summary>
    public static class BackgroundAuthContext
    {
        private static readonly AsyncLocal<string?> _token = new();
        private static readonly AsyncLocal<string?> _acceptLanguage = new();

        public static string? Token
        {
            get => _token.Value;
            set => _token.Value = value;
        }

        public static string? AcceptLanguage
        {
            get => _acceptLanguage.Value;
            set => _acceptLanguage.Value = value;
        }
    }
}
