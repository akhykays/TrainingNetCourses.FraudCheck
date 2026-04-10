namespace TrainingNetCourses.FraudCheck.DTOs.Responses;

public record TriggeredRule(
  string RuleId,
  string Description,
  int Weight
  );

