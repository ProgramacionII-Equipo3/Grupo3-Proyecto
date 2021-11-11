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
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Option<NotNull<T>> FromOption(Option<T> option) =>
            option.MapValue(v => new NotNull<T>(v));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notNull"></param>
        public static implicit operator T(NotNull<T> notNull) => notNull.Value;
    }
}
