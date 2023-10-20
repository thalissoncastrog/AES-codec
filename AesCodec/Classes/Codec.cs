using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AesCodec.Classes {
    internal class Codec {

        private byte[] TextBytes { get; set; }
        private byte[] Password { get; set; }
        private byte[] Iv {  get; set; }

        public Codec(byte[] textBytes, byte[] password) {
            this.TextBytes = textBytes;

            int count = password.Length;

            if (count < 32) {
                Array.Resize(ref password, 32);
            }

            this.Password = password;

            byte[] iv = new byte[16] {0, 1 ,2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};  // 16-byte initialization vector
            // new Random().NextBytes(iv);

            this.Iv = iv;
        }  

        public string Encode() {
            byte[] encodedBytes = null;

            // Set up the encryption objects
            using (Aes aes = Aes.Create()) {
                aes.Key = this.Password;
                aes.IV = this.Iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Encrypt the input plaintext using the AES algorithm
                using (ICryptoTransform encryptor = aes.CreateEncryptor()) {
                    encodedBytes = encryptor.TransformFinalBlock(this.TextBytes, 0, TextBytes.Length);
                }
            }

            string encodedText = Convert.ToBase64String(encodedBytes);

            return encodedText;
        }

        public string Decode() {
            byte[] decodedBytes = null;
            byte[] encodedBytes = null;

            using (Aes aesInput = Aes.Create()) {
                aesInput.Key = this.Password;
                aesInput.IV = this.Iv;
                aesInput.Mode = CipherMode.CBC;
                aesInput.Padding = PaddingMode.PKCS7;

                // Encrypt the input plaintext using the AES algorithm
                using (ICryptoTransform encryptor = aesInput.CreateEncryptor()) {
                    encodedBytes = encryptor.TransformFinalBlock(this.TextBytes, 0, TextBytes.Length);
                }
            }

            // Set up the encryption objects
            using (Aes aes = Aes.Create()) {
                aes.Key = this.Password;
                aes.IV = this.Iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Decrypt the input ciphertext using the AES algorithm
                using (ICryptoTransform decryptor = aes.CreateDecryptor()) {
                    decodedBytes = decryptor.TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);
                }
            }

            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            return decodedText;
        }
    }
}
