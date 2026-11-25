using static _4EPlatform.Domain.Enums.Enums;
using DomainEnums = _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.Pay;

public class Payment : AuditEntity<Guid>
{

    public Guid InvoiceId { get; set; }

    public Guid UserId { get; set; } = Guid.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "EGP";

    public string PaymentMethod { get; set; } = "Paymob";

    public PaymentStatus Status { get; set; }
        = PaymentStatus.Pending;

    public string? TransactionReference { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual SubscriptionInvoice Invoice { get; set; } = null!;

    public virtual ICollection<PaymobTransaction> PaymobTransactions { get; set; }
        = new List<PaymobTransaction>();
}


