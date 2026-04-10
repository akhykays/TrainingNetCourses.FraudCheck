namespace TrainingNetCourses.FraudCheck.DTOs.Requests;

public record OrderScoreRequest
{
    public required string OrderId { get; init; }
    public required string CustomerId { get; init; }
    public required decimal Amount { get; init; }
    public required string Currency { get; init; }
    public required IReadOnlyList<OrderItemDto> Items { get; init; }
    public required AddressDto BillingAddress { get; init; }
    public required AddressDto ShippingAddress { get; init; }
    public required string PaymentMethod { get; init; }
    public required string IpAddress { get; init; }
    public required DateTime CustomerCreatedAt { get; init; }
    public required DateTime OrderCreatedAt { get; init; }
}

