using CodeTune.Buddy.Interfaces;

namespace CodeTune.Buddy.Concretes;

internal class NoValidationRule<T> : IRule<T> where T : IEntityModel
{
    public string Name => "This class is using default empty validator for this operation";

    public bool IsSatisfiedBy(T item) => true;
}
