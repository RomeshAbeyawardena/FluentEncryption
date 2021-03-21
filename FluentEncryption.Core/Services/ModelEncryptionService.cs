using AutoMapper;
using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Definitions;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using FluentEncryption.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core.Services
{
    internal class ModelEncryptionService<T> : IModelEncryptionService<T>
    {
        public T Decrypt(T model)
        {
            return Invoke(model,
                (oldValue, type) => encryptionService
                    .Decrypt(
                        oldValue.ToString(), 
                        encryptionProfileFactory.GetEncryptionSettings(type)));
        }

        public T Encrypt(T model)
        {
            return Invoke(model,
                (oldValue, type) => encryptionService
                    .Encrypt(
                        oldValue.ToString(), 
                        encryptionProfileFactory.GetEncryptionSettings(type)));
        }

        public ModelEncryptionService(
            IEncryptionService encryptionService,
            IEncryptionProfileFactory encryptionProfileFactory,
            IDictionary<Type, IModelEncryptionDefinition> modelEncryptionDefinitionDictionary)
        {
            this.encryptionService = encryptionService;
            this.encryptionProfileFactory = encryptionProfileFactory;
            if (!modelEncryptionDefinitionDictionary
                .TryGetValue(typeof(T), out var modelEncryptionDefinition))
            {
                throw new NotSupportedException();
            }

            this.modelEncryptionDefinition = modelEncryptionDefinition;
        }

        private T Invoke(T model, Func<object, EncryptionProfileType, object> invoke)
        {
            var newInstance = model.Clone();
            foreach (var (key, value) in modelEncryptionDefinition.Definitions)
            {
                if(!(key is PropertyInfo property))
                {
                    continue;
                }
                var oldValue = property.GetValue(model);

                if(oldValue == null)
                {
                    continue;
                }

                property.SetValue(newInstance, invoke(oldValue, value));
            }

            return newInstance;
        }

        private readonly IEncryptionService encryptionService;
        private readonly IEncryptionProfileFactory encryptionProfileFactory;
        private readonly IModelEncryptionDefinition modelEncryptionDefinition;
    }
}
