﻿using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace People
{
    public static class CryptoHelper
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private static int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        private const int BlockSize = 128;

        // size in bytes
        public static string Encrypt(string plainText, string passPhrase, int size)
        {
            CryptoHelper.Keysize = size * 8;

            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = GenerateBitsOfRandomEntropy(BlockSize / 8);
            var ivStringBytes = GenerateBitsOfRandomEntropy(BlockSize / 8);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new AesManaged())
                {

                    symmetricKey.BlockSize = BlockSize;
                    symmetricKey.KeySize = size * 8;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Encrypt(string plainText, string passPhrase)
        {
            CryptoHelper.Keysize = 32 * 8;
            return Encrypt(plainText, passPhrase, 32);
        }

        public static string Decrypt(string plainText, string passPhrase)
        {
            CryptoHelper.Keysize = 32 * 8;
            return Decrypt(plainText, passPhrase, 32);
        }

        // size in bytes
        public static string Decrypt(string cipherText, string passPhrase, int size)
        {
            CryptoHelper.Keysize = size * 8;

            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(BlockSize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(BlockSize / 8).Take(BlockSize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((BlockSize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((BlockSize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new AesManaged())
                {
                    symmetricKey.BlockSize = BlockSize;
                    symmetricKey.KeySize = size * 8;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public static byte[] Generate256BitsOfRandomEntropy()
        {
            return GenerateBitsOfRandomEntropy(32);
        }

        // size in bytes
        public static byte[] GenerateBitsOfRandomEntropy(int size)
        {
            var randomBytes = new byte[size]; // 16 Bytes will give us 128 bits, 32 will give 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

    }
}