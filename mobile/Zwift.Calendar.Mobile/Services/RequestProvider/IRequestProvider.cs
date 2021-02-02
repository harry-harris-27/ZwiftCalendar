using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zwift.Calendar.Mobile.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token, CancellationToken cancellationToken);

        Task<TResult> PostAsync<TResult>(string uri, object data, string token, string header, CancellationToken cancellationToken);

        Task<TResult> PutAsync<TResult>(string uri, object data, string token, string header, CancellationToken cancellationToken);

        Task<TResult> PatchAsync<TResult>(string uri, object data, string token, string header, CancellationToken cancellationToken);

        Task DeleteAsync(string uri, string token, CancellationToken cancellationToken);
    }
}
