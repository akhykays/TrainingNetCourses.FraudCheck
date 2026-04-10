namespace TrainingNetCourses.FraudCheck.DTOs.Responses;

public record TriggeredRules(
  string RuleId,
  string Description,
  int Weight
  );

