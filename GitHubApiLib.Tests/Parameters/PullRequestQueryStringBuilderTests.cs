using GitHubApiLib.Parameters;
using Xunit;

namespace GitHubApiLib.Tests.Parameters
{
    public class PullRequestQueryStringBuilderTests
    {
        [Fact]
        public void build_query_string_with_no_values_supplied_returns_empty_string()
        {
            var sut = new PullRequestOptions();

            Assert.Equal("", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_state_specified_as_all_build_correct_query_string()
        {
            var sut = new PullRequestOptions { State = PullRequestState.All };

            Assert.Equal("?state=all", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_state_specified_as_open_build_correct_query_string()
        {
            var sut = new PullRequestOptions { State = PullRequestState.Open };

            Assert.Equal("?state=open", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_state_specified_as_closed_build_correct_query_string()
        {
            var sut = new PullRequestOptions { State = PullRequestState.Closed };

            Assert.Equal("?state=closed", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_state_set_as_not_specified_return_empty_string()
        {
            var sut = new PullRequestOptions { State = PullRequestState.NotSpecified };

            Assert.Equal("", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_created_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortBy = PullRequestSortBy.Created };
            
            Assert.Equal("?sort=created", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_updated_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortBy = PullRequestSortBy.Updated };
            
            Assert.Equal("?sort=updated", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_popularity_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortBy = PullRequestSortBy.Popularity };
            
            Assert.Equal("?sort=popularity", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_by_specified_as_long_running_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortBy = PullRequestSortBy.LongRunning };
            
            Assert.Equal("?sort=long-running", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_by_set_as_not_specified_return_empty_string()
        {
            var sut = new PullRequestOptions { SortBy = PullRequestSortBy.NotSpecified };
            
            Assert.Equal("", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_direction_specified_as_asc_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortDirection = SortDirection.Asc };
            
            Assert.Equal("?direction=asc", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_direction_specified_as_desc_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortDirection = SortDirection.Desc };
            
            Assert.Equal("?direction=desc", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_sort_direction_specified_as_not_specified_build_correct_query_string()
        {
            var sut = new PullRequestOptions { SortDirection = SortDirection.NotSpecified };
            
            Assert.Equal("", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_page_options_return_query_string()
        {
            var sut = new PullRequestOptions { PageOptions = new PaginationOptions { ResultsPerPage = 50 } };

            Assert.Equal("?per_page=50", sut.QueryString);
        }

        [Fact]
        public void build_query_string_with_multiple_values_specified_build_correct_query_string()
        {
            var sut = new PullRequestOptions
            {
                State = PullRequestState.Open,
                SortBy = PullRequestSortBy.Updated, 
                PageOptions = new PaginationOptions { ResultsPerPage = 1 }
            };
            
            Assert.Equal("?state=open&sort=updated&per_page=1", sut.QueryString);
        }


    }
}
