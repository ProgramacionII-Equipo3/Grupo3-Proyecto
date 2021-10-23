namespace Library.Core.Messaging
{
    /// <summary>
    /// This class represents a platform from which the program can send and receive messages.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class MessagingPlatform<TId> : IMessageReceiver<TId>, IMessageSender<TId>
    {
        /// <inheritdoc />
        public abstract void SendMessage(string msg, TId id);

        /// <inheritdoc />
        public abstract UserId GetUserId(TId id);
    }
}
