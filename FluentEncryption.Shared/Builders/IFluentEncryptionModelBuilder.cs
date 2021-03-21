using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Shared.Builders
{
    public interface IFluentEncryptionModelBuilder
    {
        IFluentEncryptionBuilder Context { get; }
    }

    public interface IFluentEncryptionModelBuilder<T> 
        : IFluentEncryptionModelBuilder
    {
        IFluentEncryptionModelBuilder<T> RegisterPropertyOrMethod<TSelector>(
            Expression<Func<T, TSelector>> propertyOrMethod,
            EncryptionProfileType profileType);

        
    }
}
