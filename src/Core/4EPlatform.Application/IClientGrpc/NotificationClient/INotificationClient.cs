


namespace _4EPlatform.Application.IClientGrpc.NotificationClient
{
    public interface INotificationClient
    {
        System.Threading.Tasks.Task SendDynamicNotifications(DynamicNotificationsRequest request);
        System.Threading.Tasks.Task SendDynamicNotificationsList(IReadOnlyCollection<DynamicNotificationsRequest> requests);
        
    }
}
