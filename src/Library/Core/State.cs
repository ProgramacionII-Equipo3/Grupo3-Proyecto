namespace Library.Core
{
    /// <summary>
    /// This class represents a state.
    /// </summary>
    public abstract class State
    {

        /// <summary>
        /// Processes a received message, returning the next state and the response message.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="data">The user's data.</param>
        /// <param name="msg">The message's text.</param>
        /// <returns>The next state and the response message. If the response message is null, the new state's default message can be used instead.</returns>
        public abstract (State?, string?) ProcessMessage(string id, ref UserData data, string msg);

        /// <summary>
        /// Returns the first message the object uses to indicate what kind of input it wants.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string GetDefaultResponse();
    }
}