using GitHubApiLib.Helpers;
using GitHubApiLib.Models;
using GitHubApiLib.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubApiLib.Clients
{
    public class PullRequestClient : BaseClient
    {
        public PullRequestClient(string userAgent) : base(userAgent) { }

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
        public async Task<List<PullRequestResponse>> GetPullRequestsAsync(string owner, string repoName, PullRequestOptions queryBuilder)
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

            return await GetAllPageResultsAsync<PullRequestResponse>(endpoint);
        }
    }

    
}
