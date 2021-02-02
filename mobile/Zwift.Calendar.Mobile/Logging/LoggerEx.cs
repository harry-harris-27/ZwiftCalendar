using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace Zwift.Calendar.Mobile.Logging
{
    [Export(typeof(ILogger<>))]
    public class LoggerEx<T> : ILogger<T>
    {

        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<T> logger;


        [ImportingConstructor]
        public LoggerEx(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            this.logger = loggerFactory.CreateLogger<T>();
        }


        public IDisposable BeginScope<TState>(TState state) => logger.BeginScope(state);

        public bool IsEnabled(LogLevel logLevel) => logger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
