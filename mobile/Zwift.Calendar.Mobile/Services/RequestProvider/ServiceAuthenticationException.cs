using System;

namespace Zwift.Calendar.Mobile.Services.RequestProvider
{
    public class ServiceAuthenticationException : Exception
    {
        public ServiceAuthenticationException() { }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }


        public string Content { get; }
    }
}
