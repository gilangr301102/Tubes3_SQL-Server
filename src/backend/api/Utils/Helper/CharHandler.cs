namespace api.Utils.Helper
{
    public class CharHandler
    {

        public static int NormalizeAlayChar(char a, char b, char c)
        {
            if (a == '1' && b == '3' && c == 'b') return 2;
            if (a == '1' && b == '2' && c == 'r') return 2;
            if (a == '1' && b == '7' && c == 'd') return 2;
            if (a == '1' && (c == 'i' || c == 'l')) return 1;
            if (a == '4' && c == 'a') return 1;
            if (a == '3' && c == 'e') return 1;
            if (a == '0' && c == 'o') return 1;
            if (a == '5' && c == 's') return 1;
            if (a == '6' && c == 'g') return 1;
            if (a == '7' && (c == 't' || c == 'j')) return 1;
            if (a == '8' && c == 'b') return 1;
            if (a == '9' && c == 'g') return 1;
            if (char.ToLower(a) == c) return 1;
            return 0;
        }
    }
}
