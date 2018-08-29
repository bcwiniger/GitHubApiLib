using GitHubApiLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GitHubApiLib.Clients
{
    public class RepositoryClient
    {
        private GitHubClient _client;
        public RepositoryClient(GitHubClient client)
        {
            _client = client;
        }

        public async Task<List<RepositoryResponse>> GetAllRepositoriesByOwnerAsync(string owner)
        {
            var endpoint = $"orgs/{owner}/repos";

            var rsp = await _client.HttpClient.GetAsync(endpoint);

            using (var strm = await rsp.Content.ReadAsStreamAsync())
            using (var strmRdr = new StreamReader(strm))
            using (var jsonReader = new JsonTextReader(strmRdr))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<List<RepositoryResponse>>(jsonReader);
            }
        }
    }
}
