using FluentEncryption.Core;
using FluentEncryption.Core.Factories;
using FluentEncryption.Shared.Builders;
using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using FluentEncryption.Tests.Assets.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentEncryption.Tests
{
    public class FluentEncryptionBuilderTests
    {
        [SetUp]
        public void Setup()
        {
            serviceDescriptors = new List<ServiceDescriptor>();
            servicesMock = new Mock<IServiceCollection>();
            fluentEncryptionModelBuilder = new Mock<IFluentEncryptionModelBuilder<TestModel>>();
            sut = new FluentEncryptionBuilder(servicesMock.Object);

            servicesMock.Setup(services => services.Add(It.IsAny<ServiceDescriptor>()))
                .Callback<ServiceDescriptor>(s => serviceDescriptors.Add(s))
                .Verifiable();
        }

        [Test,
         Description("When registering the encryption profiles, the DI container " +
            " should have an IEncryptionProfileFactory and EncryptionProfile " +
            "available for consumption by other services.")]
        public void RegisterProfile()
        {
            var expectedEncryptionProfile = new EncryptionProfile(EncryptionProfileType.Commmon, new EncryptionSettings());
            var expectedEncryptonProfile2 = new EncryptionProfile(EncryptionProfileType.Personal, new EncryptionSettings());
            sut
                .RegisterProfile(expectedEncryptionProfile)
                .RegisterProfile(expectedEncryptonProfile2);
            
            sut.BuildProfiles();

            servicesMock.Verify();
            
            Assert.IsNotNull(serviceDescriptors.SingleOrDefault(a => a.Lifetime == ServiceLifetime.Singleton 
                && a.ServiceType == typeof(IEncryptionProfileFactory)
                && a.ImplementationType == typeof(EncryptionProfileFactory)));

            var s = serviceDescriptors.SingleOrDefault(a => a.Lifetime == ServiceLifetime.Singleton 
                && a.ServiceType == typeof(IEnumerable<EncryptionProfile>)
                && a.ImplementationInstance.GetType() == typeof(EncryptionProfile[]));

            Assert.IsNotNull(s);

            var encryptionProfiles = s.ImplementationInstance as EncryptionProfile[];

            Assert.IsNotNull(encryptionProfiles
                .FirstOrDefault(a => a == expectedEncryptionProfile));

            Assert.IsNotNull(encryptionProfiles
                .FirstOrDefault(a => a == expectedEncryptonProfile2));
        }

        [Test,
         Description("When registering models, the IFluentEncryptionModelBuilder should be called to register the models, and the" +
            "instance of IFluentEncryptionModelBuilder should be stored by the FluentEncryptionBuilder instance")]
        public void RegisterModel()
        {
            fluentEncryptionModelBuilder
                .Setup(t => t.RegisterPropertyOrMethod(
                    It.IsAny<Expression<Func<TestModel, string>>>(), 
                    It.IsAny<EncryptionProfileType>()))
                .Returns(fluentEncryptionModelBuilder.Object)
                .Verifiable();

            sut.RegisterModel(
                fluentEncryptionModelBuilder.Object,
                builder => builder
                    .RegisterPropertyOrMethod(
                        member => member.EmailAddress,
                        EncryptionProfileType.Personal)
                    .RegisterPropertyOrMethod(
                        member => member.FirstName,
                        EncryptionProfileType.Commmon));

            sut.BuildModels();

            fluentEncryptionModelBuilder.Verify();

            Assert.IsNotNull(sut.FluentEncryptionModelBuilders.SingleOrDefault(a => a == fluentEncryptionModelBuilder.Object));
        }

        private List<ServiceDescriptor> serviceDescriptors;
        private Mock<IFluentEncryptionModelBuilder<TestModel>> fluentEncryptionModelBuilder;
        private Mock<IServiceCollection> servicesMock;
        private FluentEncryptionBuilder sut;
    }
}