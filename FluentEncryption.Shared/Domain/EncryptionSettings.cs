using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Domain
{
    public class EncryptionSettings
    {
        public string AlgorithmName { get; internal set; }
        public IEnumerable<byte> Key { get; internal set; }
        public IEnumerable<byte> InitialVector { get; internal set; }
    }
}
