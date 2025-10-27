namespace VoucherManagementSystem.Application.Common.Models;

public class ApplicationOperationResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected ApplicationOperationResult(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static ApplicationOperationResult Success() => new(true, string.Empty);
    public static ApplicationOperationResult Failure(string error) => new(false, error);
}

public class ApplicationOperationResult<T> : ApplicationOperationResult
{
    public T Value { get; }

    protected ApplicationOperationResult(bool isSuccess, T value, string error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static ApplicationOperationResult<T> Success(T value) => new(true, value, string.Empty);
    public static new ApplicationOperationResult<T> Failure(string error) => new(false, default!, error);
}
