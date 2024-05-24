using System.Text.RegularExpressions;

namespace api.Utils.Converter
{
    // Converter string alay into normal using regex
    public class ConverterAlayToNormal
    {
        public static string GetKonversiArrayToNormal(string inputAlay, string inputNormal)
        {
            string[] kataAlayArr = inputAlay.Split(' ');
            string[] kataNormalArr = inputNormal.Split(' ');
            int lengthKataAlay = kataAlayArr.Length;
            int lengthKataNormal = kataNormalArr.Length;
            if (lengthKataAlay != lengthKataNormal)
            {
                return inputAlay;
            }
            else
            {
                string ret = "";
                for (int i = 0; i < lengthKataAlay; i++)
                {
                    string convertedWord = KonversiAlayKeNormalLogic(kataAlayArr[i], kataNormalArr[i]);
                    if (i == 0)
                        ret += convertedWord;
                    else
                        ret += " " + convertedWord;
                }
                return ret;
            }
        }

        public static string KonversiAlayKeNormalLogic(string inputAlay, string inputNormal)
        {
            // Kamus untuk konversi angka alay ke huruf normal
            var angkaAlayToHurufNormal = new Dictionary<char, char>
        {
            { '1', 'i' },
            { '2', 'r' },
            { '3', 'e' },
            { '4', 'a' },
            { '5', 's' },
            { '6', 'g' },
            { '7', 't' },
            { '8', 'b' },
            { '9', 'g' },
            { '0', 'o' }
            // Tambahkan pasangan angka alay dan huruf normal lainnya sesuai kebutuhan
        };

            // Konversi angka alay ke huruf normal dalam inputAlay
            foreach (var kvp in angkaAlayToHurufNormal)
            {
                inputAlay = inputAlay.Replace(kvp.Key, kvp.Value);
            }

            // Pastikan semua karakter dalam inputAlay ada di inputNormal
            if (ContainsAllChars(inputAlay, inputNormal))
            {
                // Escape special characters in inputAlay
                string escapedInputAlay = Regex.Escape(inputAlay);

                // Build the regex pattern to match penyingkatan at any position within a word
                string pattern = @"(?<!\w)" + escapedInputAlay.Substring(0, 1) + ".+" + @"(?!\w)";

                // Use the pattern to find a match in the inputNormal string
                Match match = Regex.Match(inputNormal, pattern, RegexOptions.IgnoreCase);

                // Jika ditemukan, kembalikan hasilnya. Jika tidak, kembalikan inputAlay
                return match.Success ? match.Value : inputAlay;
            }
            else
            {
                // Jika ada karakter dalam inputAlay yang tidak ada di inputNormal, kembalikan inputAlay itu sendiri
                return inputAlay;
            }
        }

        public static bool ContainsAllChars(string inputAlay, string inputNormal)
        {
            foreach (char c in inputAlay)
            {
                if (!inputNormal.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
