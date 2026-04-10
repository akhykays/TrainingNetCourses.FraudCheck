namespace TrainingNetCourses.FraudCheck.DTOs.Requests;

public class AddressDto
{
    public required string Country { get; init; }
    public required string City { get; init; }
    public required string ZipCode { get; init; }
}

