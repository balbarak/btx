using Btx.Cipher;
using System;
using System.Text;

namespace Btx.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterSecret master = new MasterSecret();

            var key = AesEncryption.GenerateNewKey();

            System.Console.WriteLine("Key: ");
            System.Console.WriteLine(Convert.ToBase64String(key));

            var encryptedKey = master.Encrypt(key, master.PublicKey);

            System.Console.WriteLine("Encrypted Key: ");
            System.Console.WriteLine(Convert.ToBase64String(encryptedKey));

            var decryptedKey = master.Decrypt(encryptedKey);
            System.Console.WriteLine("Decrypted Key: ");
            System.Console.WriteLine(Convert.ToBase64String(decryptedKey));


            System.Console.ReadKey();

        }
    }
}
