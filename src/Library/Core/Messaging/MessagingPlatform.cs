namespace Library.Core.Messaging
{
    /// <summary>
    /// 
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
