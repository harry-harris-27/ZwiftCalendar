using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.Logging.AppCenter
{
    public static class AppCenterLoggerFactoryExtensions
    {
        public static ILoggerFactory AddAppCenter(this ILoggerFactory factory, Action<AppCenterLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new AppCenterLoggerOptions();
            configure(options);

            var provider = new AppCenterLoggerProvider(options);
            factory.AddProvider(provider);

            return factory;
        }
    }
}
