using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FluentEncryption.Shared.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<byte> GetBytes(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static string ToBase64String(this string value, Encoding encoding)
        {
            return Convert.ToBase64String(
                value
                .GetBytes(encoding)
                .ToArray());
        }

        public static string FromBase64String(this string base64Value, Encoding encoding)
        {
            return Convert
                .FromBase64String(base64Value)
                .GetString(encoding);
        }

        public static IEnumerable<byte> GetDerivedBytes(
            this string value, 
            IEnumerable<byte> salt,
            int length)
        {
            var derivedBytes = new Rfc2898DeriveBytes(value, salt.ToArray());

            return derivedBytes.GetBytes(length);
        }

        public static string Encrypt(
            this string value, 
            EncryptionSettings encryptionSettings,
            IStreamFactory<MemoryStream> streamFactory = default)
        {
            return Convert.ToBase64String(
                Encrypt(
                    encryptionSettings.AlgorithmName,
                    encryptionSettings.Key,
                    encryptionSettings.InitialVector,
                    value,
                    streamFactory).ToArray());
        }

        public static string Decrypt(
            this string value,
            EncryptionSettings encryptionSettings,
            IStreamFactory<MemoryStream> streamFactory = default)
        {
            return Decrypt(
                encryptionSettings.AlgorithmName,
                encryptionSettings.Key,
                encryptionSettings.InitialVector,
                Convert.FromBase64String(value),
                streamFactory);
        }

        private static IEnumerable<byte> Encrypt(
            string algorithmName, 
            IEnumerable<byte> key, 
            IEnumerable<byte> initialVector, 
            string value,
            IStreamFactory<MemoryStream> streamFactory = default)
        {
            streamFactory = streamFactory ?? new MemoryStreamFactory();

            using (var symmetricAlgorithm = SymmetricAlgorithm.Create(algorithmName))
            using (var encryptor = symmetricAlgorithm.CreateEncryptor(
                key.ToArray(), 
                initialVector.ToArray()))
            using (var stream = streamFactory.GetStream(Guid.NewGuid()))
            {
                using (var cryptoStreamWriter = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                using (var streamWriter = new StreamWriter(cryptoStreamWriter))
                    streamWriter.Write(value);

                return stream.ToArray();
            }
        }

        private static string Decrypt(
            string algorithmName, 
            IEnumerable<byte> key, 
            IEnumerable<byte> initialVector, 
            IEnumerable<byte> rawStringBytes,
            IStreamFactory<MemoryStream> streamFactory = default)
        {
            streamFactory = streamFactory ?? new MemoryStreamFactory();

            using (var symmetricAlgorithm = SymmetricAlgorithm.Create(algorithmName))
            using (var encryptor = symmetricAlgorithm.CreateDecryptor(key.ToArray(), initialVector.ToArray()))
            using (var stream = streamFactory.GetStream(Guid.NewGuid(), rawStringBytes))
            using (var cryptoStreamReader = new CryptoStream(stream, encryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStreamReader))
                return streamReader.ReadToEnd();
        }
    }
}
