using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zwift.Calendar.Mobile.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken = default)
        {
            return client.PatchAsync(new Uri(requestUri), content, cancellationToken);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken = default)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request, cancellationToken);
        }
    }
}
