using FluentEncryption.Shared.Contracts.Factories;
using System;

namespace FluentEncryption.Extension
{
    public static class ModelEncryptionServiceFactoryExtensions
    {
        public static T Encrypt<T>(
            this IModelEncryptionServiceFactory modelEncryptionServiceFactory, 
            T model)
        {
            return modelEncryptionServiceFactory
                .Encrypt(model);
        }

        public static T Decrypt<T>(
            this IModelEncryptionServiceFactory modelEncryptionServiceFactory, 
            T model)
        {
            return modelEncryptionServiceFactory
                .Decrypt(model);
        }
    }
}
