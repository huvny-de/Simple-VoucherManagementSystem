namespace VoucherManagementSystem.Domain.Entities;

public class Voucher : BaseEntity
{
    public required string UserId { get; set; }
    public required string PromotionId { get; set; }
    public required string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public string DiscountCurrency { get; set; } = "USD";
    public decimal OrderAmount { get; set; }
    public string OrderCurrency { get; set; } = "USD";
    public decimal FinalDiscountAmount { get; set; }
    public string FinalDiscountCurrency { get; set; } = "USD";
    public DateTime? UsedAt { get; set; }
    public bool IsUsed { get; set; } = false;
    public required string OrderId { get; set; }
}
