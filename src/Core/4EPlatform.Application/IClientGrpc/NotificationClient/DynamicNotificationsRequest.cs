namespace _4EPlatform.Application.IClientGrpc.NotificationClient
{
    public class DynamicNotificationField
    {
        public string FieldTitle { get; set; } = string.Empty;
        public string FieldValue { get; set; } = string.Empty;
    }

    public class DynamicNotificationsRequest
    {
        public int CardType { get; set; }
        public int ActionType { get; set; }
        public List<string> ReceiversIds { get; set; } = new();
        public List<DynamicNotificationField> DynamicFields { get; set; } = new();
        public string ActionUrl { get; set; } = string.Empty;
        public string? ReportTitle { get; set; }
        public DateTime Date { get; set; }
    }
}
