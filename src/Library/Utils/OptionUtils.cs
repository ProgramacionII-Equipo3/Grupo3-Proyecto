using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Utils
{
    /// <summary>
    /// This class holds methods which are related with nullable values.
    /// We created this class because of SRP, this class in specific is the 
    /// one used by other classes when they have to control nullable values. 
    /// </summary>
    public static class OptionUtils
    {
        /// <summary>
        /// Unwraps a nullable value, throwing an exception if it's null.
        /// </summary>
        /// <param name="value">The nullable value.</param>
        /// <typeparam name="T">The type of the underlying value.</typeparam>
        /// <returns>The value.</returns>
        public static T Unwrap<T>(this T? value) =>
            value is null
                ? throw new ArgumentNullException()
                : (T)value;

        /// <summary>
        /// Unwraps a nullable value, throwing an exception if it's null.
        /// </summary>
        /// <param name="value">The nullable value.</param>
        /// <typeparam name="T">The type of the underlying value.</typeparam>
        /// <returns>The value.</returns>
        public static T Unwrap<T>(this T? value) where T: struct =>
            value is null
                ? throw new ArgumentNullException()
                : (T)value;
    }
}