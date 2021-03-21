using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Contracts.Services
{
    public interface IModelEncryptionService<T>
    {
        T Encrypt(T model);
        T Decrypt(T model);
    }
}
