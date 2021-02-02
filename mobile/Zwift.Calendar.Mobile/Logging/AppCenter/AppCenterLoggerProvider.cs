using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

using AppCenterBase = Microsoft.AppCenter.AppCenter;

namespace Microsoft.Extensions.Logging.AppCenter
{
    public class AppCenterLoggerProvider : ILoggerProvider, ISupportExternalScope
    {

        private readonly AppCenterLoggerOptions options;
        private readonly ConcurrentDictionary<string, AppCenterLogger> loggers;

        private IExternalScopeProvider scopeProvider = NullExternalScopeProvider.Instance;


        public AppCenterLoggerProvider(AppCenterLoggerOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.loggers = new ConcurrentDictionary<string, AppCenterLogger>();

            AppCenterBase.LogLevel = this.options.AppCenterLogLevel;
            AppCenterBase.Start(this.options.AppCenterSecret, typeof(Analytics), typeof(Crashes));
        }


        public ILogger CreateLogger(string name)
        {
            return loggers.GetOrAdd(name, loggerName => new AppCenterLogger(name)
            {
                Options = options,
                ScopeProvider = scopeProvider
            });
        }

        public void Dispose()
        {

        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            this.scopeProvider = scopeProvider;
            foreach (var logger in loggers)
            {
                logger.Value.ScopeProvider = this.scopeProvider;
            }
        }

    }
}
