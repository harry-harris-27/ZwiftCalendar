using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zwift.Calendar.Mobile.Services.RequestProvider
{
    public static class IRequestProviderExtensions
    {

        #region GetAsync Overloads

        public static Task<TResult> GetAsync<TResult>(this IRequestProvider requestProvider, string uri, CancellationToken cancellationToken = default)
            => requestProvider.GetAsync<TResult>(uri, null, cancellationToken);

        #endregion

        #region PostAsync Overloads

        public static Task<TResult> PostAsync<TResult>(this IRequestProvider requestProvider, string uri)
            => requestProvider.PostAsync<TResult>(uri, null);

        public static Task<TResult> PostAsync<TResult>(this IRequestProvider requestProvider, string uri, object data)
            => requestProvider.PostAsync<TResult>(uri, data, null);

        public static Task<TResult> PostAsync<TResult>(this IRequestProvider requestProvider, string uri, object data, string token)
            => requestProvider.PostAsync<TResult>(uri, data, token, null);

        public static Task<TResult> PostAsync<TResult>(this IRequestProvider requestProvider, string uri, object data, string token, string header)
            => requestProvider.PostAsync<TResult>(uri, data, token, header, default);

        #endregion

        #region PutAsync Overloads

        public static Task<TResult> PutAsync<TResult>(this IRequestProvider requestProvider, string uri)
            => requestProvider.PutAsync<TResult>(uri, null);

        public static Task<TResult> PutAsync<TResult>(this IRequestProvider requestProvider, string uri, object data)
            => requestProvider.PutAsync<TResult>(uri, data, null);

        public static Task<TResult> PutAsync<TResult>(this IRequestProvider requestProvider, string uri, object data, string token)
            => requestProvider.PutAsync<TResult>(uri, data, token, null);

        public static Task<TResult> PutAsync<TResult>(this IRequestProvider requestProvider, string uri, object data, string token, string header)
            => requestProvider.PutAsync<TResult>(uri, data, token, header, default);

        #endregion

        #region PatchAsync Overloads

        public static Task<TResult> PatchAsync<TResult>(this IRequestProvider requestProvider, string uri)
            => requestProvider.PatchAsync<TResult>(uri, null);

        public static Task<TResult> PatchAsync<TResult>(this IRequestProvider requestProvider, string uri, object data)
            => requestProvider.PatchAsync<TResult>(uri, data, null);

        public static Task<TResult> PatchAsync<TResult>(this IRequestProvider requestProvider, string uri, object data, string token)
            => requestProvider.PatchAsync<TResult>(uri, data, token, null);

        public static Task<TResult> PatchAsync<TResult>(this IRequestProvider requestProvider, string uri, object data, string token, string header)
            => requestProvider.PatchAsync<TResult>(uri, data, token, header, default);

        #endregion

        #region DeleteAsync Overloads

        public static Task DeleteAsync(this IRequestProvider requestProvider, string uri)
            => requestProvider.DeleteAsync(uri, null);

        public static Task DeleteAsync(this IRequestProvider requestProvider, string uri, string token)
            => requestProvider.DeleteAsync(uri, token, default);

        #endregion
    }
}
