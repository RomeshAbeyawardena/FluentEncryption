using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Services
{
    internal class EncryptionService : IEncryptionService
    {
        public string Decrypt(
            string value, 
            EncryptionSettings encryptionSettings)
        {
            return value.Decrypt(
                encryptionSettings,
                memoryStreamFactory);
        }

        public string Encrypt(
            string value, 
            EncryptionSettings encryptionSettings)
        {
            return value.Encrypt(encryptionSettings,
                memoryStreamFactory);
        }

        public EncryptionService(
            IStreamFactory<MemoryStream> memoryStreamFactory)
        {
            this.memoryStreamFactory = memoryStreamFactory;
        }

        private readonly IStreamFactory<MemoryStream> memoryStreamFactory;
    }
}
