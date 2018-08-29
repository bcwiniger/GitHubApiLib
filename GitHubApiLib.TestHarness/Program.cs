using GitHubApiLib.Clients;
using GitHubApiLib.Parameters;
using System;
using System.Threading.Tasks;

namespace GitHubApiLib.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GitHubClient("application/vnd.github.v3+json", "testing", "https://api.github.com/");
            var pullRequestClient = new PullRequestClient(client);
            var parms = new PullRequestQueryStringBuilder
            {
                State = PullRequestState.All,
                PageOptions = new PaginationOptions { ResultsPerPage = 100 }
            };
            var task = pullRequestClient.GetPullRequestsAsync("ramda", "ramda", parms);

            Task.WaitAll(task);

            foreach (var pull in task.Result)
            {
                Console.WriteLine(pull.Title);
            }

            Console.ReadLine();
        }
    }
}
