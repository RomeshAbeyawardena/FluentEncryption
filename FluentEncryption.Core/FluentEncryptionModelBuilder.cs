using FluentEncryption.Core.Definitions;
using FluentEncryption.Core.Services;
using FluentEncryption.Core.Visitors;
using FluentEncryption.Shared.Builders;
using FluentEncryption.Shared.Contracts.Services;
using FluentEncryption.Shared.Definitions;
using FluentEncryption.Shared.Domain;
using FluentEncryption.Shared.Enumerations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentEncryption.Core
{
    internal class FluentEncryptionModelBuilder<T> : IFluentEncryptionModelBuilder<T>
    {
        public IFluentEncryptionBuilder Context { get; }

        public IFluentEncryptionModelBuilder<T> RegisterPropertyOrMethod<TSelector>(
            Expression<Func<T, TSelector>> propertyOrMethod, 
            EncryptionProfileType profileType)
        {
            var member = memberExpressionVisitor
                .GetLastVisitedMember(propertyOrMethod.Body);

            if(modelEncryptionDefinitionDictionary
                .TryGetValue(typeof(T), out var modelEncryptionDefinition))
            {
                modelEncryptionDefinition.Add(member, profileType);
            }
            else
            {
                modelEncryptionDefinition = new ModelEncryptionDefinition(member, profileType);

                modelEncryptionDefinitionDictionary
                    .Add(typeof(T), modelEncryptionDefinition);
            }


            return this;
        }

        public FluentEncryptionModelBuilder(
            IFluentEncryptionBuilder fluentEncryptionBuilder)
        {
            modelEncryptionDefinitionDictionary = new Dictionary<Type, IModelEncryptionDefinition>();
            Context = fluentEncryptionBuilder;
            memberExpressionVisitor = new MemberExpressionVisitor();
            fluentEncryptionBuilder.Build += FluentEncryptionBuilder_Build;
        }

        internal IDictionary<Type, IModelEncryptionDefinition> ModelEncryptionDefinitionDictionary => modelEncryptionDefinitionDictionary;

        private void FluentEncryptionBuilder_Build(IServiceCollection services)
        {
            services
                .AddSingleton(modelEncryptionDefinitionDictionary);
        }

        private readonly MemberExpressionVisitor memberExpressionVisitor;
        private readonly IDictionary<Type, IModelEncryptionDefinition> modelEncryptionDefinitionDictionary;
    }
}
