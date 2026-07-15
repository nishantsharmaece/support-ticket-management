namespace SupportTicketManagement.Application.Common;

public sealed class ServiceResult<T>
{
    private ServiceResult(
        bool isSuccess,
        T? value,
        ServiceErrorKind errorKind,
        string? errorMessage,
        IReadOnlyList<ValidationError> validationErrors)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorKind = errorKind;
        ErrorMessage = errorMessage;
        ValidationErrors = validationErrors;
    }

    public bool IsSuccess { get; }

    public T? Value { get; }

    public ServiceErrorKind ErrorKind { get; }

    public string? ErrorMessage { get; }

    public IReadOnlyList<ValidationError> ValidationErrors { get; }

    public static ServiceResult<T> Success(T value)
    {
        return new ServiceResult<T>(true, value, ServiceErrorKind.None, null, Array.Empty<ValidationError>());
    }

    public static ServiceResult<T> ValidationFailure(IReadOnlyList<ValidationError> errors)
    {
        return new ServiceResult<T>(false, default, ServiceErrorKind.Validation, "Validation failed.", errors);
    }

    public static ServiceResult<T> NotFound(string message)
    {
        return new ServiceResult<T>(false, default, ServiceErrorKind.NotFound, message, Array.Empty<ValidationError>());
    }

    public static ServiceResult<T> Conflict(string message)
    {
        return new ServiceResult<T>(false, default, ServiceErrorKind.Conflict, message, Array.Empty<ValidationError>());
    }
}
