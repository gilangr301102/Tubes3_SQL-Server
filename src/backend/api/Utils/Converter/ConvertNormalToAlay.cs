using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace api.Utils.Converter
{
    public class ConvertNormalToAlay
    {
        public static string ConvertToAlay(string name)
        {
            var alayWords = new Dictionary<char, char>
            {
                { 'a', '4' }, { 'b', '8' }, { 'e', '3' }, { 'i', '1' }, { 'o', '0' },
                { 's', '5' }, { 't', '7' }, { 'z', '2' }
            };

            var random = new Random();
            var alayBuilder = new StringBuilder();

            foreach (var word in name.Split(' '))
            {
                var abbreviatedWord = Abbreviate(word);

                var alayWord = new StringBuilder();

                // Convert each character to "alay" version
                foreach (var c in abbreviatedWord.ToLower())
                {
                    if (alayWords.ContainsKey(c))
                    {
                        alayWord.Append(alayWords[c]);
                    }
                    else if (c == 'g')
                    {
                        alayWord.Append(random.Next(2) == 0 ? '6' : '9');  // Randomly choose between '6' and '9'
                    }
                    else
                    {
                        alayWord.Append(c);
                    }
                }

                alayBuilder.Append(alayWord).Append(' ');
            }

            // Remove the trailing space
            if (alayBuilder.Length > 0)
            {
                alayBuilder.Length--;
            }

            string alayName = alayBuilder.ToString();

            return alayName;
        }

        private static string Abbreviate(string word)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            var abbreviation = new StringBuilder();

            // Remove all vowel characters
            foreach (var c in word)
            {
                if (!vowels.Contains(c))
                {
                    abbreviation.Append(c);
                }
            }

            // If the abbreviation is empty, use the first, middle (if available and consonant), and last characters
            if (abbreviation.Length == 0)
            {
                if (word.Length > 0)
                {
                    abbreviation.Append(word[0]); // Add the first character
                }

                if (word.Length > 2)
                {
                    int middleIndex = word.Length / 2;
                    char middleChar = word[middleIndex];

                    // If the middle character is a consonant, add it to the abbreviation
                    if (!vowels.Contains(middleChar))
                    {
                        abbreviation.Append(middleChar);
                    }
                }

                if (word.Length > 1)
                {
                    abbreviation.Append(word[word.Length - 1]); // Add the last character
                }
            }

            return abbreviation.ToString();
        }
    }
}
