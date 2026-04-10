namespace TrainingNetCourses.FraudCheck.Models;

public record OrderItem(
    string ProductId,
    string Name,
    int Quantity,
    decimal UnitPrice
);

