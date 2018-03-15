using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaSeqLimsTool.HttpExtensions
{
    /// <summary>
    /// Contains extension methods for the HttpClient class.
    /// </summary>
    public static class HttpClientExtensions
    {
        private const string ApplicationJson = "application/json";

        /// <summary>
        /// Creates a new instance of an HttpClient for accepting JSON content.
        /// </summary>
        /// <param name="baseUri">The base URI for the client.</param>
        /// <returns>A new HttpClient instance.</returns>
        public static HttpClient CreateJson(Uri baseUri = null)
        {
            var client = new HttpClient() { BaseAddress = baseUri };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));

            return client;
        }

        /// <summary>
        /// Sends a GET request to the specified URI and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="instance">The instance being extended.</param>
        /// <param name="requestUri">The URI the request is sent to.</param>
        /// <param name="cancellation">The token to monitor for cancellation.</param>
        /// <returns>The response of the GET request.</returns>
        public static async Task<T> GetAsync<T>(this HttpClient instance, Uri requestUri, CancellationToken cancellation = default(CancellationToken))
        {
            var response = await instance.GetAsync(requestUri, cancellation).ConfigureAwait(false);

            return await response.ReadAsync<T>().ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a POST request to the specified URI and deserializes the response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="instance">The instance being extended.</param>
        /// <param name="requestUri">The URI the request is sent to.</param>
        /// <param name="request">The request to send.</param>
        /// <param name="cancellation">The token to monitor for cancellation.</param>
        /// <returns>The response of the POST request.</returns>
        public static async Task<TResponse> PostAsync<TRequest, TResponse>(this HttpClient instance, Uri requestUri,
            TRequest request, CancellationToken cancellation = default(CancellationToken))
        {
            // prep the content
            var requestString = JsonConvert.SerializeObject(request);
            var content = new StringContent(requestString, Encoding.UTF8, ApplicationJson);

            var response = await instance.PostAsync(requestUri, content, cancellation).ConfigureAwait(false);

            return await response.ReadAsync<TResponse>().ConfigureAwait(false);
        }

        public static HttpClient GetAuthenticatedClient(string token)
        {
            var client = CreateJson();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
