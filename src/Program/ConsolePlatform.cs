using System;
using Library.Core.Messaging;

namespace Grupo3_Proyecto
{
    /// <summary>
    /// Represents the console as a messaging platform in and of itself.
    /// </summary>
    public class ConsolePlatform: MessagingPlatform<bool>
    {
        /// <inheritdoc />
        public override string GetUserId(bool id) => "ConsoleID";

        /// <inheritdoc />
        public override void SendMessage(string msg, bool id)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Receives messages from the console as if it was a messaging platform.
        /// </summary>
        public void Run()
        {
            while(true)
            {
                string? msg = Console.ReadLine();
                if(msg == null || msg == "--STOP") return;
                this.ReceiveMessage(msg, true);
            }
        }
    }
}