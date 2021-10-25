using System;

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

        /// <summary>
        /// Passes either the success value or the error value through a function, and returns the result in a new instance of <see cref="Result{U, F}" />.
        /// </summary>
        /// <param name="successFunc">The function for the success value.</param>
        /// <param name="errFunc">The function for the error value.</param>
        /// <typeparam name="U">The type returned by the success function.</typeparam>
        /// <typeparam name="F">The type returned by the error function.</typeparam>
        /// <returns></returns>
        public Result<U, F> Switch<U, F>(Func<T, U> successFunc, Func<E, F> errFunc) =>
            this.Success ? Result<U, F>.Ok(successFunc(this.successValue)) : Result<U, F>.Err(errFunc(this.errorValue));
    }
}
