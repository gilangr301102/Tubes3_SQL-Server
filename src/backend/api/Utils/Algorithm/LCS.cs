using System.Text.RegularExpressions;
using api.Utils.Helper;

namespace api.Utils.Algorithm
{
    // Longest Common Subsequence (non-contiguous)
    // Implemented using sum prefix with Dynamic Programming
    public static class LCS
    {
        public static int ComputeSimilarityCommon(string str1, string str2)
        {
            if (str1 == "" || str2 == "")
                return 0;
            int m = str1.Length;
            int n = str2.Length;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++) dp[i, 0] = 0;
            for (int j = 0; j <= n; j++) dp[0, j] = 0;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        if (dp[i - 1, j] >= dp[i, j - 1])
                            dp[i, j] = dp[i - 1, j];
                        else
                            dp[i, j] = dp[i, j - 1];
                    }
                }
            }
            return dp[m, n];
        }

        public static int ComputeSimilarityAlay(string str1, string str2)
        {
            if (str1 == "" || str2 == "")
                return 0;
            str1 = Regex.Replace(str1, @"[^\w\d]", "");
            str2 = Regex.Replace(str2, @"[^\w\d]", "");
            str2 = str2.ToLower();
            int m = str1.Length;
            int n = str2.Length;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++) dp[i, 0] = 0;
            for (int j = 0; j <= n; j++) dp[0, j] = 0;
            bool[] isVisited = new bool[m + 1];
            for (int i = 0; i <= m; i++) isVisited[i] = false;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    char currentStr1Char = str1[i - 1];
                    char nextStr1Char = (i < m && (str1[i - 1] == '1')) ? str1[i] : '\0';
                    int tempTypeNormalize = CharHandler.NormalizeAlayChar(currentStr1Char, nextStr1Char, str2[j - 1]);
                    if (tempTypeNormalize > 0 && !isVisited[i - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        // Handling case bahasa alay with two number convert to one letter
                        // For example: "12"->"R", "13"->"B", etc.
                        if (tempTypeNormalize == 2 && dp[i + 1, j] < dp[i, j])
                        {
                            dp[i + 1, j] = dp[i, j];
                            isVisited[i] = true;
                        }
                    }
                    else
                    {
                        if (dp[i - 1, j] >= dp[i, j - 1]) dp[i, j] = dp[i - 1, j];
                        else dp[i, j] = dp[i, j - 1];
                    }
                }
            }
            return dp[m, n];
        }
    }
}
