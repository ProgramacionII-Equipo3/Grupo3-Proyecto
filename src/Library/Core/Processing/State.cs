namespace Library.Core.Processing
{
    /// <summary>
    /// This class represents a state of a user session.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Processes a received message, returning the next state and the response message.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="data">The user's data.</param>
        /// <param name="msg">The message's text.</param>
        /// <returns>The next state and the response message.</returns>
        public abstract (State, string) ProcessMessage(UserId id, UserData data, string msg);
    }
}
