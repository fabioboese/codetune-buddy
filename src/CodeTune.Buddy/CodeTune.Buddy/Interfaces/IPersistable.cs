using CodeTune.Buddy.Concretes;

namespace CodeTune.Buddy.Interfaces;

public interface IPersistable<T> where T : IEntityModel
{
    Validator<T>? InsertValidator { get; }
    Validator<T>? DeleteValidator { get; }
    Validator<T>? UpdateValidator { get; }
}