using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Definitions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Factories
{
    internal class ModelEncryptionServiceFactory : IModelEncryptionServiceFactory
    {
        public IModelEncryptionService<T> GetService<T>()
        {
            return serviceProvider
                .GetRequiredService<IModelEncryptionService<T>>();
        }

        public ModelEncryptionServiceFactory(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;
    }
}
