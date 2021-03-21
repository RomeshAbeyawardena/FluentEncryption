using FluentEncryption.Core;
using FluentEncryption.Shared.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IFluentEncryptionBuilder RegisterFluentEncryption(
            this IServiceCollection services,
            Action<IFluentEncryptionBuilder> build)
        {
            var fluentEncrpytionBuilder = new FluentEncryptionBuilder(services);
            build(fluentEncrpytionBuilder);
            return fluentEncrpytionBuilder;
        }
    }
}
