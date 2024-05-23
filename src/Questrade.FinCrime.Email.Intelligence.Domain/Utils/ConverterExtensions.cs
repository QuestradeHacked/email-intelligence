namespace Questrade.FinCrime.Email.Intelligence.Domain.Utils;

public static class ConverterExtensions
{
    public static int? ToNullableInt(this string value, int? defaultValue = null)
    {
        return int.TryParse(value, out var result) ? result : defaultValue;
    }

    public static DateTime? ToNullableDateTime(this string value, DateTime? defaultValue = null)
    {
        return DateTime.TryParse(value, out var result) ? result : defaultValue;
    }
}