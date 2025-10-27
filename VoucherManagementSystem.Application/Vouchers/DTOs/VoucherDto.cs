namespace VoucherManagementSystem.Application.Vouchers.DTOs;

public class VoucherDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string PromotionId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public string DiscountCurrency { get; set; } = string.Empty;
    public decimal OrderAmount { get; set; }
    public string OrderCurrency { get; set; } = string.Empty;
    public decimal FinalDiscountAmount { get; set; }
    public string FinalDiscountCurrency { get; set; } = string.Empty;
    public DateTime? UsedAt { get; set; }
    public bool IsUsed { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
