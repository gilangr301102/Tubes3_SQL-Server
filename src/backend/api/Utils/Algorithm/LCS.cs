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
            int m = str1.Length;
            int n = str2.Length;
            int[,] dp = new int[m + 1, n + 1];
            bool[] isVisited = new bool[m + 1];
            for (int i = 0; i <= m; i++) dp[i, 0] = 0;
            for (int j = 0; j <= n; j++) dp[0, j] = 0;
            for (int i = 0; i <= m; i++) isVisited[i] = false;
            for (int i = 1; i <= m; i++)
            {
                bool isType1 = false;
                bool isType2 = false;
                for (int j = 1; j <= n; j++)
                {
                    char currentStr1Char = str1[i - 1];
                    char nextStr1Char = (i < m && (str1[i-1]=='1')) ? str1[i] : '\0';
                    int tempTypeNormalize = CharHandler.NormalizeAlayChar(currentStr1Char, nextStr1Char, str2[j - 1]);
                    if(tempTypeNormalize==1) isType1 = true;
                    else if(tempTypeNormalize==2) isType2 = true;
                    // Handle case get maximum subsequence for choose alay converter for two number into one letter
                    // For example: "12"->"R", "13"->"B", "17"->"D", etc.
                    if (tempTypeNormalize>0 && !isVisited[i-1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        if(tempTypeNormalize==2 && dp[i + 1, j] < dp[i, j]) {
                            dp[i + 1, j] = dp[i, j];
                        }
                    }
                    else
                    {
                        if (dp[i - 1, j] >= dp[i, j - 1])
                            dp[i, j] = dp[i - 1, j];
                        else
                            dp[i, j] = dp[i, j - 1];
                    }
                }
                // Handle case get maximum subsequence for choose alay converter
                // For example: alay: "171317" and "DbieD" it will be count as "DieD"
                // So we need to check if the next character is alay converter
                // If it is, we need to skip the current character
                if(isType2 && !isType1) isVisited[i] = true;
            }
            return dp[m, n];
        }
    }
}
