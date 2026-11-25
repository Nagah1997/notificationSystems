using DomainEnums = _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.Pay;

public class PaymobTransaction : AuditEntity<Guid>
{
    public Guid PaymentId { get; set; }

public string? PaymobOrderId { get; set; }

public string? PaymobTransactionId { get; set; }

public string? PaymentToken { get; set; }

public string? IntegrationId { get; set; }

public string? ResponseCode { get; set; }

public string? ResponseMessage { get; set; }

public bool IsSuccess { get; set; }

public virtual Payment Payment { get; set; } = null!;
}
