using System;
using System.Linq;
using System.Text.RegularExpressions;
using Library.Core.Messaging;

namespace Grupo3_Proyecto
{
    /// <summary>
    /// Represents the console as a messaging platform in and of itself.
    /// </summary>
    public class MultipleUsersConsolePlatform : MessagingPlatform<string>
    {
        private static Regex changeUserRegex = new Regex(
            "> ?CHANGE[ -]?(?:USER)? +(?<id>.+)",
            RegexOptions.Compiled);

        private static Regex msgRegex = new Regex(
            "For +(?<id>\\w+) *: *(?<msg>.+?) *",
            RegexOptions.Compiled);

        /// <inheritdoc />
        public override string GetUserId(string id) => id;

        private string currentId;

        /// <summary>
        /// Initializes an instance of <see cref="MultipleUsersConsolePlatform" />.
        /// </summary>
        /// <param name="currentId">The platform's initial id.</param>
        public MultipleUsersConsolePlatform(string currentId)
        {
            this.currentId = currentId;
            Console.WriteLine($"Current ID: {this.currentId}");
        }

        /// <inheritdoc />
        public override void SendMessage(string msg, string id)
        {
            foreach (string line in msg.Split('\n'))
            {
                Console.WriteLine($"    For {id}: {line}");
            }
        }

        /// <summary>
        /// Receives messages from the console as if it was a messaging platform.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                string? msg = Console.ReadLine();
                if (msg == null || msg == "--STOP") return;

                string id = currentId;

                Match match = msgRegex.Match(msg);

                if (match.Success)
                {
                    this.ReceiveMessage(
                        match.Groups["msg"].Value,
                        match.Groups["id"].Value);
                    continue;
                }

                match = changeUserRegex.Match(msg);

                if (match.Success)
                {
                    this.currentId = match.Groups["id"].Value;
                    Console.WriteLine($"Current ID: {this.currentId}");
                    this.ReceiveMessage("/help", this.currentId);
                    continue;
                }

                this.ReceiveMessage(msg, id);
            }
        }
    }
}