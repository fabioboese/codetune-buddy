using CodeTune.Buddy.Constants;
using System.Globalization;

namespace CodeTune.Buddy.Extensions;

public static class StringExtensions
{
    public static byte ToByte(this string str, byte defaultValue = default(byte))
                => str.ToNullableByte() ?? defaultValue;

    public static byte? ToNullableByte(this string str)
        => (byte.TryParse(str, out byte value)) ? value : null;

    public static short ToInt16(this string str, short defaultValue = default(short))
        => str.ToNullableInt16() ?? defaultValue;

    public static short? ToNullableInt16(this string str)
        => (short.TryParse(str, out short value)) ? value : null;


    public static int ToInt32(this string str, int defaultValue = default(int))
        => str.ToNullableInt32() ?? defaultValue;

    public static int? ToNullableInt32(this string str)
        => (int.TryParse(str, out int value)) ? value : null;


    public static long ToInt64(this string str, long defaultValue = default(long))
        => str.ToNullableInt64() ?? defaultValue;

    public static long? ToNullableInt64(this string str)
        => (long.TryParse(str, out long value)) ? value : null;

    public static decimal ToDecimal(this string str, decimal defaultValue = default(decimal), NumberStyles style = NumberStyles.Float, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => str.ToNullableDecimal(style, culture) ?? defaultValue;

    public static decimal? ToNullableDecimal(this string str, NumberStyles style = NumberStyles.Float, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => (decimal.TryParse(str, style, CultureConstants.Cultures[culture], out decimal value)) ? value : null;

    public static float ToSingle(this string str, float defaultValue = default(float), NumberStyles style = NumberStyles.Float, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => str.ToNullableSingle(style, culture) ?? defaultValue;

    public static float? ToNullableSingle(this string str, NumberStyles style = NumberStyles.Float, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => (float.TryParse(str, style, CultureConstants.Cultures[culture], out float value)) ? value : null;

    public static double ToDouble(this string str, double defaultValue = default(double), NumberStyles style = NumberStyles.Float, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => str.ToNullableDouble(style, culture) ?? defaultValue;

    public static double? ToNullableDouble(this string str, NumberStyles style = NumberStyles.Float, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => (double.TryParse(str, style, CultureConstants.Cultures[culture], out double value)) ? value : null;

    public static DateTime? ToDateTime(this string str, DateTime defaultValue = default(DateTime), DateTimeStyles style = DateTimeStyles.AssumeLocal, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => str.ToNullableDateTime(style, culture) ?? defaultValue;

    public static DateTime? ToNullableDateTime(this string str, DateTimeStyles style = DateTimeStyles.AssumeLocal, CultureConstants.CultureEnum culture = CultureConstants.CultureEnum.Brazil)
        => (DateTime.TryParse(str, CultureConstants.Cultures[culture], style, out DateTime value) ? value : null);

}
