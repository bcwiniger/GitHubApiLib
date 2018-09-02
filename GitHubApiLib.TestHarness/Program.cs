using GitHubApiLib.Clients;
using GitHubApiLib.Models;
using GitHubApiLib.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubApiLib.TestHarness
{
    class Program
    {
        private static readonly string _userAgent = "testing";
        static void Main(string[] args)
        {
            GetPullRequestsForOrg("ramda", PullRequestState.All);
        }
        

        static void GetPullRequestsForOrg(string org, PullRequestState state)
        {
            try
            {
                var repoClient = new RepositoryClient(_userAgent);
                var repoTask = repoClient.GetAllRepositoriesByOwnerAsync(org);
                Task.WaitAll(repoTask);

                var pullReqClient = new PullRequestClient(_userAgent);
                var parms = new PullRequestOptions
                {
                    State = state,
                    PageOptions = new PaginationOptions { ResultsPerPage = 100 }
                };
                
                var tasks = new List<Task<List<PullRequestResponse>>>();
                Parallel.ForEach(repoTask.Result, repo =>
                {
                    var pullReqTask = pullReqClient.GetPullRequestsAsync(org, repo.Name, parms);
                    tasks.Add(pullReqTask);
                });
                Task.WaitAll(tasks.ToArray());

                var pullRequestsForOrg = new List<PullRequestResponse>();
                foreach (var task in tasks) pullRequestsForOrg.AddRange(task.Result);
                
                Console.WriteLine(pullRequestsForOrg.Count);
            }
            catch (Exception ex)
            {
                if (ex is AggregateException aggEx) ex = ex.InnerException;
                Console.WriteLine(ex.Message, ex);
            }
            finally
            {
                Console.Read();
            }
        }
    }
}
