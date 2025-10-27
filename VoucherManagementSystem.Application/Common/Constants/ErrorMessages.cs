namespace VoucherManagementSystem.Application.Common.Constants;

public static class ErrorMessages
{
    // User messages
    public const string UserNotFound = "User not found";
    public const string UserWithEmailExists = "User with this email already exists";
    public const string InvalidEmail = "Invalid email address";
    public const string InvalidUserData = "Invalid user data provided";
    
    // Promotion messages
    public const string PromotionNotFound = "Promotion not found";
    public const string PromotionNotValid = "Promotion is no longer valid";
    public const string PromotionExpired = "Promotion has expired";
    public const string PromotionNotActive = "Promotion is not active";
    public const string PromotionCodeExists = "Promotion code already exists";
    public const string UserNotEligibleForPromotion = "User is not eligible for this promotion";
    
    // Voucher messages
    public const string VoucherNotFound = "Voucher not found";
    public const string VoucherAlreadyUsed = "Voucher has already been used";
    public const string VoucherExpired = "Voucher has expired";
    public const string InvalidVoucherData = "Invalid voucher data provided";
    
    // General messages
    public const string OperationFailed = "Operation failed";
    public const string InvalidRequest = "Invalid request";
    public const string DatabaseError = "Database error occurred";
    public const string UnauthorizedAccess = "You do not have permission to access this resource";
}
