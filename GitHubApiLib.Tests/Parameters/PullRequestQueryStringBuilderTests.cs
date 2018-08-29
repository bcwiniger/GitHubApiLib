using GitHubApiLib.Parameters;
using Xunit;

namespace GitHubApiLib.Tests.Parameters
{
    public class PullRequestQueryStringBuilderTests
    {
        [Fact]
        public void build_query_string_with_no_values_supplied_returns_empty_string()
        {
            var sut = new PullRequestQueryStringBuilder();

            var result = sut.Build();

            Assert.Equal("", result);
        }

        [Fact]
        public void build_query_string_with_state_specified_as_all_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { State = PullRequestState.All };

            var result = sut.Build();

            Assert.Equal("?state=all", result);
        }

        [Fact]
        public void build_query_string_with_state_specified_as_open_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { State = PullRequestState.Open };

            var result = sut.Build();

            Assert.Equal("?state=open", result);
        }

        [Fact]
        public void build_query_string_with_state_specified_as_closed_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { State = PullRequestState.Closed };

            var result = sut.Build();

            Assert.Equal("?state=closed", result);
        }

        [Fact]
        public void build_query_string_with_state_set_as_not_specified_return_empty_string()
        {
            var sut = new PullRequestQueryStringBuilder { State = PullRequestState.NotSpecified };

            var result = sut.Build();

            Assert.Equal("", result);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_created_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortBy = PullRequestSortBy.Created };

            var result = sut.Build();

            Assert.Equal("?sort=created", result);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_updated_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortBy = PullRequestSortBy.Updated };

            var result = sut.Build();

            Assert.Equal("?sort=updated", result);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_popularity_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortBy = PullRequestSortBy.Popularity };

            var result = sut.Build();

            Assert.Equal("?sort=popularity", result);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_long_running_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortBy = PullRequestSortBy.LongRunning };

            var result = sut.Build();

            Assert.Equal("?sort=long-running", result);
        }

        [Fact]
        public void build_query_string_with_sort_by_set_as_not_specified_return_empty_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortBy = PullRequestSortBy.NotSpecified };

            var result = sut.Build();

            Assert.Equal("", result);
        }

        [Fact]
        public void build_query_string_with_sort_direction_specified_as_asc_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortDirection = SortDirection.Asc };

            var result = sut.Build();

            Assert.Equal("?direction=asc", result);
        }

        [Fact]
        public void build_query_string_with_sort_direction_specified_as_desc_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortDirection = SortDirection.Desc };

            var result = sut.Build();

            Assert.Equal("?direction=desc", result);
        }

        [Fact]
        public void build_query_string_with_sort_direction_specified_as_not_specified_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { SortDirection = SortDirection.NotSpecified };

            var result = sut.Build();

            Assert.Equal("", result);
        }

        [Fact]
        public void build_query_string_with_multiple_values_specified_build_correct_query_string()
        {
            var sut = new PullRequestQueryStringBuilder { State = PullRequestState.Open, SortBy = PullRequestSortBy.Updated };

            var result = sut.Build();

            Assert.Equal("?state=open&?sort=updated", result);
        }
    }
}
