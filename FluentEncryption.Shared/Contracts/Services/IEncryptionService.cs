using FluentEncryption.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Contracts.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string oldValue, EncryptionSettings encryptionSettings);
        string Decrypt(string v, EncryptionSettings encryptionSettings);
    }
}
