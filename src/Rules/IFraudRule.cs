using TrainingNetCourses.FraudCheck.Models;

namespace TrainingNetCourses.FraudCheck.Rules;

public interface IFraudRule
{
    string RuleId { get; }
    string Description { get; }
    int Weight { get; }

    bool Evaluate(Order order);
}

