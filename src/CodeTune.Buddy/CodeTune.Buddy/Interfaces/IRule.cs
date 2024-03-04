using CodeTune.Buddy.Attributes;

namespace CodeTune.Buddy.Interfaces;

public interface IRule
{
    string Name { get; }
}

public interface IEntityRule : IRule
{

}

public interface IReferenceRule : IRule
{
    string RefName { get; }
}

[RuleGroup(1)]
public interface IRule<T> : IEntityRule
    where T : IEntityModel
{
    bool IsSatisfiedBy(T item);
}

[RuleGroup(2)]
public interface IRule<T, TRef> : IReferenceRule
    where T : IEntityModel
    where TRef : IEntityModel
{
    bool IsSatisfiedBy(T item1, TRef refItem);
}
