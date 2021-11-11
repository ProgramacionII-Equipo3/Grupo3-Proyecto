using System;
using Library.Core.Invitations;

namespace Library.Core.Distribution
{
    /// <summary>
    /// This class represents the highest level of encapsulation in message processing.
    /// </summary>
    public static class MessageManager
    {
        /// <summary>
        /// Processes a received message, returning the text of the response message.
        /// </summary>
        /// <param name="msg">The received message.</param>
        /// <returns>The response message's text.</returns>
        public static string ProcessMessage(Message msg)
        {
            if(SessionManager.GetById(msg.Id) is UserSession session)
            {
                return session.ProcessMessage(msg.Text);
            } else
            {
                return processMessageFromUnknownUser(msg);
            }
        }

        private static string processMessageFromUnknownUser(Message msg)
        {
            string[] args = msg.Text.Split(' ');

            if(
                args.Length != 2 ||
                args[0] != "/start" ||
                string.IsNullOrWhiteSpace(args[1])
            )
                return "Send the message /start ( <invitation-code> | -e | --entrepreneur ) to register to the platform.";
            
            if(args[1] == "-e" || args[1] == "--entrepreneur")
            {
                // TODO: Implement subclass of State for new entrepreneurs. 
                SessionManager.NewUser(msg.Id, default, null);
            }

            string invitationCode = args[1];
            return InvitationManager.ValidateInvitation(invitationCode, msg.Id) is string result
                ? result
                : "Invalid invitation code";
        }
    }
}
