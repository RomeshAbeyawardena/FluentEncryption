using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<byte> GetBytes(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static string ToBase64String(this string value, Encoding encoding)
        {
            return Convert.ToBase64String(
                value
                .GetBytes(encoding)
                .ToArray());
        }

        public static string FromBase64String(this string base64Value, Encoding encoding)
        {
            return Convert
                .FromBase64String(base64Value)
                .GetString(encoding);
        }
    }
}
