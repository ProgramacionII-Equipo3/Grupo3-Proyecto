namespace Library.Core.Processing
{
    /// <summary>
    /// Represents the functionality of handling one or more message input until realizing a certain operation successfully,
    /// or until the user indicates to stop trying.
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Handles a received input message, returning a success signal,
        /// a response string (indicating it's not done yet), or an interrupt signal.
        /// </summary>
        /// <param name="msg">The input message.</param>
        /// <returns>
        /// (..., response), being response a response string, <br />
        /// (true, null) for a success signal, or <br />
        /// (false, null) for an interrupt signal.
        /// </returns>
        public Result<bool, string> ProcessInput(string msg);

        /// <summary>
        /// Returns the first message the object uses to indicate what kind of input it wants.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetDefaultResponse();

        /// <summary>
        /// Resets the processor, so it can be used again.
        /// </summary>
        public void Reset();
    }
}
