namespace Library.Core.Messaging
{
    /// <summary>
    /// This interface represents a platform from which the program can receive messages.
    /// </summary>
    /// <typeparam name="TId">The type of the ids which identify the users who send the messages.</typeparam>
    public interface IMessageReceiver<TId>
    {
        /// <summary>
        /// Converts a valid id of the platform into a <see cref="UserId" />.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>The resulting <see cref="UserId" />.</returns>
        public UserId GetUserId(TId id);

        /// <summary>
        /// Handles the event of receiving a message.
        /// </summary>
        /// <param name="msg">The message's text.</param>
        /// <param name="id">The message's id.</param>
        public void OnGetMessage(string msg, TId id);
    }
}
