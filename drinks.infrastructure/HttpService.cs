namespace drinks.infrastructure
{
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class HttpService
    {
        private static readonly string Url = ConfigurationManager.AppSettings["WebApi"];

        public static Task<HttpResponseMessage> PostAsync<T>(string requestUri, T value)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.PostAsJsonAsync<T>(requestUri, value, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostAsync(string requestUri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.PostAsJsonAsync(requestUri, CancellationToken.None);
        }
    }
}
