using System;
using System.Text.RegularExpressions;

namespace api.Utils.Helper
{
    public class StringHandler
    {
        public static string NormalizeToMatch(string str1, string str2)
        {
            // Remove spaces and convert to lowercase
            string normalizedStr1 = Regex.Replace(str1.ToLower(), @"\s+", "");
            string normalizedStr2 = Regex.Replace(str2.ToLower(), @"\s+", "");

            // Replace characters based on "bahasa alay" variations
            normalizedStr1 = NormalizeBahasaAlay(normalizedStr1);
            normalizedStr2 = NormalizeBahasaAlay(normalizedStr2);

            // Check if str1 can be normalized to match the length of str2
            if (normalizedStr1.Length < normalizedStr2.Length)
            {
                // Pad str1 with spaces
                normalizedStr1 = normalizedStr1.PadRight(normalizedStr2.Length, ' ');
            }
            else if (normalizedStr1.Length > normalizedStr2.Length)
            {
                // Truncate str1 to match the length of str2
                normalizedStr1 = normalizedStr1.Substring(0, normalizedStr2.Length);
            }

            return normalizedStr1;
        }

        private static string NormalizeBahasaAlay(string str)
        {
            // Replace characters based on "bahasa alay" variations
            str = str.Replace('1', 'i');
            str = str.Replace('4', 'a');
            str = str.Replace('3', 'e');
            str = str.Replace('0', 'o');
            str = str.Replace('5', 's');

            return str;
        }
    }
}
