namespace Library
{
    /// <summary>
    /// This class represents a not nullable value.
    /// </summary>
    public struct NotNull<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public T Value { get; }

        private NotNull(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Converts an <see cref="Option{T}" /> with a value into an <see cref="Option{T}" /> with a <see cref="NotNull{T}" /> with the same value.
        /// </summary>
        /// <param name="option">The initial <see cref="Option{T}" />.</param>
        /// <returns>The resulting <see cref="Option{T}" />.</returns>
        public static Option<NotNull<T>> FromOption(Option<T> option) =>
            option.MapValue(v => new NotNull<T>(v));

        /// <summary>
        /// Gets the value out of the <see cref="NotNull{T}" />.
        /// </summary>
        /// <param name="notNull">The <see cref="NotNull{T}" />.</param>
        public static implicit operator T(NotNull<T> notNull) => notNull.Value;
    }
}
