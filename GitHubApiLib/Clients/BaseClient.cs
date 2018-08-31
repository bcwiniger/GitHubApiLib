using GitHubApiLib.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubApiLib.Clients
{
    public abstract class BaseClient
    {
        private HttpClient HttpClient { get; set; }

        public BaseClient(string userAgent)
        {
            SetupHttpClient(userAgent, "https://api.github.com/", "application/vnd.github.v3+json");
        }

        private void SetupHttpClient(string userAgent, string baseAddress, string acceptedFormat)
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue(acceptedFormat));
            HttpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            HttpClient.BaseAddress = new Uri(baseAddress);
        }

        /// <summary>
        /// Aggregates a list of results from a single page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException" />
        protected async Task<List<T>> GetSinglePageResultsAsync<T>(string endpoint)
        {
            var rsp = await HttpClient.GetAsync(endpoint);
            rsp.EnsureSuccessStatusCode();

            return await DeserializeResultsAsync<T>(rsp);
        }

        /// <summary>
        /// Aggregates a list of results, iterating from the starting page to the last page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException" />
        protected async Task<List<T>> GetAllPageResultsAsync<T>(string endpoint)
        {
            LinkHeader pageLinks = new LinkHeader();
            var results = new List<T>();
            do
            {
                var rsp = await HttpClient.GetAsync(endpoint);
                rsp.EnsureSuccessStatusCode();

                results.AddRange(await DeserializeResultsAsync<T>(rsp));
                
                pageLinks = new LinkHeader().Parse(rsp.Headers);

                endpoint = pageLinks.NextPage;
            }
            while (!string.IsNullOrEmpty(pageLinks.NextPage));

            return results;
        }

        private async Task<List<T>> DeserializeResultsAsync<T>(HttpResponseMessage rsp)
        {
            using (var strm = await rsp.Content.ReadAsStreamAsync())
            using (var strmRdr = new StreamReader(strm))
            using (var jsonReader = new JsonTextReader(strmRdr))
            {
                var serializer = new JsonSerializer();
                 return serializer.Deserialize<List<T>>(jsonReader);
            }
        }
    }
}
