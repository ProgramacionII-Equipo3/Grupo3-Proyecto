namespace Library
{
    /// <summary>
    /// This struct represents a value which could be a success (with a success value) or an error (with an error value).
    /// </summary>
    /// <typeparam name="T">The type of success value.</typeparam>
    /// <typeparam name="E">The type of error value.</typeparam>
    public struct Result<T, E>
    {
        /// <summary>
        /// Whether the value is a success or not.
        /// </summary>
        public bool Success { get; }

        private T successValue { get; }

        private E errorValue { get; }

        private Result(bool Success, T successValue, E errorValue)
        {
            this.Success = Success;
            this.successValue = successValue;
            this.errorValue = errorValue;
        }

        /// <summary>
        /// Creates an instance of <see cref="Result{T, E}"/> which represents an error.
        /// </summary>
        /// <param name="errorValue">The error value.</param>
        /// <returns>An instance of <see cref="Result{T, E}"/></returns>
        public static Result<T, E> Err(E errorValue) =>
            new Result<T, E>(false, default, errorValue);

        /// <summary>
        /// Creates an instance of <see cref="Result{T, E}"/> which represents a success.
        /// </summary>
        /// <param name="successValue">The success value.</param>
        /// <returns>An instance of <see cref="Result{T, E}"/></returns>
        public static Result<T, E> Ok(T successValue) =>
            new Result<T, E>(true, successValue, default);
    }
}
