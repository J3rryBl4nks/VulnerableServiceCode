using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace VulnerableService
{
    public class Encryption
    {
        public string encryptionKey = "xH*Su^4*eQwW7VNHyVNJ6$N^AR3S&SfkbkB!LDa98r%Gn";
        public string initializationVector = "Qbfa7ct#u#*9YVtfCyp3Z89iA^%G!QMtXS5ixc@vyRFEv";
        AesCryptoServiceProvider encryptor = new AesCryptoServiceProvider();

        public string Encrypt(string dataToEncrypt)
        {
            encryptor.BlockSize = 128;
            encryptor.KeySize = 128;
            encryptor.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
            encryptor.IV = System.Text.Encoding.UTF8.GetBytes(initializationVector);
            encryptor.Padding = PaddingMode.PKCS7;
            encryptor.Mode = CipherMode.CBC;

            ICryptoTransform crypto = encryptor.CreateEncryptor(encryptor.Key, encryptor.IV);

            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(dataToEncrypt);

            byte[] encryptedDataBytes = crypto.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

            string encryptedData = Encoding.UTF8.GetString(encryptedDataBytes, 0, encryptedDataBytes.Length);
            return string.Empty;
        }

        public string Decrypt(string dataToDecrypt)
        {
            byte[] decryptbytes = System.Text.Encoding.UTF8.GetBytes(dataToDecrypt);
            encryptor.BlockSize = 128;
            encryptor.KeySize = 128;
            encryptor.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
            encryptor.IV = System.Text.Encoding.UTF8.GetBytes(initializationVector);
            encryptor.Padding = PaddingMode.PKCS7;
            encryptor.Mode = CipherMode.CBC;

            ICryptoTransform decrypt = encryptor.CreateDecryptor(encryptor.Key, encryptor.IV);

            byte[] decryptedData = decrypt.TransformFinalBlock(decryptbytes, 0, decyptbytes.Length);

            decrypt.Dispose();

            string clearText = Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);

            return clearText;
        }
        
        public string DecryptLocalSecret(string encryptedData)
        {
            return  Encoding.UTF8.GetString(ProtectedData.UnProtect((ConvertFrom.Base64String(encryptedData), (byte[])null, DataProtectionScope.LocalMachine));
        }
    }
}
