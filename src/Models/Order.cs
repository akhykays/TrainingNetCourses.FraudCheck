namespace TrainingNetCourses.FraudCheck.Models;

public record Order(
    string Id,
    string CustomerId,
    decimal Amount,
    string Currency,
    IReadOnlyList<OrderItem> OrderItems,
    Address BillingAddress,
    Address ShippingAddress,
    string PaymentMethod,
    string IPAddress,
    DateTime CustomerCreatedAt,
    DateTime OrderCreatedAt
);

