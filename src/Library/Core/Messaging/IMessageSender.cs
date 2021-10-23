namespace Library.Core.Messaging
{
    /// <summary>
    /// This interface represents a platform from which the program can send messages.
    /// </summary>
    /// <typeparam name="TId">The type of the ids which identify the users who send the messages.</typeparam>
    public interface IMessageSender<TId>
    {
        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="msg">The message's text.</param>
        /// <param name="id">The message's id.</param>
        public void SendMessage(string msg, TId id);
    }
}
