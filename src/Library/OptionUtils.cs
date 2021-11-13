using System.Collections.Generic;
using System.Linq;

namespace Library
{
    /// <summary>
    /// This class holds methods which are related with the <see cref="Option{T}"/> struct.
    /// </summary>
    public static class OptionUtils
    {
        /// <summary>
        /// Returns an <see cref="Option{T}" /> containing the first value of an <see cref="IEnumerable{T}" />, if there is.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <typeparam name="T">The type of the elements of the enumerable.</typeparam>
        /// <returns></returns>
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> enumerable) =>
            enumerable.Count() == 0 ? Option<T>.None : Option<T>.From(enumerable.FirstOrDefault());
    }
}
