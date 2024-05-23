using System;

namespace api.Utils.Algorithm
{
    public class BoyerMoore
    {
        // Boyer Moore Algorithm for pattern matching using good suffix heuristic
        static void PreprocessStrongSuffix(int[] shift, int[] bpos, string pat, int m)
        {
            int i = m, j = m + 1;
            bpos[i] = j;

            while (i > 0)
            {
                while (j <= m && pat[i - 1] != pat[j - 1])
                {
                    if (shift[j] == 0)
                        shift[j] = j - i;

                    j = bpos[j];
                }
                i--;
                j--;
                bpos[i] = j;
            }
        }

        // Preprocessing for case 2
        static void PreprocessCase2(int[] shift, int[] bpos, string pat, int m)
        {
            int j = bpos[0];
            for (int i = 0; i <= m; i++)
            {
                if (shift[i] == 0)
                    shift[i] = j;

                if (i == j)
                    j = bpos[j];
            }
        }

        // Search for a pattern in given text using Boyer Moore algorithm with Good suffix rule
        public static bool Search(string text, string pat)
        {
            int s = 0, j;
            int m = pat.Length;
            int n = text.Length;

            int[] bpos = new int[m + 1];
            int[] shift = new int[m + 1];

            // Do preprocessing
            PreprocessStrongSuffix(shift, bpos, pat, m);
            PreprocessCase2(shift, bpos, pat, m);

            while (s <= n - m)
            {
                j = m - 1;

                while (j >= 0 && pat[j] == text[s + j])
                    j--;

                if (j < 0)
                {
                    return true; // Pattern found
                }
                else
                {
                    s += shift[j + 1];
                }
            }

            return false; // Pattern not found
        }
    }
}
