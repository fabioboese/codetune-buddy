using System.Collections.Immutable;
using System.Globalization;

namespace CodeTune.Buddy.Constants;

public class CultureConstants
{
    public const string BRAZIL_CULTURE_CODE = "pt-BR";
    public const string USA_CULTURE_CODE = "en-US";

    public enum CultureEnum
    {
        Brazil = 1,
        Usa = 2,
        Custom = 3
    }

    public static ImmutableDictionary<CultureEnum, CultureInfo> Cultures = LoadCultures();

    private static ImmutableDictionary<CultureEnum, CultureInfo> LoadCultures()
    {
        var cultureDictionaryBuilder = ImmutableDictionary.CreateBuilder<CultureEnum, CultureInfo>();
        cultureDictionaryBuilder.Add(CultureEnum.Brazil, new CultureInfo(BRAZIL_CULTURE_CODE));
        cultureDictionaryBuilder.Add(CultureEnum.Usa, new CultureInfo(USA_CULTURE_CODE));
        return cultureDictionaryBuilder.ToImmutable();
    }
}
