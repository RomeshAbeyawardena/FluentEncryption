using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Services
{
    internal class EncryptionService : IEncryptionService
    {
        public string Decrypt(string v, EncryptionSettings encryptionSettings)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string oldValue, EncryptionSettings encryptionSettings)
        {
            throw new NotImplementedException();
        }
    }
}
