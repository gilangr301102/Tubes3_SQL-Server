namespace api.Utils.Algorithm
{
    public class KMP
    {
        static List<int> PrefixFunc(string s)
        {
            int n = s.Length;
            List<int> f = new(n)
            {
                0 // Initialize the first element
            };
            for (int i = 1; i < n; i++)
            {
                int j = f[i - 1];
                while (j > 0 && s[i] != s[j])
                    j = f[j - 1];
                f.Add(j + (s[i] == s[j] ? 1 : 0));
            }
            return f;
        }

        public bool CountOccurrences(string s, string t)
        {
            string ts = t + "#" + s;
            int n = t.Length;
            _ = s.Length;
            int nm = ts.Length;
            List<int> f = PrefixFunc(ts);
            int res = 0;
            for (int i = n + 1; i < nm; i++)
            {
                if (f[i] == n) return true;
            }
            return false;
        }
    }
}
