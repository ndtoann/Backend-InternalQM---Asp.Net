using System.Security.Cryptography;
using System.Text;

namespace Backend_InternalQM.Utils
{
    public static class CheckPass
    {
        public static string ComputeSha256Hash(this string username, string salt, string pass)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes($"{username} - {salt} - {pass}"));
            foreach (var theByte in crypto)
                hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public static bool ValidPassword(this string username, string salt, string pass, string passHash)
        {
            return ComputeSha256Hash(username, salt, pass).Equals(passHash);
        }

        public static string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
