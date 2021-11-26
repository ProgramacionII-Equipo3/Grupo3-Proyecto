using System;

namespace Library
{
    /// <summary>
    /// This class acts as a wrapper for a value type to act as a reference type.
    /// </summary>
    public class ClassWrapper<T> where T : struct
    {
        /// <summary>
        /// Gets the inner value;
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Initializes an instance of <see cref="ClassWrapper{T}" />.
        /// </summary>
        /// <param name="value">The inner value.</param>
        public ClassWrapper(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Casts a value into a <see cref="ClassWrapper{T}" /> which holds it.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator ClassWrapper<T>(T value) =>
            new ClassWrapper<T>(value);

        /// <summary>
        /// Casts the <see cref="ClassWrapper{T}" /> into a <see cref="Nullable{T}" />.
        /// </summary>
        /// <param name="wrapper">The wrapper to cast.</param>
        public static implicit operator Nullable<T>(ClassWrapper<T> wrapper) =>
            wrapper is null
                ? new Nullable<T>()
                : new Nullable<T>(wrapper.Value);
    }
}