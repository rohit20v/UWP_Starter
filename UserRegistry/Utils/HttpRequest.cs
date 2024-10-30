using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace UserRegistry.Utils
{
    internal class HttpRequest
    {
        private static HttpClient _httpClient = new();

        public static List<T> FetchApi<T>(string url)
        {
            return _httpClient.GetFromJsonAsync<List<T>>(url).GetAwaiter().GetResult();

        }

    }
}
