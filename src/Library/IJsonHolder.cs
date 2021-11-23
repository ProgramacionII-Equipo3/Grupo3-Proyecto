namespace Library
{
    /// <summary>
    /// This interface represents the functionality of acting as a JSON data holder
    /// which can be transformed back and forth into a concrete type of object.
    /// </summary>
    /// <typeparam name="U">The type of objects the implementation can be transformed into.</typeparam>
    public interface IJsonHolder<U>
    {
        /// <summary>
        /// Transforms the JSON data into an object.
        /// </summary>
        public U ToValue();
        
        /// <summary>
        /// Gets the JSON data of an object.
        /// </summary>
        /// <param name="value">The object.</param>
        public void FromValue(U value);
    }    
}
