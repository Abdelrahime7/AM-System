namespace Domain.Enums;

public enum OrderStatus
{
    Pending,
    Approved,
    Rejected,
    SentToDelivery,
    AssignedDriver,
    OutForDelivery,
    Delivered,
    Returned,
    Cancelled
}