using FluentEncryption.Shared.Definitions;
using FluentEncryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Definitions
{
    internal class ModelEncryptionDefinition : IModelEncryptionDefinition
    {
        public IModelEncryptionDefinition Add(MemberInfo member, EncryptionProfileType type)
        {
            memberProfileTypeDictionary.Add(member, type);
            return this;
        }

        public  IReadOnlyDictionary<MemberInfo, EncryptionProfileType> Definitions => new ReadOnlyDictionary<MemberInfo, EncryptionProfileType>(memberProfileTypeDictionary);

        public ModelEncryptionDefinition(
            MemberInfo member, 
            EncryptionProfileType type)
        {
            memberProfileTypeDictionary = new Dictionary<MemberInfo, EncryptionProfileType>
            {
                { member, type }
            };
        }

        private readonly IDictionary<MemberInfo, EncryptionProfileType> memberProfileTypeDictionary;
    }
}
