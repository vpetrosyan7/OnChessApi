using System.Security.Cryptography;
using System.Text;

namespace OnChessApi.Services
{
    public class CryptService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public CryptService()
        {
            _key = Encoding.ASCII.GetBytes("3gT8#PzW7^F5!cQn");
            _iv = Encoding.ASCII.GetBytes("gT87^5!Q");

            this.Init();
        }

        public void Init()
        {
            using RC2 rc2 = RC2.Create();

            rc2.Key = _key;
            rc2.IV = _iv;
        }

        public string Encrypt(string input)
        {
            using (RC2 rc2 = RC2.Create())
            {
                ICryptoTransform encryptor = rc2.CreateEncryptor(_key, _iv);

                using (MemoryStream msEncrypt = new())
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new(csEncrypt))
                    {
                        swEncrypt.Write(input);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string input)
        {
            using (RC2 rc2 = RC2.Create())
            {
                ICryptoTransform decryptor = rc2.CreateDecryptor(_key, _iv);

                using (MemoryStream msDecrypt = new(Convert.FromBase64String(input)))
                using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public string EncryptPassword(string password, string salt)
        {
            return this.EncryptMD5($"{password}_{salt}");
        }

        public bool VerifyPassword(string password, string salt, string hash)
        {
            return string.Equals(this.EncryptPassword(password, this.Decrypt(salt)), hash, StringComparison.Ordinal);
        }

        public string EncryptMD5(string input)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    StringBuilder sb = new();
                    foreach (byte b in md5.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    {
                        sb.Append(b.ToString("x2"));
                    }

                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
