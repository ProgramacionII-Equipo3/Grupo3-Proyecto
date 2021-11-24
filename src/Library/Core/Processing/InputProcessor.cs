namespace Library.Core.Processing
{
    /// <summary>
    /// Represents the functionality of receiving one or more input messages, and generating an object with that input.
    /// We created this class because of DIP, it depends of an abstract class.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public abstract class InputProcessor<T> : InputHandler
    {
        /// <summary>
        /// Generates the resulting object with the obtained input.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///     This function should be called only after a call to <see cref="InputHandler.ProcessInput(string)" /> returns: <br />
        ///         Result&lt;bool, string&gt;.Ok(true) <br />
        ///     which is a signal that the object's ready to produce the result. Doing so under other circumstances may result in undefined behaviour.
        ///     </para>
        /// </remarks>
        /// <returns>
        /// Result.Ok(result), being result the resulting object, or<br />
        /// Result.Err(error), being error an error string.
        /// </returns>
        protected abstract Result<T, string> getResult();

        /// <summary>
        /// Receives an input message, returning the resulting object if it's ready.
        /// </summary>
        /// <param name="msg">The input message.</param>
        /// <returns>
        /// Option.Some(Result.Ok(result)), being result the resulting object, <br />
        /// Option.Some(Result.Err(response)), being response a response string, or <br />
        /// Option.None for an interrupt signal.
        /// </returns>
        public Result<T, string>? GenerateFromInput(string msg) =>
            this.ProcessInput(msg).Map<Result<T, string>?>(
                ready =>
                {
                    if (ready)
                    {
                        Result<T, string> result = this.getResult();
                        return result.Map(
                            result => Result<T, string>.Ok(result),
                            error =>
                            {
                                this.Reset();
                                return Result<T, string>.Err($"{error}\n{this.GetDefaultResponse()}");
                            });
                    }
                    else
                    {
                        return null;
                    }
                },
                e => Result<T, string>.Err(e));
    }
}
