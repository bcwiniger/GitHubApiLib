using System.Collections.Generic;
using System.Linq;

namespace GitHubApiLib.Parameters
{
    public class PullRequestQueryStringBuilder
    {
        public PullRequestState State { private get; set; } = PullRequestState.NotSpecified;

        public PullRequestSortBy SortBy { private get; set; } = PullRequestSortBy.NotSpecified;

        public SortDirection SortDirection { private get; set; } = SortDirection.NotSpecified;

        public PaginationOptions PageOptions { private get; set; } = null;

        public string QueryString => Build();

        private string Build()
        {
            if (!QueryStringOptionsExist()) return "";

            var queryStrings = new List<string>
            {
                GetStateQueryString(),
                GetSortByQueryString(),
                GetSortDirectionQueryString()
            };

            if (PageOptions != null) queryStrings.Add(PageOptions.QueryString);

            var queryString = string.Join('&', queryStrings.Where(x => !string.IsNullOrEmpty(x)));
            return $"?{queryString}";
        }

        private bool QueryStringOptionsExist()
        {
            return State != PullRequestState.NotSpecified
                || SortBy != PullRequestSortBy.NotSpecified
                || SortDirection != SortDirection.NotSpecified
                || PageOptions != null;
        }

        private string GetStateQueryString()
        {
            switch (State)
            {
                case PullRequestState.Open:
                    return "state=open";
                case PullRequestState.Closed:
                    return "state=closed";
                case PullRequestState.All:
                    return "state=all";
                default:
                    return "";
            }
        }

        private string GetSortByQueryString()
        {
            switch (SortBy)
            {
                case PullRequestSortBy.Created:
                    return "sort=created";
                case PullRequestSortBy.Updated:
                    return "sort=updated";
                case PullRequestSortBy.Popularity:
                    return "sort=popularity";
                case PullRequestSortBy.LongRunning:
                    return "sort=long-running";
                default:
                    return "";
            }
        }

        private string GetSortDirectionQueryString()
        {
            switch (SortDirection)
            {
                case SortDirection.Asc:
                    return "direction=asc";
                case SortDirection.Desc:
                    return "direction=desc";
                default:
                    return "";
            }
        }
    }


    public enum PullRequestState
    {
        NotSpecified,
        Open,
        Closed,
        All
    }

    public enum PullRequestSortBy
    {
        NotSpecified,
        Created,
        Updated,
        Popularity,
        LongRunning
    }

    public enum SortDirection
    {
        NotSpecified,
        Asc,
        Desc
    }
}
