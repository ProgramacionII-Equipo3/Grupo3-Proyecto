using Library.Core.Distribution;

namespace Library.Core.Messaging
{
    /// <summary>
    /// This class represents a platform from which the program can send and receive messages.
    /// We created this class because of DIP, that way the other classes depend of abractions.
    /// </summary>
    /// <typeparam name="TId">The type of the ids the platform uses to identify its users.</typeparam>
    public abstract class MessagingPlatform<TId> : IMessageReceiver<TId>, IMessageSender<TId>
    {
        /// <inheritdoc />
        public abstract void SendMessage(string msg, TId id);

        /// <inheritdoc />
        public abstract string GetUserId(TId id);

        /// <summary>
        /// Handles the event of receiving a message, sending a response.
        /// </summary>
        /// <param name="msg">The received message's text.</param>
        /// <param name="id">The received message's user id.</param>
        public void ReceiveMessage(string msg, TId id)
        {
            string newMsg = Singleton<MessageManager>.Instance.ProcessMessage(new Message(msg, this.GetUserId(id)));
            this.SendMessage(newMsg, id);
        }
    }
}
