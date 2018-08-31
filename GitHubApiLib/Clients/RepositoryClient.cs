using GitHubApiLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubApiLib.Clients
{
    public class RepositoryClient : BaseClient
    {
        public RepositoryClient(string userAgent) : base(userAgent) { }

        public async Task<List<RepositoryResponse>> GetAllRepositoriesByOwnerAsync(string owner)
        {
            var endpoint = $"orgs/{owner}/repos";
            return await GetAllPageResultsAsync<RepositoryResponse>(endpoint);
        }
    }
}
