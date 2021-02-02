using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zwift.Calendar.Mobile.Extensions;

namespace Zwift.Calendar.Mobile.Services.RequestProvider
{
    [Export(typeof(IRequestProvider))]
    public class RequestProvider : IRequestProvider
    {

        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions { };

        private readonly ILogger<RequestProvider> logger;


        [ImportingConstructor]
        public RequestProvider(ILogger<RequestProvider> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<TResult> GetAsync<TResult>(string uri, string token, CancellationToken cancellationToken)
        {
            HttpClient client = CreateHttpClient(token);
            HttpResponseMessage response = await client.GetAsync(uri, cancellationToken);

            await HandleResponse(response);

            var stream = await response.Content.ReadAsStreamAsync();
            TResult result = await JsonSerializer.DeserializeAsync<TResult>(stream, serializerOptions);
            stream.Dispose();

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, object data, string token, string header, CancellationToken cancellationToken)
        {
            HttpClient client = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(client, header);
            }

            var content = new StringContent(JsonSerializer.Serialize(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content, cancellationToken);

            await HandleResponse(response);

            var stream = await response.Content.ReadAsStreamAsync();
            TResult result = await JsonSerializer.DeserializeAsync<TResult>(stream, serializerOptions);
            stream.Dispose();

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, object data, string token, string header, CancellationToken cancellationToken)
        {
            HttpClient client = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(client, header);
            }

            var content = new StringContent(JsonSerializer.Serialize(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PutAsync(uri, content, cancellationToken);

            await HandleResponse(response);

            var stream = await response.Content.ReadAsStreamAsync();
            TResult result = await JsonSerializer.DeserializeAsync<TResult>(stream, serializerOptions);
            stream.Dispose();

            return result;
        }

        public async Task<TResult> PatchAsync<TResult>(string uri, object data, string token, string header, CancellationToken cancellationToken)
        {
            HttpClient client = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(client, header);
            }

            var content = new StringContent(JsonSerializer.Serialize(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PatchAsync(uri, content, cancellationToken);

            await HandleResponse(response);

            var stream = await response.Content.ReadAsStreamAsync();
            TResult result = await JsonSerializer.DeserializeAsync<TResult>(stream, serializerOptions);
            stream.Dispose();

            return result;
        }

        public async Task DeleteAsync(string uri, string token, CancellationToken cancellationToken)
        {
            HttpClient client = CreateHttpClient(token);
            await client.DeleteAsync(uri, cancellationToken);
        }


        private HttpClient CreateHttpClient(string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null) 
                return;

            if (string.IsNullOrEmpty(parameter)) 
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException();
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

    }
}
