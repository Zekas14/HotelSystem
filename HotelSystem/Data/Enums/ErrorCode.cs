
namespace HotelSystem.Models.Enums;

public enum ErrorCode
{
    None = 0,
    UnKnownError,
    FieldIsEmpty,
    InvalidInput,
    ItemAlreadyExists,
    AllRoomIsBooked,
    ItemNotFound,
    InvalidAmount,
}
