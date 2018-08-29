using GitHubApiLib.Clients;
using GitHubApiLib.Exceptions;
using GitHubApiLib.Helpers;
using Xunit;

namespace GitHubApiLib.Tests.Helpers
{
    public class EnsureThatTests
    {
        [Fact]
        public void value_is_not_empty_throws_exception_when_value_is_empty()
        {
            var sut = "";

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.ValueIsNotEmpty(sut));
        }

        [Fact]
        public void value_is_not_empty_throws_exception_when_value_is_null()
        {
            string sut = null;

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.ValueIsNotEmpty(sut));
        }

        [Fact]
        public void value_is_not_empty_does_not_throw_exception_when_value_is_present()
        {
            var sut = "s";

            EnsureThat.ValueIsNotEmpty(sut);
        }

        [Fact]
        public void value_is_not_null_throws_exception_when_value_is_null()
        {
            GitHubClient sut = null;

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.ValueIsNotNull(sut));
        }

        [Fact]
        public void value_is_not_null_does_not_throw_exception_when_value_is_not_null()
        {
            var sut = new GitHubClient("testing");

            EnsureThat.ValueIsNotNull(sut);
        }

        [Fact]
        public void value_is_greater_than_throws_exception_when_value_is_less_than_lower_bound()
        {
            var sut = 0;
            var lowerBound = 1;

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.ValueIsGreaterThan(sut, lowerBound));
        }

        [Fact]
        public void value_is_greater_than_throws_exception_when_value_is_equal_to_lower_bound()
        {
            var sut = 0;
            var lowerBound = 0;

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.ValueIsGreaterThan(sut, lowerBound));
        }

        [Fact]
        public void value_is_greater_than_does_not_throw_exception_when_value_is_greater_than_lower_bound()
        {
            var sut = 5;
            var lowerBound = 1;

            EnsureThat.ValueIsGreaterThan(sut, lowerBound);
        }

        [Fact]
        public void collection_is_not_empty_throws_exception_when_collection_is_empty()
        {
            var sut = new string[0];

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.CollectionIsNotEmpty(sut));
        }

        [Fact]
        public void collection_is_not_empty_throws_exception_when_collection_is_null()
        {
            string[] sut = null;

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.CollectionIsNotEmpty(sut));
        }

        [Fact]
        public void collection_is_not_empty_does_not_throw_exception_when_collection_is_not_empty()
        {
            var sut = new string[] { "s" };

            EnsureThat.CollectionIsNotEmpty(sut);
        }
    }
}
