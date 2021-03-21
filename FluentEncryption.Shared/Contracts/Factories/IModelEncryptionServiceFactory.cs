using FluentEncryption.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Contracts.Factories
{
    public interface IModelEncryptionServiceFactory
    {
        IModelEncryptionService<T> GetService<T>();
    }
}
