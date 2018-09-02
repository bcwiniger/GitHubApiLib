## Overview
This is a sample library that queries the GitHub API. 

`Clients\RepositoryClient.cs` can be used to return all the repositories belonging to an organization. 

`Clients\PullRequestClient.cs` can be used to query the pull requests belonging to an individual repository. If `GetPullRequestsAsync(string owner, string repoName)` is used the query uses the default parameters. Customized parameters can be supplied via the `GetPullRequestsAsync(string owner, string repoName, PullRequestOptions queryBuilder)` method. Available parameters include
- The pull request state
  - Open 
  - Closed
  - All
- Sort By
  - Created
  - Updated 
  - Popularity
  - Long Running
- Sort Direction
  - Asc
  - Desc
- Pagination Options 
  - GitHub defaults to 30 records returned per page. Using this option you can set the records to page between 1 and 100.

The `GitHubApiLib.TestHarness` shows example usage. The `GetPullRequestsForOrg(string org, PullRequestState state)` call will take a supplied organization and return all pull requests across all the organization's repositories which are in the supplied state. 

## Dependencies
- Install .NET Core 2.1

## To Run 
- `> cd GitHubApiLib.TestHarness`
- `> dotnet run`

## To Run Unit Tests
- `> cd GitHubApiLib.Tests`
- `> dotnet test`