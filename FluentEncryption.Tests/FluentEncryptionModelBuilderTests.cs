using FluentEncryption.Core;
using FluentEncryption.Shared.Builders;
using FluentEncryption.Shared.Definitions;
using FluentEncryption.Shared.Enumerations;
using FluentEncryption.Tests.Assets.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static FluentEncryption.Tests.FluentEncryptionBuilderTests;

namespace FluentEncryption.Tests
{
    public class FluentEncryptionModelBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            servicesMock = new Mock<IServiceCollection>();
            serviceDescriptors = new List<ServiceDescriptor>();
            servicesMock.Setup(services => services.Add(It.IsAny<ServiceDescriptor>()))
                .Callback<ServiceDescriptor>(s => serviceDescriptors.Add(s))
                .Verifiable();

            fluentEncryptionBuilder = new Mock<IFluentEncryptionBuilder>();

            sut = new FluentEncryptionModelBuilder<TestModel>(fluentEncryptionBuilder.Object);
        }

        [Test]
        public void RegisterPropertyOrMethod()
        {
            
            sut.RegisterPropertyOrMethod(s => s.EmailAddress, EncryptionProfileType.Personal);
            sut.RegisterPropertyOrMethod(s => s.FirstName, EncryptionProfileType.Commmon);

            Assert.True(sut.ModelEncryptionDefinitionDictionary.TryGetValue(
                typeof(TestModel), 
                out var modelEncryptionDefinition));

            Assert.True(modelEncryptionDefinition.Definitions.Count == 2);

            fluentEncryptionBuilder.Raise(n => n.Build += null, 
                servicesMock.Object);

            Assert.IsNotNull(serviceDescriptors.SingleOrDefault(
                a => a.Lifetime == ServiceLifetime.Singleton 
                && a.ServiceType == typeof(IDictionary<Type, IModelEncryptionDefinition>)));
        }

        private List<ServiceDescriptor> serviceDescriptors;
        private Mock<IServiceCollection> servicesMock;
        private Mock<IFluentEncryptionBuilder> fluentEncryptionBuilder; 
        private FluentEncryptionModelBuilder<TestModel> sut;
    }
}
