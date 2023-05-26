using System.Security.Cryptography;
using System.Text;

namespace Sistema.Class
{
    public class ClassSeguridad
    {
        private readonly string _key;

        TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();

        public ClassSeguridad()
        {
            _key = "SISTEMA_NET";
            tripleDESCryptoServiceProvider.Key = TruncateHash(_key, tripleDESCryptoServiceProvider.KeySize / 8);
            tripleDESCryptoServiceProvider.IV = TruncateHash("", tripleDESCryptoServiceProvider.BlockSize / 8);
        }

        private byte[] TruncateHash(string key, int length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            var oldHash = hash;
            hash = new byte[length - 1 + 1];

            if (oldHash != null)
                Array.Copy(oldHash, hash, Math.Min(length - 1 + 1, oldHash.Length));

            return hash;
        }

        public string EncryptData(string plaintext)
        {
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(@$"{plaintext}");
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, tripleDESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string EncryptDataEnviroment(string key, string plaintext)
        {
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(@$"{plaintext}");
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, tripleDESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();
            string value = Convert.ToBase64String(ms.ToArray());
            Environment.SetEnvironmentVariable(key, value);
            return value;
        }

        public string DecryptData(string encryptedtext)
        {
            byte[] encryptedBytes = Convert.FromBase64String(@$"{encryptedtext}");

            MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream decStream = new CryptoStream(ms, tripleDESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);

            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            return Encoding.Unicode.GetString(ms.ToArray());
        }

        public (int usuario, int hotel, string token) DecryptDataCookie(string encryptedtext)
        {
            byte[] encryptedBytes = Convert.FromBase64String(@$"{encryptedtext}");

            MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream decStream = new CryptoStream(ms, tripleDESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);

            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            string[] data = Encoding.Unicode.GetString(ms.ToArray()).Split("|");

            if (data.Length > 2)
            {
                return (ClassUtilidad.parseMultiple(data[0], ClassUtilidad.TipoDato.Integer).numero, ClassUtilidad.parseMultiple(data[1], ClassUtilidad.TipoDato.Integer).numero, data[2]);
            }

            return (0, 0, String.Empty);
        }

        public (string, int, bool) DecryptDataEnviroment(string encryptedtext, ClassUtilidad.TipoDato tipo)
        {
            (string str, int i, bool logic) = ("", 0, false);

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(Environment.GetEnvironmentVariable(@$"{encryptedtext}"));

                MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream decStream = new CryptoStream(ms, tripleDESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);

                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();

                switch ((int)tipo)
                {
                    case 0:
                        str = Encoding.Unicode.GetString(ms.ToArray());
                        break;
                    case 1:
                        int.TryParse(Encoding.Unicode.GetString(ms.ToArray()), out i);
                        break;
                    case 2:
                        bool.TryParse(Encoding.Unicode.GetString(ms.ToArray()), out logic);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(@$"DecryptDataEnviroment: error al encriptar la variable de entorno {encryptedtext}");
            }

            return (str, i, logic);
        }

        public string GenerateToken()
        {
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(@$"{ClassUtilidad.GUID()}");
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, tripleDESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray()).Replace('=', 'x');
        }
    }
}
