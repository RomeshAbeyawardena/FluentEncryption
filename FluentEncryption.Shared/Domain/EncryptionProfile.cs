using FluentEncryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Domain
{
    public class EncryptionProfile
    {
        public EncryptionProfileType Type { get; set; }
        public EncryptionSettings Settings { get; set; }

        public EncryptionProfile(EncryptionProfileType type, EncryptionSettings settings)
        {
            Type = type;
            Settings = settings;
        }
    }
}
