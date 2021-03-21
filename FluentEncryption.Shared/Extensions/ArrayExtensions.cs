using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Extensions
{
    public static class ArrayExtensions
    {
        public static string GetString(
            this IEnumerable<byte> bytes,
            Encoding encoding)
        {
            return encoding.GetString(
                bytes.ToArray());
        }
    }
}
