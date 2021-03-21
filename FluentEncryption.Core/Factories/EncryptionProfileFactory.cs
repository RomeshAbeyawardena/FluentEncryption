using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Factories
{
    internal class EncryptionProfileFactory : IEncryptionProfileFactory
    {
        public EncryptionSettings GetEncryptionSettings(EncryptionProfileType type)
        {
            return encryptionProfiles
                .SingleOrDefault(profile => profile.Type ==  type)?
                .Settings ?? throw new NotSupportedException();
        }

        public EncryptionProfileFactory(IEnumerable<EncryptionProfile> encryptionProfiles)
        {
            this.encryptionProfiles = encryptionProfiles;
        }

        private readonly IEnumerable<EncryptionProfile> encryptionProfiles;
    }
}
