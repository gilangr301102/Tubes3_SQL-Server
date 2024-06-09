using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace api.Utils.Helper
{
    public static class AesEncryption
    {
        private static readonly byte[] key = Encoding.UTF8.GetBytes("Yi1@2zXP9#n&zJ7!Fm4qLp3v"); // Must be 16, 24, or 32 bytes for AES
        private static readonly byte[] iv = new byte[16]; // Initialization vector

        public static string EncryptString(string plainText)
        {
            // if (plainText == null)
            //     throw new ArgumentNullException(nameof(plainText));

            // byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // // Add padding if necessary
            // int padding = 16 - (plainBytes.Length % 16);
            // byte[] paddedBytes = new byte[plainBytes.Length + padding];
            // Array.Copy(plainBytes, paddedBytes, plainBytes.Length);

            // // Encrypt each block manually
            // using (MemoryStream msEncrypt = new MemoryStream())
            // {
            //     for (int i = 0; i < paddedBytes.Length; i += 16)
            //     {
            //         byte[] block = new byte[16];
            //         Array.Copy(paddedBytes, i, block, 0, 16);

            //         byte[] encryptedBlock = EncryptBlock(block);
            //         msEncrypt.Write(encryptedBlock, 0, encryptedBlock.Length);
            //     }
            //     byte[] encryptedBytes = msEncrypt.ToArray();
            //     return Convert.ToBase64String(encryptedBytes);
            // }
            return plainText;
        }

        public static string DecryptString(string cipherText)
        {
            // if (cipherText == null)
            //     throw new ArgumentNullException(nameof(cipherText));

            // byte[] cipherBytes = Convert.FromBase64String(cipherText);

            // // Decrypt each block manually
            // using (MemoryStream msDecrypt = new MemoryStream())
            // {
            //     for (int i = 0; i < cipherBytes.Length; i += 16)
            //     {
            //         byte[] block = new byte[16];
            //         Array.Copy(cipherBytes, i, block, 0, 16);

            //         byte[] decryptedBlock = DecryptBlock(block);
            //         msDecrypt.Write(decryptedBlock, 0, decryptedBlock.Length);
            //     }

            //     byte[] decryptedBytes = msDecrypt.ToArray();
            //     // Remove padding
            //     int padding = decryptedBytes[decryptedBytes.Length - 1];
            //     byte[] unpaddedBytes = new byte[decryptedBytes.Length - padding];
            //     Array.Copy(decryptedBytes, unpaddedBytes, unpaddedBytes.Length);
            //     return Encoding.UTF8.GetString(unpaddedBytes);
            // }
            return cipherText;
        }

        private static byte[] EncryptBlock(byte[] block)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                    {
                        // Create a CryptoStream to perform the encryption
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            // Write the input data to the CryptoStream
                            csEncrypt.Write(block, 0, block.Length);
                            // Flush the CryptoStream to ensure all data is written
                            csEncrypt.FlushFinalBlock();
                        }
                    }
                    // Get the encrypted bytes from the MemoryStream
                    byte[] encryptedBytes = msEncrypt.ToArray();
                    return encryptedBytes;
                }
            }
        }

        private static byte[] DecryptBlock(byte[] block)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream msDecrypt = new MemoryStream(block))
                {
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (MemoryStream msDecryptedData = new MemoryStream())
                            {
                                // Read the entire encrypted block
                                csDecrypt.CopyTo(msDecryptedData);
                                byte[] decryptedBytes = msDecryptedData.ToArray();

                                // Remove padding
                                int padding = decryptedBytes[decryptedBytes.Length - 1];
                                byte[] unpaddedBytes = new byte[decryptedBytes.Length - padding];
                                Array.Copy(decryptedBytes, unpaddedBytes, unpaddedBytes.Length);
                                return unpaddedBytes;
                            }
                        }
                    }
                }
            }
        }
    }
}
