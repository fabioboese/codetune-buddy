using CodeTune.Buddy.Enums;
using CodeTune.Buddy.Exceptions;
using CodeTune.Buddy.Interfaces;

namespace CodeTune.Buddy.Concretes;
public class Validator<T> where T : IEntityModel
{

    private Dictionary<string, object?> references = new Dictionary<string, object?>();
    private readonly Func<T, string, object?>? getReference = null;
    protected ICollection<IRule> Rules = new List<IRule>();
    protected ValidationStrategyEnum Strategy { get; set; } = ValidationStrategyEnum.Unanimous;


    public Validator(params IRule[] rules)
    {
        this.Rules = rules;
    }

    public Validator(Func<T, string, object?> getReferences, params IRule[] rules)
    {
        this.getReference = getReferences;
        this.Rules = rules;
    }


    public bool Validate(T entity, ValidationMethod method = ValidationMethod.SkipEarly)
    {
        return ExecuteValidation(entity, method).isValid;
    }


    public IEnumerable<IRule>? GetBrokenRules(T entity, ValidationMethod method = ValidationMethod.SkipEarly)
    {
        return ExecuteValidation(entity, method).violatedRules?.AsEnumerable();
    }


    public (bool isValid, IEnumerable<IRule>? violatedRules, IEnumerable<IRule>? compliedRules)
        GetValidation(T entity, ValidationMethod method = ValidationMethod.SkipEarly)
    {
        var validationResult = ExecuteValidation(entity, method);
        return (validationResult.isValid, validationResult.violatedRules?.AsEnumerable(), validationResult.compliedRules?.AsEnumerable());
    }

    public void Assert(T entity, ValidationMethod method = ValidationMethod.SkipEarly)
    {
        var validationResult = ExecuteValidation(entity, method);
        if (!validationResult.isValid)
            throw new ValidationException<T>(validationResult.violatedRules!.ToArray());
    }

    public IEnumerable<string> GetRequiredReferences()
    {
        return Rules
            .Where(x => x.GetType().IsAssignableTo(typeof(IReferenceRule)))
            .Select(x => ((IReferenceRule)x).RefName)
            .Distinct();
    }

    public void AddReference(string refName, object? reference)
    {
        if (references.ContainsKey(refName))
            references[refName] = reference;
        else
            references.Add(refName, reference);
    }

    private (bool isValid, List<IRule>? violatedRules, List<IRule>? compliedRules)
        ExecuteValidation(T entity, ValidationMethod method = ValidationMethod.SkipEarly)
    {
        var requiredReferences = GetRequiredReferences().ToArray();
        if (requiredReferences.Any())
        {
            if (getReference == null) return (false, null, null);

            foreach (var reqReference in requiredReferences)
                this.AddReference(reqReference, getReference(entity, reqReference));
        }

        Func<int, int, int, bool>? earlyEvaluation = null;
        if (method == ValidationMethod.SkipEarly)
            earlyEvaluation = GetEarlyEvaluator();

        var CompliedRules = new List<IRule>();
        var ViolatedRules = new List<IRule>();
        foreach (var rule in Rules)
        {
            if (rule is IReferenceRule)
            {
                var refType = rule.GetType()
                    .GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRule<,>))
                    .Single()
                    .GetGenericArguments()[1];
                var reference = references[((IReferenceRule)rule).RefName];
                var evaluator = rule.GetType().GetMethod("IsSatisfiedBy")!;
                (((bool)evaluator.Invoke(rule, new[] { entity, reference })!) ? CompliedRules : ViolatedRules).Add(rule);
            }
            else
                (((IRule<T>)rule).IsSatisfiedBy(entity) ? CompliedRules : ViolatedRules).Add(rule);

            if (earlyEvaluation != null)
                if (earlyEvaluation.Invoke(Rules.Count, CompliedRules.Count, ViolatedRules.Count))
                    break;
        }

        switch (Strategy)
        {
            case ValidationStrategyEnum.Unanimous: return (ViolatedRules.Count == 0, ViolatedRules, CompliedRules);
            case ValidationStrategyEnum.Affirmative: return (CompliedRules.Count > 0, ViolatedRules, CompliedRules);
            case ValidationStrategyEnum.Consensus: return (CompliedRules.Count > ViolatedRules.Count, ViolatedRules, CompliedRules);
            default: return (false, null, null);
        }
    }

    private Func<int, int, int, bool> GetEarlyEvaluator()
    {
        switch (Strategy)
        {
            case ValidationStrategyEnum.Unanimous: return (count, pos, neg) => neg > 0;
            case ValidationStrategyEnum.Affirmative: return (count, pos, neg) => pos > 0;
            case ValidationStrategyEnum.Consensus: return (count, pos, neg) => Math.Abs(pos - neg) > count;
            default: return (count, pos, neg) => false;
        }
    }
}
