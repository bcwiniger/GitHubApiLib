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
    public class GitHubClient : BaseClient
    {
        public GitHubClient(string acceptedFormat, string userAgent, string baseAddress) 
            : base(acceptedFormat, userAgent, baseAddress)
        {
        }
    }

    public abstract class BaseClient
    {
        private HttpClient HttpClient { get; set; }

        public BaseClient(string acceptedFormat, string userAgent, string baseAddress)
        {
            SetupHttpClient(acceptedFormat, userAgent, baseAddress);
        }

        private void SetupHttpClient(string acceptedFormat, string userAgent, string baseAddress)
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue(acceptedFormat));
            HttpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            HttpClient.BaseAddress = new Uri(baseAddress);
        }

        public async Task<List<T>> GetAllPageResultsAsync<T>(string endpoint)
        {
            LinkHeader pageLinks = new LinkHeader();
            var results = new List<T>();
            do
            {
                var rsp = await HttpClient.GetAsync(endpoint);

                rsp.EnsureSuccessStatusCode();

                if (rsp.Headers.TryGetValues("link", out var linkHeader))
                {
                    EnsureThat.CollectionIsNotEmpty(linkHeader);
                    pageLinks = new LinkHeader().Parse(linkHeader.ToList()[0]);
                }

                using (var strm = await rsp.Content.ReadAsStreamAsync())
                using (var strmRdr = new StreamReader(strm))
                using (var jsonReader = new JsonTextReader(strmRdr))
                {
                    var serializer = new JsonSerializer();
                    results.AddRange(serializer.Deserialize<List<T>>(jsonReader));
                }

                endpoint = pageLinks.NextPage;
            }
            while (!string.IsNullOrEmpty(pageLinks.NextPage));

            return results;
        }
    }
}
