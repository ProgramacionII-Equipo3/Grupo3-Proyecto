namespace Library
{
    /// <summary>
    /// This class acts as a boxed wrapped around a valued type,
    /// so it can be used in contexts where a class type is expected.
    /// </summary>
    /// <typeparam name="T">The valued type.</typeparam>
    public class RefWrapper<T> where T: struct
    {
        /// <summary>
        /// The inner value;
        /// </summary>
        public T value;

        ///
        public RefWrapper(T value)
        {
            this.value = value;
        }
    }
}