using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Infra.Helper
{
    public static class UsernameGenerator
    {
        public static string Generate(string name, Func<string, bool> usernameExists)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            var normalized = Normalize(name);

            var parts = normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string baseUsername;

            if (parts.Length == 1)
            {
                baseUsername = parts[0];
            }
            else
            {
                baseUsername = $"{parts[0]}.{parts.Last()}";
            }

            baseUsername = baseUsername.ToLower();

            var finalUsername = baseUsername;
            int counter = 2;

            while (usernameExists(finalUsername))
            {
                finalUsername = $"{baseUsername}{counter}";
                counter++;
            }

            return finalUsername;
        }

        private static string Normalize(string input)
        {
            input = input.ToLowerInvariant();

            var normalized = input.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            normalized = sb.ToString().Normalize(NormalizationForm.FormC);

            normalized = Regex.Replace(normalized, @"[^a-z0-9 ]", "");

            return normalized.Trim();
        }
    }
}
