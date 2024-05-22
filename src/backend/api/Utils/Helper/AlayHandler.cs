namespace api.Utils.Helper
{
    public class AlayHandler
    {
        public static bool IsTwoStringEqualInAlay(char a, char b)
        {
            a = char.ToLower(a);
            b = char.ToLower(b);

            if(a == b) return true;
            if(a == 'i' && b == '1') return true;
            if(a == '1' && b == 'i') return true;
            if (a == 'a' && b == '4') return true;
            if (a == '4' && b == 'a') return true;
            if (a == 'e' && b == '3') return true;
            if (a == '3' && b == 'e') return true;
            if (a == 'o' && b == '0') return true;
            if (a == '0' && b == 'o') return true;
            if (a == 's' && b == '5') return true;
            if (a == '5' && b == 's') return true;
            return false;
        }
    }
}
