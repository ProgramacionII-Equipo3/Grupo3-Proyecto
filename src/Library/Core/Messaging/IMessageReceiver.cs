namespace Library.Core.Messaging
{
    /// <summary>
    /// This interface represents a platform from which the program can receive messages.
    /// We created this interface because of ISP, that's why we separated the interfaces 
    /// in IMessageReceiver and IMessageSender.
    /// </summary>
    /// <typeparam name="TId">The type of the ids the platform uses to identify its users.</typeparam>
    public interface IMessageReceiver<TId>
    {
        /// <summary>
        /// Converts a valid id of the platform into an user id.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>The resulting user id.</returns>
        public string GetUserId(TId id);

        /// <summary>
        /// Handles the event of receiving a message.
        /// </summary>
        /// <param name="msg">The message's text.</param>
        /// <param name="id">The message's id.</param>
        public void ReceiveMessage(string msg, TId id);
    }
}
