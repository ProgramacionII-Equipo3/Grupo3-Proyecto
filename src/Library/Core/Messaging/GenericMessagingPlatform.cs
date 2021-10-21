using System;
using Library.Core.Distribution;
using System.Threading.Tasks;

namespace Library.Core.Messaging
{
    /// <summary>
    /// This class represents the highest level of encapsulation in receiving and sending messages.
    /// </summary>
    public class GenericMessagingPlatform : IGenericSender, IGenericReceiver
    {
        public void SendMessage(Message msg)
        {
            msg.Id.SendMessage(msg.Text);
        }

        public void OnGetMessage(Message msg)
        {
            Message newMsg = new Message(MessageManager.ProcessMessage(msg), msg.Id);
            this.SendMessage(newMsg);
        }
    }
}
