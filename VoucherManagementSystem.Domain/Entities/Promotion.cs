namespace VoucherManagementSystem.Domain.Entities;

public class Promotion : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public string DiscountCurrency { get; set; } = "USD";
    public decimal DiscountPercentage { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public string MinimumOrderCurrency { get; set; } = "USD";
    public decimal MaximumDiscountAmount { get; set; }
    public string MaximumDiscountCurrency { get; set; } = "USD";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UsageLimit { get; set; }
    public int UsedCount { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public List<string> ApplicableUserIds { get; set; } = new();
}
