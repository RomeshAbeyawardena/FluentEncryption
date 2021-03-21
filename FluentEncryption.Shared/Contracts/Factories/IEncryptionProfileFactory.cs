using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Contracts.Factories
{
    public interface IEncryptionProfileFactory
    {
        EncryptionSettings GetEncryptionSettings(
            EncryptionProfileType type);
    }
}
