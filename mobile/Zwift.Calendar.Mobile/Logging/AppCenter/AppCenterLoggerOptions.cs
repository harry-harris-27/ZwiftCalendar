using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.Logging.AppCenter
{
    public class AppCenterLoggerOptions
    {
        public bool IncludeScopes { get; set; }
        public string AppCenterSecret { get; set; }
        public Microsoft.AppCenter.LogLevel AppCenterLogLevel { get; set; } = Microsoft.AppCenter.LogLevel.None;
    }
}
