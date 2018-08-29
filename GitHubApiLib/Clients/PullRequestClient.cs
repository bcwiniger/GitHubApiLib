using GitHubApiLib.Helpers;
using GitHubApiLib.Models;
using GitHubApiLib.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubApiLib.Clients
{
    public class PullRequestClient
    {
        private GitHubClient _client;
        public PullRequestClient(GitHubClient client)
        {
            EnsureThat.ValueIsNotNull(client);
            _client = client;
        }

        /// <summary>
        /// Get pull requests belonging to a particular repository for a particular organization.
        /// Query parameters are not set and therefore default to GitHub API defaults.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
        public async Task<List<PullRequestResponse>> GetPullRequestsAsync(string owner, string repoName)
        {
            EnsureThat.ValueIsNotEmpty(owner);
            EnsureThat.ValueIsNotEmpty(repoName);

            return await GetPullRequestsAsync(owner, repoName, "");
        }

        /// <summary>
        /// Get pull requests belonging to a particular repository for a particular organization with supplied query parameters.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repoName"></param>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public async Task<List<PullRequestResponse>> GetPullRequestsAsync(string owner, string repoName, PullRequestQueryStringBuilder queryBuilder)
        {
            EnsureThat.ValueIsNotNull(queryBuilder);
            EnsureThat.ValueIsNotEmpty(owner);
            EnsureThat.ValueIsNotEmpty(repoName);

            return await GetPullRequestsAsync(owner, repoName, queryBuilder.QueryString);
        }

        private async Task<List<PullRequestResponse>> GetPullRequestsAsync(string owner, string repoName, string queryString)
        {
            EnsureThat.ValueIsNotEmpty(owner);
            EnsureThat.ValueIsNotEmpty(repoName);

            var pullRequests = new List<PullRequestResponse>();

            var endpoint = $"repos/{owner}/{repoName}/pulls{queryString}";

            LinkHeader pageLinks = new LinkHeader();
            do
            {
                var rsp = await _client.HttpClient.GetAsync(endpoint);

                rsp.EnsureSuccessStatusCode();

                if(rsp.Headers.TryGetValues("link", out var linkHeader))
                {
                    EnsureThat.CollectionIsNotEmpty(linkHeader);
                    pageLinks = new LinkHeader().Parse(linkHeader.ToList()[0]);
                }
                
                using (var strm = await rsp.Content.ReadAsStreamAsync())
                using (var strmRdr = new StreamReader(strm))
                using (var jsonReader = new JsonTextReader(strmRdr))
                {
                    var serializer = new JsonSerializer();
                    pullRequests.AddRange(serializer.Deserialize<List<PullRequestResponse>>(jsonReader));
                }

                endpoint = pageLinks.NextPage;
            }
            while (!string.IsNullOrEmpty(pageLinks.NextPage));

            return pullRequests;
        }
    }

    
}
