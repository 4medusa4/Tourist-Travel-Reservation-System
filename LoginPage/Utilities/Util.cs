using System.Security.Cryptography;
using System.Text;

namespace LoginPage.Utilities
{
    internal class Util
    {
        public static string getNextId(string id, int inc)
        {
            if(id != null)
            {
                string prefix = "";
                int prefix_len = 0;
                int str_len = id.Length;
                for (int i = 0; i < str_len; i++)
                {
                    if(char.IsLetter(id[i]))
                    {
                        prefix_len++;
                        prefix += char.ToString(id[i]);
                    }
                }
                id = id.Substring(prefix_len);
                int sufix_len = id.Length;
                int num = int.Parse(id) + inc;
                id = "" + num;
                for (int i = 0; i < sufix_len; i++)
                {
                    if (sufix_len > id.Length)
                    {
                        id = "0" + id;
                    }
                }
                return prefix + id;
            }
            return "";
        }

        public static string encrypt(string text)
        {
            byte[] enc = null;
            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256hash = SHA256.Create())
            {
                enc = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                for (int j = 0; j < enc.Length; j++)
                {
                    builder.Append(enc[j].ToString("x2"));
                }
            }
            return builder.ToString();
        }
    }
}
