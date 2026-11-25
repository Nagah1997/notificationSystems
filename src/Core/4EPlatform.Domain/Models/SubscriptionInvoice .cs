using _4EPlatform.Domain.Models.Pay;
using System;
using System.Collections.Generic;
using System.Text;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models
{
    public class SubscriptionInvoice :AuditEntity<Guid>
    {
   
        public Guid SubscriptionId { get; set; }

        public string InvoiceNumber { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "EGP";

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public InvoiceStatus Status { get; set; }
            = InvoiceStatus.Unpaid;

        public DateTime IssuedAt { get; set; }

        public DateTime? PaidAt { get; set; }

        public  virtual Subscription Subscription { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }

}

