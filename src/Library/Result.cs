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
        /// Creates an instance of <see cref="Result{T, E}"/> which represents a success.
        /// </summary>
        /// <param name="successValue">The success value.</param>
        /// <returns>An instance of <see cref="Result{T, E}"/></returns>
        public static Result<T, E> Ok(T successValue) =>
            new Result<T, E>(true, successValue, default);

        /// <summary>
        /// Creates an instance of <see cref="Result{T, E}"/> which represents an error.
        /// </summary>
        /// <param name="errorValue">The error value.</param>
        /// <returns>An instance of <see cref="Result{T, E}"/></returns>
        public static Result<T, E> Err(E errorValue) =>
            new Result<T, E>(false, default, errorValue);

        /// <summary>
        /// Passes either the success value or the error value through a function, returning the result.
        /// </summary>
        /// <param name="successFunc">The function for the success value.</param>
        /// <param name="errFunc">The function for the error value.</param>
        /// <typeparam name="U">The type returned by the functions.</typeparam>
        public U Map<U>(Func<T, U> successFunc, Func<E, U> errFunc) =>
            this.Success ? successFunc(this.successValue) : errFunc(this.errorValue);

        /// <summary>
        /// Passes either the success value or the error value through a function, and returns the result in a new instance of <see cref="Result{U, F}" />.
        /// </summary>
        /// <param name="successFunc">The function for the success value.</param>
        /// <param name="errFunc">The function for the error value.</param>
        /// <typeparam name="U">The type returned by the success function.</typeparam>
        /// <typeparam name="F">The type returned by the error function.</typeparam>
        public Result<U, F> Switch<U, F>(Func<T, U> successFunc, Func<E, F> errFunc) =>
            this.Map(
                v => Result<U, F>.Ok(successFunc(v)),
                e => Result<U, F>.Err(errFunc(e)));

        /// <summary>
        /// Passes the success value through a function if there is, returning a new <see cref="Result{U, E}" /> with it.
        /// </summary>
        /// <param name="successFunc">The function for the success value.</param>
        /// <typeparam name="U">The type of the new success value.</typeparam>
        public Result<U, E> SwitchOk<U>(Func<T, U> successFunc) =>
            this.Switch(successFunc, e => e);

        /// <summary>
        /// Passes the error value through a function if there is, returning a new <see cref="Result{T, F}" /> with it.
        /// </summary>
        /// <param name="errFunc">The function for the error value.</param>
        /// <typeparam name="F">The type of the new error value.</typeparam>
        public Result<T, F> SwitchErr<F>(Func<E, F> errFunc) =>
            this.Switch(v => v, errFunc);

        /// <summary>
        /// If the result is a success, returns the result of the given function.
        /// If it's an error, returns that error.
        /// </summary>
        /// <param name="successFunc">The function for the success value.</param>
        /// <typeparam name="U">The success type of the result returned by the function.</typeparam>
        public Result<U, E> AndThen<U>(Func<T, Result<U, E>> successFunc) =>
            this.Map(
                v => successFunc(v),
                Result<U, E>.Err);

        /// <summary>
        /// Calls a function if the result is an Ok.
        /// </summary>
        /// <param name="successAction">The success function.</param>
        public void RunIfOk(Action<T> successAction)
        {
            this.SwitchOk(v =>
            {
                successAction(v);
                return true;
            });
        }

        /// <summary>
        /// Calls a function if the result is an Err.
        /// </summary>
        /// <param name="errAction">The error function.</param>
        public void RunIfErr(Action<E> errAction)
        {
            this.SwitchErr(e =>
            {
                errAction(e);
                return true;
            });
        }
    }
}
