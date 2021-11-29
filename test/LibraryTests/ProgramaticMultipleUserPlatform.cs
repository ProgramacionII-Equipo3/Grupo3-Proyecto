using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core.Messaging;

namespace ProgramTests
{
    /// <summary>
    /// Acts as a messaging platform in which the messages it sends are determined in advance.
    /// </summary>
    public class ProgramaticMultipleUserPlatform : MessagingPlatform<string>
    {
        private readonly IList<(string, string)> receivedMessages = new List<(string, string)>();

        /// <summary>
        /// The received messages.
        /// </summary>
        public (string, string)[] ReceivedMessages => this.receivedMessages.ToArray();

        /// <inheritdoc />
        public override string GetUserId(string id) => id;

        /// <inheritdoc />
        public override void SendMessage(string msg, string id)
        {
            this.receivedMessages.Add((id, msg));
        }

        /// <summary>
        /// Receives an array of messages from the same user.
        /// </summary>
        /// <param name="id">The messages' id.</param>
        /// <param name="messages">The messages' contents.</param>
        public List<(string, string)> ReceiveMessages(string id, params string[] messages)
        {
            foreach(string msg in messages)
            {
                this.ReceiveMessage(msg, id);
            }
            var r = this.receivedMessages.ToList();
            foreach(var (msgId, msg) in this.receivedMessages)
            {
                foreach(string line in msg.Split('\n'))
                {
                    Console.WriteLine($"For {msgId}: {line}");
                }
                Console.WriteLine($"    --------");
            }
            this.receivedMessages.Clear();
            return r;
        }
   }
}