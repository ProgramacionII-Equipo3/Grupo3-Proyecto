namespace Library.Core.States
{
    /// <summary>
    /// Represents a piece of data which can be obtained from input.
    /// </summary>
    public interface IFromInput
    {
        /// <summary>
        /// Receives an input message, and stores the information it represents if it's valid.
        /// </summary>
        /// <param name="msg">The input message.</param>
        /// <returns>null if the input is valid, an error string if it's invalid.</returns>
        public string GetInput(string msg);

        /// <summary>
        /// Returns a default string to send in order to ask for that input.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetDefaultString();
    }
}
