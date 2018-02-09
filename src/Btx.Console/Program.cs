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
            //TestEncryption(master);

            string msg = "Hello world";
            var data = Encoding.UTF8.GetBytes(msg);

            var result = BtxEncryption.Encrypt(data, master, master.PublicKey);

            System.Console.WriteLine("Encrypted Key: ");
            System.Console.WriteLine(result.Key);
            System.Console.WriteLine("Encrypted Data: ");
            System.Console.WriteLine(result.Data);

            var decryptedData = BtxEncryption.Decrypt(result, master);

            System.Console.WriteLine("Decrypted Key: ");
            System.Console.WriteLine(decryptedData.Key);
            System.Console.WriteLine("Decrypted Data: ");
            System.Console.WriteLine(decryptedData.Data);


            System.Console.ReadKey();

        }

        private static void TestEncryption(MasterSecret master)
        {
            var key = AesEncryption.GenerateNewKey();

            System.Console.WriteLine("Key: ");
            System.Console.WriteLine(Convert.ToBase64String(key));

            var encryptedKey = master.Encrypt(key, master.PublicKey);

            System.Console.WriteLine("Encrypted Key: ");
            System.Console.WriteLine(Convert.ToBase64String(encryptedKey));

            var decryptedKey = master.Decrypt(encryptedKey);
            System.Console.WriteLine("Decrypted Key: ");
            System.Console.WriteLine(Convert.ToBase64String(decryptedKey));
        }
    }
}
