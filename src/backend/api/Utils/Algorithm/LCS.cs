﻿namespace api.Utils.Algorithm
{
    public class LCS
    {
        // Longest Common Subsequence (non-contiguous)
        // Implemented using sum prefix with Dynamic Programming
        public int ComputeSimilarity(string str1, string str2)
        {
            if (str1 == "" || str2 == "")
                return 0;
            int m = str1.Length;
            int n = str2.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                dp[i, 0] = 0;
            }
            for (int j = 0; j <= n; j++)
            {
                dp[0, j] = 0;
            }
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
    }
}
