using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace _4EPlatform_API_Template.Extention
{
    /// <summary>
    /// SC-04 / SC-12: transport security.
    /// - gRPC port (service-to-service): HTTPS on a developer machine with the ASP.NET Core development
    ///   certificate (dotnet dev-certs https --trust); local gRPC urls in appsettings are https://localhost:{grpc port}
    ///   and IdentityUrl is https://localhost:7152/ (UserManagement gRPC port, also serves HTTP/1.1 locally).
    /// - REST port (HTTP/1.1, UI / Postman / IdentityServer): HTTP in Kestrel. On the servers external calls are
    ///   HTTPS through the Kubernetes Nginx Ingress (central certificate, TLS terminated there).
    ///
    /// Inside the container Kestrel does NOT enable TLS (the image has no certificate), so the pod starts exactly
    /// as today and in-cluster gRPC stays plain HTTP/2. Detection uses DOTNET_RUNNING_IN_CONTAINER, which the
    /// official mcr.microsoft.com/dotnet/aspnet images set to "true".
    /// Override if ever needed: Https:UseKestrelHttps = true/false (env var Https__UseKestrelHttps).
    /// </summary>
    public static class HttpsSettings
    {
        // UserManagement (IdentityServer) ports, the same in every environment
        private const int UserManagementGrpcPort = 7152;
        private const int UserManagementRestPort = 5111;

        public static bool UseKestrelHttps(IConfiguration configuration)
        {
            var configured = configuration.GetValue<bool?>("Https:UseKestrelHttps");
            if (configured.HasValue)
                return configured.Value;

            var runningInContainer = string.Equals(
                Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), "true", StringComparison.OrdinalIgnoreCase);
            return !runningInContainer;
        }

        /// <summary>
        /// appsettings.json uses https://localhost gRPC urls (GrpcServiceSettings:*) because locally the gRPC port is HTTPS.
        /// Inside the container the gRPC port is HTTP, so a loopback https url that was NOT overridden by the deployment
        /// is switched back to http (exactly the previous value). Non-loopback urls (set by the deployment) are never touched.
        /// Call right after WebApplication.CreateBuilder so every reader of IConfiguration sees the aligned value.
        /// </summary>
        public static void AlignLoopbackUrlsWithKestrel(this ConfigurationManager configuration)
        {
            if (UseKestrelHttps(configuration))
                return;

            var overrides = new Dictionary<string, string?>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (value == null
                    || !Uri.TryCreate(value, UriKind.Absolute, out var uri)
                    || uri.Scheme != Uri.UriSchemeHttps || !uri.IsLoopback)
                    continue;

                if (key.StartsWith("GrpcServiceSettings:", StringComparison.OrdinalIgnoreCase))
                {
                    overrides[key] = "http://" + value.Substring("https://".Length);
                }
                else if (string.Equals(key, "IdentityUrl", StringComparison.OrdinalIgnoreCase))
                {
                    // locally IdentityServer is reached on the UserManagement gRPC port over TLS (https://localhost:7152/);
                    // in the container that port is HTTP/2 only, so go back to the REST port (http://localhost:5111/)
                    var port = uri.Port == UserManagementGrpcPort ? UserManagementRestPort : uri.Port;
                    // keep the original path exactly ("" or "/...")
                    var rest = value.Substring("https://".Length);
                    var slash = rest.IndexOf('/');
                    var path = slash < 0 ? string.Empty : rest.Substring(slash);
                    overrides[key] = $"http://{uri.Host}:{port}{path}";
                }
            }

            if (overrides.Count > 0)
                configuration.AddInMemoryCollection(overrides);
        }

        /// <summary>
        /// gRPC port: locally HTTPS with HTTP/1.1 + HTTP/2 (ALPN picks h2 for gRPC and http/1.1 for REST/IdentityServer,
        /// so IdentityUrl can be https://localhost:7152/); inside the container plain HTTP/2 (h2c) as before.
        /// REST port: HTTP (HTTPS via the Ingress on the servers).
        /// </summary>
        public static void ListenGrpcAndRest(this KestrelServerOptions options, IConfiguration configuration, int grpcPort, int restPort)
        {
            var useHttps = UseKestrelHttps(configuration);

            options.Listen(IPAddress.Any, grpcPort, o =>
            {
                if (useHttps)
                {
                    o.Protocols = HttpProtocols.Http1AndHttp2;
                    o.UseHttps();
                }
                else
                {
                    // without TLS one port cannot negotiate HTTP/1.1 and HTTP/2, gRPC needs HTTP/2
                    o.Protocols = HttpProtocols.Http2;
                }
            });

            options.Listen(IPAddress.Any, restPort, o => o.Protocols = HttpProtocols.Http1);
        }

        /// <summary>
        /// Security:RequireHttpsMetadata if set, otherwise true when the authority is https.
        /// (true with an http authority makes JwtBearer fail, which is why it was hardcoded to false.)
        /// </summary>
        public static bool RequireHttpsMetadata(IConfiguration configuration, string? authority)
        {
            var configured = configuration.GetValue<bool?>("Security:RequireHttpsMetadata");
            if (configured.HasValue)
                return configured.Value;
            return authority?.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}
