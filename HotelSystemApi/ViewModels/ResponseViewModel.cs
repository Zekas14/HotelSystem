using HotelSystem.Models.Enums;

namespace HotelSystem.ViewModels;

public class ResponseViewModel<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; } = true;
    public ErrorCode ErrorCode { get; set; }
    public string Message { get; set; }
}

public class SuccessResponseViewModel<T> : ResponseViewModel<T>
{
    public SuccessResponseViewModel(T data, string message = "") 
    {
        Data = data;
        IsSuccess = true;
        Message = message;
        ErrorCode = ErrorCode.None;
    }
}

public class FaluireResponseViewModel<T> : ResponseViewModel<T>
{
    public FaluireResponseViewModel(ErrorCode errorCode, string message = "")
    {
        Data = default;
        IsSuccess = false;
        Message = message;
        ErrorCode = errorCode;
    }
}
