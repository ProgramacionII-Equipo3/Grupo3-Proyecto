using Library.Core.Invitations;
using Library.States.Entrepreneurs;

namespace Library.Core.Distribution
{
    /// <summary>
    /// This class represents the highest level of encapsulation in message processing.
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// Processes a received message, returning the text of the response message.
        /// </summary>
        /// <param name="msg">The received message.</param>
        /// <returns>The response message's text.</returns>
        public string ProcessMessage(Message msg)
        {
            if (Singleton<SessionManager>.Instance.GetById(msg.Id) is UserSession session)
            {
                return session.ProcessMessage(msg.Text);
            }
            else
            {
                return ProcessMessageFromUnknownUser(msg);
            }
        }


        private string ProcessMessageFromUnknownUser(Message msg)
        {
            string[] args = msg.Text.Split(' ');

            if (
                args.Length != 2 ||
                args[0] != "/start" ||
                string.IsNullOrWhiteSpace(args[1]))
            {
                return "Send the message /start ( <invitation-code> | -e | --entrepreneur ) to register to the platform.";
            }

            string arg = args[1].Trim();
            if (arg == "-e" || arg == "--entrepreneur")
            {
                State newState = new NewEntrepreneurState(msg.Id);

                // TODO: Implement subclass of State for new entrepreneurs. 
                Singleton<SessionManager>.Instance.NewUser(msg.Id, default, newState);

                return newState.GetDefaultResponse();
            }

            string invitationCode = arg;
            return Singleton<InvitationManager>.Instance.ValidateInvitation(invitationCode, msg.Id) is string result
                ? result
                : "Invalid invitation code";
        }
    }
}
