using System;
using Library.Core.Distribution;

namespace Library.Core.Messaging
{
    /// <summary>
    /// This interface represents the responsibility of handling the event of receiving a message.
    /// </summary>
    public interface IGenericReceiver
    {
        /// <summary>
        /// Handle a received message.
        /// </summary>
        /// <param name="msg">The received message.</param>
        public void OnGetMessage(Message msg);
    }
}
