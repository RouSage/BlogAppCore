using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogAppCore.Domain
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(this string text, int length)
        {
            string str = text.RemoveDiacritics().ToLower();

            // Invalid chars
            str = Regex.Replace(str, @"[^A-Za-z0-9\s-]", "");
            // Remove spaces
            str = Regex.Replace(str, @"\s+", "-").Trim();

            str = str.Substring(0, str.Length <= length ? str.Length : 50).Trim();

            return str;
        }

        public static string RemoveDiacritics(this string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return s.Normalize(NormalizationForm.FormC);
        }
    }
}