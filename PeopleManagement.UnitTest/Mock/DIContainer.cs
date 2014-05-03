using Autofac;
using Autofac.Core;
using PeopleManagement.Domain;
using PeopleManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.UnitTest.Mock
{
    /// <summary>
    /// Dependency Injection container for creating classes with mock items
    /// </summary>
    public static class DIContainer
    {
        private static IContainer _container;

        static DIContainer()
        {
            var builder = new ContainerBuilder();

            // Register PeopleContext factory
            builder.RegisterType<MockPeopleContextFactory>()
                   .As<IPeopleContextFactory>()
                   .SingleInstance();

            // Register  Repository factory
            builder.RegisterType<DefaultRepositoryFactory>()
                   .As<IRepositoryFactory>()
                   .SingleInstance();

            // Register PeopleManagement business logic
            builder.RegisterType<PeopleManagementService>()
                   .As<IPeopleManagementService>()
                   .SingleInstance();

            _container = builder.Build();
        }

        /// <summary>
        /// Creates an instance of given service type
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
        {
            return (TService)_container.Resolve(typeof(TService));
        }
    }
}
