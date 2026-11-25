using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.Pay
{
    public class PaymentWebhookEvent : AuditEntity<Guid>
    {
        public Guid? ProviderEventId { get; set; }

        public Guid? PaymobTransactionId { get; set; }

        public string Payload { get; set; } = null!;

        public bool IsProcessed { get; set; }

        public DateTime? ProcessedAt { get; set; }

        public DateTime ReceivedAt { get; set; } =
            DateTime.UtcNow;

        public string? ProcessingError { get; set; }
    }
}
