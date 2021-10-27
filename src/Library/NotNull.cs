namespace Library
{
    /// <summary>
    /// This class represents a not nullable value.
    /// </summary>
    public struct NotNull<T>
    {
        ///
        public T Value { get; }

        private NotNull(T value)
        {
            this.Value = value;
        }

        ///
        public static Option<NotNull<T>> FromOption(Option<T> option) =>
            option.MapValue(v => new NotNull<T>(v));

        ///
        public static implicit operator T(NotNull<T> notNull) => notNull.Value;
    }
}
