namespace Library
{
    /// <summary>
    /// This class represents classes which are supposed to have only one instance.
    /// </summary>
    /// <typeparam name="T">The class which has only one instance.</typeparam>
    public static class Singleton<T> where T: new()
    {
        /// <summary>
        /// The instance.
        /// </summary>
        public static readonly T Instance = new T();
    }
}
