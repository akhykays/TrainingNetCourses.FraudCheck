namespace TrainingNetCourses.FraudCheck.DTOs.Responses;

public record OrderScoreResponse(
    string OrderId,
    int Score,
    string Decision,
    IReadOnlyList<TriggeredRule> TriggeredRules,
    DateTime ProcessedAt
    );

