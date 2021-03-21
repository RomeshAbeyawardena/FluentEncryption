using FluentEncryption.Shared.Contracts.Factories;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Factories
{
    internal class MemoryStreamFactory : IStreamFactory<MemoryStream>
    {
        public MemoryStream GetStream(Guid key)
        {
            return memoryStreamManager.GetStream(key);
        }

        public MemoryStream GetStream(Guid key, IEnumerable<byte> bytes)
        {
            return memoryStreamManager
                .GetStream(key, string.Empty, bytes.ToArray());
        }

        public MemoryStreamFactory(
            RecyclableMemoryStreamManager memoryStreamManager = default)
        {
            this.memoryStreamManager = memoryStreamManager 
                ?? new RecyclableMemoryStreamManager();
        }

        private readonly RecyclableMemoryStreamManager memoryStreamManager;
    }
}
