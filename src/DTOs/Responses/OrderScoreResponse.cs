namespace TrainingNetCourses.FraudCheck.DTOs.Responses;

public record OrderScoreResponse(
    string OrderId,
    int Score,
    string Decision,
    IReadOnlyList<TriggeredRules> TriggeredRules,
    DateTime ProcessedAt
    );

