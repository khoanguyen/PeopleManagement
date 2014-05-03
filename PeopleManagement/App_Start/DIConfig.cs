using Autofac;
using PeopleManagement.Domain;
using PeopleManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.WebApi;

namespace PeopleManagement
{
    public static class DIConfig
    {
        /// <summary>
        /// Configures Dependency Resolvers
        /// </summary>
        public static void Configure()
        {
            var builder = new ContainerBuilder();
          
            // Register component
            builder.RegisterApiControllers(typeof(DIConfig).Assembly);

            builder.RegisterType<DefaultPeopleContextFactory>()
                   .As<IPeopleContextFactory>()
                   .SingleInstance();

            builder.RegisterType<DefaultRepositoryFactory>()
                   .As<IRepositoryFactory>()
                   .SingleInstance();

            builder.RegisterType<PeopleManagementService>()
                   .As<IPeopleManagementService>()
                   .SingleInstance();
           
            var container = builder.Build();

            // Set up Web Api Dependency Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}