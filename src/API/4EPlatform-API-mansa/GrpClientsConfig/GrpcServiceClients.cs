namespace _4EPlatform_API_Template.GrpClientsConfig
{
    public static class GrpcServiceClients
    {
        public static IHttpClientBuilder AddGrpcServiceClient<TService>(
     this IServiceCollection services,
     string serviceUrl)
     where TService : ClientBase
        {
            return services.AddGrpcClient<TService>(o =>
            {
                o.Address = new Uri(serviceUrl);
                o.CallOptionsActions.Add(async (callOptionsContext) =>
                {
                    var tokenService = callOptionsContext.ServiceProvider.GetRequiredService<ITokenService>();
                    var callOptions = tokenService.AcquireToken().Result;
                    callOptionsContext.CallOptions = callOptions;

                });
                o.ChannelOptionsActions.Add(channelOptions =>
                {
                    channelOptions.MaxReceiveMessageSize = 500 * 1024 * 1024; // 500 MB
                    channelOptions.MaxSendMessageSize = 500 * 1024 * 1024; // 500 MB
                });
            });
        }
    }
}
