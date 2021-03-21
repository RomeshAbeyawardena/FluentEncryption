using FluentEncryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Definitions
{
    public interface IModelEncryptionDefinition
    {
        IReadOnlyDictionary<MemberInfo, EncryptionProfileType> Definitions { get; }

        IModelEncryptionDefinition Add(
            MemberInfo member, 
            EncryptionProfileType type);
    }
}
