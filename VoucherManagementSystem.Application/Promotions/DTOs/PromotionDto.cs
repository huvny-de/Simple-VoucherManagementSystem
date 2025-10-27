namespace VoucherManagementSystem.Application.Promotions.DTOs;

public class PromotionDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public string DiscountCurrency { get; set; } = string.Empty;
    public decimal DiscountPercentage { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public string MinimumOrderCurrency { get; set; } = string.Empty;
    public decimal MaximumDiscountAmount { get; set; }
    public string MaximumDiscountCurrency { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public bool IsActive { get; set; }
    public bool IsValid { get; set; }
    public List<string> ApplicableUserIds { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
