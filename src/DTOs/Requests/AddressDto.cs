namespace TrainingNetCourses.FraudCheck.DTOs.Requests;

public record AddressDto
{
    public required string Country { get; init; }
    public required string City { get; init; }
    public required string ZipCode { get; init; }
}

