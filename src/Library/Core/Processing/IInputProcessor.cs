namespace Library.Core.Processing
{
    /// <summary>
    /// Represents the functionality of receiving one or more input messages, and generating an object with that input.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public interface IInputProcessor<T> : IInputHandler where T: class
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
        protected (T, string) getResult();

        /// <summary>
        /// Receives an input message, returning the resulting object if it's ready.
        /// </summary>
        /// <param name="msg">The input message.</param>
        /// <returns>
        /// (result, null), being result the resulting object, <br />
        /// (null, response), being response a response string, or <br />
        /// (null, null) for an interrupt signal.
        /// </returns>
        public (T, string) GenerateFromInput(string msg)
        {
            var (ready, response) = this.ProcessInput(msg);
            if(response != null) return (null, response);
            if(ready)
            {
                var (result, error) = this.getResult();
                if(error != null)
                {
                    this.Reset();
                    return (null, $"{error}\n{this.GetDefaultResponse()}");
                }
                return (result, null);
            }
            else return (null, null);
        }
    }
}
