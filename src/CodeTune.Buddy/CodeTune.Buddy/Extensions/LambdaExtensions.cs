using System.Linq.Expressions;
using System.Reflection;


namespace CodeTune.Buddy.Extensions;
public static class LambdaExtensions
{
    public static string GetDescriptionAttribute<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
            => GetDescriptionAttribute(expression.GetPropertyInfo());

    public static string GetName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
        => expression.GetPropertyInfo().Name;

    private static string GetDescriptionAttribute(this PropertyInfo propertyInfo)
    {
        var desc = propertyInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute));
        if (desc.Count() > 0)
            return ((System.ComponentModel.DescriptionAttribute)desc.First()).Description;
        else
            return propertyInfo.Name;
    }

    private static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyLambda)
    {
        MemberExpression? member = propertyLambda.Body as MemberExpression;
        if (member == null)
            throw new ArgumentException(string.Format(
                "Expression '{0}' refers to a method, not a property.",
                propertyLambda.ToString()));

        PropertyInfo? propInfo = member.Member as PropertyInfo;
        if (propInfo == null)
            throw new ArgumentException(string.Format(
                "Expression '{0}' refers to a field, not a property.",
                propertyLambda.ToString()));

        return propInfo;
    }
}
