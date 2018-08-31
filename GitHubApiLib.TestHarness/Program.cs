using GitHubApiLib.Clients;
using GitHubApiLib.Models;
using GitHubApiLib.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubApiLib.TestHarness
{
    class Program
    {
        private static readonly string _userAgent = "testing";
        static void Main(string[] args)
        {
            GetAllPullRequests();
        }

        static void GetAllPullRequests()
        {
            try
            {
                var repoClient = new RepositoryClient(_userAgent);
                var repoTask = repoClient.GetAllRepositoriesByOwnerAsync("ramda");
                Task.WaitAll(repoTask);

                var pullReqClient = new PullRequestClient(_userAgent);
                var parms = new PullRequestOptions
                {
                    State = PullRequestState.All,
                    PageOptions = new PaginationOptions { ResultsPerPage = 100 }
                };

                var pullRequestsForOrg = new List<PullRequestResponse>();
                Parallel.ForEach(repoTask.Result, repo =>
                {
                    var pullReqTask = pullReqClient.GetPullRequestsAsync("ramda", repo.Name, parms);
                    Task.WaitAll(pullReqTask);
                    pullRequestsForOrg.AddRange(pullReqTask.Result);
                });

                Console.WriteLine(pullRequestsForOrg.Count);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }
    }
}
