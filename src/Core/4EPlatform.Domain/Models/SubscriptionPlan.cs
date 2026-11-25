namespace _4EPlatform.Domain.Models;

public class SubscriptionPlan :AuditEntity<Guid>
{
    
 public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string TargetRole { get; set; } = null!;

    public decimal Price { get; set; }

    public string Currency { get; set; } = "EGP";

    public int DurationInDays { get; set; } = 30;

    public bool IsActive { get; set; } = true;
    public virtual ICollection<Subscription> Subscriptions { get; set; }
        = new List<Subscription>();
}

