using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Contracts.Factories
{
    public interface IStreamFactory<TStream>
        where TStream: Stream
    {
        TStream GetStream(Guid key);
        TStream GetStream(Guid key, IEnumerable<byte> bytes);
    }
}
