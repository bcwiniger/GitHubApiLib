using GitHubApiLib.Parameters;
using Xunit;

namespace GitHubApiLib.Tests.Parameters
{
    public class PaginationOptionsTests
    {
        [Fact]
        public void build_query_string_results_per_page_is_null_return_empty_string()
        {
            var sut = new PaginationOptions();

            Assert.Equal("", sut.QueryString);
        }

        [Fact]
        public void build_query_string_results_per_page_within_range_return_query_string()
        {
            var sut = new PaginationOptions { ResultsPerPage = 50 };

            Assert.Equal("?per_page=50", sut.QueryString);
        }

        [Fact]
        public void build_query_string_results_per_page_below_range_return_per_page_of_one()
        {
            var sut = new PaginationOptions { ResultsPerPage = -1 };

            Assert.Equal("?per_page=1", sut.QueryString);
        }

        [Fact]
        public void build_query_string_results_per_page_above_range_return_per_page_of_one_hundred()
        {
            var sut = new PaginationOptions { ResultsPerPage = 200 };

            Assert.Equal("?per_page=100", sut.QueryString);
        }
    }
}
