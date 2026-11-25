using static _4EPlatform.Domain.Enums.Enums;
using DomainEnums = _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models;

public class Subscription:AuditEntity<Guid>
{
  public Guid UserId { get; set; } = Guid.Empty;

    public Guid PlanId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public SubscriptionStatus Status { get; set; }
        = SubscriptionStatus.Pending;

    public bool AutoRenew { get; set; }

    public DateTime? CancelledAt { get; set; }

    public byte[]? RowVersion { get; set; }

    public  virtual SubscriptionPlan Plan { get; set; } = null!;

    public  virtual ICollection<SubscriptionInvoice> Invoices { get; set; }
        = new List<SubscriptionInvoice>();
}

