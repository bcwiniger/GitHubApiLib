using GitHubApiLib.Exceptions;
using GitHubApiLib.Helpers;
using System.Collections.Generic;
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
            object sut = null;

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.ValueIsNotNull(sut));
        }

        [Fact]
        public void value_is_not_null_does_not_throw_exception_when_value_is_not_null()
        {
            var sut = new object();

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
        public void collection_count_equals_throws_exception_when_count_is_not_equal()
        {
            var sut = new List<string> { "a" };

            Assert.Throws<InvalidArgumentException>(() => EnsureThat.CollectionCountEquals(sut, 5));
        }

        [Fact]
        public void collection_count_equals_does_not_throw_exception_when_count_is_equal()
        {
            var sut = new List<string> { "a" };

            EnsureThat.CollectionCountEquals(sut, 1);
        }
    }
}
