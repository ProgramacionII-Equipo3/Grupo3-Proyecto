using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core.Messaging;

namespace UnitTests
{
    /// <summary>
    /// Acts as a messaging platform in which the messages it sends are determined in advance.
    /// </summary>
    public class ProgramaticPlatform: MessagingPlatform<bool>
    {
        private string[] messagesToSend;

        private bool done = false;

        private readonly IList<string> receivedMessages = new List<string>();

        /// <summary>
        /// The received messages.
        /// </summary>
        public string[] ReceivedMessages => this.receivedMessages.ToArray();

        private readonly string id;

        /// <summary>
        /// Initializes an instance of <see cref="ProgramaticPlatform" />
        /// </summary>
        /// <param name="id">The platform's id.</param>
        /// <param name="messages">The messages to send.</param>
        public ProgramaticPlatform(string id, params string[] messages)
        {
            this.id = id;
            this.messagesToSend = messages;
        }

        /// <inheritdoc />
        public override string GetUserId(bool id) => this.id;

        /// <inheritdoc />
        public override void SendMessage(string msg, bool id)
        {
            this.receivedMessages.Add(msg);
        }

        /// <summary>
        /// Receives messages from the console as if it was a messaging platform.
        /// </summary>
        public void Run()
        {
            if(this.done) return;

            foreach(string i in messagesToSend)
            {
                this.ReceiveMessage(i, true);
            }
            this.done = true;
        }
    }
}