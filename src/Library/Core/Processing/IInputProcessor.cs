namespace Library.Core.Processing
{
    /// <summary>
    /// Represents the functionality of receiving one or more input messages, and generating an object with that input.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public interface IInputProcessor<T> : IInputHandler
    {
        /// <summary>
        /// Generates the resulting object with the obtained input.
        /// </summary>
        /// <remarks>
        /// This function should be called only after a call to <see cref="IInputHandler.ProcessInput(string)" /> returns (true, null),
        /// which is a signal that the object's ready to produce the result.
        /// </remarks>
        /// <returns>
        /// (result, null), being result the resulting object, or<br />
        /// (null, error), being error an error string.
        /// </returns>
        protected Result<T, string> getResult();

        /// <summary>
        /// Receives an input message, returning the resulting object if it's ready.
        /// </summary>
        /// <param name="msg">The input message.</param>
        /// <returns>
        /// (result, null), being result the resulting object, <br />
        /// (null, response), being response a response string, or <br />
        /// (null, null) for an interrupt signal.
        /// </returns>
        public Option<Result<T, string>> GenerateFromInput(string msg) =>
            this.ProcessInput(msg).Map<Option<Result<T, string>>>(
                ready =>
                {
                    if (ready)
                    {
                        Result<T, string> result = this.getResult();
                        return Option<Result<T, string>>.From(
                            result.Map(
                                result => Result<T, string>.Ok(result),
                                error =>
                                {
                                    this.Reset();
                                    return Result<T, string>.Err($"{error}\n{this.GetDefaultResponse()}");
                                }
                            )
                        );
                    }
                    else return Option<Result<T, string>>.None();
                },
                e => Option<Result<T, string>>.From(Result<T, string>.Err(e))
            );
    }
}
