namespace Library.Core.Processing
{
    /// <summary>
    /// Represents the functionality of receiving one or more input messages, and generating an object with that input.
    /// </summary>
    /// <typeparam name="T">The type of the resulting object.</typeparam>
    public interface IInputProcessor<T> where T: class
    {

        /// <summary>
        /// Resets the processor, so it can be used again.
        /// </summary>
        public void Reset();

        /// <summary>
        /// Returns the first message the object uses to indicate what kind of input it wants.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetDefaultResponse();

        /// <summary>
        /// Handles a received input message, <br />
        /// returning a response string if it's not ready yet, <br />
        /// a ready-to-process-signal if it is, or <br />
        /// an interrupt signal if the user wants to interrupt the process.
        /// </summary>
        /// <remarks>
        /// After the function returns (true, null) once,
        /// it's recommended that it does nothing and always returns (true, null) on subsequent calls.
        /// </remarks>
        /// <param name="msg">The input message.</param>
        /// <returns>
        /// (..., response), being response a response string, <br />
        /// (true, null) for a ready-to-process signal, or <br />
        /// (false, null) for an interrupt signal.
        /// </returns>
        protected (bool, string) getInput(string msg);

        /// <summary>
        /// Generates the resulting object with the obtained input.
        /// </summary>
        /// <remarks>
        /// This function should be called only after a call to <see cref="getInput(string)" /> returns (true, null),
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
        public (T, string) ProcessInput(string msg)
        {
            var (ready, response) = this.getInput(msg);
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
