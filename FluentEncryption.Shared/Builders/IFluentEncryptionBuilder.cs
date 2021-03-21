using FluentEncryption.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Builders
{
    public delegate void BuildDelegate(IServiceCollection services);
    public interface IFluentEncryptionBuilder
    {
        event BuildDelegate Build;
        IFluentEncryptionBuilder RegisterModel<T>(
            Action<IFluentEncryptionModelBuilder<T>> action);
        IFluentEncryptionBuilder RegisterProfile(EncryptionProfile encryptionProfile);
        IFluentEncryptionBuilder BuildProfilesAndModels();
    }
}
