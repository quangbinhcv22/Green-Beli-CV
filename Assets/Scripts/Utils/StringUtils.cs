using System.Linq;
using System.Text.RegularExpressions;

namespace Utils
{
    public static class StringUtils
    {
        public static string ToTitleCase(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            const int nextFirstIndex = 1;
            return text.First().ToString().ToUpper() + text.Substring(nextFirstIndex).ToLower();
        }

        public static string RemovingNonAlphaCharacters(this string text)
        {
            return Regex.Replace(text, "[^a-zA-Z ' ']", string.Empty);
        }
    }
}