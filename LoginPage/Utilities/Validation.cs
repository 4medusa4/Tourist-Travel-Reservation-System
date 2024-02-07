namespace LoginPage.Utilities
{
    internal class Validation
    {
        public static bool isAlpha(string text)
        {
            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                if (char.IsLetter(text[i]))
                    return true;
            }
            return false;
        }
    }
}
