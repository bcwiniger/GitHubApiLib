using GitHubApiLib.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GitHubApiLib.Helpers
{
    public static class EnsureThat
    {
        /// <summary>
        /// Ensures string is not empty
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="InvalidArgumentException"/>
        public static void ValueIsNotEmpty(string s)
        {
            if (string.IsNullOrEmpty(s)) throw new InvalidArgumentException("Argument cannot be empty.");
        }

        /// <summary>
        /// Ensures object is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <exception cref="InvalidArgumentException"/>
        public static void ValueIsNotNull<T>(T val)
        {
            if (val == null) throw new InvalidArgumentException("Object cannot be null.");
        }

        /// <summary>
        /// Ensures that <paramref name="val"/> is greater than <paramref name="lowerExclusiveBound"/>
        /// </summary>
        /// <param name="val"></param>
        /// <param name="lowerExclusiveBound"></param>
        /// <exception cref="InvalidArgumentException" />
        public static void ValueIsGreaterThan(int val, int lowerExclusiveBound)
        {
            if (val <= lowerExclusiveBound) throw new InvalidArgumentException($"Value of [{val}] must be greater than [{lowerExclusiveBound}]");
        }

        /// <summary>
        /// Ensures that <paramref name="arr"/> count equals <paramref name="val"/>
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <exception cref="InvalidArgumentException" />
        public static void CollectionCountEquals(IEnumerable<object> arr, int val)
        {
            if (arr == null || arr.Count() != val) throw new InvalidArgumentException($"Collection count equals [{arr.Count()}] instead of [{val}]");
        }
    }
}
