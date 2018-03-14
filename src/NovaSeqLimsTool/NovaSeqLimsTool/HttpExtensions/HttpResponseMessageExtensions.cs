using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace NovaSeqLimsTool.HttpExtensions
{
    /// <summary>
    /// Contains extension methods for the HttpResponseMessage class.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Reads the content as the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="instance">The instance being extended.</param>
        /// <returns>The response.</returns>
        public static async Task<T> ReadAsync<T>(this HttpResponseMessage instance)
        {
            // if we weren't successful we want to make sure to throw an exception
            instance.EnsureSuccessStatusCode();

            var responseString = await instance.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
