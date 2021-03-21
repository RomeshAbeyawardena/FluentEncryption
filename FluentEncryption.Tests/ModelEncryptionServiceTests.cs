using FluentEncryption.Core.Definitions;
using FluentEncryption.Core.Services;
using FluentEncryption.Shared.Contracts.Factories;
using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Definitions;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using FluentEncryption.Tests.Assets.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentEncryption.Shared.Extensions;

namespace FluentEncryption.Tests
{
    public class ModelEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            encryptionProfileFactoryMock = new Mock<IEncryptionProfileFactory>();
            encryptionServiceMock = new Mock<IEncryptionService>();

            dictionary = new Dictionary<Type, IModelEncryptionDefinition>();
            
            var testModelType = typeof(TestModel);

            var modelEncryptionDefinition = new ModelEncryptionDefinition(
                    testModelType.GetProperty(nameof(TestModel.EmailAddress)), 
                    EncryptionProfileType.Commmon);

            modelEncryptionDefinition
                .Add(testModelType.GetProperty(nameof(TestModel.FirstName)), 
                    EncryptionProfileType.Commmon);

            dictionary.Add(
                testModelType, 
                modelEncryptionDefinition);

            sut = new ModelEncryptionService<TestModel>(
                encryptionServiceMock.Object,
                encryptionProfileFactoryMock.Object,
                dictionary);
        }

        [Test]
        public void Decrypt()
        {
            encryptionServiceMock
                .Setup(a => a.Decrypt(It.IsAny<string>(), It.IsAny<EncryptionSettings>()))
                .Returns<string, EncryptionSettings>((s, es) => s.FromBase64String(Encoding.UTF8))
                .Verifiable();


            var result = sut.Decrypt(new TestModel { 
                EmailAddress = "bob@gmail.com".ToBase64String(Encoding.UTF8), 
                FirstName = "Bob".ToBase64String(Encoding.UTF8),
                DateOfBirth = new DateTime(1987,08, 11)});

            encryptionServiceMock
                .Verify(a => a.Decrypt(
                    It.IsAny<string>(), 
                    It.IsAny<EncryptionSettings>()), 
                    Times.Exactly(2));

            Assert.AreEqual("bob@gmail.com", result.EmailAddress);
            Assert.AreEqual("Bob", result.FirstName);
        }

        [Test]
        public void Encrypt()
        {
            encryptionServiceMock
                .Setup(a => a.Encrypt(It.IsAny<string>(), It.IsAny<EncryptionSettings>()))
                .Returns<string, EncryptionSettings>((s, es) => s.ToBase64String(Encoding.UTF8))
                .Verifiable();


            var result = sut.Encrypt(new TestModel { 
                EmailAddress = "bob@gmail.com", 
                FirstName = "Bob",
                DateOfBirth = new DateTime(1987,08, 11)});

            encryptionServiceMock
                .Verify(a => a.Encrypt(
                    It.IsAny<string>(), 
                    It.IsAny<EncryptionSettings>()), 
                    Times.Exactly(2));

            Assert.AreNotEqual("bob@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("Bob", result.FirstName);
        }

        private IDictionary<Type, IModelEncryptionDefinition> dictionary;
        private Mock<IEncryptionProfileFactory> encryptionProfileFactoryMock;
        private Mock<IEncryptionService> encryptionServiceMock;
        private ModelEncryptionService<TestModel> sut;
    }
}
