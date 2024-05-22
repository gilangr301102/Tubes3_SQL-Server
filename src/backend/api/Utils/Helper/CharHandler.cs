namespace api.Utils.Helper
{
    public class CharHandler
    {

        public static char NormalizeAlayChar(char a)
        {
            if (a == '1') return 'i';
            if (a == '4') return 'a';
            if (a == '3') return 'e';
            if (a == '0') return 'o';
            if (a == '5') return 's';

            return char.ToLower(a);
        }
    }
}
