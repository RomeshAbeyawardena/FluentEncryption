using FluentEncryption.Core.Factories;
using FluentEncryption.Core.Services;
using FluentEncryption.Shared.Builders;
using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FluentEncryption.Core
{
    internal class FluentEncryptionBuilder : IFluentEncryptionBuilder
    {
        public event BuildDelegate Build;

        public IFluentEncryptionBuilder RegisterModel<T>(
            Action<IFluentEncryptionModelBuilder<T>> action)
        {
            return RegisterModel(
                new FluentEncryptionModelBuilder<T>(this), 
                action);
        }

        public IFluentEncryptionBuilder RegisterProfile(
            EncryptionProfile encryptionProfile)
        {
            encryptionProfiles.Add(encryptionProfile);
            return this;
        }

        public IFluentEncryptionBuilder BuildProfilesAndModels()
        {
            BuildProviders();
            BuildProfiles();
            BuildModels();
           
            return this;
        }

        public FluentEncryptionBuilder(
            IServiceCollection services)
        {
            this.services = services;
            encryptionProfiles = new List<EncryptionProfile>();
            fluentEncryptionModelBuilders = new List<IFluentEncryptionModelBuilder>();
        }

        internal void BuildProfiles()
        {
            services
                .AddSingleton<IEncryptionProfileFactory, EncryptionProfileFactory>()
                .AddSingleton<IEnumerable<EncryptionProfile>>(encryptionProfiles.ToArray());
        }

        internal void BuildProviders()
        {
            services
                .AddSingleton<IModelEncryptionServiceFactory, ModelEncryptionServiceFactory>()
                .AddSingleton<IEncryptionService, EncryptionService>()
                .AddSingleton(typeof(IModelEncryptionService<>), typeof(ModelEncryptionService<>));
        }

        internal void BuildModels()
        {
            Build?.Invoke(services);
        }

        internal IFluentEncryptionBuilder RegisterModel<T>(
            IFluentEncryptionModelBuilder<T> fluentEncryptionModelBuilder,
            Action<IFluentEncryptionModelBuilder<T>> action)
        {
            fluentEncryptionModelBuilder = fluentEncryptionModelBuilder 
                ?? new FluentEncryptionModelBuilder<T>(this);

            action(fluentEncryptionModelBuilder);

            fluentEncryptionModelBuilders
                .Add(fluentEncryptionModelBuilder);

            return this;
        }

        internal IEnumerable<IFluentEncryptionModelBuilder> FluentEncryptionModelBuilders => fluentEncryptionModelBuilders;

        private readonly List<IFluentEncryptionModelBuilder> fluentEncryptionModelBuilders; 
        private readonly List<EncryptionProfile> encryptionProfiles;
        private readonly IServiceCollection services;
    }
}
