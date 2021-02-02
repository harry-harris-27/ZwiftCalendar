using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AppCenter;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Reflection;
using Xamarin.Essentials;
using Zwift.Calendar.Mobile.Composition;

namespace Zwift.Calendar.Mobile
{
    public class AppBootstrapper
    {

        private readonly string appCenterSecret;

        private readonly ILoggerFactory loggerFactory = new LoggerFactory();
        private ILogger<AppBootstrapper> logger;

        private CompositionHost container;


        public AppBootstrapper(string appCenterSecret)
        {
            this.appCenterSecret = appCenterSecret ?? throw new ArgumentNullException(nameof(appCenterSecret));
        }

        
        public void Configure()
        {
#if DEBUG
            loggerFactory.AddProvider(new DebugLoggerProvider());
#else
            loggerFactory.AddAppCenter(options => 
            {
                options.AppCenterSecret = appCenterSecret;
                options.AppCenterLogLevel = Microsoft.AppCenter.LogLevel.Warn;
            });
#endif
            logger = this.loggerFactory.CreateLogger<AppBootstrapper>();

            // Get the assemblies that we want to include in our container
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Create conventions for sharing exports (default) and non-shared exports
            ConventionBuilder defaultConventions = new ConventionBuilder();
            Type exportAttributeType = typeof(ExportAttribute);
            Type nonSharedAttributeType = typeof(NonSharedAttribute);
            defaultConventions.ForTypesMatching(type => type.IsDefined(exportAttributeType) && type.IsDefined(nonSharedAttributeType)).Export();
            defaultConventions.ForTypesMatching(type => type.IsDefined(exportAttributeType) && !type.IsDefined(nonSharedAttributeType)).Export().Shared();

            ContainerConfiguration configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies)
                .WithDefaultConventions(defaultConventions)
                .WithExport<IPermissionsService>(new PermissionsService())
                .WithExport(loggerFactory)
                .WithExport(loggerFactory.CreateLogger("Static"));

            // Create the default container to be used by the application
            container = configuration.CreateContainer();

            BuildUp(this);
        }

        public void BuildUp(object instance)
        {
            container.SatisfyImports(instance);
        }

        public IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetExports(service);
        }

        public object GetInstance(Type service, string key)
        {
            try
            {
                return container.GetExport(service, key);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to resolve type '{ service.Name }'");
            }

            return null;
        }

        public IEnumerable<Assembly> SelectAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

    }
}
