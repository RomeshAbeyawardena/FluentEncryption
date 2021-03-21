using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Extensions
{
    public static class GenericExtensions
    {
        public static T Clone<T>(this T value)
        {
            var properties = typeof(T).GetProperties();

            var newInstance = Activator.CreateInstance<T>();
            foreach (var property in properties)
            {
                var oldValue = property.GetValue(value);

                if(oldValue == null)
                {
                    continue;
                }

                property.SetValue(newInstance, oldValue);
            }

            return newInstance;
        }
    }
}
