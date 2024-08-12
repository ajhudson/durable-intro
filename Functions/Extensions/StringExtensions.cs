using System.Text.RegularExpressions;

namespace Functions.Extensions;

public static class StringExtensions
{
    private static readonly Regex extractIntegersPattern = new(@"(\+|\-)*\d+", RegexOptions.Compiled);
    
    public static int ExtractFirstInt(this string value)
    {
        var firstInt = extractIntegersPattern.Match(value);
        int.TryParse(firstInt.Value, out var parsedInt);

        return parsedInt;
    }
}