namespace TrainingNetCourses.FraudCheck.DTOs.Requests;

public class OrderItemDto
{
    public required string ProductId { get; init; }
    public required string Name { get; init; }
    public required int Quantity { get; init; }
    public required decimal UnitPrice { get; init; }
}

