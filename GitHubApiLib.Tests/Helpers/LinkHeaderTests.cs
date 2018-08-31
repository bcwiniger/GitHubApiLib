using GitHubApiLib.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace GitHubApiLib.Tests.Helpers
{
    public class LinkHeaderTests
    {
        private static string _prevUrl = "https://api.github.com/repositories/1/pulls?page=46";
        private static string _prev = $"<{_prevUrl}>; rel=\"prev\"";
        private static string _nextUrl = "https://api.github.com/repositories/1/pulls?page=48";
        private static string _next = $"<{_nextUrl}>; rel=\"next\"";
        private static string _lastUrl = "https://api.github.com/repositories/1/pulls?page=48";
        private static string _last = $"<{_lastUrl}>; rel=\"last\"";
        private static string _firstUrl = "https://api.github.com/repositories/1/pulls?page=1";
        private static string _first = $"<{_firstUrl}>; rel=\"first\"";

        [Fact]
        public void parse_middle_page_contains_all_links_parse_out_correctly()
        {
            var linkHeader = $"{_prev},{_next},{_last},{_first}";
            var rsp = new HttpResponseMessage();
            rsp.Headers.Add("link", new List<string> { linkHeader });
            var sut = new LinkHeader();

            var result = sut.Parse(rsp.Headers);

            Assert.Equal(_prevUrl, result.PreviousPage);
            Assert.Equal(_nextUrl, result.NextPage);
            Assert.Equal(_lastUrl, result.LastPage);
            Assert.Equal(_firstUrl, result.FirstPage);
        }

        [Fact]
        public void parse_first_page_contains_next_and_last_links_parse_out_correctly()
        {
            var linkHeader = $"{_next},{_last}";
            var rsp = new HttpResponseMessage();
            rsp.Headers.Add("link", new List<string> { linkHeader });
            var sut = new LinkHeader();

            var result = sut.Parse(rsp.Headers);

            Assert.Equal(_nextUrl, result.NextPage);
            Assert.Equal(_lastUrl, result.LastPage);
            Assert.Null(result.PreviousPage);
            Assert.Null(result.FirstPage);
        }

        [Fact]
        public void parse_last_page_contains_first_and_previous_links_parse_out_correctly()
        {
            var linkHeader = $"{_first},{_prev}";
            var rsp = new HttpResponseMessage();
            rsp.Headers.Add("link", new List<string> { linkHeader });
            var sut = new LinkHeader();

            var result = sut.Parse(rsp.Headers);

            Assert.Equal(_firstUrl, result.FirstPage);
            Assert.Equal(_prevUrl, result.PreviousPage);
            Assert.Null(result.NextPage);
            Assert.Null(result.LastPage);
        }

        [Fact]
        public void parse_no_links_exist_return_null_page_links()
        {
            var rsp = new HttpResponseMessage();
            var sut = new LinkHeader();

            var result = sut.Parse(rsp.Headers);

            Assert.Null(result.FirstPage);
            Assert.Null(result.PreviousPage);
            Assert.Null(result.NextPage);
            Assert.Null(result.LastPage);
        }

        [Fact]
        public void parse_link_header_format_changed_return_null_page_links()
        {
            var linkHeader = $"{_first},{_prev}";
            var rsp = new HttpResponseMessage();
            rsp.Headers.Add("link", new List<string> { linkHeader, linkHeader });
            var sut = new LinkHeader();

            var result = sut.Parse(rsp.Headers);

            Assert.Null(result.FirstPage);
            Assert.Null(result.PreviousPage);
            Assert.Null(result.NextPage);
            Assert.Null(result.LastPage);
        }

        [Fact]
        public void parse_url_delimiter_changed_return_null_page_links()
        {
            var linkHeader = $"{_first.Replace(';', ':')}";
            var rsp = new HttpResponseMessage();
            rsp.Headers.Add("link", new List<string> { linkHeader, linkHeader });
            var sut = new LinkHeader();

            var result = sut.Parse(rsp.Headers);

            Assert.Null(result.FirstPage);
            Assert.Null(result.PreviousPage);
            Assert.Null(result.NextPage);
            Assert.Null(result.LastPage);
        }
    }
}
