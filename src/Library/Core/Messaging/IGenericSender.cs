using System;

namespace Library.Core.Messaging
{
    /// <summary>
    /// This interface represents the responsibility of sending a message.
    /// </summary>
    public interface IGenericSender
    {
        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="msg">The message to send.</param>
        public void SendMessage(Message msg);
    }
}
